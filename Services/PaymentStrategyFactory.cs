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
    public class PaymentStrategyFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public PaymentStrategyFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _strategies = new Dictionary<PaymentMethod, Type>
        {
            { PaymentMethod.Demo, typeof(DemoPaymentStrategy) },
        };
        }

        private readonly Dictionary<PaymentMethod, Type> _strategies;

        public IPaymentStrategy CreateStrategy(PaymentMethod method)
        {
            if (_strategies.TryGetValue(method, out var strategyType))
            {
                return (IPaymentStrategy)ActivatorUtilities.CreateInstance(_serviceProvider, strategyType);
            }
            throw new ArgumentException($"Payment method {method} is not supported");
        }
    }
}
