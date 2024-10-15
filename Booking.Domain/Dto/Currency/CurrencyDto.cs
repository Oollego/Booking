using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Dto.Currency
{
    public class CurrencyDto
    {
        public string CurrencyCode { get; set; } = null!;
        public string CurrencyName { get; set; } = null!;
        public string CurrencyChar { get; set; } = null!;
        public double ExchangeRate { get; set; }
    }
}
