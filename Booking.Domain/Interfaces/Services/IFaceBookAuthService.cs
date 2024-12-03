﻿using Booking.Domain.Interfaces.Services.ServiceDto;
using Booking.Domain.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Interfaces.Services
{
    public interface IFaceBookAuthService
    {
        Task<BaseResult<UserAuth>> GetUserFromFacebookTokenAsync(string accessToken);
    }
}