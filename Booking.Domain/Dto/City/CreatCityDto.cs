using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Dto.City
{
    public class CreateCityDto
    {
        public string CityName { get; set; } = null!;
        public long CountryId { get; set; }
    }
}
