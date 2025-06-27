using MyMiniBank.Api.Services.Interface; // required for IUserService
using MyMiniBank.Api.Models.DTOs; // required for RegisterRequest, LoginRequest, RegisterResponse, LoginResponse
using Microsoft.EntityFrameworkCore; // required for DbContext and EF Core methods
using MyMiniBank.Api.Models.DataBaseContext; // required for AppDbContext
using MyMiniBank.Api.Models.Entities; // required for User entity
using MyMiniBank.Api.Utilities;
namespace MyMiniBank.Api.Services;

public class UserService : IUserService
{
    private const decimal InitialBalance = 1000000m; // Initial balance for new bank accounts
    private readonly AppDbContext _dbContext;
    private readonly JwtTokenService _jwtTokenService;
    public UserService(AppDbContext context, JwtTokenService jwtTokenService)
    {
        _dbContext = context;
        _jwtTokenService = jwtTokenService;
    }

    /// <summary>
    /// Registers a new user asynchronously..
    /// This method checks if the username already exists in the database. 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<ApiResponse<RegisterResponse>> RegisterAsync(RegisterRequest request)
    {
        var exists = await _dbContext.Users
            .AnyAsync(u => u.Username == request.Username || u.Email == request.Email);
        if (exists)
        {
            return ApiResponse<RegisterResponse>.Fail("Username already exists");
        }

        // Start a transaction to ensure atomicity
        await using var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                CreatedAt = DateTime.UtcNow,
                Role = "User"
            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            
            // Generate a random account number for the new user
            // Ensure the account number generation is successful
            if (!AccountNumberGenerator.TryGenerate("01", out string accountNumber))
            {
                return ApiResponse<RegisterResponse>.Fail("帳號產生失敗，請稍後再試");
            }

            var bankAccount = new BankAccount
            {
                UserId = user.Id, // Associate the bank account with the newly created user
                AccountNumber = accountNumber, // Generate a random account number
                AccountType = AccountType.Savings, // Default account type
                Balance = 0.0m,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Status = AccountStatus.Active // Default status
            };

            _dbContext.BankAccounts.Add(bankAccount);
            await _dbContext.SaveChangesAsync();

            // Commit the transaction
            await transaction.CommitAsync();

            return ApiResponse<RegisterResponse>.Ok(new RegisterResponse
            {
                Username = user.Username
            }, "User registered successfully");
        }
        catch (Exception ex)
        {
            // Rollback the transaction in case of an error
            await transaction.RollbackAsync();
            return ApiResponse<RegisterResponse>.Fail($"An error occurred while registering the user: {ex.Message}");
        }
    }

    public async Task<ApiResponse<LoginResponse>> LoginAsync(LoginRequest request)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Username == request.Username);

        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            return ApiResponse<LoginResponse>.Fail("Invalid username or password");
        }
        var jwtUserPayload = new JwtUserPayload
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            Role = user.Role
        };

        var token = _jwtTokenService.GenerateToken(jwtUserPayload);

        return ApiResponse<LoginResponse>.Ok(new LoginResponse
        {
            Username = user.Username,
            Token = token,
            ExpiresAt = DateTime.UtcNow.AddDays(7) // Token expiration time
        }, "Login successful");
    }

}