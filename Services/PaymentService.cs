using Books_Store_Management_App.Models.Payment.Enums;
using Books_Store_Management_App.Models.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books_Store_Management_App.Services
{
    public class PaymentService
    {
        private readonly PaymentStrategyFactory _strategyFactory;

        public PaymentService(PaymentStrategyFactory strategyFactory)
        {
            _strategyFactory = strategyFactory;
        }

        public async Task<PaymentResult> ProcessPayment(PaymentMethod method, PaymentRequest request)
        {
            var strategy = _strategyFactory.CreateStrategy(method);
            return await strategy.ProcessPayment(request);
        }
    }
}
