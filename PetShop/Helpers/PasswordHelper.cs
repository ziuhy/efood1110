
using System;
using System.Security.Cryptography;

namespace PetShop   // <-- 與 HomeController 同命名空間
{
    public static class PasswordHelper
    {
        private const int SaltSize = 16;   // 128 bit
        private const int HashSize = 20;   // 160 bit
        private const int Iterations = 10000;

        public static string HashPassword(string password)
        {
            byte[] salt = new byte[SaltSize];
            using (var rng = RandomNumberGenerator.Create())
                rng.GetBytes(salt);

            // 兼容舊版 .NET Framework
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations);
            byte[] hash = pbkdf2.GetBytes(HashSize);

            byte[] result = new byte[SaltSize + HashSize];
            Buffer.BlockCopy(salt, 0, result, 0, SaltSize);
            Buffer.BlockCopy(hash, 0, result, SaltSize, HashSize);

            return Convert.ToBase64String(result);
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            if (string.IsNullOrEmpty(hashedPassword)) return false;

            byte[] hashedBytes = Convert.FromBase64String(hashedPassword);
            if (hashedBytes.Length != SaltSize + HashSize) return false;

            byte[] salt = new byte[SaltSize];
            Buffer.BlockCopy(hashedBytes, 0, salt, 0, SaltSize);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations);
            byte[] hash = pbkdf2.GetBytes(HashSize);

            for (int i = 0; i < HashSize; i++)
                if (hashedBytes[SaltSize + i] != hash[i])
                    return false;

            return true;
        }
    }
}