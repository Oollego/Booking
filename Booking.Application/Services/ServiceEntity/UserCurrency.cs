using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Services.ServiceEntity
{
    internal class UserCurrency
    {
        public string CurrencyChar { get; set; } = null!;
        public double ExchangeRate { get; set; }
    }
}
