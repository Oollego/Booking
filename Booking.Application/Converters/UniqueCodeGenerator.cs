using Booking.Domain.Interfaces.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Converters
{
    internal class UniqueCodeGenerator : IUniqueCodeGenerator
    {
        public string GenerateUniqueBookingCode()
        {
            byte[] buffer = new byte[8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(buffer);
            }

            long number = Math.Abs(BitConverter.ToInt64(buffer, 0));

            string code = number.ToString().Substring(0, 10);

            return code;
        }
    }
}
