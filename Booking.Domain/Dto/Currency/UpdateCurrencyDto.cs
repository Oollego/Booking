using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Dto.Currency
{
    public class UpdateCurrencyDto
    {
        public string CurrencyCode { get; set; } = null!;
        public double ExchangeRate { get; set; }
    }
}
