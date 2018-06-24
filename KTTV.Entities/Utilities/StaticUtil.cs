using System;
using System.Security.Cryptography;
using System.Text;

namespace KTTV.Entities.Utilities
{
    public static class StaticUtil
    {
        private const string Key = "C!!8C6C5C-B551-4E0B-B1ED-294D3FD4F@@E";

        /// <summary>
        /// Encrypts the specified string.
        /// </summary>
        /// <param name="toEncryptStr">To encrypt.</param>
        /// <param name="useHashing">if set to <c>true</c> [use hashing].</param>
        /// <returns>System.String.</returns>
        public static string Encrypt(string toEncryptStr, bool useHashing = true)
        {
            byte[] keyArray;
            var toEncryptArray = Encoding.UTF8.GetBytes(toEncryptStr);

            if (useHashing)
            {
                var hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(Key));
                hashmd5.Clear();
            }
            else
                keyArray = Encoding.UTF8.GetBytes(Key);

            var tdes = new TripleDESCryptoServiceProvider
            {
                Key = keyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            var cTransform = tdes.CreateEncryptor();
            var resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            tdes.Clear();

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        /// <summary>
        /// Decrypts the specified cipher string.
        /// </summary>
        /// <param name="cipherString">The cipher string.</param>
        /// <param name="useHashing">if set to <c>true</c> [use hashing].</param>
        /// <returns>System.String.</returns>
        public static string Decrypt(string cipherString, bool useHashing = true)
        {
            byte[] keyArray;
            var toEncryptArray = Convert.FromBase64String(cipherString);

            if (useHashing)
            {
                var hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(Key));
                hashmd5.Clear();
            }
            else
            {
                keyArray = Encoding.UTF8.GetBytes(Key);
            }

            var tdes = new TripleDESCryptoServiceProvider
            {
                Key = keyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            var cTransform = tdes.CreateDecryptor();
            var resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            tdes.Clear();

            return Encoding.UTF8.GetString(resultArray);
        }
    }
}
