using System;
using System.Security.Cryptography;
using System.Text;
using Welcome.Others;

namespace Welcome.Model
{
    public class User
    {
        private string password;
        public virtual int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password 
        {
            get
            {
                return DecryptPassword(password);
            }
            set
            {
                password = EncryptPassword(value);
            }
        }
        public string FacultyNumber { get; set; }
        public UserRoleEnum Role { get; set; }

        public DateTime Expires { get; set; }

        private string EncryptPassword(string password)
        {
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.GenerateKey();
                rijAlg.GenerateIV();

                ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                byte[] encryptedBytes;

                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                        csEncrypt.Write(passwordBytes, 0, passwordBytes.Length);
                        csEncrypt.FlushFinalBlock();
                        encryptedBytes = msEncrypt.ToArray();
                    }
                }

                byte[] ivWithKey = new byte[rijAlg.IV.Length + rijAlg.Key.Length];
                Array.Copy(rijAlg.IV, ivWithKey, rijAlg.IV.Length);
                Array.Copy(rijAlg.Key, 0, ivWithKey, rijAlg.IV.Length, rijAlg.Key.Length);

                return Convert.ToBase64String(ivWithKey.Concat(encryptedBytes).ToArray());
            }
        }

        private string DecryptPassword(string encryptedPassword)
        {
            byte[] ivWithKeyAndCipherText = Convert.FromBase64String(encryptedPassword);

            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                byte[] iv = new byte[rijAlg.IV.Length];
                byte[] key = new byte[rijAlg.Key.Length];
                byte[] cipherText = new byte[ivWithKeyAndCipherText.Length - (iv.Length + key.Length)];

                Array.Copy(ivWithKeyAndCipherText, iv, iv.Length);
                Array.Copy(ivWithKeyAndCipherText, iv.Length, key, 0, key.Length);
                Array.Copy(ivWithKeyAndCipherText, iv.Length + key.Length, cipherText, 0, cipherText.Length);

                rijAlg.Key = key;
                rijAlg.IV = iv;

                ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                using (var msDecrypt = new MemoryStream(cipherText))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }

    }
}

