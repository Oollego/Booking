using Booking.Application.Resources;
using Booking.Domain.Dto.Hotel;
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
    internal class HotelCreateUpdateValidator : IHotelCreateUpdateValidator
    {
        public BaseResult CreateUpdateDtoValidator(CreateUpdateHotelDto dto)
        {
            if (dto == null || dto.Id < 0 || dto.CityId <= 0 || dto.HotelChainId <= 0 || dto.HotelTypeId <= 0 ||
                dto.HotelName == "" || dto.HotelAddress == "" || dto.HotelPhone == "" ||
                dto.HotelName == null || dto.HotelAddress == null || dto.HotelPhone == null)
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
