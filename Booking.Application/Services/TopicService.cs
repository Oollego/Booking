using AutoMapper;
using Booking.Application.Resources;
using Booking.Domain.Dto.Reason;
using Booking.Domain.Dto.Topic;
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
    public class TopicService : ITopicService
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Topic> _topicRepository;
        private readonly IS3BucketRepository _bucketRepository;
        private readonly IFileService _fileService;
        private readonly IImageToLinkConverter _imageToLinkConverter;

        public TopicService(ILogger logger, IMapper mapper, IBaseRepository<Topic> topicRepository, 
            IS3BucketRepository bucketRepository, IFileService fileService, IImageToLinkConverter imageToLinkConverter)
        {
            _logger = logger;
            _mapper = mapper;
            _topicRepository = topicRepository;
            _bucketRepository = bucketRepository;
            _fileService = fileService;
            _imageToLinkConverter = imageToLinkConverter;
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
        public async Task<BaseResult<TopicDto>> DeleteTopicAsync(long id)
        {
            if (id <= 0)
            {
                _logger.Warning(ErrorMessage.InvalidParameters);
                return new BaseResult<TopicDto>
                {
                    ErrorCode = (int)ErrorCodes.InvalidParameters,
                    ErrorMessage = ErrorMessage.InvalidParameters
                };
            }

            var topic = await _topicRepository.GetAll().Where(r => r.Id == id).FirstOrDefaultAsync();

            if (topic == null)
            {
                _logger.Warning(ErrorMessage.TopicNotFound);
                return new BaseResult<TopicDto>
                {
                    ErrorCode = (int)ErrorCodes.TopicNotFound,
                    ErrorMessage = ErrorMessage.TopicNotFound
                };
            }

            string deleteKey = Path.Combine(S3Folders.TopicImg, topic.TopicImage);
            var s3Result = await _bucketRepository.DeleteFileAsync(AppSource.ImgBucket, deleteKey);

            if (s3Result.Success) 
            {
                topic = _topicRepository.Remove(topic);
                await _topicRepository.SaveChangesAsync();

                return new BaseResult<TopicDto>
                {
                    Data = _mapper.Map<TopicDto>(topic)
                };
            }

            _logger.Warning(ErrorMessage.StorageServerError);
            return new BaseResult<TopicDto>
            {
                ErrorCode = (int)ErrorCodes.StorageServerError,
                ErrorMessage = ErrorMessage.StorageServerError
            };


        }
        public async Task<CollectionResult<TopicDto>> GetAllTopicsAsync()
        {
            var topics = await _topicRepository.GetAll().AsNoTracking().Select(x => new TopicDto
            {
                Id = x.Id,
                TopicTitel = x.TopicTitel,
                TopicText = x.TopicText,
                TopicImage = _imageToLinkConverter.ConvertImageToLink(x.TopicImage, S3Folders.TopicImg)
            }).ToListAsync();

            if (topics == null)
            {
                _logger.Warning(ErrorMessage.TopicNotFound);
                return new CollectionResult<TopicDto>
                {
                    ErrorCode = (int)ErrorCodes.TopicNotFound,
                    ErrorMessage = ErrorMessage.TopicNotFound
                };
            }

            return new CollectionResult<TopicDto>
            {
                Count = topics.Count,
                Data = topics
            };
        }
        public async Task<BaseResult<TopicDto>> GetTopicByIdAsync(long id)
        {
            if (id <= 0)
            {
                _logger.Warning(ErrorMessage.InvalidParameters);
                return new BaseResult<TopicDto>
                {
                    ErrorCode = (int)ErrorCodes.InvalidParameters,
                    ErrorMessage = ErrorMessage.InvalidParameters
                };
            }

            var topic = await _topicRepository.GetAll().Where(r => r.Id == id).Select(x => new TopicDto
            {
                Id = x.Id,
                TopicTitel = x.TopicTitel,
                TopicText = x.TopicText,
                TopicImage = _imageToLinkConverter.ConvertImageToLink(x.TopicImage, S3Folders.TopicImg)
            }).FirstOrDefaultAsync();

            if (topic == null)
            {
                _logger.Warning(ErrorMessage.TopicNotFound);
                return new BaseResult<TopicDto>
                {
                    ErrorCode = (int)ErrorCodes.TopicNotFound,
                    ErrorMessage = ErrorMessage.TopicNotFound
                };
            }

            return new BaseResult<TopicDto>
            {
                Data = topic
            };
        }
        public async Task<BaseResult<TopicDto>> UpdateTopicAsync(UpdateTopicDto dto)
        {
            

            if (dto.Titel == null || dto.Titel == "" || dto.Text == "" || dto.Text == null)
            {
                _logger.Warning(ErrorMessage.InvalidParameters);
                return new BaseResult<TopicDto>
                {
                    ErrorCode = (int)ErrorCodes.InvalidParameters,
                    ErrorMessage = ErrorMessage.InvalidParameters
                };
            }

            var topic = await _topicRepository.GetAll().Where(r => r.Id == dto.Id).FirstOrDefaultAsync();

            if (topic == null)
            {
                _logger.Warning(ErrorMessage.TopicNotFound);
                return new BaseResult<TopicDto>
                {
                    ErrorCode = (int)ErrorCodes.TopicNotFound,
                    ErrorMessage = ErrorMessage.TopicNotFound
                };
            }

            if(dto.Image != null)
            {
                string newfileName = _fileService.GetRandomFileName(dto.Image.FileName);

                if (newfileName != "")
                {
                    string key = Path.Combine(S3Folders.TopicImg, newfileName);
                    var s3Responce = await _bucketRepository.UploadFileAsync(AppSource.ImgBucket, key, dto.Image.OpenReadStream(), dto.Image.ContentType);

                    if (!s3Responce.Success)
                    {
                        _logger.Warning(ErrorMessage.StorageServerError + s3Responce.ErrorMessage);
                        return new BaseResult<TopicDto>
                        {
                            ErrorCode = (int)ErrorCodes.StorageServerError,
                            ErrorMessage = ErrorMessage.StorageServerError
                        };
                    }
                    string deleteKey = Path.Combine(S3Folders.TopicImg, topic.TopicImage);
                    await _bucketRepository.DeleteFileAsync(AppSource.ImgBucket, deleteKey);

                    topic.TopicImage = newfileName;
                }
            }

            topic.TopicTitel = dto.Titel;
            topic.TopicText = dto.Text;

            var newTopic = _topicRepository.Update(topic);
            await _topicRepository.SaveChangesAsync();

            return new BaseResult<TopicDto>
            {
                Data = _mapper.Map<TopicDto>(newTopic)
            };
        }
    }
}
