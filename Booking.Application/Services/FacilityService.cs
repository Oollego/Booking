using Booking.Application.Resources;
using Booking.Domain.Dto.City;
using Booking.Domain.Dto.Facility;
using Booking.Domain.Dto.Hotel;
using Booking.Domain.Dto.SearchFilter;
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
    public class FacilityService : IFacilityService
    {
        private readonly IBaseRepository<Hotel> _hotelRepository;
        private readonly ILogger _logger = null!;
        private readonly IImageToLinkConverter _imageToLinkConverter;
        private readonly IBaseRepository<Facility> _facilityRepository;
        private readonly IBaseRepository<FacilityGroup> _facilityGroupRepository;

        public FacilityService(IBaseRepository<Hotel> hotelRepository, ILogger logger, IImageToLinkConverter imageToLinkConverter, IBaseRepository<Facility> facilityRepository, IBaseRepository<FacilityGroup> facilityGroupRepository)
        {
            _hotelRepository = hotelRepository;
            _logger = logger;
            _imageToLinkConverter = imageToLinkConverter;
            _facilityRepository = facilityRepository;
            _facilityGroupRepository = facilityGroupRepository;
        }

        public async Task<BaseResult<FacilityDto>> CreatFacilityAsync(CreateFacilityDto dto)
        {
            if (dto == null ||  dto.FacilityName == null || dto.FacilityName.Length <= 0 || dto.FacilityGroupId <= 0  )
            {
                return new BaseResult<FacilityDto>
                {
                    ErrorCode = (int)ErrorCodes.InvalidParameters,
                    ErrorMessage = ErrorMessage.InvalidParameters
                };
            }

            var facility = _facilityRepository.GetAll()
                .Include(f => f.FacilityGroup)
                .FirstOrDefault(f => f.FacilityName == dto.FacilityName && f.FacilityGroup.Id == dto.FacilityGroupId);

            if (facility != null)
            {
                return new BaseResult<FacilityDto>
                {
                    ErrorCode = (int)ErrorCodes.FacilityAlreadyExists,
                    ErrorMessage = ErrorMessage.FacilityAlreadyExists
                };
            }

            var group = _facilityGroupRepository.GetAll()
                .Where(g => g.Id == dto.FacilityGroupId)
                .FirstOrDefault();

            if (group == null)
            {
                return new BaseResult<FacilityDto>
                {
                    ErrorCode = (int)ErrorCodes.FacilityAlreadyExists,
                    ErrorMessage = ErrorMessage.FacilityAlreadyExists
                };
            }

            Facility newFacility = new Facility
            {
                FacilityName = dto.FacilityName,
                FacilityGroupId = dto.FacilityGroupId
            };

            newFacility = await _facilityRepository.CreateAsync(newFacility);
            await _facilityRepository.SaveChangesAsync();

            return new BaseResult<FacilityDto>
            {
                Data = new FacilityDto
                {
                    Id = newFacility.Id,
                    FacilityName = newFacility.FacilityName,
                    FacilityGroupId = newFacility.FacilityGroupId,
                }
            };
        }

        public async Task<BaseResult<FacilityDto>> DeleteFacilityAsync(long id)
        {
            if (id <= 0)
            {
                return new BaseResult<FacilityDto>
                {
                    ErrorCode = (int)ErrorCodes.InvalidParameters,
                    ErrorMessage = ErrorMessage.InvalidParameters
                };
            }

            var facility = await _facilityRepository.GetAll().FirstOrDefaultAsync(c => c.Id == id);

            if (facility == null)
            {
                return new BaseResult<FacilityDto>
                {
                    ErrorCode = (int)ErrorCodes.FacilityNotFound,
                    ErrorMessage = ErrorMessage.FacilityNotFound
                };
            }

            facility = _facilityRepository.Remove(facility);
            await _facilityRepository.SaveChangesAsync();

            return new BaseResult<FacilityDto>
            {
                Data = new FacilityDto
                {
                    Id = facility.Id,
                    FacilityName = facility.FacilityName,
                    FacilityGroupId = facility.FacilityGroupId
                }
            };
        }

        public async Task<CollectionResult<GroupFacilitiesDto>> GetAllFacilities()
        {
           
            var facilities = await _facilityRepository.GetAll()
                .Include(f => f.FacilityGroup)
                .GroupBy(f => f.FacilityGroup)
                .Select(g => new GroupFacilitiesDto
                {
                    GroupId = g.Key.Id,
                    GroupName = g.Key.FacilityGroupName,
                    GroupIcon = _imageToLinkConverter.ConvertImageToLink(g.Key.FacilityGroupIcon ?? "", S3Folders.FacilitiesImg),
                    Facilities = g.Select(f => new UserFacilityDto 
                    {
                        Id = f.Id,
                        FacilityName = f.FacilityName
                    }).ToList()
                }).ToListAsync();

           

            return new CollectionResult<GroupFacilitiesDto>
            {
                Count = facilities.Count,
                Data = facilities
            };

        }

        public async Task<BaseResult<FacilityDto>> GetFacilityByIdAsync(long id)
        {
            if (id < 0)
            {
                return new BaseResult<FacilityDto>
                {
                    ErrorCode = (int)ErrorCodes.InvalidParameters,
                    ErrorMessage = ErrorMessage.InvalidParameters
                };
            }

            var facility = await _facilityRepository.GetAll()
                .Where(c => c.Id == id)
                .Select(c => new FacilityDto
                {
                    Id = c.Id,
                    FacilityName = c.FacilityName,
                    FacilityGroupId = c.FacilityGroupId
                })
                .FirstOrDefaultAsync();

            if (facility == null)
            {
                return new BaseResult<FacilityDto>
                {
                    ErrorCode = (int)ErrorCodes.FacilityNotFound,
                    ErrorMessage = ErrorMessage.FacilityNotFound
                };
            }

            return new BaseResult<FacilityDto>
            {
                Data = facility
            };
        }

        public async Task<BaseResult<FacilityDto>> UpdateFacilityAsync(FacilityDto dto)
        {
            if (dto == null || dto.FacilityName == null || dto.FacilityName.Length <= 0 || dto.FacilityGroupId <= 0 ||
                dto.Id < 0)
            {
                return new BaseResult<FacilityDto>
                {
                    ErrorCode = (int)ErrorCodes.InvalidParameters,
                    ErrorMessage = ErrorMessage.InvalidParameters
                };
            }

            var facility = await _facilityRepository.GetAll()
                .FirstOrDefaultAsync(c => c.Id == dto.Id);

            if (facility == null)
            {
                return new BaseResult<FacilityDto>
                {
                    ErrorMessage = ErrorMessage.FacilityNotFound,
                    ErrorCode = (int)ErrorCodes.FacilityNotFound
                };
            }

            facility.FacilityName = dto.FacilityName;
            facility.FacilityGroupId = dto.FacilityGroupId;
            
            var updatedFacility = _facilityRepository.Update(facility);
            await _facilityRepository.SaveChangesAsync();

            return new BaseResult<FacilityDto>
            {
                Data = new FacilityDto
                {
                    Id = updatedFacility.Id,
                    FacilityName = updatedFacility.FacilityName,
                    FacilityGroupId = updatedFacility.FacilityGroupId
                }
            };
        }

        public async Task<CollectionResult<FacilityInfoDto>> GetHotelFacilitiesAsync(long hotelId)
        {
            if (hotelId < 0)
            {
                _logger.Warning(ErrorMessage.InvalidParameters);
                return new CollectionResult<FacilityInfoDto>()
                {
                    ErrorCode = (int)ErrorCodes.InvalidParameters,
                    ErrorMessage = ErrorMessage.InvalidParameters
                };
            }

            var fasilities = await _hotelRepository.GetAll()
                .Where(h => h.Id == hotelId)
                .Include(h => h.Facilities)
                    .ThenInclude(f => f.FacilityGroup)
                .Select(h =>
                    h.Facilities.GroupBy(f => new { f.FacilityGroup.FacilityGroupName, f.FacilityGroup.FacilityGroupIcon })
                    .Select(g => new FacilityInfoDto
                    {
                        GroupName = g.Key.FacilityGroupName,
                        GroupIcon = _imageToLinkConverter.ConvertImageToLink(g.Key.FacilityGroupIcon ?? "", S3Folders.FacilitiesImg),
                        Facilities = g.Select(f => f.FacilityName).OrderBy(f => f).ToList()
                    }).OrderBy(g => g.GroupName).ToList()
                ).FirstOrDefaultAsync();

            if (fasilities == null)
            {
                _logger.Warning(ErrorMessage.FacilityNotFound);
                return new CollectionResult<FacilityInfoDto>()
                {
                    ErrorMessage = ErrorMessage.FacilityNotFound,
                    ErrorCode = (int)ErrorCodes.FacilityNotFound
                };
            }

            //if (fasilities.Count() == 0)
            //{
            //    _logger.Warning(ErrorMessage.FacilityNotFound, fasilities.Count());
            //    return new CollectionResult<FacilityInfoDto>()
            //    {
            //        ErrorMessage = ErrorMessage.FacilityNotFound,
            //        ErrorCode = (int)ErrorCodes.FacilityNotFound
            //    };
            //}

            return new CollectionResult<FacilityInfoDto>()
            {
                Data = fasilities,
                Count = fasilities.Count()
            };
        }
    }
}
