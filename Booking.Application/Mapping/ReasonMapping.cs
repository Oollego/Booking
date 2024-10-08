using AutoMapper;
using Booking.Domain.Dto.Country;
using Booking.Domain.Dto.Reason;
using Booking.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Mapping
{
    internal class ReasonMapping: Profile
    {
        public ReasonMapping()
        {
            CreateMap<TravelReason, ReasonDto>().ReverseMap();
        }
     }
}
