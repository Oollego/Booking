using Booking.Domain.Dto.User;
using Booking.Domain.Result;
using Booking.Domain.Dto;

namespace Booking.Domain.Interfaces.Services
{
    /// <summary>
    /// Service for registration and authorization
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// User registration
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<BaseResult<UserDto>> Register(RegisterUserDto dto);

        /// <summary>
        /// Confirm registration by code
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<BaseResult<UserDto>> ConfirmRegister(ConfirmRegisterDto dto);

        /// <summary>
        /// User authorization
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<BaseResult<TokenDto>> Login(LoginUserDto dto);
    }
}
