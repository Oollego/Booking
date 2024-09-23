using Booking.Application.Resources;
using Booking.Domain.Dto.Review;
using Booking.Domain.Dto.Hotel;
using Booking.Domain.Entity;
using Booking.Domain.Enum;
using Booking.Domain.Interfaces.Repositories;
using Booking.Domain.Interfaces.Services;
using Booking.Domain.Result;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using Microsoft.EntityFrameworkCore.Storage;
using System.Security.Claims;

namespace Booking.Application.Services
{
    public class ReviewService : IReviewService
    {
        IBaseRepository<Review> _reviewRepository = null!;
        ILogger _logger = null!;

        public ReviewService(IBaseRepository<Review> reviewRepository, ILogger logger)
        {
            _reviewRepository = reviewRepository;
            _logger = logger;
        }

        public async Task<CollectionResult<HotelReviewDto>> GetHotelReviewsAsync(long hotelId, int qty)
        {
            if (hotelId < 1)
            {
                return new CollectionResult<HotelReviewDto>()
                {
                    ErrorMessage = ErrorMessage.InvalidParameters,
                    ErrorCode = (int)ErrorCodes.InvalidParameters
                };
            }

            var reviews = await _reviewRepository.GetAll().Where(x => x.HotelId == hotelId)
                .Include(x => x.User).ThenInclude(x => x.UserProfile)
                .Select(x => new HotelReviewDto()
                {
                    UserName = x.User.UserProfile.UserName,
                    UserSurname = x.User.UserProfile.UserSurname ?? "",
                    Comment = x.ReviewComment,
                    Avatar = x.User.UserProfile.Avatar!,
                    Date = x.ReviewDate.Date.ToString(),
                    FacilityScore = x.FacilityScore,
                    StaffScore = x.StaffScore,
                    CleanlinessScore = x.CleanlinessScore,
                    ComfortScore = x.ComfortScore,
                    LocationScore = x.LocationScore,
                    ValueScore = x.ValueScore,
                    ReviewsCount = x.Hotel.Reviews.Count()
                })
                .Take(qty)
                .OrderByDescending(x => x.Date)
                .ToListAsync();

            if (reviews == null)
            {
                _logger.Warning(ErrorMessage.HotelNotFound);
                return new CollectionResult<HotelReviewDto>()
                {
                    ErrorMessage = ErrorMessage.ReviewNotFound,
                    ErrorCode = (int)ErrorCodes.ReviewNotFound
                };
            }
            
            if (reviews.Count == 0)
            {
                _logger.Warning(ErrorMessage.HotelNotFound, reviews.Count);
                return new CollectionResult<HotelReviewDto>()
                {
                    ErrorMessage = ErrorMessage.ReviewNotFound,
                    ErrorCode = (int)ErrorCodes.ReviewNotFound
                };
            }

            return new CollectionResult<HotelReviewDto>()
            {
                Data = reviews,
                Count = reviews.Count
            };
        }
    }
}
