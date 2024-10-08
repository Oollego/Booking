using Amazon.S3;
using Booking.Domain.Interfaces.Repositories;
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
        private readonly IS3BucketRepository _bucketRepository;
        /// <summary>
        ///
        /// </summary>
        public ImagesController(IAmazonS3 s3Client, IS3BucketRepository bucketRepository)
        {
            _s3Client = s3Client;
            _bucketRepository = bucketRepository;
        }

        /// <summary>
        /// Получить изображение по имени
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetImg(string key)
        {
            if (key == null || key == "") return BadRequest();

            var stream = await _bucketRepository.GetFileAsync("booking-img", key);

            if (stream.Success)
            {
                return File(stream.Data!, "image/jpeg");
            }
            else
            {
                return NoContent();
            }

            //var bucketExists = await Amazon.S3.Util.AmazonS3Util.DoesS3BucketExistV2Async(_s3Client, "booking-img");
            //if (!bucketExists) return NotFound($"booking-img does not exist.");
            //var s3Object = await _s3Client.GetObjectAsync("booking-img", key);
            //string a = s3Object.Headers.ContentType;
            //return File(s3Object.ResponseStream, s3Object.Headers.ContentType);
            //
        }
    }
}
