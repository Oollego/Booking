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
using Booking.Domain.Interfaces.Validations;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using System.Net.Http;
using System.Runtime.InteropServices;

namespace Booking.Application.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IBaseRepository<Review> _reviewRepository;
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<Hotel> _hotelRepository;
        private readonly IImageToLinkConverter _imageToLinkConverter;
        private readonly ILogger _logger;
        private readonly IReviewDtoValidator _reviewDtoValidator;
        private readonly IMapper _mapper;


        public ReviewService(IBaseRepository<Review> reviewRepository, ILogger logger, IImageToLinkConverter imageToLinkConverter,
            IReviewDtoValidator reviewDtoValidator, IMapper mapper, IBaseRepository<User> userRepository, IBaseRepository<Hotel> hotelRepository)
        {
            _reviewRepository = reviewRepository;
            _logger = logger;
            _imageToLinkConverter = imageToLinkConverter;
            _reviewDtoValidator = reviewDtoValidator;
            _mapper = mapper;
            _userRepository = userRepository;
            _hotelRepository = hotelRepository;
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
                    Id = x.Id,
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
                _logger.Warning(ErrorMessage.InvalidParameters);
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
                    Id = x.Id,
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

        public async Task<BaseResult<ReviewDto>> CreateReviewAsync(CreateReviewDto dto, string? email)
        {
            if (dto == null)
            {
                return new BaseResult<ReviewDto>()
                {
                    ErrorMessage = ErrorMessage.InvalidParameters,
                    ErrorCode = (int)ErrorCodes.InvalidParameters
                };
            }

            var dtoValidator = _reviewDtoValidator.CreateReviewDtoValidator(dto);

            if (!dtoValidator.IsSuccess)
            {
                return new BaseResult<ReviewDto>()
                {
                    ErrorMessage = dtoValidator.ErrorMessage,
                    ErrorCode = dtoValidator.ErrorCode
                };
            }

            var baseReview = await _reviewRepository.GetAll().AsNoTracking()
                .Include(r => r.User)
                .Where(r => r.User.UserEmail == email)
                .Where(r => r.HotelId == dto.HotelId)
                .Where(r => r.ReviewDate.Date == DateTime.Now.Date)
                .FirstOrDefaultAsync();

            if (baseReview != null)
            {
                return new BaseResult<ReviewDto>()
                {
                    ErrorMessage = ErrorMessage.ReasonAlreadyExists,
                    ErrorCode = (int)ErrorCodes.ReasonAlreadyExists
                };
            }

            var hotel = await _hotelRepository.GetAll()
              .AsNoTracking()
              .Where(h => h.Id == dto.HotelId)
              .FirstOrDefaultAsync();

            if (hotel == null)
            {
                return new BaseResult<ReviewDto>()
                {
                    ErrorMessage = ErrorMessage.HotelNotFound,
                    ErrorCode = (int)ErrorCodes.HotelNotFound
                };
            }

            var user = await _userRepository.GetAll().AsNoTracking()
                .Where(u => u.UserEmail == email)
                .FirstOrDefaultAsync();

            if(user == null)
            {
                return new BaseResult<ReviewDto>()
                {
                    ErrorMessage = ErrorMessage.UserNotFound,
                    ErrorCode = (int)ErrorCodes.UserNotFound
                };
            }

            Review review = new Review()
            {
                ReviewComment = dto.ReviewComment,
                ReviewDate = DateTime.Now,
                StaffScore = dto.StaffScore,
                CleanlinessScore = dto.CleanlinessScore,
                ComfortScore = dto.ComfortScore,
                FacilityScore = dto.FacilityScore,
                LocationScore = dto.LocationScore,
                ValueScore = dto.ValueScore,
                HotelId = dto.HotelId,
                UserId = user.Id
            };

            review = await _reviewRepository.CreateAsync(review);
            await _reviewRepository.SaveChangesAsync();

            return new BaseResult<ReviewDto>()
            {
                Data = _mapper.Map<ReviewDto>(review)
            };

        }

        public async Task<BaseResult<ReviewDto>> UpdateReviewAsync(ReviewDto dto, string? email)
        {
            if (dto == null)
            {
                return new BaseResult<ReviewDto>()
                {
                    ErrorMessage = ErrorMessage.InvalidParameters,
                    ErrorCode = (int)ErrorCodes.InvalidParameters
                };
            }

            var dtoValidator = _reviewDtoValidator.ReviewValidator(dto);

            if (!dtoValidator.IsSuccess)
            {
                return new BaseResult<ReviewDto>()
                {
                    ErrorMessage = dtoValidator.ErrorMessage,
                    ErrorCode = dtoValidator.ErrorCode
                };
            }

            var hotel = await _hotelRepository.GetAll()
                .AsNoTracking()
                .Where(h => h.Id == dto.HotelId)
                .FirstOrDefaultAsync();

            if (hotel == null)
            {
                return new BaseResult<ReviewDto>()
                {
                    ErrorMessage = ErrorMessage.HotelNotFound,
                    ErrorCode = (int)ErrorCodes.HotelNotFound
                };
            }

            var review = await _reviewRepository.GetAll()
                .Include(r => r.User)
                .Where(r => r.User.UserEmail == email)
                .Where(r => r.Id == dto.Id)
                .FirstOrDefaultAsync();

            if (review == null)
            {
                return new BaseResult<ReviewDto>()
                {
                    ErrorMessage = ErrorMessage.ReviewNotFound,
                    ErrorCode = (int)ErrorCodes.ReviewNotFound
                };
            }

            review.ReviewComment = dto.ReviewComment;
            review.ReviewDate = DateTime.Now;
            review.StaffScore = dto.StaffScore;
            review.CleanlinessScore = dto.CleanlinessScore;
            review.ComfortScore = dto.ComfortScore;
            review.FacilityScore = dto.FacilityScore;
            review.LocationScore = dto.LocationScore;
            review.ValueScore = dto.ValueScore;
            review.HotelId = dto.HotelId;

            review = _reviewRepository.Update(review);
            await _reviewRepository.SaveChangesAsync();

            return new BaseResult<ReviewDto>()
            {
                Data = _mapper.Map<ReviewDto>(review)
            };
        }

        public async Task<BaseResult> DeleteReviewAsync(long id, string? email)
        {
            if (id < 0)
            {
                return new BaseResult()
                {
                    ErrorMessage = ErrorMessage.InvalidParameters,
                    ErrorCode = (int)ErrorCodes.InvalidParameters
                };
            }

            var review = _reviewRepository.GetAll()
                .Include(r => r.User)
                .Where(r => r.User.UserEmail == email)
                .Where(r => r.Id == id)
                .FirstOrDefault();

            if(review == null)
            {
                return new BaseResult()
                {
                    ErrorMessage = ErrorMessage.ReviewNotFound,
                    ErrorCode = (int)ErrorCodes.ReviewNotFound
                };
            }

            review = _reviewRepository.Remove(review);
            await _reviewRepository.SaveChangesAsync();

            return new BaseResult();
        }
        public async Task<BaseResult<ReviewResponseDto>> GetReviewByIdAsync(long id)
        {
            if (id < 0)
            {
                return new BaseResult<ReviewResponseDto>()
                {
                    ErrorMessage = ErrorMessage.InvalidParameters,
                    ErrorCode = (int)ErrorCodes.InvalidParameters
                };
            }

            var review = await _reviewRepository.GetAll().AsNoTracking()
                .Include(r => r.User)
                    .ThenInclude(u => u.UserProfile)
                    .ThenInclude(p => p.City)
                    .ThenInclude(c => c!.Country)
                .Where(r => r.Id == id)
                .Select(r => new ReviewResponseDto
                {
                    Id = r.Id,
                    UserName = r.User.UserProfile.UserName,
                    UserSurname = r.User.UserProfile.UserSurname ?? "",
                    ReviewComment = r.ReviewComment,
                    Avatar = _imageToLinkConverter.ConvertImageToLink(r.User.UserProfile.Avatar!, S3Folders.AvatarImg),
                    ReviewDate = r.ReviewDate.Date.ToString(),
                    FacilityScore = r.FacilityScore,
                    StaffScore = r.StaffScore,
                    CleanlinessScore = r.CleanlinessScore,
                    ComfortScore = r.ComfortScore,
                    LocationScore = r.LocationScore,
                    ValueScore = r.ValueScore,
                    Country = (r.User.UserProfile.City != null && r.User.UserProfile.City.Country != null)
                               ? r.User.UserProfile.City.Country.CountryName
                               : ""
                })
                .FirstOrDefaultAsync();

            if (review == null)
            {
                return new BaseResult<ReviewResponseDto>
                {
                    ErrorMessage = ErrorMessage.ReviewNotFound,
                    ErrorCode = (int)ErrorCodes.ReviewNotFound
                };
            }

            return new BaseResult<ReviewResponseDto>
            {
                Data = review
            };

        }
    }
}
