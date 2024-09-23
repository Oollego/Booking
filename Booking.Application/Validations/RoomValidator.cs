using Booking.Application.Resources;
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
    internal class RoomValidator : IRoomValidator
    {
        public BaseResult ValidateOnNull(Room? model)
        {
            if (model == null)
            {
                return new BaseResult()
                {
                    ErrorMessage = ErrorMessage.RoomNotFound,
                    ErrorCode = (int)ErrorCodes.RoomNotFound,
                };
            }
            return new BaseResult();
        }

        public BaseResult CreateValidator(Room? room, Hotel? hotel)
        {
            if (room != null)
            {
                return new BaseResult()
                {
                    ErrorMessage = ErrorMessage.RoomAlreadyExists,
                    ErrorCode = (int)ErrorCodes.RoomAlreadyExists
                };
            }

            if (hotel == null)
            {
                return new BaseResult()
                {
                    ErrorMessage = ErrorMessage.HotelNotFound,
                    ErrorCode = (int)ErrorCodes.HotelNotFound
                };
            }
            return new BaseResult();
        }
    }
}
