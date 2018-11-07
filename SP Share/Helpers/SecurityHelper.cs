using System.Security.Cryptography;
using System.Text;
using System;

namespace SP_Share.Helpers
{
    public static class SecurityHelper
    {
        /// <summary>
        /// Returns Hashed password given a plain password
        /// </summary>
        /// <param name="password">plain text password</param>
        /// <param name="salt">password</param>
        /// <returns></returns>
        public static string HashPassword(string password, ref string salt)
        {
            if (string.IsNullOrEmpty(salt))
            {
                var saltByte = new byte[16];
                var randomData = RandomNumberGenerator.Create();
                randomData.GetBytes(saltByte);
                salt = Convert.ToBase64String(saltByte);
            }
            var passwordByte = Encoding.Unicode.GetBytes(salt + password);
            var hashedBytes = SHA512.Create().ComputeHash(passwordByte);
            var hashedPassword = Convert.ToBase64String(hashedBytes);
            return hashedPassword;
        }
    }
}