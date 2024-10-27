using Booking.Application.Resources;
using Booking.Domain.Dto.Reason;
using Booking.Domain.Dto.Room;
using Booking.Domain.Dto.Topic;
using Booking.Domain.Dto.UserTopicDto;
using Booking.Domain.Entity;
using Booking.Domain.Enum;
using Booking.Domain.Interfaces.Converters;
using Booking.Domain.Interfaces.Repositories;
using Booking.Domain.Interfaces.Services;
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
    public class UserTopicService: IUserTopicService
    {
        private readonly ILogger _logger;
        private readonly IBaseRepository<UserProfileTopic> _userTopicRepository;
        private readonly IBaseRepository<UserProfile> _userProfileRepository;
        private readonly IImageToLinkConverter _imageToLinkConverter;

        public UserTopicService(ILogger logger, IBaseRepository<UserProfileTopic> userTopicRepository,
            IBaseRepository<UserProfile> userProfileRepository, IImageToLinkConverter imageToLinkConverter)
        {
            _logger = logger;
            _userTopicRepository = userTopicRepository;
            _userProfileRepository = userProfileRepository;
            _imageToLinkConverter = imageToLinkConverter;
        }

        public async Task<BaseResult<UserTopicDto>> CreatUserTopicAsync(long topicId, string? email)
        {
            if (topicId <= 0)
            {
                _logger.Warning(ErrorMessage.InvalidParameters);
                return new BaseResult<UserTopicDto>
                {
                    ErrorCode = (int)ErrorCodes.InvalidParameters,
                    ErrorMessage = ErrorMessage.InvalidParameters
                };
            }

            var userProfile = await _userProfileRepository.GetAll()
                .Include(up => up.User)
                .Where(up => up.User.UserEmail == email)
                .FirstOrDefaultAsync();

            if (userProfile == null) 
            {
                _logger.Warning(ErrorMessage.AuthenticationRequired);
                return new BaseResult<UserTopicDto>
                {
                    ErrorCode = (int)ErrorCodes.AuthenticationRequired,
                    ErrorMessage = ErrorMessage.AuthenticationRequired
                };
            }

            var userTopic = await _userTopicRepository.GetAll()
                .Where(ut => ut.TopicId == topicId)
                .FirstOrDefaultAsync();


            if (userTopic != null)
            {
                _logger.Warning(ErrorMessage.TopicAlreadyExists);
                return new BaseResult<UserTopicDto>
                {
                    ErrorCode = (int)ErrorCodes.TopicAlreadyExists,
                    ErrorMessage = ErrorMessage.TopicAlreadyExists
                };
            }

            var newUserTopic = new UserProfileTopic()
            {
                TopicId = topicId,
                UserProfileId = userProfile.Id
            };

            newUserTopic = await _userTopicRepository.CreateAsync(newUserTopic);
            await _userTopicRepository.SaveChangesAsync();

            return new BaseResult<UserTopicDto>()
            {
                Data = new UserTopicDto
                {
                    TopicId = newUserTopic.TopicId,
                    UserProfileId = userProfile.Id
                }
            };
        }

        public async Task<BaseResult<UserTopicDto>> DeleteUserTopicAsync(long topicId, string? email)
        {
            if (topicId <= 0)
            {
                _logger.Warning(ErrorMessage.InvalidParameters);
                return new BaseResult<UserTopicDto>
                {
                    ErrorCode = (int)ErrorCodes.InvalidParameters,
                    ErrorMessage = ErrorMessage.InvalidParameters
                };
            }

            var userTopic = await _userTopicRepository.GetAll()
                .Include(ut => ut.Topic)
                .Include(ut => ut.UserProfile)
                    .ThenInclude(up => up.User)
                .Where(ut => ut.Topic.Id == topicId)
                .Where(ut => ut.UserProfile.User.UserEmail == email)
                .FirstOrDefaultAsync();

            if (userTopic == null)
            {
                _logger.Warning(ErrorMessage.TopicNotFound);
                return new BaseResult<UserTopicDto>
                {
                    ErrorCode = (int)ErrorCodes.TopicNotFound,
                    ErrorMessage = ErrorMessage.TopicNotFound
                };
            }

            userTopic = _userTopicRepository.Remove(userTopic);
            await _userTopicRepository.SaveChangesAsync();

            var userTopicDto = new UserTopicDto()
            {
                TopicId = userTopic.TopicId,
                UserProfileId = userTopic.UserProfileId
            };

            return new BaseResult<UserTopicDto>()
            {
                Data = userTopicDto,
            };
        }

        public async Task<CollectionResult<TopicDto>> GetAllUserTopicsAsync(string? email)
        {
            if (email == null)
            {
                _logger.Warning(ErrorMessage.AuthenticationRequired);
                return new CollectionResult<TopicDto>
                {
                    ErrorCode = (int)ErrorCodes.AuthenticationRequired,
                    ErrorMessage = ErrorMessage.AuthenticationRequired
                };
            }

            var topics = await _userTopicRepository.GetAll().AsNoTracking()
                .Include(ut => ut.Topic)
                .Include(ut => ut.UserProfile)
                    .ThenInclude(up => up.User)
                .Where(ut => ut.UserProfile.User.UserEmail == email)
                .Select(ut => new TopicDto
                {
                    Id = ut.TopicId,
                    TopicTitel = ut.Topic.TopicTitel,
                    TopicText = ut.Topic.TopicText,
                    TopicImage = _imageToLinkConverter.ConvertImageToLink(ut.Topic.TopicImage, S3Folders.TopicImg)
                }).ToListAsync();


            if (topics.Count == 0 || topics == null)
            {
                _logger.Warning(ErrorMessage.TopicNotFound);
                return new CollectionResult<TopicDto>()
                {
                    ErrorMessage = ErrorMessage.TopicNotFound,
                    ErrorCode = (int)ErrorCodes.TopicNotFound
                };
            }

            return new CollectionResult<TopicDto>()
            {
                Data = topics,
                Count = topics.Count
            };

        }

        public async Task<BaseResult<TopicDto>> GetUserTopicByIdAsync(long id, string? email)
        {
            if (email == null)
            {
                _logger.Warning(ErrorMessage.AuthenticationRequired);
                return new BaseResult<TopicDto>
                {
                    ErrorCode = (int)ErrorCodes.AuthenticationRequired,
                    ErrorMessage = ErrorMessage.AuthenticationRequired
                };
            }

            var topic = await _userTopicRepository.GetAll()
                .Include(ut => ut.Topic)
                .Include(ut => ut.UserProfile)
                    .ThenInclude(up => up.User)
                .Where(ut => ut.UserProfile.User.UserEmail == email)
                .Where(ut => ut.TopicId == id)
                .Select(ut => new TopicDto
                {
                    Id = ut.TopicId,
                    TopicTitel = ut.Topic.TopicTitel,
                    TopicText = ut.Topic.TopicText,
                    TopicImage = _imageToLinkConverter.ConvertImageToLink(ut.Topic.TopicImage, S3Folders.TopicImg)
                }).FirstOrDefaultAsync();
 
            if ( topic == null)
            {
                _logger.Warning(ErrorMessage.TopicNotFound);
                return new BaseResult<TopicDto>()
                {
                    ErrorMessage = ErrorMessage.TopicNotFound,
                    ErrorCode = (int)ErrorCodes.TopicNotFound
                };
            }

            return new BaseResult<TopicDto>()
            {
                Data = topic,
            };
        }
    }
}
