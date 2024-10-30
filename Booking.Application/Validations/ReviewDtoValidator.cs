using Booking.Application.Resources;
using Booking.Domain.Dto.Review;
using Booking.Domain.Entity;
using Booking.Domain.Enum;
using Booking.Domain.Interfaces.Validations;
using Booking.Domain.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Validations
{
    internal class ReviewDtoValidator : IReviewDtoValidator
    {

        public BaseResult ReviewValidator (ReviewDto dto)
        {
            if (dto.Id < 0)
            {
                return new BaseResult()
                {
                    ErrorMessage = ErrorMessage.InvalidParameters,
                    ErrorCode = (int)ErrorCodes.InvalidParameters
                };
            }
           var createValidator = CreateReviewDtoValidator (dto);

            if (!createValidator.IsSuccess)
            {
                return new BaseResult()
                {
                    ErrorMessage = createValidator.ErrorMessage,
                    ErrorCode = createValidator.ErrorCode
                };
            }

            return new BaseResult();
        }

        public BaseResult CreateReviewDtoValidator(CreateReviewDto dto)
        {
            if(dto.HotelId < 0)
            {
                return new BaseResult()
                {
                    ErrorMessage = ErrorMessage.InvalidParameters,
                    ErrorCode = (int)ErrorCodes.InvalidParameters
                };
            }

           if(CheckReviewScore(dto.ValueScore) || 
                CheckReviewScore(dto.StaffScore) ||
                CheckReviewScore(dto.CleanlinessScore) ||
                CheckReviewScore(dto.ComfortScore) ||
                CheckReviewScore(dto.FacilityScore) ||
                CheckReviewScore(dto.LocationScore))
           {
                return new BaseResult()
                {
                    ErrorMessage = ErrorMessage.InvalidScoreParametr,
                    ErrorCode = (int)ErrorCodes.InvalidScoreParametr
                };
            }
            return new BaseResult();
        }

        private bool CheckReviewScore(int score)
        {
            if(score < 0 || score > 10)
            {
                return true;
            }

            return false;
        }
    }
}
