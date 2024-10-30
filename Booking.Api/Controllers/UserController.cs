using Booking.Domain.Dto.PayMethod;
using Booking.Domain.Dto.User;
using Booking.Domain.Interfaces.Services;
using Booking.Domain.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Booking.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserProfileService _userProfileService;

        /// <summary>
        /// 
        /// </summary>
        public UserController(IUserService userService, IUserProfileService userProfileService)
        {
            _userService = userService;
            _userProfileService = userProfileService;
        }

        
        /// <summary>
        /// Изменение почты пользователя.
        /// </summary>
        [HttpPut("update_email")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<BaseResult>> UpdateEmail([FromBody] string newEmail)
        {
            var email = GetUserEmail();

            if (email == null)
            {
                return Unauthorized();
            }

            var response = await _userService.UpdateUserEmailAsync(newEmail, email);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Подтверждение кода, который пришел на почту.
        /// </summary>
        [HttpPut("confirm_email")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<BaseResult>> ConfirmEmail(ConfirmRegisterDto dto)
        {
            var email = GetUserEmail();

            if (email == null)
            {
                return Unauthorized();
            }

            var response = await _userService.ConfirmNewEmailAsync(dto, email);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Изменение пароля.
        /// </summary>
        [HttpPut("update_pasword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<BaseResult>> UpdatePass(PassDto dto)
        {
            var email = GetUserEmail();

            if (email == null)
            {
                return Unauthorized();
            }

            var response = await _userService.ChangeUserPasswordAsync(dto, email);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Изменение телефона.
        /// </summary>
        [HttpPut("update_phone")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<BaseResult>> UpdatePhone([FromBody] string phone)
        {
            var email = GetUserEmail();

            if (email == null)
            {
                return Unauthorized();
            }

            var response = await _userProfileService.UpdateUserPhoneNumber(phone, email);

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
