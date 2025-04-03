using System;

namespace Application.Helper
{
    public static class GenerateWalletAccount
    {
        public static string GenerateFormattedAccountNumber()
        {
            string accountNumber = Generate14DigitAccountNumber();
            return $"{accountNumber.Substring(0, 4)}-{accountNumber.Substring(4, 4)}-{accountNumber.Substring(8, 6)}";
        }

        private static string Generate14DigitAccountNumber()
        {
            Guid guid = Guid.NewGuid();
            byte[] guidBytes = guid.ToByteArray();
            long numericValue = BitConverter.ToInt64(guidBytes, 0) ^ BitConverter.ToInt64(guidBytes, 8);
            long positiveValue = Math.Abs(numericValue);
            return (positiveValue % 100000000000000).ToString("D14");
        }
    }
}