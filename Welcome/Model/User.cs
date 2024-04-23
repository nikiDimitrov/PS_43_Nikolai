using System;
using System.Security.Cryptography;
using Welcome.Others;

namespace Welcome.Model
{
    public class User
    {
        private string password;
        public int Id { get; set; }
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

                using (var msEncrypt = new System.IO.MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new System.IO.StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(password);
                        }
                        encryptedBytes = msEncrypt.ToArray();
                    }
                }

                return Convert.ToBase64String(encryptedBytes);
            }
        }

        private string DecryptPassword(string encryptedPassword)
        {
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                byte[] cipherText = Convert.FromBase64String(encryptedPassword);

                ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                string decryptedPassword;

                using (var msDecrypt = new System.IO.MemoryStream(cipherText))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new System.IO.StreamReader(csDecrypt))
                        {
                            decryptedPassword = srDecrypt.ReadToEnd();
                        }
                    }
                }

                return decryptedPassword;
            }
        }
    }
}

