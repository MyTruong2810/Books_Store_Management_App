using Books_Store_Management_App.Models;
using Books_Store_Management_App.Models.Payment.Enums;
using Books_Store_Management_App.Services.Payment.Implementations;
using Books_Store_Management_App.Services.Payment.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books_Store_Management_App.Services
{
    /// <summary>
    /// Lớp PaymentStrategyFactory chứa phương thức tạo chiến lược thanh toán dựa trên phương thức thanh toán được chỉ định.
    /// </summary>
    public class PaymentStrategyFactory
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Phương thức khởi tạo của lớp PaymentStrategyFactory.
        /// </summary>
        /// <param name="serviceProvider">Dịch vụ cung cấp các phương thức thanh toán.</param>
        public PaymentStrategyFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _strategies = new Dictionary<PaymentMethod, Type>
            {
                { PaymentMethod.Demo, typeof(DemoPaymentStrategy) },
                { PaymentMethod.ZaloPay, typeof(ZaloPayPaymentStrategy) }
            };
        }

        private readonly Dictionary<PaymentMethod, Type> _strategies;

        /// <summary>
        /// Tạo chiến lược thanh toán dựa trên phương thức thanh toán được chỉ định.
        /// </summary>
        /// <param name="method">Phương thức thanh toán.</param>
        /// <returns>Đối tượng chiến lược thanh toán tương ứng.</returns>
        /// <exception cref="ArgumentException">Nếu phương thức thanh toán không được hỗ trợ.</exception>
        public IPaymentStrategy CreateStrategy(PaymentMethod method)
        {
            if (_strategies.TryGetValue(method, out var strategyType))
            {
                return (IPaymentStrategy)ActivatorUtilities.CreateInstance(_serviceProvider, strategyType);
            }
            throw new ArgumentException($"Phương thức thanh toán {method} không được hỗ trợ");
        }
    }
}
