﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Interfaces.Converters
{
    public interface IUniqueCodeGenerator
    {
        string GenerateUniqueBookingCode();
    }
}
