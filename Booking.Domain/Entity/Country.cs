using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entity
{
    public class Country
    {
        public long Id { get; set; }
        public string CountryName { get; set; } = null!;
        public string Flag { get; set; } = null!;
        public List<City> Cities { get; set; } = null!;
    }
}
