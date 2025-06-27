namespace MyMiniBank.Api.Models.Config
{
    /// <summary>
    /// Represents the settings for JWT authentication.
    /// </summary>
    public class JwtSettings
    {
        /// <summary>
        /// Gets or sets the secret key used for signing the JWT tokens.
        /// </summary>
        public required string tKey { get; set; }
        /// <summary>
        /// Gets or sets the issuer of the JWT tokens.
        /// </summary>
        public required string Issuer { get; set; }

        /// <summary>
        /// Gets or sets the audience for which the JWT tokens are intended.
        /// </summary>
        public required string Audience { get; set; }
        /// <summary>
        /// Gets or sets the number of days after which the JWT tokens will expire.
        /// This is used to set the expiration time for the tokens.
        /// </summary>
        public int ExpirationDays { get; set; }
    }
}