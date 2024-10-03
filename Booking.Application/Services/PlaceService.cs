using Booking.Application.Resources;
using Booking.Domain.Dto.Place;
using Booking.Domain.Dto.Review;
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
    public class PlaceService : IPlaceService
    {
        IBaseRepository<NearPlace> _placeRepository = null!;
        private readonly IImageToLinkConverter _imageToLinkConverter = null!;
        ILogger _logger = null!;
        public PlaceService(ILogger logger, IBaseRepository<NearPlace> placeRepository, IImageToLinkConverter imageToLinkConverter)
        {
            _logger = logger;
            _placeRepository = placeRepository;
            _imageToLinkConverter = imageToLinkConverter;
        }

        public async Task<CollectionResult<NearPlaceGroupDto>> GetHotelPlacesAsync(long hotelId)
        {
            if (hotelId < 1)
            {
                return new CollectionResult<NearPlaceGroupDto>()
                {
                    ErrorMessage = ErrorMessage.InvalidParameters,
                    ErrorCode = (int)ErrorCodes.InvalidParameters
                };
            }

            var nearPlaces = await _placeRepository.GetAll().Where(x => x.HotelId == hotelId)
                .Include(p => p.NearPlacesGroup)
                .ToListAsync();

            var places = nearPlaces.GroupBy(p => p.NearPlacesGroup)
            .Select(p => new NearPlaceGroupDto()
            {
                GroupName = p.Key.PlaceGroupName,
                GroupIcon = p.Key.GroupIcon!,
                NearPlaces = p.Key.NearPlaces.Select(x => new NearPlaceDto()
                {
                    PlaceName = x.PlaceName,
                    Distance = x.Distance,
                    DistanceMetric = x.DistanceMetric
                }).ToList()
            }).ToList();
  
            if (places == null)
            {
                _logger.Warning(ErrorMessage.NearPlaceNotFound);
                return new CollectionResult<NearPlaceGroupDto>()
                {
                    ErrorMessage = ErrorMessage.NearPlaceNotFound,
                    ErrorCode = (int)ErrorCodes.NearPlaceNotFound
                };
            }

            if (places.Count == 0)
            {
                _logger.Warning(ErrorMessage.NearPlaceNotFound, places.Count);
                return new CollectionResult<NearPlaceGroupDto>()
                {
                    ErrorMessage = ErrorMessage.NearPlaceNotFound,
                    ErrorCode = (int)ErrorCodes.NearPlaceNotFound
                };
            }

            return new CollectionResult<NearPlaceGroupDto>()
            {
                Data = places,
                Count = places.Count
            };
        }
    }
}
