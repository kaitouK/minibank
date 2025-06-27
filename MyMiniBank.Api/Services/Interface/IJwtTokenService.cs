using MyMiniBank.Api.Models.DTOs;

namespace MyMiniBank.Api.Services.Interface
{
    /// <summary>
    /// Defines the contract for JWT token services in the banking application.
    /// This interface provides methods for generating and validating JWT tokens.
    /// </summary>
    public interface IJwtTokenService
    {
        /// <summary>
        /// Generates a JWT token for a given user ID.
        /// </summary>
        /// <param name="userId">The ID of the user for whom the token is generated.</param>
        /// <returns>A string representing the generated JWT token.</returns>
        string GenerateToken(JwtUserPayload user);
    }
}