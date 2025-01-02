using System;
using System.Security.Cryptography;

namespace ZaloPay.Helper.Crypto
{
    public enum ZaloPayHMAC
    {
        HMACMD5,
        HMACSHA1,
        HMACSHA256,
        HMACSHA512
    }

    /// <summary>
    /// Class hỗ trợ tính toán giá trị HMAC cho một thông điệp.
    /// </summary>
    public class HmacHelper
    {
        /// <summary>
        /// Tính toán giá trị HMAC cho một thông điệp sử dụng thuật toán đã cho và khóa đã cho.
        /// </summary>
        /// <param name="algorithm">Thuật toán HMAC.</param>
        /// <param name="key">Khóa sử dụng để tính toán HMAC.</param>
        /// <param name="message">Thông điệp cần tính toán HMAC.</param>
        /// <returns>Giá trị HMAC tính toán được dưới dạng chuỗi hexa.</returns>
        public static string Compute(ZaloPayHMAC algorithm = ZaloPayHMAC.HMACSHA256, string key = "", string message = "")
        {
            byte[] keyByte = System.Text.Encoding.UTF8.GetBytes(key);
            byte[] messageBytes = System.Text.Encoding.UTF8.GetBytes(message);
            byte[] hashMessage = null;

            switch (algorithm)
            {
                case ZaloPayHMAC.HMACMD5:
                    hashMessage = new HMACMD5(keyByte).ComputeHash(messageBytes);
                    break;
                case ZaloPayHMAC.HMACSHA1:
                    hashMessage = new HMACSHA1(keyByte).ComputeHash(messageBytes);
                    break;
                case ZaloPayHMAC.HMACSHA256:
                    hashMessage = new HMACSHA256(keyByte).ComputeHash(messageBytes);
                    break;
                case ZaloPayHMAC.HMACSHA512:
                    hashMessage = new HMACSHA512(keyByte).ComputeHash(messageBytes);
                    break;
                default:
                    hashMessage = new HMACSHA256(keyByte).ComputeHash(messageBytes);
                    break;
            }

            return BitConverter.ToString(hashMessage).Replace("-", "").ToLower();
        }
    }
}