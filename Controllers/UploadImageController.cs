using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using Base64ToImageURL.Models;
using System.IO;
using System;

namespace Base64ToImageURL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadImageController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post(UploadImage uploadImage)
        {
            byte[] bytes = Convert.FromBase64String(uploadImage.Image);

            Image image;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = Image.FromStream(ms);

                string guid = Guid.NewGuid().ToString();
                string imagePath = "images/" + guid + "." + GetFileExtension(uploadImage.Image);

                image.Save("wwwroot/" + imagePath, ImageFormat.Png);
            }
            
            return Ok();
        }
        public static string GetFileExtension(string base64String)
        {
            var data = base64String.Substring(0, 5);

            switch (data.ToUpper())
            {
                case "IVBOR":
                    return "png";
                case "/9J/4":
                    return "jpg";
                case "AAAAF":
                    return "mp4";
                case "JVBER":
                    return "pdf";
                case "AAABA":
                    return "ico";
                case "UMFYI":
                    return "rar";
                case "E1XYD":
                    return "rtf";
                case "U1PKC":
                    return "txt";
                case "MQOWM":
                case "77U/M":
                    return "srt";
                default:
                    return string.Empty;
            }
        }
    }
}
