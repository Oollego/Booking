using Amazon.S3;
using Amazon.S3.Model;
using Booking.Application.Resources;
using Booking.Domain.Entity;
using Booking.Domain.Interfaces.Repositories;
using Booking.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Services
{
    public class Aws3sService
    {
        private readonly IS3BucketRepository _bucketRepository;
        private readonly IFileService _fileService;

        public Aws3sService(IS3BucketRepository bucketRepository, IFileService fileService)
        {
            _bucketRepository = bucketRepository;
            _fileService = fileService;
        }

        //public async Task<bool> SaveUserAvatarToBucket(Stream stream, string fileName)
        //{
        //    string newfileName = _fileService.GetRandomFileName(fileName);
        //    string key = Path.Combine(S3Folders.AvatarImg, newfileName);

        //    var s3Responce = await _bucketRepository.UploadFileAsync(AppSource.ImgBucket, key, avatarStream, httpResult.Data!.ContentType ?? "image/jpeg");

        //    if (s3Responce.Success)
        //    {
        //        user.AvatarUrl = newfileName;
        //        var userProfile = await _userProfileRepository.GetAll()
        //            .Include(up => up.User)
        //            .Where(up => up.User.UserEmail == payload.Email)
        //            .FirstOrDefaultAsync();

        //        if (userProfile != null && userProfile.Avatar != null)
        //        {
        //            string oldAvatarKey = Path.Combine(S3Folders.AvatarImg, userProfile.Avatar);
        //            _ = await _bucketRepository.DeleteFileAsync(AppSource.ImgBucket, oldAvatarKey);
        //        }
        //    }
        //} 
        ////public async Task<IActionResult> GetImg(string key)
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
