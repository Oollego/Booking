using Booking.Application.Services;
using Booking.Domain.Dto.Book;
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
    public class BookingController : ControllerBase
    {
        private readonly IBookService _bookService;

        /// <summary>
        /// 
        /// </summary>
        public BookingController(IBookService bookService)
        {
            _bookService = bookService;
        }

        /// <summary>
        /// Забронировать отель.
        /// </summary>
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<BaseResult<long>>> MakeBooking(CreateBookDto dto)
        {
            var email = GetUserEmail();

            if (email == null)
            {
                return Unauthorized();
            }

            var response = await _bookService.AddUserBooking(dto, email);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Получить всю историю бронирования пользователя.
        /// </summary>
        [HttpGet("user_bookings")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<CollectionResult<BookDto>>> AllUserBookings()
        {
            var email = GetUserEmail();

            if (email == null)
            {
                return Unauthorized();
            }

            var response = await _bookService.GetAllUserBooks(email);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Получить бронь по Id.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<BaseResult<BookDto>>> GetBookingById(long id)
        {
            var email = GetUserEmail();

            if (email == null)
            {
                return Unauthorized();
            }

            var response = await _bookService.GetBookById(id, email);

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

