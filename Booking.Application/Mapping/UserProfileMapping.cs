using AutoMapper;
using Booking.Domain.Dto.UserProfile;
using Booking.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Mapping
{
    internal class UserProfileMapping: Profile
    {
        public UserProfileMapping() 
        {
            CreateMap<UserProfileDto, UserProfile>().ReverseMap();
        }
    }
}
