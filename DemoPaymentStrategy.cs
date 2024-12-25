using Books_Store_Management_App.Models;
using Books_Store_Management_App.Models.Payment;
using Books_Store_Management_App.Services.Payment.Interfaces;
using System.Threading.Tasks;

/// <summary>
/// Lớp DemoPaymentStrategy triển khai giao diện IPaymentStrategy và cung cấp các phương thức xử lý thanh toán.
/// </summary>
public class DemoPaymentStrategy : IPaymentStrategy
{
    private readonly PaymentRepository _paymentRepository;

    public DemoPaymentStrategy(PaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    /// <summary>
    /// Xử lý thanh toán dựa trên yêu cầu thanh toán được cung cấp.
    /// </summary>
    /// <param name="request">Yêu cầu thanh toán.</param>
    /// <returns>Kết quả xử lý thanh toán, bao gồm trạng thái thành công và thông báo.</returns>
    public async Task<PaymentResult> ProcessPayment(PaymentRequest request)
    {
        var (success, message) = await _paymentRepository.ProcessDemoPayment(request);

        return new PaymentResult
        {
            Success = success,
            TransactionId = success ? message : null,
            Message = success ? "Thanh toán thành công" : message
        };
    }

    /// <summary>
    /// Truy vấn thông tin đơn hàng dựa trên appTransId.
    /// </summary>
    /// <param name="appTransId">ID giao dịch của ứng dụng.</param>
    /// <returns>Kết quả truy vấn đơn hàng, bao gồm trạng thái thành công và thông báo.</returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<PaymentResult> QueryOrder(string appTransId)
    {
        throw new NotImplementedException();
    }
}
