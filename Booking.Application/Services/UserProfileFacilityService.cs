using Amazon;
using Booking.Application.Resources;
using Booking.Domain.Dto.Facility;
using Booking.Domain.Dto.PayMethod;
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
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Services
{
    public class UserProfileFacilityService : IUserProfileFacilityService
    {
        private readonly ILogger _logger;
        private readonly IBaseRepository<UserProfileFacility> _userFacilityRepository;
        private readonly IBaseRepository<UserProfile> _userProfileRepository;
        private readonly IBaseRepository<Facility> _facilityRepository;
        private readonly IImageToLinkConverter _imageToLinkConverter;

        public UserProfileFacilityService(ILogger logger, IBaseRepository<UserProfileFacility> userFacilityRepository, 
            IBaseRepository<UserProfile> userProfileRepository, IBaseRepository<Facility> facilityRepository, 
            IImageToLinkConverter imageToLinkConverter)
        {
            _logger = logger;
            _userFacilityRepository = userFacilityRepository;
            _userProfileRepository = userProfileRepository;
            _facilityRepository = facilityRepository;
            _imageToLinkConverter = imageToLinkConverter;
        }

        public async Task<CollectionResult<UserFacilityDto>> CreateRangeUserFacilityAsync(IdFacilityDto dto, string? email)
        {
            foreach (var id in dto.FacilityId)
            {
                if (id <= 0)
                {
                    _logger.Warning(ErrorMessage.InvalidParameters);
                    return new CollectionResult<UserFacilityDto>
                    {
                        ErrorCode = (int)ErrorCodes.InvalidParameters,
                        ErrorMessage = ErrorMessage.InvalidParameters
                    };
                }
            }

            var facilities = await _facilityRepository.GetAll()
                .Where(f => dto.FacilityId.Contains(f.Id))
                .Select(f => new { FacilityId = f.Id, FacilityName = f.FacilityName })
                .ToListAsync();

            if(dto.FacilityId.Count != facilities.Count())
            {
                _logger.Warning(ErrorMessage.SomeOfFacilitiesNotFound);
                return new CollectionResult<UserFacilityDto>
                {
                    ErrorCode = (int)ErrorCodes.SomeOfFacilitiesNotFound,
                    ErrorMessage = ErrorMessage.SomeOfFacilitiesNotFound
                };
            }

           var user = await _userProfileRepository.GetAll()
                .Include(x => x.User)
                .Where(x => x.User.UserEmail == email)
                .FirstOrDefaultAsync();


            if (user == null)
            {
                _logger.Warning(ErrorMessage.AuthenticationRequired);
                return new CollectionResult<UserFacilityDto>
                {
                    ErrorCode = (int)ErrorCodes.AuthenticationRequired,
                    ErrorMessage = ErrorMessage.AuthenticationRequired
                };
            }


            List<long> baseFacilities = await _userFacilityRepository.GetAll()
                .Include(uf => uf.UserProfile)
                    .ThenInclude(up => up.User)
                .Where(uf => dto.FacilityId.Contains(uf.FacilityId))
                .Where(uf => uf.UserProfile.User.UserEmail == email)
                .Select(uf => uf.FacilityId)
                .ToListAsync();

            var profileFacilities = new List<UserProfileFacility>();

            if (baseFacilities != null && baseFacilities.Count != 0)
            {
                profileFacilities = dto.FacilityId.Except(baseFacilities)
                    .Select(x => new UserProfileFacility 
                    {
                        FacilityId = x,
                        UserProfileId = user.Id                       
                    }).ToList();
            }
            else
            {
                profileFacilities = dto.FacilityId
                    .Select(x => new UserProfileFacility
                    {
                        FacilityId = x,
                        UserProfileId = user.Id
                    }).ToList();
            }

            var deletedfacilities = await _userFacilityRepository.CreateRangeAsync(profileFacilities);
            await _userFacilityRepository.SaveChangesAsync();

            var resultFacilities = facilities
                .Where(f => deletedfacilities.Any(x => x.FacilityId == f.FacilityId))
                .Select(x => new UserFacilityDto
                {
                    Id = x.FacilityId,
                    FacilityName = x.FacilityName
                }).ToList(); ;

            return new CollectionResult<UserFacilityDto>()
            {
                Count = resultFacilities.Count(),
                Data = resultFacilities
            };
        }

        public async Task<CollectionResult<UserFacilityDto>> DeleteRangeUserFacilitAsync(IdFacilityDto dto, string? email)
        {
            foreach(var id in dto.FacilityId)
            {
                if (id <= 0)
                {
                    _logger.Warning(ErrorMessage.InvalidParameters);
                    return new CollectionResult<UserFacilityDto>
                    {
                        ErrorCode = (int)ErrorCodes.InvalidParameters,
                        ErrorMessage = ErrorMessage.InvalidParameters
                    };
                }
            }

             var facilities = await _facilityRepository.GetAll()
                .Where(f => dto.FacilityId.Contains(f.Id))
                .Select(f => new { FacilityId = f.Id, FacilityName = f.FacilityName })
                .ToListAsync();

            if(dto.FacilityId.Count != facilities.Count())
            {
                _logger.Warning(ErrorMessage.SomeOfFacilitiesNotFound);
                return new CollectionResult<UserFacilityDto>
                {
                    ErrorCode = (int)ErrorCodes.SomeOfFacilitiesNotFound,
                    ErrorMessage = ErrorMessage.SomeOfFacilitiesNotFound
                };
            }

            var userfacilities = await _userFacilityRepository.GetAll()
                .Include(uf => uf.UserProfile)
                    .ThenInclude(up => up.User)
                .Include(uf => uf.Facility)
                .Where(uf => dto.FacilityId.Contains(uf.FacilityId))
                .Where(uf => uf.UserProfile.User.UserEmail == email)
                .ToListAsync();

            if (userfacilities == null || userfacilities.Count == 0)
            {
                _logger.Warning(ErrorMessage.FacilityNotFound);
                return new CollectionResult<UserFacilityDto>
                {
                    ErrorCode = (int)ErrorCodes.FacilityNotFound,
                    ErrorMessage = ErrorMessage.FacilityNotFound
                };
            }

            var deletedfacilities = _userFacilityRepository.RemoveRange(userfacilities)
                .Select(x =>  new UserFacilityDto
            {
                    Id = x.FacilityId,
                    FacilityName = x.Facility.FacilityName
            }).ToList();

            await _userFacilityRepository.SaveChangesAsync();


            return new CollectionResult<UserFacilityDto>()
            {
                Count = userfacilities.Count(),
                Data = deletedfacilities
            };
        }

        public async Task<CollectionResult<GroupFacilitiesDto>> GetAllUserFacilitiesAsync(string? email)
        {
            if (email == null)
            {
                _logger.Warning(ErrorMessage.AuthenticationRequired);
                return new CollectionResult<GroupFacilitiesDto>
                {
                    ErrorCode = (int)ErrorCodes.AuthenticationRequired,
                    ErrorMessage = ErrorMessage.AuthenticationRequired
                };
            }

            var facilities = await _userFacilityRepository.GetAll()
                .Include(uf => uf.UserProfile)
                    .ThenInclude(up => up.User)
                .Include(uf => uf.Facility)
                    .ThenInclude(f => f.FacilityGroup)
                .Where(uf => uf.UserProfile.User.UserEmail == email)
                .GroupBy(uf => uf.Facility.FacilityGroup)
                .Select(g => new GroupFacilitiesDto
                {
                    GroupId = g.Key.Id,
                    GroupName = g.Key.FacilityGroupName,
                    GroupIcon = _imageToLinkConverter.ConvertImageToLink(g.Key.FacilityGroupIcon ?? "", S3Folders.FacilitiesImg),
                    Facilities = g.Select(uf => new UserFacilityDto
                    {
                        Id = uf.Facility.Id,
                        FacilityName = uf.Facility.FacilityName
                    }).ToList()
                }).ToListAsync();

            if (facilities.Count == 0 || facilities == null)
            {
                _logger.Warning(ErrorMessage.FacilityNotFound);
                return new CollectionResult<GroupFacilitiesDto>()
                {
                    ErrorMessage = ErrorMessage.FacilityNotFound,
                    ErrorCode = (int)ErrorCodes.FacilityNotFound
                };
            }

            return new CollectionResult<GroupFacilitiesDto>()
            {
                Data = facilities,
                Count = facilities.Count
            };
        }

        public async Task<BaseResult<GroupFacilityDto>> GetUserFacilityByIdAsync(long facilityId, string? email)
        {
            if (email == null)
            {
                _logger.Warning(ErrorMessage.AuthenticationRequired);
                return new BaseResult<GroupFacilityDto>
                {
                    ErrorCode = (int)ErrorCodes.AuthenticationRequired,
                    ErrorMessage = ErrorMessage.AuthenticationRequired
                };
            }

            var facility = await _userFacilityRepository.GetAll()
                .Include(uf => uf.UserProfile)
                    .ThenInclude(up => up.User)
                .Include(uf => uf.Facility)
                    .ThenInclude(f => f.FacilityGroup)
                .Where(uf => uf.UserProfile.User.UserEmail == email)
                .Where(uf => uf.FacilityId == facilityId)
                .GroupBy(uf => uf.Facility.FacilityGroup)
                .Select(g => new GroupFacilityDto
                {
                    GroupId = g.Key.Id,
                    GroupName = g.Key.FacilityGroupName,
                    GroupIcon = _imageToLinkConverter.ConvertImageToLink(g.Key.FacilityGroupIcon ?? "", S3Folders.FacilitiesImg),
                    Facility = g.Select(uf => new UserFacilityDto
                    {
                        Id = uf.Facility.Id,
                        FacilityName = uf.Facility.FacilityName
                    }).FirstOrDefault()?? new UserFacilityDto()
                }).FirstOrDefaultAsync();

            if ( facility == null)
            {
                _logger.Warning(ErrorMessage.FacilityNotFound);
                return new BaseResult<GroupFacilityDto>()
                {
                    ErrorMessage = ErrorMessage.FacilityNotFound,
                    ErrorCode = (int)ErrorCodes.FacilityNotFound
                };
            }

            return new BaseResult<GroupFacilityDto>()
            {
                Data = facility,
            };
        }
    }
}
