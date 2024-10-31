using AutoMapper;
using Booking.Application.Resources;
using Booking.Domain.Dto.FacilityGroup;
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
    public class FacilityGroupService : IFacilityGroupService
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IBaseRepository<FacilityGroup> _facilityGroupRepository;
        private readonly IS3BucketRepository _bucketRepository;
        private readonly IFileService _fileService;
        private readonly IImageToLinkConverter _imageToLinkConverter;

        public FacilityGroupService(ILogger logger, IMapper mapper, IBaseRepository<FacilityGroup> facilityGroupRepository, IS3BucketRepository bucketRepository, IFileService fileService, IImageToLinkConverter imageToLinkConverter)
        {
            _logger = logger;
            _mapper = mapper;
            _facilityGroupRepository = facilityGroupRepository;
            _bucketRepository = bucketRepository;
            _fileService = fileService;
            _imageToLinkConverter = imageToLinkConverter;
        }


        public async Task<BaseResult<FacilityGroupDto>> CreatFacilityGroupAsync(CreateFacilityGroupDto dto)
        {
            string newfileName = _fileService.GetRandomFileName(dto.FacilityGroupIcon.FileName);

            if (dto.FacilityGroupName == null || dto.FacilityGroupName == "")
            {
                _logger.Warning(ErrorMessage.InvalidParameters);
                return new BaseResult<FacilityGroupDto>
                {
                    ErrorCode = (int)ErrorCodes.InvalidParameters,
                    ErrorMessage = ErrorMessage.InvalidParameters
                };
            }

            var topicBaseResult = await _facilityGroupRepository.GetAll().Where(t => t.FacilityGroupName == dto.FacilityGroupName).FirstOrDefaultAsync();

            if (topicBaseResult != null)
            {
                _logger.Warning(ErrorMessage.FacilityGroupAlreadyExists);
                return new BaseResult<FacilityGroupDto>
                {
                    ErrorCode = (int)ErrorCodes.FacilityGroupAlreadyExists,
                    ErrorMessage = ErrorMessage.FacilityGroupAlreadyExists
                };
            }

            string key = Path.Combine(S3Folders.FacilitiesImg, newfileName);
            var s3Responce = await _bucketRepository.UploadFileAsync(AppSource.ImgBucket, key, dto.FacilityGroupIcon.OpenReadStream(), dto.FacilityGroupIcon.ContentType);

            if (s3Responce.Success)
            {
                FacilityGroup group = new FacilityGroup
                {
                    FacilityGroupName = dto.FacilityGroupName,
                    FacilityGroupIcon = newfileName
                };
                var groupResult = await _facilityGroupRepository.CreateAsync(group);
                await _facilityGroupRepository.SaveChangesAsync();

                return new BaseResult<FacilityGroupDto>
                {
                    Data = new FacilityGroupDto
                    {
                        Id = group.Id,
                        FacilityGroupName = groupResult.FacilityGroupName,
                        FacilityGroupIcon = groupResult.FacilityGroupIcon
                    }
                };
            }

            _logger.Warning(ErrorMessage.StorageServerError + s3Responce.ErrorMessage);
            return new BaseResult<FacilityGroupDto>
            {
                ErrorCode = (int)ErrorCodes.StorageServerError,
                ErrorMessage = ErrorMessage.StorageServerError
            };
        }

        public async Task<BaseResult<FacilityGroupDto>> DeleteFacilityGroupAsync(long id)
        {
            if (id <= 0)
            {
                _logger.Warning(ErrorMessage.InvalidParameters);
                return new BaseResult<FacilityGroupDto>
                {
                    ErrorCode = (int)ErrorCodes.InvalidParameters,
                    ErrorMessage = ErrorMessage.InvalidParameters
                };
            }

            var group = await _facilityGroupRepository.GetAll().Where(r => r.Id == id).FirstOrDefaultAsync();

            if (group == null)
            {
                _logger.Warning(ErrorMessage.FacilityGroupNotFound);
                return new BaseResult<FacilityGroupDto>
                {
                    ErrorCode = (int)ErrorCodes.FacilityGroupNotFound,
                    ErrorMessage = ErrorMessage.FacilityGroupNotFound
                };
            }

            string deleteKey = Path.Combine(S3Folders.FacilitiesImg, group.FacilityGroupIcon);
            var s3Result = await _bucketRepository.DeleteFileAsync(AppSource.ImgBucket, deleteKey);

            if (s3Result.Success)
            {
                group = _facilityGroupRepository.Remove(group);
                await _facilityGroupRepository.SaveChangesAsync();

                return new BaseResult<FacilityGroupDto>
                {
                    Data = new FacilityGroupDto
                    {
                        Id = group.Id,
                        FacilityGroupName = group.FacilityGroupName,
                        FacilityGroupIcon = group.FacilityGroupIcon
                    }
                };
            }

            _logger.Warning(ErrorMessage.StorageServerError);
            return new BaseResult<FacilityGroupDto>
            {
                ErrorCode = (int)ErrorCodes.StorageServerError,
                ErrorMessage = ErrorMessage.StorageServerError
            };

        }

        public async Task<CollectionResult<FacilityGroupDto>> GetAllFacilityGroupsAsync()
        {
            var groups = await _facilityGroupRepository.GetAll().Select(x => new FacilityGroupDto
            {
                Id = x.Id,
                FacilityGroupName = x.FacilityGroupName,
                FacilityGroupIcon = _imageToLinkConverter.ConvertImageToLink(x.FacilityGroupIcon, S3Folders.FacilitiesImg)
            }).ToListAsync();

            if (groups == null)
            {
                _logger.Warning(ErrorMessage.FacilityGroupNotFound);
                return new CollectionResult<FacilityGroupDto>
                {
                    ErrorCode = (int)ErrorCodes.FacilityGroupNotFound,
                    ErrorMessage = ErrorMessage.FacilityGroupNotFound
                };
            }

            return new CollectionResult<FacilityGroupDto>
            {
                Count = groups.Count,
                Data = groups
            };
        }

        public async Task<BaseResult<FacilityGroupDto>> GetFacilityGroupByIdAsync(long id)
        {
            if (id <= 0)
            {
                _logger.Warning(ErrorMessage.InvalidParameters);
                return new BaseResult<FacilityGroupDto>
                {
                    ErrorCode = (int)ErrorCodes.InvalidParameters,
                    ErrorMessage = ErrorMessage.InvalidParameters
                };
            }

            var group = await _facilityGroupRepository.GetAll().Where(r => r.Id == id).Select(x => new FacilityGroupDto
            {
                Id = x.Id,
                FacilityGroupName = x.FacilityGroupName,
                FacilityGroupIcon = _imageToLinkConverter.ConvertImageToLink(x.FacilityGroupIcon, S3Folders.FacilitiesImg)
            }).FirstOrDefaultAsync();

            if (group == null)
            {
                _logger.Warning(ErrorMessage.FacilityGroupNotFound);
                return new BaseResult<FacilityGroupDto>
                {
                    ErrorCode = (int)ErrorCodes.FacilityGroupNotFound,
                    ErrorMessage = ErrorMessage.FacilityGroupNotFound
                };
            }

            return new BaseResult<FacilityGroupDto>
            {
                Data = group
            };
        }
        public async Task<BaseResult<FacilityGroupDto>> UpdateFacilityGroupAsync(UpdateFacilityGroupDto dto)
        {
            if (dto.FacilityGroupName == null || dto.FacilityGroupName == "" || dto.Id < 0)
            {
                _logger.Warning(ErrorMessage.InvalidParameters);
                return new BaseResult<FacilityGroupDto>
                {
                    ErrorCode = (int)ErrorCodes.InvalidParameters,
                    ErrorMessage = ErrorMessage.InvalidParameters
                };
            }

            var group = await _facilityGroupRepository.GetAll().Where(r => r.Id == dto.Id).FirstOrDefaultAsync();

            if (group == null)
            {
                _logger.Warning(ErrorMessage.FacilityGroupNotFound);
                return new BaseResult<FacilityGroupDto>
                {
                    ErrorCode = (int)ErrorCodes.FacilityGroupNotFound,
                    ErrorMessage = ErrorMessage.FacilityGroupNotFound
                };
            }

            if (dto.FacilityGroupIcon != null)
            {
                string newfileName = _fileService.GetRandomFileName(dto.FacilityGroupIcon.FileName);

                if (newfileName != "")
                {
                    string key = Path.Combine(S3Folders.FacilitiesImg, newfileName);
                    var s3Responce = await _bucketRepository.UploadFileAsync(AppSource.ImgBucket, key, dto.FacilityGroupIcon.OpenReadStream(), dto.FacilityGroupIcon.ContentType);

                    if (!s3Responce.Success)
                    {
                        _logger.Warning(ErrorMessage.StorageServerError + s3Responce.ErrorMessage);
                        return new BaseResult<FacilityGroupDto>
                        {
                            ErrorCode = (int)ErrorCodes.StorageServerError,
                            ErrorMessage = ErrorMessage.StorageServerError
                        };
                    }
                    string deleteKey = Path.Combine(S3Folders.FacilitiesImg, group.FacilityGroupIcon);
                    await _bucketRepository.DeleteFileAsync(AppSource.ImgBucket, deleteKey);

                    group.FacilityGroupIcon = newfileName;
                }
            }

            group.FacilityGroupName = dto.FacilityGroupName;
          
            var newGroup = _facilityGroupRepository.Update(group);
            await _facilityGroupRepository.SaveChangesAsync();

            return new BaseResult<FacilityGroupDto>
            {
                Data = new FacilityGroupDto
                {
                    Id = group.Id,
                    FacilityGroupName = newGroup.FacilityGroupName,
                    FacilityGroupIcon = newGroup.FacilityGroupIcon
                }
            };
        }
    }
}
