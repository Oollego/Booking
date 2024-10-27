using AutoMapper;
using Booking.Application.Resources;
using Booking.Domain.Dto.Facility;
using Booking.Domain.Dto.Topic;
using Booking.Domain.Dto.UserProfile;
using Booking.Domain.Entity;
using Booking.Domain.Enum;
using Booking.Domain.Interfaces.Converters;
using Booking.Domain.Interfaces.Repositories;
using Booking.Domain.Interfaces.Services;
using Booking.Domain.Interfaces.Validations;
using Booking.Domain.Result;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IBaseRepository<UserProfile> _userProfileRepository;
        private readonly ILogger _logger;
        private readonly IUserProfileValidator _userProfileValidator;
        private readonly IS3BucketRepository _bucketRepository;
        private readonly IFileService _fileService;
        private readonly IImageToLinkConverter _imageToLinkConverter;
        private readonly IBaseRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public UserProfileService(IBaseRepository<UserProfile> userProfileRepository, ILogger logger,
            IUserProfileValidator userProfileValidator, IS3BucketRepository bucketRepository, IFileService fileService,
            IImageToLinkConverter imageToLinkConverter, IMapper mapper, IBaseRepository<User> userRepository)
        {
            _userProfileRepository = userProfileRepository;
            _logger = logger;
            _userProfileValidator = userProfileValidator;
            _bucketRepository = bucketRepository;
            _fileService = fileService;
            _imageToLinkConverter = imageToLinkConverter;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<BaseResult> CreateUserProfileAsync(FileUserProfileDto dto, string? email)
        {
            //var validator = _userProfileValidator.FileUserProfileDtoValidator(dto);

            //if (!validator.IsSuccess)
            //{
            //    _logger.Warning(validator.ErrorMessage);
            //    return new BaseResult<UserProfileIdDto>
            //    {
            //        ErrorCode = validator.ErrorCode,
            //        ErrorMessage = validator.ErrorMessage
            //    };
            //}

           

            //if (dto.Titel == null || dto.Titel == "" || dto.Text == "" || dto.Text == null || dto.Image == null || newfileName == "")
            //{
            //    _logger.Warning(ErrorMessage.InvalidParameters);
            //    return new BaseResult<TopicDto>
            //    {
            //        ErrorCode = (int)ErrorCodes.InvalidParameters,
            //        ErrorMessage = ErrorMessage.InvalidParameters
            //    };
            //}

            var userProfileInBase = await _userProfileRepository.GetAll()
                .Include(up => up.User)
                .Where(up => up.User.UserEmail == email)
                .FirstOrDefaultAsync();


            if (userProfileInBase != null)
            {
                _logger.Warning(ErrorMessage.UserProfileAlreadyExists);
                return new BaseResult<UserProfileIdDto>
                {
                    ErrorCode = (int)ErrorCodes.UserProfileAlreadyExists,
                    ErrorMessage = ErrorMessage.UserProfileAlreadyExists
                };
            }

            var user = await _userRepository.GetAll().Where(u => u.UserEmail == email).FirstOrDefaultAsync();

            if (user == null)
            {
                _logger.Warning(ErrorMessage.UserNotFound);
                return new BaseResult<UserProfileIdDto>
                {
                    ErrorCode = (int)ErrorCodes.UserNotFound,
                    ErrorMessage = ErrorMessage.UserNotFound
                };
            }

            if (dto.Avatar != null && dto.Avatar.Length > 0)
            {
                string newfileName = _fileService.GetRandomFileName(dto.Avatar.FileName);
                string key = Path.Combine(S3Folders.AvatarImg, newfileName);
                var s3Responce = await _bucketRepository.UploadFileAsync(AppSource.ImgBucket, key, dto.Avatar.OpenReadStream(), dto.Avatar.ContentType);

                if (s3Responce.Success)
                {
                    UserProfile userProfile = new UserProfile
                    {
                        UserName = dto.UserName,
                        UserSurname = dto.UserSurname,
                        UserPhone = dto.UserPhone,
                        DateOfBirth = dto.DateOfBirth,
                        Avatar = newfileName,
                        IsUserPet = dto.IsUserPet,
                        UserId = user.Id,
                        CurrencyCodeId = dto.CurrencyCodeId,
                        TravelReasonId = dto.TravelReasonId,
                        CityId = dto.CityId
                    };
                    var topicResult = await _userProfileRepository.CreateAsync(userProfile);
                    await _userProfileRepository.SaveChangesAsync();

                    return new BaseResult();
                }

                _logger.Warning(ErrorMessage.StorageServerError + s3Responce.ErrorMessage);
                return new BaseResult<UserProfileIdDto>
                {
                    ErrorCode = (int)ErrorCodes.StorageServerError,
                    ErrorMessage = ErrorMessage.StorageServerError
                };
            }
            else
            {
                UserProfile userProfile = new UserProfile
                {
                    UserName = dto.UserName,
                    UserSurname = dto.UserSurname,
                    UserPhone = dto.UserPhone,
                    DateOfBirth = dto.DateOfBirth,
                    IsUserPet = dto.IsUserPet,
                    UserId = user.Id,
                    CurrencyCodeId = dto.CurrencyCodeId,
                    TravelReasonId = dto.TravelReasonId,
                    CityId = dto.CityId
                };
                var topicResult = await _userProfileRepository.CreateAsync(userProfile);
                await _userProfileRepository.SaveChangesAsync();

                return new BaseResult();
                
            }
        }

        public async Task<BaseResult<UserProfileDto>> GetUserProfileAsync(string? email)
        {
            var userProfile = await _userProfileRepository.GetAll()
                 .Include(up => up.User)
                 .Where(up => up.User.UserEmail == email)
                 .Select(up => new UserProfileDto
                 {
                     UserName = up.UserName,
                     UserSurname = up.UserSurname,
                     Avatar = _imageToLinkConverter.ConvertImageToLink(up.Avatar ?? "", S3Folders.AvatarImg),
                     UserPhone = up.UserPhone,
                     DateOfBirth = up.DateOfBirth,
                     IsUserPet = up.IsUserPet,
                     CurrencyCodeId = up.CurrencyCodeId ?? "UAH",
                     TravelReasonId = up.TravelReasonId,
                     CityId = up.CityId
                })
                 .FirstOrDefaultAsync();

            if (userProfile == null)
            {
                _logger.Warning(ErrorMessage.UserProfileNotFound);
                return new BaseResult<UserProfileDto>
                {
                    ErrorCode = (int)ErrorCodes.UserProfileNotFound,
                    ErrorMessage = ErrorMessage.UserProfileNotFound
                };
            }

            return new BaseResult<UserProfileDto>
            {
                Data = userProfile
            };
        }

        public async Task<BaseResult> UpdateUserProfileAsync(FileUserProfileDto dto, string? email)
        {
          //  var validator = _userProfileValidator.FileUserProfileDtoValidator(dto);

            //if (!validator.IsSuccess)
            //{
            //    _logger.Warning(validator.ErrorMessage);
            //    return new BaseResult()
            //    {
            //        ErrorCode = validator.ErrorCode,
            //        ErrorMessage = validator.ErrorMessage
            //    };
            //}



            //if (dto.Titel == null || dto.Titel == "" || dto.Text == "" || dto.Text == null || dto.Image == null || newfileName == "")
            //{
            //    _logger.Warning(ErrorMessage.InvalidParameters);
            //    return new BaseResult<TopicDto>
            //    {
            //        ErrorCode = (int)ErrorCodes.InvalidParameters,
            //        ErrorMessage = ErrorMessage.InvalidParameters
            //    };
            //}

            var userProfile = await _userProfileRepository.GetAll()
                .Include(up => up.User)
                .Where(up => up.User.UserEmail == email)
                .FirstOrDefaultAsync();


            if (userProfile == null)
            {
                _logger.Warning(ErrorMessage.UserProfileNotFound);
                return new BaseResult
                {
                    ErrorCode = (int)ErrorCodes.UserProfileNotFound,
                    ErrorMessage = ErrorMessage.UserProfileNotFound
                };
            }
 
            if (dto.Avatar != null && dto.Avatar.Length > 0)
            {

                string newfileName = _fileService.GetRandomFileName(dto.Avatar.FileName);
                string key = Path.Combine(S3Folders.AvatarImg, newfileName);
                var s3UploadResponce = await _bucketRepository.UploadFileAsync(AppSource.ImgBucket, key, dto.Avatar.OpenReadStream(), dto.Avatar.ContentType);

                if (userProfile.Avatar != null)
                {
                    string deleteKey = Path.Combine(S3Folders.AvatarImg, userProfile.Avatar);
                    var s3DeleteResponce = await _bucketRepository.DeleteFileAsync(AppSource.ImgBucket, deleteKey);

                    if (!s3DeleteResponce.Success)
                    {
                        _logger.Warning(ErrorMessage.StorageServerError + s3DeleteResponce.ErrorMessage);
                    }
                }

                if (s3UploadResponce.Success)
                {

                    userProfile.UserName = dto.UserName;
                    userProfile.UserSurname = dto.UserSurname;
                    userProfile.UserPhone = dto.UserPhone;
                    userProfile.DateOfBirth = dto.DateOfBirth;
                    userProfile.Avatar = newfileName;
                    userProfile.IsUserPet = dto.IsUserPet;
                    userProfile.CurrencyCodeId = dto.CurrencyCodeId;
                    userProfile.TravelReasonId = dto.TravelReasonId;
                    userProfile.CityId = dto.CityId;

                    var topicResult = _userProfileRepository.Update(userProfile);
                    await _userProfileRepository.SaveChangesAsync();

                    return new BaseResult();
                }

                _logger.Warning(ErrorMessage.StorageServerError + s3UploadResponce.ErrorMessage);
                return new BaseResult
                {
                    ErrorCode = (int)ErrorCodes.StorageServerError,
                    ErrorMessage = ErrorMessage.StorageServerError
                };
            }
            else
            {
                userProfile.UserName = dto.UserName;
                userProfile.UserSurname = dto.UserSurname;
                userProfile.UserPhone = dto.UserPhone;
                userProfile.DateOfBirth = dto.DateOfBirth;
                userProfile.IsUserPet = dto.IsUserPet;
                userProfile.CurrencyCodeId = dto.CurrencyCodeId;
                userProfile.TravelReasonId = dto.TravelReasonId;
                userProfile.CityId = dto.CityId;

                var topicResult = _userProfileRepository.Update(userProfile);
                await _userProfileRepository.SaveChangesAsync();

                return new BaseResult();
            }
        }
    }
}
