using AutoMapper;
using Booking.Domain.Dto.Reason;
using Booking.Domain.Dto.Review;
using Booking.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Mapping
{
    internal class ReviewMapping: Profile 
    {
        public ReviewMapping()
        {
            CreateMap<Review, ReviewDto>().ReverseMap();
        }
    }
}
