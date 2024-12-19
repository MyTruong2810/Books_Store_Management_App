using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using QRCoder;
using Windows.Storage.Streams;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;


namespace Books_Store_Management_App.Helpers
{
    /// <summary>
    /// Dịch vụ tạo mã QR Code từ dữ liệu truyền vào.
    /// </summary>
    public class QRCodeGeneratorService
    {
        /// <summary>
        /// Tạo mã QR Code từ dữ liệu truyền vào.
        /// </summary>
        /// <param name="data">Chuỗi dữ liệu cần mã hóa thành mã QR.</param>
        /// <returns>Ảnh Bitmap của mã QR được tạo.</returns>
        public async static Task<BitmapImage> GenerateQRCode(string data)
        {
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q))
            using (PngByteQRCode qrCode = new PngByteQRCode(qrCodeData))
            {
                byte[] qrCodeImage = qrCode.GetGraphic(20);

                using (var stream = new InMemoryRandomAccessStream())
                {
                    await stream.WriteAsync(qrCodeImage.AsBuffer());
                    stream.Seek(0); // Đặt chỉ số về đầu stream

                    // Tạo BitmapImage từ stream
                    var bitmapImage = new BitmapImage();
                    await bitmapImage.SetSourceAsync(stream);

                    return bitmapImage;
                }
            }
        }
    }
}
