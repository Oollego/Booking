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
        Task<CollectionResult<HotelReviewDto>> GetLastReviewsAsync(int qty);
        Task<BaseResult<ReviewDto>> CreateReviewAsync(CreateReviewDto dto, string? email);
        Task<BaseResult<ReviewDto>> UpdateReviewAsync(ReviewDto dto, string? email);
        Task<BaseResult> DeleteReviewAsync(long id, string? email);
        Task<BaseResult<ReviewResponseDto>> GetReviewByIdAsync(long reviewId);

    }
}
