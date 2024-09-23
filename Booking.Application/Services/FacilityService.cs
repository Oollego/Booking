using Booking.Application.Resources;
using Booking.Domain.Dto.Facility;
using Booking.Domain.Dto.Hotel;
using Booking.Domain.Dto.SearchFilter;
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
    public class FacilityService : IFacilityService
    {
        private readonly IBaseRepository<Hotel> _hotelRepository = null!;
        private readonly ILogger _logger = null!;

        public FacilityService(IBaseRepository<Hotel> facilityRepository, ILogger logger)
        {
            _hotelRepository = facilityRepository;
            _logger = logger;
        }

        public async Task<CollectionResult<FacilityInfoDto>> GetHotelFacilitiesAsync(long hotelId)
        {
            if (hotelId < 0 )
            {
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
                        GroupIcon = g.Key.FacilityGroupIcon ?? "",
                        Facilities = g.Select(f => f.FacilityName).OrderBy(f => f).ToList()
                    }).OrderBy(g => g.GroupName).ToList()
                ).FirstOrDefaultAsync();

            if (fasilities == null)
            {
                _logger.Warning(ErrorMessage.FasilityNotFound);
                return new CollectionResult<FacilityInfoDto>()
                {
                    ErrorMessage = ErrorMessage.FasilityNotFound,
                    ErrorCode = (int)ErrorCodes.FasilityNotFound
                };
            }

            if (fasilities.Count() == 0)
            {
                _logger.Warning(ErrorMessage.FasilityNotFound, fasilities.Count());
                return new CollectionResult<FacilityInfoDto>()
                {
                    ErrorMessage = ErrorMessage.FasilityNotFound,
                    ErrorCode = (int)ErrorCodes.FasilityNotFound
                };
            }

            return new CollectionResult<FacilityInfoDto>()
            {
                Data = fasilities,
                Count = fasilities.Count()
            };
        }
    }
}
