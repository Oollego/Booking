using AutoMapper;
using Booking.Domain.Dto.PayMethod;
using Booking.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Mapping
{
    internal class PayMethodMapping: Profile
    {
        public PayMethodMapping()
        {
            CreateMap<PayMethod, PayMethodDto>().ReverseMap();
        }
    }
}
