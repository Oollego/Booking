using Booking.Domain.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Booking.Domain.Dto.User;
using Booking.Domain.Dto;
using Booking.Domain.Interfaces.Services;
using Booking.Application.Services;
using Booking.Domain.Dto.Token;
using System.Security.Claims;

namespace Booking.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService = null!;
        private readonly IHashService _hashService = null!;
        private readonly ISocialAuthService _socialAuthService;

        /// <summary>
        /// 
        /// </summary>
        public AuthController(IAuthService authService, IHashService hashService, ISocialAuthService socialAuthService)
        {
            _authService = authService;
            _hashService = hashService;
            _socialAuthService = socialAuthService;
        }
        /// <summary>
        /// Регистрация пользователя. Отправляет код подтверждения на почту. Код действует 5 минут. Пользователь в базе не создается.
        /// </summary>
        [HttpPost("register")]
        public async Task<ActionResult<BaseResult<UserDto>>> Register([FromBody] RegisterUserDto dto)
        {
            var response = await _authService.Register(dto);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Подтверждение почты пользователя. Отправляем код подтверждения. Создается пользователь в базе.
        /// </summary>
        [HttpPost("confirm_register")]
        public async Task<ActionResult<BaseResult<UserDto>>> ConfirmRegister([FromBody] ConfirmRegisterDto dto)
        {
            var response = await _authService.ConfirmRegister(dto);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Аутентификация пользователя. Получаем AccessToken и RefreshToken. Access Token нужно включать в заголовок запроса ‘Authorization: Bearer (YourAccessToken)’.
        /// </summary>
        [HttpPost("Login")]
        public async Task<ActionResult<BaseResult<TokenDto>>> Login([FromBody] LoginUserDto dto)
        {
            var response = await _authService.Login(dto);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Аутентификация пользователя через Google.
        /// </summary>
        [HttpPost("GoogleAuth")]
        public async Task<ActionResult<BaseResult<TokenDto>>> GoogleAuth([FromBody] SocialTokenDto dto)
        {
            var response = await _socialAuthService.SignInWithGoogle(dto);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Аутентификация пользователя через Facebook.
        /// </summary>
        [HttpPost("FacebookAuth")]
        public async Task<ActionResult<BaseResult<TokenDto>>> FaceBookAuth([FromBody] SocialTokenDto dto)
        {
            var response = await _socialAuthService.SignInWithFaceBook(dto);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Аутентификация пользователя через Google .
        /// </summary>
        [HttpPost("AuthOut")]
        public async Task<ActionResult<BaseResult<TokenDto>>> OutFromAccount()
        {
            var email = GetUserEmail();

            var response = await _authService.AuthOut(email);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        private string? GetUserEmail()
        {
            var user = HttpContext.User.Identity as ClaimsIdentity;

            if (user != null && user.IsAuthenticated)
            {
                return user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            }

            return null;
        }
    }
}
