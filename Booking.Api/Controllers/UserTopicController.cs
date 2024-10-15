using Booking.Application.Services;
using Booking.Domain.Dto.Country;
using Booking.Domain.Dto.Topic;
using Booking.Domain.Dto.UserTopicDto;
using Booking.Domain.Entity;
using Booking.Domain.Interfaces.Repositories;
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
    public class UserTopicController : ControllerBase
    {
        private readonly IUserTopicService _userTopicService;

        /// <summary>
        /// 
        /// </summary>
        public UserTopicController(IUserTopicService userTopicService)
        {
            _userTopicService = userTopicService;
        }

        /// <summary>
        /// 
        /// </summary>
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<BaseResult<UserTopicDto>>> CreatUserTopic(CreateUserTopicDto dto)
        {
            var user = HttpContext.User.Identity as ClaimsIdentity;
            string? email = null;

            if (user is not null && user.IsAuthenticated)
            {
                email = user.Claims.First().Value;
            }
            else
            {
                return Unauthorized();
            }

            var response = await _userTopicService.CreatUserTopicAsync(dto.TopicId, email);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// 
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<BaseResult<UserTopicDto>>> DeleteUserTopic(long id)
        {
            var user = HttpContext.User.Identity as ClaimsIdentity;
            string? email = null;

            if (user is not null && user.IsAuthenticated)
            {
                email = user.Claims.First().Value;
            }
            else
            {
                return Unauthorized();
            }

            var response = await _userTopicService.DeleteUserTopicAsync(id, email);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// 
        /// </summary>
        [HttpGet("user_topics")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<CollectionResult<TopicDto>>> GetAllUserTopic()
        {
            var user = HttpContext.User.Identity as ClaimsIdentity;
            string? email = null;

            if (user is not null && user.IsAuthenticated)
            {
                email = user.Claims.First().Value;
            }
            else
            {
                return Unauthorized();
            }

            var response = await _userTopicService.GetAllUserTopicsAsync(email);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// 
        /// </summary>
        [HttpGet("user_topic")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<BaseResult<TopicDto>>> GetUserTopic(long id)
        {
            var user = HttpContext.User.Identity as ClaimsIdentity;
            string? email = null;

            if (user is not null && user.IsAuthenticated)
            {
                email = user.Claims.First().Value;
            }
            else
            {
                return Unauthorized();
            }

            var response = await _userTopicService.GetUserTopicByIdAsync(id, email);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
