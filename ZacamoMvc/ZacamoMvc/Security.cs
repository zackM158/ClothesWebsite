using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ZacamoMvc
{
    public static class Security
    {
        private static string CreateSalt()
        {
            // Generate a random salt
            RNGCryptoServiceProvider rNgCryptoServiceProvider = new RNGCryptoServiceProvider();
            byte[] salt = new byte[24];
            rNgCryptoServiceProvider.GetBytes(salt);

            return Convert.ToBase64String(salt);
        }

        public static string Sha256(out string salt, string pass) //use a random salt
        {
            salt = CreateSalt();
            return GetSHA256(salt + pass);
        }

        public static string Sha256(string salt, string pass) //overload to use a provided salt
        {
            return GetSHA256(salt + pass);
        }

        private static string GetSHA256(string pass)
        {
            string newhash = string.Empty;

            byte[] text = UTF8Encoding.UTF8.GetBytes(pass);
            SHA256Managed sha256 = new SHA256Managed();
            byte[] hash = sha256.ComputeHash(text);

            foreach (byte theByte in hash)
            {
                newhash += theByte.ToString("x2");
            }

            return newhash;
        }
    }
}