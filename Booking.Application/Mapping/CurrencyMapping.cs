using AutoMapper;
using Booking.Domain.Dto.Currency;
using Booking.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Mapping
{
    internal class CurrencyMapping: Profile
    {
        public CurrencyMapping() 
        {
            CreateMap<CurrencyDto, Currency>().ReverseMap();
        }
    }
}
