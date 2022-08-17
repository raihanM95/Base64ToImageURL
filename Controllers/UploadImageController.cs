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
            }

            image.Save("wwwroot/images/test2." + uploadImage.Type, ImageFormat.Png);

            return Ok();
        }
    }
}
