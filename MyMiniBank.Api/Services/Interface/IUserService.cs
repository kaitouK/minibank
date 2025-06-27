using MyMiniBank.Api.Models.DTOs;
namespace MyMiniBank.Api.Services.Interface
{
    ///<summary>
    /// Interface for user-related services in the MyMiniBank API.
    /// This interface defines methods for user authentication and registration.
    /// It is implemented by the UserService class.
    ///</summary>
    /// <remarks>
    /// This interface is part of the MyMiniBank API project, which provides banking functionalities.
    /// The IUserService interface is designed to handle user operations such as login and registration.
    /// It abstracts the user-related business logic, allowing for easier testing and maintenance.
    /// The methods defined in this interface are asynchronous, returning tasks that can be awaited.
    /// </remarks>
    public interface IUserService
    {
        /// <summary>
        /// Authenticates a user with the provided login request.
        /// </summary>
        /// <param name="loginRequest">The login request containing username and password.</param>
        /// <returns>A task that represents the asynchronous operation, containing the login response.</returns>
        Task<ApiResponse<LoginResponse>> LoginAsync(LoginRequest loginRequest);

        /// <summary>
        /// Registers a new user with the provided registration request.
        /// </summary>
        /// <param name="registerRequest">The registration request containing user details.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task<ApiResponse<RegisterResponse>> RegisterAsync(RegisterRequest registerRequest);
    }
}
