using System;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Library.Helpers
{
    public static class PasswordManager
    {
        public static string HashPassword(string password, string salt = "exampleProject")
        {
            var saltedBytes = Encoding.ASCII.GetBytes(salt);

            var hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password,
                saltedBytes,
                KeyDerivationPrf.HMACSHA512,
                10000,
                256 / 8));
            return hashedPassword;
        }
    }
}
