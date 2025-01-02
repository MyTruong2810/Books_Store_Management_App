using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;


namespace ZaloPay.Helper.Crypto
{
    /// <summary>
    /// Lớp RSAHelper cung cấp các phương thức để mã hóa và giải mã dữ liệu sử dụng thuật toán RSA.
    /// </summary>
    public class RSAHelper
    {
        /// <summary>
        /// Mã hóa dữ liệu sử dụng khóa công khai RSA.
        /// </summary>
        /// <param name="data">Dữ liệu cần mã hóa.</param>
        /// <param name="publicKey">Khóa công khai dùng để mã hóa.</param>
        /// <returns>Chuỗi đã mã hóa dưới dạng Base64.</returns>
        public static string Encrypt(string data, string publicKey)
        {
            byte[] publicKeyBytes = Convert.FromBase64String(publicKey);
            AsymmetricKeyParameter asymmetricKeyParameter = PublicKeyFactory.CreateKey(publicKeyBytes);
            RsaKeyParameters rsaKeyParameters = (RsaKeyParameters)asymmetricKeyParameter;

            RSAParameters rsaParameters = new RSAParameters
            {
                Modulus = rsaKeyParameters.Modulus.ToByteArrayUnsigned(),
                Exponent = rsaKeyParameters.Exponent.ToByteArrayUnsigned()
            };

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.ImportParameters(rsaParameters);

            byte[] dataToEncrypt = Encoding.UTF8.GetBytes(data);
            byte[] encryptedData = rsa.Encrypt(dataToEncrypt, false);
            var hash = Convert.ToBase64String(encryptedData);

            return hash;
        }

        /// <summary>
        /// Mã hóa dữ liệu sử dụng khóa công khai RSA từ phiên bản 1.
        /// </summary>
        /// <param name="data">Dữ liệu cần mã hóa.</param>
        /// <param name="publicKey">Khóa công khai dùng để mã hóa.</param>
        /// <returns>Chuỗi đã mã hóa dưới dạng Base64.</returns>
        public static string EncryptV1(string data, string publicKey)
        {
            string hash = "";
            try
            {
                byte[] keys = Convert.FromBase64String(publicKey);
                X509Certificate2 cert = new X509Certificate2(keys);
                hash = Encrypt(data, cert);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return hash;
        }

        /// <summary>
        /// Mã hóa dữ liệu sử dụng chứng chỉ X509Certificate2.
        /// </summary>
        /// <param name="plainText">Dữ liệu cần mã hóa.</param>
        /// <param name="cert">Chứng chỉ X509Certificate2 dùng để mã hóa.</param>
        /// <returns>Chuỗi đã mã hóa dưới dạng Base64.</returns>
        public static string Encrypt(string plainText, X509Certificate2 cert)
        {
            RSACryptoServiceProvider publicKey = (RSACryptoServiceProvider)cert.PublicKey.Key;
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] encryptedBytes = publicKey.Encrypt(plainBytes, false);
            string encryptedText = Convert.ToBase64String(encryptedBytes);
            return encryptedText;
        }

        /// <summary>
        /// Giải mã dữ liệu đã mã hóa sử dụng chứng chỉ X509Certificate2.
        /// </summary>
        /// <param name="encryptedText">Dữ liệu đã mã hóa dưới dạng Base64.</param>
        /// <param name="cert">Chứng chỉ X509Certificate2 dùng để giải mã.</param>
        /// <returns>Chuỗi đã giải mã.</returns>
        public static string Decrypt(string encryptedText, X509Certificate2 cert)
        {
            RSACryptoServiceProvider privateKey = (RSACryptoServiceProvider)cert.PrivateKey;
            byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
            byte[] decryptedBytes = privateKey.Decrypt(encryptedBytes, false);
            string decryptedText = Encoding.UTF8.GetString(decryptedBytes);
            return decryptedText;
        }
    }
}