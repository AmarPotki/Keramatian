using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;


namespace Keramatian.Services.Impl
{
    public class Cryptographer : ICryptographer
    {
        // Rijndael Key of 16-bytes
        private static readonly byte[] Key = new byte[] { 45, 7, 85, 41, 34, 216, 14, 156, 3, 8, 9, 65, 226, 95, 68, 36 };
        public string CreateSalt()
        {
            int size = 64;
            //Generate a cryptographic random number.
            var rng = new RNGCryptoServiceProvider();
            var buff = new byte[size];
            rng.GetBytes(buff);

            // Return a Base64 string representation of the random number.
            return Convert.ToBase64String(buff);
        }

        public string ComputeHash(string valueToHash)
        {
            HashAlgorithm algorithm = SHA512.Create();
            byte[] hash = algorithm.ComputeHash(Encoding.UTF8.GetBytes(valueToHash));

            return Convert.ToBase64String(hash);
        }

        public string GetPasswordHash(string password, string salt)
        {
            return ComputeHash(password + salt);
        }

        public string GetHashFromKeyAndSalt(string key, string salt)
        {
            return GetPasswordHash(key, salt);
        }

        public string Encrypt(string data)
        {
            try
            {
                // byte data
                byte[] internalData =
                    System.Text.Encoding.ASCII.GetBytes(data);

                // Create a new Rijndael object to generate a key
                // and initialization vector (IV).
                RijndaelManaged rijndaelAlgo = new RijndaelManaged();
                rijndaelAlgo.Key = Key;
                rijndaelAlgo.IV = new byte[rijndaelAlgo.IV.Length];

                // Create a CryptoStream using the MemoryStream
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(
                        ms, rijndaelAlgo.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(internalData, 0, internalData.Length);
                        cs.FlushFinalBlock();

                        return System.Convert.ToBase64String(ms.ToArray());
                    }
                }
                //return "";
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string Decrypt(string data)
        {
            try
            {
                // byte data
                byte[] internalData =
                    System.Convert.FromBase64String(data);

                // Create a new Rijndael object to generate a key
                // and initialization vector (IV).
                RijndaelManaged rijndaelAlgo = new RijndaelManaged();
                rijndaelAlgo.Key = Key;
                rijndaelAlgo.IV = new byte[rijndaelAlgo.IV.Length];

                // Create a CryptoStream using the MemoryStream
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(
                        ms, rijndaelAlgo.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(internalData, 0, internalData.Length);
                        cs.FlushFinalBlock();

                        return Encoding.ASCII.GetString(ms.ToArray());
                    }
                }
                //return "";
            }

            catch (Exception e)
            {
                throw e;
            }
        }
    }
}