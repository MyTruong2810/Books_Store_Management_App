using Books_Store_Management_App.Models.Payment;
using Books_Store_Management_App.Services.Payment.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Books_Store_Management_App.Models;

namespace Books_Store_Management_App.Services.Payment.Implementations
{
    public class ZaloPayPaymentStrategy : IPaymentStrategy
    {
        private readonly string _appId = ConfigurationManager.Instance.GetAppId();
        private readonly string _key1 = ConfigurationManager.Instance.GetSecretKey1();
        private readonly string _key2 = ConfigurationManager.Instance.GetSecretKey2();
        private readonly string _createOrderUrl = ConfigurationManager.Instance.GetCreateOrderUrl();
        private readonly string _queryOrderUrl = ConfigurationManager.Instance.GetQueryOrderUrl();
        public Task<PaymentResult> ProcessPayment(PaymentRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<PaymentResult> QueryOrder(string appTransId)
        {
            throw new NotImplementedException();
        }
    }
}
