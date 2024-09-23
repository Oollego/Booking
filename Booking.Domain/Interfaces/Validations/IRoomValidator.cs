using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Domain.Entity;
using Booking.Domain.Result;

namespace Booking.Domain.Interfaces.Validations
{
    public interface IRoomValidator: IBaseValidator<Room>
    {
        /// <summary>
        /// Check if room exist, if room exist in db then can't create new the same room
        /// If Hotel is missing then this hotel is no exist.
        /// </summary>
        /// <param name="room"></param>
        /// <param name="hotel"></param>
        /// <returns></returns>
        BaseResult CreateValidator(Room? room, Hotel? hotel);
    }
}
