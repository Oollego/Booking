using Booking.Application.Resources;
using Booking.Domain.Entity;
using Booking.Domain.Enum;
using Booking.Domain.Interfaces.Repositories;
using Booking.Domain.Interfaces.Services;
using Booking.Domain.Interfaces.Services.ServiceDto;
using Booking.Domain.Result;
using Google.Apis.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Services
{
    public class GoogleAuthService: IGoogleAuthService
    {
        //private readonly string _clientId;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientRepository _httpClientRepository;
        private readonly IFileService _fileService;
        private readonly IS3BucketRepository _bucketRepository;
        private readonly IBaseRepository<UserProfile> _userProfileRepository;

        public GoogleAuthService(ILogger logger, IConfiguration configuration, IHttpClientRepository httpClientRepository,
            IFileService fileService, IS3BucketRepository bucketRepository, IBaseRepository<UserProfile> userProfileRepository)
        {
            _configuration = configuration;
            _logger = logger;
            _httpClientRepository = httpClientRepository;
            _fileService = fileService;
            _bucketRepository = bucketRepository;
            _userProfileRepository = userProfileRepository;
        }

        async Task<BaseResult<UserAuth>> IGoogleAuthService.ValidateIdTokenAsync(string idToken)
        {
            GoogleJsonWebSignature.Payload payload;
            string _clientId = _configuration.GetSection("Google").GetSection("ClientId").Value ?? "";
            try
            {
                var settings = new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new List<string> { _clientId }
                };

                payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);
            }
            catch (Exception ex)
            {
                _logger.Warning(ErrorMessage.GoogleAuthFailed, ex.Message);
                return new BaseResult<UserAuth>
                {
                    ErrorMessage = ErrorMessage.GoogleAuthFailed,
                    ErrorCode = (int)ErrorCodes.GoogleAuthFailed
                };
            }

            var httpResult = await _httpClientRepository.GetStreamFromUrlAsync(payload.Picture);

            UserAuth user = new UserAuth()
            {
                Email = payload.Email,
                Name = payload.GivenName,
                Surname = payload.FamilyName,
            };

            if (httpResult.Success)
            {
                using Stream avatarStream = httpResult.Data!.StreamData;

                string newfileName = _fileService.GetRandomFileName(httpResult.Data.FileName);
                string key = Path.Combine(S3Folders.AvatarImg, newfileName);

                var s3Responce = await _bucketRepository.UploadFileAsync(AppSource.ImgBucket, key, avatarStream, httpResult.Data!.ContentType ?? "image/jpeg");

                if (s3Responce.Success)
                {
                    user.AvatarUrl = newfileName;
                    var userProfile = await _userProfileRepository.GetAll()
                        .Include(up => up.User)
                        .Where(up => up.User.UserEmail == payload.Email)
                        .FirstOrDefaultAsync();

                    if(userProfile != null && userProfile.Avatar != null)
                    {
                        string oldAvatarKey = Path.Combine(S3Folders.AvatarImg, userProfile.Avatar);
                        _ = await _bucketRepository.DeleteFileAsync(AppSource.ImgBucket, oldAvatarKey);
                    }
                } 
            }

            return new BaseResult<UserAuth>
            {
                Data = user,
            };
        }
    }
}
