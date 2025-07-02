using System.Text.RegularExpressions;
namespace MyMiniBank.Api.Utilities.RegexHelpers
{
    public static class AccountVerify
    {
        public const string AccountNumberPattern = @"^\d{10}$"; // 10位數字
        public const string AccountHolderNamePattern = @"^[\p{L}\s]+$"; // 只允許字母和空格

        public static bool IsValidAccountNumber(string accountNumber)
        {
            return Regex.IsMatch(accountNumber, AccountNumberPattern);
        }

        public static bool IsValidAccountHolderName(string name)
        {
            return Regex.IsMatch(name, AccountHolderNamePattern);
        }
    }
}