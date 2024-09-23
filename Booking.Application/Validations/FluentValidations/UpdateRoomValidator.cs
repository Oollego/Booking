using Booking.Domain.Dto.Room;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Validations.FluentValidations
{
    public class UpdateRoomValidator: AbstractValidator<UpdateRoomDto>
    {
        public UpdateRoomValidator() 
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.RoomName).NotEmpty().MaximumLength(254);
        }

    }
}
