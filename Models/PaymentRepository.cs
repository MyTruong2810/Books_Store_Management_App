using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Books_Store_Management_App.Models.Payment;
using Npgsql;
using NpgsqlTypes;

namespace Books_Store_Management_App.Models
{
    /// <summary>
    /// Class xử lý các thao tác liên quan đến thanh toán.
    /// </summary>
    public class PaymentRepository
    {
        private readonly string _connectionString = ConfigurationManager.Instance.GetConnectionString();

        /// <summary>
        /// Xử lý thanh toán dựa trên yêu cầu thanh toán được cung cấp.
        /// </summary>
        /// <param name="paymentRequest">Yêu cầu thanh toán.</param>
        /// <returns>Kết quả xử lý thanh toán, bao gồm trạng thái thành công và thông báo.</returns>
        public async Task<(bool success, string message)> ProcessDemoPayment(PaymentRequest paymentRequest)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            using var transaction = await connection.BeginTransactionAsync();
            try
            {
                // 1. Lấy thông tin tài khoản admin nhận tiền
                var adminAccount = await GetAdminAccount(connection);
                if (adminAccount == null)
                {
                    return (false, "Không tìm thấy tài khoản admin.");
                }

                // 2. Lấy tài khoản người dùng
                if (paymentRequest.MemberPaymentId != null)
                {
                    var memberBalance = await GetMemberBalance(connection, paymentRequest.MemberPaymentId.Value);
                    if (memberBalance < Decimal.Parse(paymentRequest.Amount))
                    {
                        return (false, "Số dư không đủ để thanh toán.");
                    }

                    // 3. Trừ tiền từ tài khoản người dùng
                    await UpdateMemberBalance(connection, paymentRequest.MemberPaymentId.Value, -Decimal.Parse(paymentRequest.Amount));
                }
                else if (!string.IsNullOrEmpty(paymentRequest.MemberPhoneNumber))
                {
                    // 2. Lấy tài khoản người dùng
                    var memberPaymentId = await GetMemberIdByPhoneNumber(connection, paymentRequest.MemberPhoneNumber);

                    if (memberPaymentId == 0)
                    {
                        return (false, "Không tìm thấy tài khoản người dùng.");
                    }

                    var memberBalance = await GetMemberBalance(connection, memberPaymentId);
                    if (memberBalance < Decimal.Parse(paymentRequest.Amount))
                    {
                        return (false, "Số dư không đủ để thanh toán.");
                    }

                    // 3. Trừ tiền từ tài khoản người dùng
                    await UpdateMemberBalance(connection, memberPaymentId, -Decimal.Parse(paymentRequest.Amount));
                }

                // 4. Cộng tiền vào tài khoản admin
                await UpdateMemberBalance(connection, adminAccount.Id, Decimal.Parse(paymentRequest.Amount));

                // 5. Tạo bản ghi giao dịch
                int transactionId = await CreateTransactionRecord(
                    connection,
                    paymentRequest.MemberPaymentId,
                    adminAccount.Id,
                    Decimal.Parse(paymentRequest.Amount),
                    paymentRequest.orderId,
                    paymentRequest.Description
                );

                await transaction.CommitAsync();
                return (true, transactionId.ToString());
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return (false, ex.Message);
            }
        }

        /// <summary>
        /// Phương thức lấy thông tin tài khoản admin.
        /// </summary>
        /// <param name="connection">Kết nối đến cơ sở dữ liệu.</param>
        /// <returns>Thông tin tài khoản admin.</returns>
        private async Task<Account> GetAdminAccount(NpgsqlConnection connection)
        {
            const string sql = @"
                SELECT id, balance, is_admin, time_create
                FROM accounts
                WHERE is_admin = true
                LIMIT 1
            ";

            using var command = new NpgsqlCommand(sql, connection);
            using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new Account
                {
                    Id = reader.GetInt32(0),
                    Balance = reader.GetDecimal(1),
                    IsAdmin = reader.GetBoolean(2),
                    CreatedAt = reader.GetDateTime(3)
                };
            }

            return null;
        }

        /// <summary>
        /// Phương thức lấy số dư của tài khoản thành viên.
        /// </summary>
        /// <param name="connection">Kết nối đến cơ sở dữ liệu.</param>
        /// <param name="memberId">ID của thành viên.</param>
        /// <returns>Số dư của tài khoản thành viên.</returns>
        private async Task<Decimal> GetMemberBalance(NpgsqlConnection connection, int memberId)
        {
            const string sql = @"
                SELECT balance
                FROM accounts
                WHERE id = @memberId
            ";

            using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("memberId", memberId);

            var result = await command.ExecuteScalarAsync();
            return result == null ? 0 : (Decimal)result;
        }

        /// <summary>
        /// Cập nhật số dư của tài khoản thành viên.
        /// </summary>
        /// <param name="connection">Kết nối đến cơ sở dữ liệu.</param>
        /// <param name="memberId">ID của thành viên.</param>
        /// <param name="amount">Số tiền cần cập nhật.</param>
        /// <returns></returns>
        private async Task UpdateMemberBalance(NpgsqlConnection connection, int? memberId, Decimal amount)
        {
            const string sql = @"
                UPDATE accounts
                SET balance = balance + @amount
                WHERE id = @memberId
            ";

            using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("memberId", memberId);
            command.Parameters.AddWithValue("amount", amount);

            await command.ExecuteNonQueryAsync();
        }
         

        /// Phương thức tạo bản ghi giao dịch mới trong cơ sở dữ liệu.
        /// </summary>
        /// <param name="connection">Kết nối đến cơ sở dữ liệu.</param>
        /// <param name="fromUserId">ID của người gửi.</param>
        /// <param name="toUserId">ID của người nhận.</param>
        /// <param name="amount">Số tiền giao dịch.</param>
        /// <param name="orderId">ID của đơn hàng.</param>
        /// <param name="content">Nội dung giao dịch.</param>
        /// <returns>ID của bản ghi giao dịch mới được tạo.</returns>
        private async Task<int> CreateTransactionRecord(
            NpgsqlConnection connection,
            int? fromUserId,
            int toUserId,
            Decimal amount,
            int orderId,
            string content)
        {
            const string sql = @"
                INSERT INTO transactions
                    (from_user_id, to_user_id, amount, content, order_id)
                VALUES(@fromUserId, @toUserId, @amount, @content, @orderId)
                RETURNING id
            ";

            using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("fromUserId", NpgsqlDbType.Integer, (object)fromUserId ?? DBNull.Value);
            command.Parameters.AddWithValue("toUserId", toUserId);
            command.Parameters.AddWithValue("amount", amount);
            command.Parameters.AddWithValue("content", content);
            command.Parameters.AddWithValue("orderId", orderId);

            return (int)await command.ExecuteScalarAsync();
        }

        /// <summary>
        /// Phương thức lấy ID thành viên dựa trên số điện thoại.
        /// </summary>
        /// <param name="connection">Kết nối đến cơ sở dữ liệu.</param>
        /// <param name="phoneNumber">Số điện thoại của thành viên.</param>
        /// <returns>ID của thành viên tương ứng với số điện thoại, hoặc 0 nếu không tìm thấy.</returns>
        private async Task<int> GetMemberIdByPhoneNumber(NpgsqlConnection connection, string phoneNumber)
        {
            const string sql = @"
                SELECT cvv
                FROM customer
                WHERE phone = @phoneNumber
            ";

            using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("phoneNumber", phoneNumber);

            var result = await command.ExecuteScalarAsync();

            return result == null ? 0 : (int)result;
        }
    }
}
