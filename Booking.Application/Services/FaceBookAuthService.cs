using Amazon.Runtime;
using Booking.Application.Resources;
using Booking.Domain.Entity;
using Booking.Domain.Enum;
using Booking.Domain.Interfaces.Repositories;
using Booking.Domain.Interfaces.Services;
using Booking.Domain.Interfaces.Services.ServiceDto;
using Booking.Domain.Models.FaceBook;
using Booking.Domain.Result;
using Booking.Domain.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Services
{
    public class FaceBookAuthService: IFaceBookAuthService
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientRepository _httpClientRepository;
        private readonly IFileService _fileService;
        private readonly IS3BucketRepository _bucketRepository;
        private readonly IBaseRepository<UserProfile> _userProfileRepository;
        private readonly HttpClient _httpClient;
        private readonly FaceBookSettings _facebookAuthSettings;

        public FaceBookAuthService(ILogger logger, IConfiguration configuration,
            IHttpClientRepository httpClientRepository, IFileService fileService,
            IS3BucketRepository bucketRepository, IBaseRepository<UserProfile> userProfileRepository,
            IHttpClientFactory httpClientFactory, IOptions<FaceBookSettings> facebookAuthSettings)
        {
            _logger = logger;
            _configuration = configuration;
            _httpClientRepository = httpClientRepository;
            _fileService = fileService;
            _bucketRepository = bucketRepository;
            _userProfileRepository = userProfileRepository;
            _httpClient = httpClientFactory.CreateClient("Facebook");
            _facebookAuthSettings = facebookAuthSettings.Value;
        }

        async Task<BaseResult<UserAuth>> IFaceBookAuthService.GetUserFromFacebookTokenAsync(string accessToken)
        {
            var validatedFbToken = await ValidateFacebookToken(accessToken);

            if (!validatedFbToken.IsSuccess)
            {
                return new BaseResult<UserAuth>
                {
                    ErrorMessage = validatedFbToken.ErrorMessage,
                    ErrorCode = validatedFbToken.ErrorCode
                };
            }

            var userInfo = await GetFacebookUserInformation(accessToken);

            if (!validatedFbToken.IsSuccess)
            {
                return new BaseResult<UserAuth>
                {
                    ErrorMessage = validatedFbToken.ErrorMessage,
                    ErrorCode = validatedFbToken.ErrorCode
                };
            }

            if (userInfo.Data == null)
            {
                return new BaseResult<UserAuth>
                {
                    ErrorMessage = ErrorMessage.NoDataAvailableInSocialAuthentication,
                    ErrorCode = (int)ErrorCodes.NoDataAvailableInSocialAuthentication
                };
            }
            var httpResult = await _httpClientRepository.GetStreamFromUrlAsync(userInfo.Data.Picture.Data.Url);

            
            UserAuth userAuth = new UserAuth
            {
                Name = userInfo.Data.FirstName,
                Surname = userInfo.Data.LastName,
                Email = userInfo.Data.Email,
            };

            if (httpResult.Success)
            {
                using Stream avatarStream = httpResult.Data!.StreamData;

                string newfileName = _fileService.GetRandomFileName(httpResult.Data.FileName);
                string key = Path.Combine(S3Folders.AvatarImg, newfileName);

                var s3Responce = await _bucketRepository.UploadFileAsync(AppSource.ImgBucket, key, avatarStream, httpResult.Data!.ContentType ?? "image/jpeg");

                if (s3Responce.Success)
                {
                    userAuth.AvatarUrl = newfileName;
                    var userProfile = await _userProfileRepository.GetAll()
                        .Include(up => up.User)
                        .Where(up => up.User.UserEmail == userInfo.Data.Email)
                        .FirstOrDefaultAsync();

                    if (userProfile != null && userProfile.Avatar != null)
                    {
                        string oldAvatarKey = Path.Combine(S3Folders.AvatarImg, userProfile.Avatar);
                        _ = await _bucketRepository.DeleteFileAsync(AppSource.ImgBucket, oldAvatarKey);
                    }
                }
            }
           

            return new BaseResult<UserAuth>
            {
                Data = userAuth
            };
        }

        private async Task<BaseResult<FacebookTokenValidationResponse>> ValidateFacebookToken(string accessToken)
        {
            try
            {
                string TokenValidationUrl = _facebookAuthSettings.TokenValidationUrl;
                var url = string.Format(TokenValidationUrl, accessToken, _facebookAuthSettings.AppId, _facebookAuthSettings.AppSecret);
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var responseAsString = await response.Content.ReadAsStringAsync();

                    var tokenValidationResponse = JsonConvert.DeserializeObject<FacebookTokenValidationResponse>(responseAsString);
                    return new BaseResult<FacebookTokenValidationResponse>
                    {
                        Data = tokenValidationResponse
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.StackTrace ?? "" , ex);
            }

            return new BaseResult<FacebookTokenValidationResponse>
            {
                ErrorMessage = ErrorMessage.FailedToGetResponseFromFaceBook,
                ErrorCode = (int)ErrorCodes.FailedToGetResponseFromFaceBook
            };

        }

        private async Task<BaseResult<FacebookUserInfoResponse>> GetFacebookUserInformation(string accessToken)
        {
            try
            {
                string userInfoUrl = _facebookAuthSettings.UserInfoUrl;
                string url = string.Format(userInfoUrl, accessToken);

                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var responseAsString = await response.Content.ReadAsStringAsync();
                    var userInfoResponse = JsonConvert.DeserializeObject<FacebookUserInfoResponse>(responseAsString);
                    return new BaseResult<FacebookUserInfoResponse>
                    {
                        Data = userInfoResponse
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.StackTrace ?? "", ex);
            }

            return new BaseResult<FacebookUserInfoResponse>
            {
                ErrorMessage = ErrorMessage.FailedToGetResponseFromFaceBook,
                ErrorCode = (int)ErrorCodes.FailedToGetResponseFromFaceBook
            };

        }

        
    }
}