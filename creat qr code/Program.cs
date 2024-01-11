using System;
using ZXing;
using ZXing.QrCode;
using System.Drawing;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        string data = "https://www.facebook.com/imperiagrandplazaduchoa.mikgroup?mibextid=ZbWKwL"; // Dữ liệu bạn muốn chứa trong mã QR code

        // Tạo đối tượng mã QR code
        BarcodeWriter barcodeWriter = new BarcodeWriter();
        barcodeWriter.Format = BarcodeFormat.QR_CODE;
        barcodeWriter.Options = new QrCodeEncodingOptions
        {
            Width = 1600,  // Độ rộng của hình ảnh mã QR code
            Height = 1600, // Độ cao của hình ảnh mã QR code
        };

        // Tạo hình ảnh mã QR code với màu nền trong suốt và màu đỏ
        Bitmap qrCodeBitmap = barcodeWriter.Write(data);
        qrCodeBitmap.MakeTransparent(Color.White); // Chuyển màu nền trắng thành trong suốt
        //ReplaceColor(qrCodeBitmap, Color.Black, Color.Yellow); // Thay đổi màu đen thành màu đỏ

        ReplaceColor(qrCodeBitmap, Color.Black, Color.Yellow); // Thay đổi màu đen thành màu đỏ


        // Lưu hình ảnh mã QR code dưới dạng file PNG
        qrCodeBitmap.Save("QRCode.png", System.Drawing.Imaging.ImageFormat.Png);

        Console.WriteLine("QR Code đã được tạo và lưu thành công.");
    }


    // Hàm tìm màu cần thay đổi trong ảnh bitmap
    static Color FindColorToReplace(Bitmap bitmap)
    {
        // Duyệt qua từng pixel để tìm màu cần thay đổi
        for (int x = 0; x < bitmap.Width; x++)
        {
            for (int y = 0; y < bitmap.Height; y++)
            {
                Color pixelColor = bitmap.GetPixel(x, y);
                if (pixelColor != Color.Black) // Loại trừ màu trắng
                {
                    return pixelColor;
                }
            }
        }
        return Color.Yellow; // Trả về màu đen nếu không tìm thấy màu khác trắng
    }

    // Hàm thay đổi màu trong ảnh bitmap
    static void ReplaceColor(Bitmap bitmap, Color oldColor, Color newColor)
    {
        int dem = 0;
        for (int x = 0; x < bitmap.Width; x++)
        {
            for (int y = 0; y < bitmap.Height; y++)
            {
                Color pixelColor = bitmap.GetPixel(x, y);
                if (pixelColor.A != 0)
                {
                    bitmap.SetPixel(x, y, newColor);
                    dem++;
                    Console.WriteLine("vẽ " + dem);
                }
            }
        }
    }
}
