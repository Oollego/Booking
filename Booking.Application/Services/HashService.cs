using Booking.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Services
{
    public class HashService: IHashService
    {
        public string HexString(string input)
        {
            using var hasher = System.Security.Cryptography.MD5.Create();
            byte[] bytes = hasher.ComputeHash(
                System.Text.Encoding.UTF8.GetBytes(input)
            );
            return Convert.ToHexString(bytes);
        }
    }
}
