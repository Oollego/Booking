using Booking.Application.Resources;
using Booking.Domain.Dto.UserProfile;
using Booking.Domain.Enum;
using Booking.Domain.Interfaces.Validations;
using Booking.Domain.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Booking.Application.Validations
{
    public class UserProfileValidator : IUserProfileValidator
    {
        public BaseResult FileUserProfileDtoValidator(FileUserProfileDto dto)
        {

            if (dto.UserName.Length > 0 || dto.UserSurname.Length > 0)
            {
                return new BaseResult
                {
                    ErrorCode = (int)ErrorCodes.InvalidParameters,
                    ErrorMessage = ErrorMessage.InvalidParameters
                };
            }

            return new BaseResult();
        }
    }
}
