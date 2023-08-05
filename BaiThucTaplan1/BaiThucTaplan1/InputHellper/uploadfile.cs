using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
namespace BaiThucTaplan1.InputHellper
{
    public class uploadfile
    {
        static string cloudName = "dhtsdhlwb";
        static string apiKey = "821939156185157";
        static string apiSecret = "mJakeS-voT4dFfo28B4zzNJEm8U";
        static private readonly Random rnd = new Random();
        static public Account account = new Account(cloudName, apiKey, apiSecret);
        static public Cloudinary _cloudinary = new Cloudinary(account);
        public static async Task<string> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("No file selected.");
            }
            using (var stream = file.OpenReadStream())
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, stream),
                    PublicId = "xyz-abc" + "_" + DateTime.Now.Ticks + "image",// ID công khai tùy ý cho file phải tuyệt đối là không được giống nhau
                    Transformation = new Transformation().Width(400).Height(400).Crop("fill") // Cố định kích thước ảnh thành tỷ lệ 3*4 pixel
                };
                var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                if (uploadResult.Error != null)
                {
                    // Xử lý lỗi tải lên
                    throw new Exception(uploadResult.Error.Message);
                }
                // Lấy URL công khai của file tải lên
                string imageUrl = uploadResult.SecureUrl.ToString();
                // Tiếp tục xử lý hoặc lưu thông tin về file trong cơ sở dữ liệu
                return imageUrl;

            }
        }
    }
}
