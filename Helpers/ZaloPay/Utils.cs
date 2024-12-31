using System;

namespace ZaloPay.Helper
{
    /// <summary>
    /// Cung cấp các phương thức hữu ích liên quan đến thời gian và ngày tháng.
    /// </summary>
    public class Utils
    {
        /// <summary>
        /// Chuyển đổi một đối tượng DateTime thành timestamp Unix.
        /// </summary>
        /// <param name="date">Đối tượng DateTime cần chuyển đổi.</param>
        /// <returns>Giá trị timestamp Unix tương ứng với đối tượng DateTime.</returns>
        public static long GetTimeStamp(DateTime date)
        {
            return (long)(date.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds;
        }

        /// <summary>
        /// Chuyển đổi thời gian hiện tại thành timestamp Unix.
        /// </summary>
        /// <returns>Giá trị timestamp Unix tương ứng với thời gian hiện tại.</returns>
        public static long GetTimeStamp()
        {
            return GetTimeStamp(DateTime.Now);
        }
    }
}