using Amazon.S3;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace Booking.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IAmazonS3 _s3Client;
        /// <summary>
        ///
        /// </summary>
        public ImagesController(IAmazonS3 s3Client)
        {
            _s3Client = s3Client;
        }

        /// <summary>
        /// Получить изображение по имени
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetImg(string key)
        {
            var bucketExists = await Amazon.S3.Util.AmazonS3Util.DoesS3BucketExistV2Async(_s3Client, "booking-img");
            if (!bucketExists) return NotFound($"booking-img does not exist.");
            var s3Object = await _s3Client.GetObjectAsync("booking-img", key);
            return File(s3Object.ResponseStream, s3Object.Headers.ContentType);
        }
    }
}
