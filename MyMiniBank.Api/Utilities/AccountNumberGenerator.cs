using System;
using System.Collections.Generic;

namespace MyMiniBank.Api.Utilities
{
    public static class AccountNumberGenerator
    {
        private static long _nextAccountBody = 1;
        private static readonly HashSet<string> ExistingAccounts = new HashSet<string>();

        public static bool TryGenerate(string branchCode, out string accountNumber)
        {
            accountNumber = string.Empty;

            // ✅ 檢查分行碼格式是否合法
            if (string.IsNullOrWhiteSpace(branchCode) || branchCode.Length != 2 || !int.TryParse(branchCode, out _))
            {
                // 回傳 false 表示失敗
                return false;
            }

            string accountBody = _nextAccountBody.ToString("D8");
            _nextAccountBody++;

            string baseNumber = branchCode + accountBody;
            string checkDigit = CalculateLuhnCheckDigit(baseNumber);
            string fullAccountNumber = baseNumber + checkDigit;

            if (!ExistingAccounts.Add(fullAccountNumber))
            {
                // 非常罕見，重複帳號（可以遞迴嘗試或回傳失敗）
                return false;
            }

            accountNumber = fullAccountNumber;
            return true;
        }

        private static string CalculateLuhnCheckDigit(string input)
        {
            int sum = 0;
            bool doubleDigit = true;

            for (int i = input.Length - 1; i >= 0; i--)
            {
                int digit = input[i] - '0';
                if (doubleDigit)
                {
                    digit *= 2;
                    if (digit > 9) digit -= 9;
                }
                sum += digit;
                doubleDigit = !doubleDigit;
            }

            int check = (10 - (sum % 10)) % 10;
            return check.ToString();
        }
    }
}
