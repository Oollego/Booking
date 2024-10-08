using AutoMapper;
using Booking.Application.Resources;
using Booking.Domain.Dto.Reason;
using Booking.Domain.Dto.Topic;
using Booking.Domain.Entity;
using Booking.Domain.Enum;
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
    public class TopicService : ITopicService
    {
        private readonly ILogger _logger = null!;
        private readonly IMapper _mapper = null!;
        private readonly IBaseRepository<Topic> _topicRepository = null!;
        private readonly IS3BucketRepository _bucketRepository = null!;
        private readonly IFileService _fileService = null!;

        public TopicService(ILogger logger, IMapper mapper, IBaseRepository<Topic> topicRepository, IS3BucketRepository bucketRepository, IFileService fileService)
        {
            _logger = logger;
            _mapper = mapper;
            _topicRepository = topicRepository;
            _bucketRepository = bucketRepository;
            _fileService = fileService;
        }


        public async Task<BaseResult<TopicDto>> CreatTopicAsync(CreateTopicDto dto)
        {
            string newfileName = _fileService.GetRandomFileName(dto.Image.FileName);

            if (dto.Titel == null || dto.Titel == "" || dto.Text == "" || dto.Text == null || dto.Image == null || newfileName == "")
            {
                _logger.Warning(ErrorMessage.InvalidParameters);
                return new BaseResult<TopicDto>
                {
                    ErrorCode = (int)ErrorCodes.InvalidParameters,
                    ErrorMessage = ErrorMessage.InvalidParameters
                };
            }

            var topicBaseResult = await _topicRepository.GetAll().Where(t => t.TopicTitel == dto.Titel).FirstOrDefaultAsync();

            if (topicBaseResult != null)
            {
                _logger.Warning(ErrorMessage.TopicAlreadyExists);
                return new BaseResult<TopicDto>
                {
                    ErrorCode = (int)ErrorCodes.TopicAlreadyExists,
                    ErrorMessage = ErrorMessage.TopicAlreadyExists
                };
            }
            
            string key = Path.Combine(S3Folders.TopicImg, newfileName);
            var s3Responce = await _bucketRepository.UploadFileAsync(AppSource.ImgBucket, key, dto.Image.OpenReadStream(), dto.Image.ContentType);

            if (s3Responce.Success)
            {
                Topic topic = new Topic
                {
                    TopicImage = newfileName,
                    TopicText = dto.Text,
                    TopicTitel = dto.Titel
                }; 
                var topicResult = await _topicRepository.CreateAsync(topic);
                await _topicRepository.SaveChangesAsync();

                return new BaseResult<TopicDto>
                {
                    Data = _mapper.Map<TopicDto>(topic)
                };
            }

            _logger.Warning(ErrorMessage.StorageServerError + s3Responce.ErrorMessage);
            return new BaseResult<TopicDto>
            {
                ErrorCode = (int)ErrorCodes.StorageServerError,
                ErrorMessage = ErrorMessage.StorageServerError
            };


        }

        public Task<BaseResult<TopicDto>> DeleteTopicAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<CollectionResult<TopicDto>> GetAllTopicsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<BaseResult<TopicDto>> GetTopicByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResult<TopicDto>> UpdateTopicAsync(TopicDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
