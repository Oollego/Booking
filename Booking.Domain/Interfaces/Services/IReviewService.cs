using Booking.Domain.Dto.Review;
using Booking.Domain.Dto.Hotel;
using Booking.Domain.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Interfaces.Services
{
    public interface IReviewService
    {
        Task<CollectionResult<HotelReviewDto>> GetHotelReviewsAsync(long hotelId, int qty);
    }
}
