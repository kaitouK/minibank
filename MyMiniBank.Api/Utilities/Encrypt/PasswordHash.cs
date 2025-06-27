namespace MyMiniBank.Api.Utilities.Encrypt
{
    public static class PasswordHash
    {
        /// <summary>
        /// Hashes a password using a secure hashing algorithm.
        /// This method is used to securely store user passwords.
        /// </summary>
        /// <param name="password">
        /// The password to be hashed.
        /// </param>
        /// <returns></returns>
        public static string HashPassword(string password)
        {
            if(string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Password cannot be null or empty.", nameof(password));
            }
            // Use a secure hashing algorithm to hash the password
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        /// <summary>
        /// Verifies a password against a hashed password.
        /// This method is used to check if the provided password matches the stored hashed password.
        /// </summary>
        /// <param name="password">
        /// The password to verify.
        /// </param>
        /// <param name="hashedPassword">
        /// The hashed password to verify against.
        /// This should be the result of a previous call to HashPassword.
        /// </param>
        /// <returns></returns>
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            // Verify the provided password against the hashed password
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
