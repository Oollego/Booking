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
using Booking.Domain.Interfaces.Converters;

namespace Booking.Application.Services
{
    public class ReviewService : IReviewService
    {
        IBaseRepository<Review> _reviewRepository = null!;
        private readonly IImageToLinkConverter _imageToLinkConverter = null!;
        ILogger _logger = null!;

        public ReviewService(IBaseRepository<Review> reviewRepository, ILogger logger, IImageToLinkConverter imageToLinkConverter)
        {
            _reviewRepository = reviewRepository;
            _logger = logger;
            _imageToLinkConverter = imageToLinkConverter;
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
                    Avatar = _imageToLinkConverter.ConvertImageToLink(x.User.UserProfile.Avatar!, S3Folders.AvatarImg),
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
                _logger.Warning(ErrorMessage.ReviewNotFound);
                return new CollectionResult<HotelReviewDto>()
                {
                    ErrorMessage = ErrorMessage.ReviewNotFound,
                    ErrorCode = (int)ErrorCodes.ReviewNotFound
                };
            }
            
            if (reviews.Count == 0)
            {
                _logger.Warning(ErrorMessage.ReviewNotFound, reviews.Count);
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

        public async Task<CollectionResult<HotelReviewDto>> GetLastReviewsAsync( int qty)
        {
            if (qty < 0)
            {
                return new CollectionResult<HotelReviewDto>()
                {
                    ErrorMessage = ErrorMessage.InvalidParameters,
                    ErrorCode = (int)ErrorCodes.InvalidParameters
                };
            }

            var reviews = await _reviewRepository.GetAll()
                .Include(x => x.User).ThenInclude(x => x.UserProfile)
                .OrderByDescending(x => x.ReviewDate)
                .Take(qty)
                .Select(x => new HotelReviewDto()
                {
                    UserName = x.User.UserProfile.UserName,
                    UserSurname = x.User.UserProfile.UserSurname ?? "",
                    Comment = x.ReviewComment,
                    Avatar = _imageToLinkConverter.ConvertImageToLink(x.User.UserProfile.Avatar!, S3Folders.AvatarImg),
                    Date = x.ReviewDate.Date.ToString(),
                    FacilityScore = x.FacilityScore,
                    StaffScore = x.StaffScore,
                    CleanlinessScore = x.CleanlinessScore,
                    ComfortScore = x.ComfortScore,
                    LocationScore = x.LocationScore,
                    ValueScore = x.ValueScore,
                    ReviewsCount = x.Hotel.Reviews.Count()
                })
                .ToListAsync();

            if (reviews == null)
            {
                _logger.Warning(ErrorMessage.ReviewNotFound);
                return new CollectionResult<HotelReviewDto>()
                {
                    ErrorMessage = ErrorMessage.ReviewNotFound,
                    ErrorCode = (int)ErrorCodes.ReviewNotFound
                };
            }

            if (reviews.Count == 0)
            {
                _logger.Warning(ErrorMessage.ReviewNotFound, reviews.Count);
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
