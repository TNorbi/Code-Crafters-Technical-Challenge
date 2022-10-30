using System.Security.Cryptography;
using System.Text;

namespace One_Time_Password_Web_Application.Data.AES
{
    public class AesEncryptDecrypt
    {
        private const string AesIV = @"1234123456785678"; //@"d8zOcR9K9xqpl8Cd";//@"!QAZ2WSX#EDC4RFV";
        private const string AesKey = @"4566456678997899"; //@"NDsVwQwRbwbuYDcX2PRGwNewMediaCod"; //@"5TGB&YHN7UJM(IK<";

        public static byte[] EncryptToBytesUsingCBC(string toEncrypt)
        {
            byte[] src = Encoding.UTF8.GetBytes(toEncrypt);
            byte[] dest = new byte[src.Length];
            using (var aes = Aes.Create())
            {
                aes.BlockSize = 128;
                aes.KeySize = 128;
                aes.IV = Encoding.UTF8.GetBytes(AesIV);
                aes.Key = Encoding.UTF8.GetBytes(AesKey);
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.Zeros;
                // encryption
                using (ICryptoTransform encrypt = aes.CreateEncryptor(aes.Key, aes.IV))
                {
                    return encrypt.TransformFinalBlock(src, 0, src.Length);
                }
            }
        }

        public static string EncryptUsingCBC(string toEncrypt)
        {
            return Convert.ToBase64String(EncryptToBytesUsingCBC(toEncrypt));
        }

        public static string DecryptToBytesUsingCBC(byte[] toDecrypt)
        {
            byte[] src = toDecrypt;
            byte[] dest = new byte[src.Length];
            using (var aes = Aes.Create())
            {
                aes.BlockSize = 128;
                aes.KeySize = 128;
                aes.IV = Encoding.UTF8.GetBytes(AesIV);
                aes.Key = Encoding.UTF8.GetBytes(AesKey);
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.Zeros;
                // decryption
                using (ICryptoTransform decrypt = aes.CreateDecryptor(aes.Key, aes.IV))
                {
                    byte[] decryptedText = decrypt.TransformFinalBlock(src, 0, src.Length);

                    return Encoding.UTF8.GetString(decryptedText);
                }
            }
        }
        public static string DecryptUsingCBC(string toDecrypt)
        {

            return DecryptToBytesUsingCBC(Convert.FromBase64String(toDecrypt));
        }
    }
}
