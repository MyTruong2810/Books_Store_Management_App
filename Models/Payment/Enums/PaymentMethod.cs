using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books_Store_Management_App.Models.Payment.Enums
{
    /// <summary>
    /// Enum xác định phương thức thanh toán.
    /// </summary>
    public enum PaymentMethod
    {
        Momo,
        VNPay,
        Paypal,
        ZaloPay,
        Demo
    }
}
