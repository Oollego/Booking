using Booking.Domain.Dto.Hotel;
using Booking.Domain.Entity;
using Booking.Domain.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Interfaces.Validations
{
    public interface IHotelCreateUpdateValidator
    {
        BaseResult CreateUpdateDtoValidator(CreateUpdateHotelDto dto);
    }
}
