using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using ZaloPay.Helper;
using Newtonsoft.Json;
using ZaloPay.Helper.Crypto;

namespace Books_Store_Management_App.Models.ZaloPay
{
    /// <summary>
    /// Dịch vụ tích hợp với ZaloPay để tạo và kiểm tra trạng thái đơn hàng.
    /// </summary>
    public class ZaloPayService
    {
        private readonly string _appId = ConfigurationManager.Instance.GetAppId();
        private readonly string _key1 = ConfigurationManager.Instance.GetSecretKey1();
        private readonly string _key2 = ConfigurationManager.Instance.GetSecretKey2();
        private readonly string _createOrderUrl = ConfigurationManager.Instance.GetCreateOrderUrl();
        private readonly string _queryOrderUrl = ConfigurationManager.Instance.GetQueryOrderUrl();

        private readonly HttpClient _httpClient;
        public ZaloPayService()
        {
            _httpClient = new HttpClient();
        }

        /// <summary>
        /// Tạo đơn hàng mới trên ZaloPay.
        /// </summary>
        /// <param name="amount">Số tiền của đơn hàng.</param>
        /// <param name="appUser">Thông tin người dùng như: id/username/tên/số điện thoại/email của user.</param>
        /// <param name="items">Item của đơn hàng, do ứng dụng tự định nghĩa</param>
        /// <returns>URL của đơn hàng đã tạo và mã apptransid của đơn hàng.</returns>
        /// <exception cref="Exception">Ném ra khi có lỗi xảy ra trong quá trình tạo đơn hàng.</exception>
        public async Task<Tuple<string, string>> CreateOrderAsync(string amount, string appUser, List<OrderItem> items)
        {
            try
            {
                Random rnd = new Random();
                var embed_data = new { };
                var itemss = new[] { new { } };
                var param = new Dictionary<string, string>();
                var app_trans_id = rnd.Next(1000000);

                param.Add("app_id", _appId);
                param.Add("app_user", appUser);
                param.Add("app_time", Utils.GetTimeStamp().ToString());
                param.Add("amount", amount);
                param.Add("app_trans_id", DateTime.Now.ToString("yyMMdd") + "_" + app_trans_id); // mã giao dich có định dạng yyMMdd_xxxx
                param.Add("embed_data", JsonConvert.SerializeObject(embed_data));
                param.Add("item", JsonConvert.SerializeObject(itemss));
                param.Add("description", "Bookstore - Thanh toán đơn hàng #" + app_trans_id);
                param.Add("bank_code", "");

                var data = _appId + "|" + param["app_trans_id"] + "|" + param["app_user"] + "|" + param["amount"] + "|"
                    + param["app_time"] + "|" + param["embed_data"] + "|" + param["item"];
                param.Add("mac", HmacHelper.Compute(ZaloPayHMAC.HMACSHA256, _key1, data));

                var result = await HttpHelper.PostFormAsync(_createOrderUrl, param);

                if (result != null && result.ContainsKey("return_code") && (Int64)result["return_code"] == 1)
                {
                    string apptransid = (string)param["app_trans_id"];
                    return new Tuple<string, string>((string)result["order_url"], apptransid);
                }
                else
                {
                    throw new Exception("Không thể tạo đơn hàng: " + result["sub_return_message"]);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi tạo đơn hàng: " + ex.Message);
            }
        }

        /// <summary>
        /// Kiểm tra trạng thái của đơn hàng trên ZaloPay.
        /// </summary>
        /// <param name="apptransid">Mã giao dịch của đơn hàng cần kiểm tra.</param>
        /// <returns>Thông tin trạng thái của đơn hàng.</returns>
        /// <exception cref="Exception">Ném ra khi có lỗi xảy ra trong quá trình kiểm tra trạng thái đơn hàng.</exception>
        public async Task<string> CheckOrderStatusAsync(string apptransid)
        {
            try
            {
                var param = new Dictionary<string, string>();
                param.Add("app_id", _appId);
                param.Add("app_trans_id", apptransid);

                var data = $"{_appId}|{apptransid}|{_key1}";

                param.Add("mac", HmacHelper.Compute(ZaloPayHMAC.HMACSHA256, _key1, data));

                var result = await HttpHelper.PostFormAsync(_queryOrderUrl, param);

                /*  response data:
                    returncode	int	1 : thành công
                    <> : chưa thanh toán / thanh toán thất bại / quá thời hạn truy vấn
                    returnmessage	String	Thông tin trạng thái đơn hàng
                    isprocessing	boolean	true: giao dịch đang xử lý
                    false: giao dịch chưa thực hiện / giao dịch đã kết thúc xử lý
                    amount	long	Số tiền giao dịch
                    discountamount	long	Số tiền giảm giá
                    zptransid	long	Mã giao dịch của ZaloPay 
                 */

                if (result != null && result.ContainsKey("return_code") && (Int64)result["return_code"] == 1)
                {
                    return (string)result["sub_return_message"];
                }
                else
                {
                    throw new Exception("Không thể kiểm tra trạng thái đơn hàng: " + result["sub_return_message"]);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi kiểm tra trạng thái đơn hàng: " + ex.Message);
            }
        }
    }
}
