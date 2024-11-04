using Booking.Domain.Dto.Hotel;
using Booking.Domain.Dto.Review;
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
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        /// <summary>
        /// 
        /// </summary>
        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        /// <summary>
        /// Получить последние отзывы.
        /// </summary>
        [HttpGet("last_reviews")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<HotelReviewDto>>> GetLastReviews(int qty)
        {
            var response = await _reviewService.GetLastReviewsAsync(qty);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Получить отзыв по Id.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<ReviewResponseDto>>> GetReview(long id)
        {
            var response = await _reviewService.GetReviewByIdAsync(id);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Получить отзывы пользователя
        /// </summary>
        [HttpGet("users_reviews")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<CollectionResult<ReviewResponseDto>>> GetUsersReviews()
        {
            var email = GetUserEmail();

            if (email == null)
            {
                return Unauthorized();
            }

            var response = await _reviewService.GetUsersReviewsAsync(email);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Изменить отзыв.
        /// </summary>
        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<BaseResult<ReviewDto>>> UpdateReview(ReviewDto dto)
        {
            var email = GetUserEmail();

            if (email == null)
            {
                return Unauthorized();
            }

            var response = await _reviewService.UpdateReviewAsync(dto, email);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Добавить отзыв.
        /// </summary>
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<BaseResult<ReviewDto>>> CreateReview(CreateReviewDto dto)
        {
            var email = GetUserEmail();

            if (email == null)
            {
                return Unauthorized();
            }

            var response = await _reviewService.CreateReviewAsync(dto, email);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Удалить отзыв.
        /// </summary>
        [HttpDelete()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<BaseResult<ReviewDto>>> DeleteReview(long id)
        {
            var email = GetUserEmail();

            if (email == null)
            {
                return Unauthorized();
            }

            var response = await _reviewService.DeleteReviewAsync(id, email);

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
