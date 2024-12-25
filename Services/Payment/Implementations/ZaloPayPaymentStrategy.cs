using Books_Store_Management_App.Models.Payment;
using Books_Store_Management_App.Services.Payment.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Books_Store_Management_App.Models;
using Newtonsoft.Json;
using ZaloPay.Helper.Crypto;
using ZaloPay.Helper;

namespace Books_Store_Management_App.Services.Payment.Implementations
{
    /// <summary>
    /// Chiến lược thanh toán ZaloPay.
    /// </summary>
    public class ZaloPayPaymentStrategy : IPaymentStrategy
    {
        private readonly string _appId = ConfigurationManager.Instance.GetAppId();
        private readonly string _key1 = ConfigurationManager.Instance.GetSecretKey1();
        private readonly string _key2 = ConfigurationManager.Instance.GetSecretKey2();
        private readonly string _createOrderUrl = ConfigurationManager.Instance.GetCreateOrderUrl();
        private readonly string _queryOrderUrl = ConfigurationManager.Instance.GetQueryOrderUrl();

        /// <summary>
        /// Xử lý thanh toán.
        /// </summary>
        /// <param name="request">Yêu cầu thanh toán.</param>
        /// <returns>Kết quả thanh toán.</returns>
        public async Task<PaymentResult> ProcessPayment(PaymentRequest request)
        {
            try
            {
                var embed_data = new { merchantinfo = "embeddata123" };
                var itemss = new[] { new { } };
                var param = new Dictionary<string, string>();
                var app_trans_id = Guid.NewGuid().ToString();

                param.Add("appid", _appId);
                param.Add("appuser", request.AppUser);
                param.Add("apptime", Utils.GetTimeStamp().ToString());
                param.Add("amount", request.Amount);
                param.Add("apptransid", DateTime.Now.ToString("yyMMdd") + "_" + app_trans_id); // mã giao dich có định dạng yyMMdd_xxxx
                param.Add("embeddata", JsonConvert.SerializeObject(embed_data));
                param.Add("item", JsonConvert.SerializeObject(itemss));
                param.Add("description", "Bookstore - Thanh toán đơn hàng #" + app_trans_id);
                param.Add("bank_code", "");

                var data = _appId + "|" + param["apptransid"] + "|" + param["appuser"] + "|" + param["amount"] + "|"
                    + param["apptime"] + "|" + param["embeddata"] + "|" + param["item"];
                param.Add("mac", HmacHelper.Compute(ZaloPayHMAC.HMACSHA256, _key1, data));

                var result = await HttpHelper.PostFormAsync(_createOrderUrl, param);

                if (result == null)
                {
                    return new PaymentResult
                    {
                        Success = false,
                        Message = "Failed to process payment"
                    };
                }

                if (result["returncode"].ToString() == "1")
                {
                    return new PaymentResult
                    {
                        Success = true,
                        Message = result["returnmessage"].ToString(),
                        orderUrl = result["orderurl"].ToString(),
                        appTransId = param["apptransid"],
                        qrCode = result["qrcode"].ToString()
                    };
                }

                return new PaymentResult
                {
                    Success = false,
                    Message = result["returnmessage"].ToString()
                };
            }
            catch (Exception ex)
            {
                return new PaymentResult
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        /// <summary>
        /// Truy vấn thông tin đơn hàng.
        /// </summary>
        /// <param name="appTransId">Mã giao dịch ứng dụng.</param>
        /// <returns>Kết quả truy vấn đơn hàng.</returns>
        public async Task<PaymentResult> QueryOrder(string appTransId)
        {
            try
            {
                var param = new Dictionary<string, string>();
                param.Add("appid", _appId);
                param.Add("apptransid", appTransId);

                var data = $"{_appId}|{appTransId}|{_key1}";

                param.Add("mac", HmacHelper.Compute(ZaloPayHMAC.HMACSHA256, _key1, data));

                var result = await HttpHelper.PostFormAsync(_queryOrderUrl, param);

                if (result == null)
                {
                    return new PaymentResult
                    {
                        Success = false,
                        Message = "Failed to query order"
                    };
                }

                if (result["returncode"].ToString() == "1")
                {
                    return new PaymentResult
                    {
                        Success = true,
                        Message = "Order queried successfully",
                        appTransId = result["apptransid"].ToString(),
                    };
                }

                return new PaymentResult
                {
                    Success = false,
                    Message = result["returnmessage"].ToString()
                };

            }
            catch (Exception ex)
            {
                return new PaymentResult
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}
