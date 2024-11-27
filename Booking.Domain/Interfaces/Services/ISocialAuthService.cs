using Booking.Domain.Dto.Token;
using Booking.Domain.Dto;
using Booking.Domain.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Interfaces.Services
{
    public interface ISocialAuthService
    {
        public Task<BaseResult<TokenDto>> SignInWithGoogle(SocialTokenDto dto);
    }
}
