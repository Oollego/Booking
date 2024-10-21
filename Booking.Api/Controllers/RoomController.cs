using Asp.Versioning;
using Azure;
using Booking.Domain.Dto.Room;
using Booking.Domain.Entity;
using Booking.Domain.Interfaces.Services;
using Booking.Domain.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Booking.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    //[Authorize]
    [ApiController]
    //[ApiVersion("1.0")]
    [Route("api/[controller]")]
    //[Route("api/v{version:apiVersion}/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService = null!;

        /// <summary>
        /// 
        /// </summary>
        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        /// <summary>
        /// Получить комнату по RoomId.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<RoomDto>>> GetRoom(long id)
        {
            var email = GetUserEmail();

            var response = await _roomService.GetRoomByIdAsync(id, email);

            if (response.IsSuccess)
            { 
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Удалить комнату по RoomId.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<RoomDto>>> Delete(long id)
        {
            var response = await _roomService.DeleteRoomAsync(id);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Добавить новую комнату для отеля.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<RoomDto>>> Create([FromBody] CreateRoomDto dto)
        {
            var response = await _roomService.CreatRoomAsync(dto);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Редактировать комнату.
        /// </summary>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<RoomDto>>> Update([FromBody] UpdateRoomDto dto)
        {
            var response = await _roomService.UpdateRoomAsync(dto);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        private string? GetUserEmail()
        {
            var user = HttpContext.User.Identity as ClaimsIdentity;
            string? email = null;

            if (user is not null && user.IsAuthenticated)
            {
                email = user.Claims.Where(x => x.Type.Contains("emailaddress")).Select(x => x.Value).FirstOrDefault();
            }
            return email;
        }
    }
}
