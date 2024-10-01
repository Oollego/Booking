using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Services
{
    public class Aws3sService
    {
        private readonly IAmazonS3 _s3Client;

        public Aws3sService(IAmazonS3 s3Client)
        {
            _s3Client = s3Client;
        }

        //public async Task<IActionResult> GetImg(string key)
        //{
        //    var bucketExists = await Amazon.S3.Util.AmazonS3Util.DoesS3BucketExistV2Async(_s3Client, "booking-img");
        //    if (!bucketExists) return NotFound($"booking-img does not exist.");
        //    var s3Object = await _s3Client.GetObjectAsync("booking-img", key);
        //    return File(s3Object.ResponseStream, s3Object.Headers.ContentType);

        //}
        //public async Task<IActionResult> UploadFileAsync(IFormFile file, string bucketName, string? prefix)
        //{
        //    var bucketExists = await _s3Client.DoesS3BucketExistAsync(bucketName);
        //    if (!bucketExists) return NotFound($"Bucket {bucketName} does not exist.");
        //    var request = new PutObjectRequest()
        //    {
        //        BucketName = bucketName,
        //        Key = string.IsNullOrEmpty(prefix) ? file.FileName : $"{prefix?.TrimEnd('/')}/{file.FileName}",
        //        InputStream = file.OpenReadStream()
        //    };
        //    request.Metadata.Add("Content-Type", file.ContentType);
        //    await _s3Client.PutObjectAsync(request);
        //    return Ok($"File {prefix}/{file.FileName} uploaded to S3 successfully!");
        //}
    }
}
