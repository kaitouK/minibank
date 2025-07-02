using System.Text.RegularExpressions;
namespace MyMiniBank.Api.Utilities.RegexHelpers
{
    public static class UserVerify
    {
        public const string EmailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"; // Email格式
        public const string UsernamePattern = @"^[a-zA-Z0-9_]{3,20}$"; // 3到20個字母、數字或下劃線
        public const string PasswordPattern = @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$"; // 至少8個字符，至少1個字母和1個數字

        public static bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, EmailPattern);
        }

        public static bool IsValidPassword(string password)
        {
            return Regex.IsMatch(password, PasswordPattern);
        }
        public static bool IsValidUsername(string username)
        {
            return Regex.IsMatch(username, UsernamePattern);
        }
    }
}