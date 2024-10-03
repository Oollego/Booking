using Booking.Domain.Dto.Place;
using Booking.Domain.Dto.Review;
using Booking.Domain.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Interfaces.Services
{
    public interface IPlaceService
    {
        Task<CollectionResult<NearPlaceGroupDto>> GetHotelPlacesAsync(long hotelId);
    }
}
