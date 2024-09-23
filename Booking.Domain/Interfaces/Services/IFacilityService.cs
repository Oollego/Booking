using Booking.Domain.Dto.Facility;
using Booking.Domain.Dto.Hotel;
using Booking.Domain.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Interfaces.Services
{
    public interface IFacilityService
    {
       Task<CollectionResult<FacilityInfoDto>> GetHotelFacilitiesAsync(long hotelId);
    }
}
