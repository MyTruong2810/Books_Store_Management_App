using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace ZaloPay.Helper
{
    /// <summary>
    /// Lớp HttpHelper cung cấp các phương thức tiện ích để thực hiện các yêu cầu HTTP.
    /// </summary>
    public class HttpHelper
    {
        private static readonly HttpClient httpClient = new HttpClient();

        /// <summary>
        /// Gửi yêu cầu HTTP POST bất đồng bộ và nhận kết quả dưới dạng đối tượng kiểu T.
        /// </summary>
        /// <typeparam name="T">Kiểu dữ liệu đối tượng kết quả</typeparam>
        /// <param name="uri">Địa chỉ URL yêu cầu</param>
        /// <param name="content">Nội dung yêu cầu</param>
        /// <returns>Đối tượng kết quả kiểu T</returns>
        public static async Task<T> PostAsync<T>(string uri, HttpContent content)
        {
            var response = await httpClient.PostAsync(uri, content);
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseString);
        }

        /// <summary>
        /// Gửi yêu cầu HTTP POST bất đồng bộ và nhận kết quả dưới dạng từ điển các cặp khóa-giá trị.
        /// </summary>
        /// <param name="uri">Địa chỉ URL yêu cầu</param>
        /// <param name="content">Nội dung yêu cầu</param>
        /// <returns>Từ điển các cặp khóa-giá trị</returns>
        public static Task<Dictionary<string, object>> PostAsync(string uri, HttpContent content)
        {
            return PostAsync<Dictionary<string, object>>(uri, content);
        }

        /// <summary>
        /// Gửi yêu cầu HTTP POST bất đồng bộ với nội dung dạng form và nhận kết quả dưới dạng đối tượng kiểu T.
        /// </summary>
        /// <typeparam name="T">Kiểu dữ liệu đối tượng kết quả</typeparam>
        /// <param name="uri">Địa chỉ URL yêu cầu</param>
        /// <param name="data">Dữ liệu yêu cầu dạng form</param>
        /// <returns>Đối tượng kết quả kiểu T</returns>
        public static Task<T> PostFormAsync<T>(string uri, Dictionary<string, string> data)
        {
            return PostAsync<T>(uri, new FormUrlEncodedContent(data));
        }

        /// <summary>
        /// Gửi yêu cầu HTTP POST bất đồng bộ với nội dung dạng form và nhận kết quả dưới dạng từ điển các cặp khóa-giá trị.
        /// </summary>
        /// <param name="uri">Địa chỉ URL yêu cầu</param>
        /// <param name="data">Dữ liệu yêu cầu dạng form</param>
        /// <returns>Từ điển các cặp khóa-giá trị</returns>
        public static Task<Dictionary<string, object>> PostFormAsync(string uri, Dictionary<string, string> data)
        {
            return PostFormAsync<Dictionary<string, object>>(uri, data);
        }

        /// <summary>
        /// Gửi yêu cầu HTTP GET bất đồng bộ và nhận kết quả dưới dạng đối tượng kiểu T.
        /// </summary>
        /// <typeparam name="T">Kiểu dữ liệu đối tượng kết quả</typeparam>
        /// <param name="uri">Địa chỉ URL yêu cầu</param>
        /// <returns>Đối tượng kết quả kiểu T</returns>
        public static async Task<T> GetJson<T>(string uri)
        {
            var response = await httpClient.GetAsync(uri);
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseString);
        }

        /// <summary>
        /// Gửi yêu cầu HTTP GET bất đồng bộ và nhận kết quả dưới dạng từ điển các cặp khóa-giá trị.
        /// </summary>
        /// <param name="uri">Địa chỉ URL yêu cầu</param>
        /// <returns>Từ điển các cặp khóa-giá trị</returns>
        public static Task<Dictionary<string, object>> GetJson(string uri)
        {
            return GetJson<Dictionary<string, object>>(uri);
        }
    }
}