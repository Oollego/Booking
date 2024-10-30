using Booking.Application.Services;
using Booking.Domain.Dto.Faq;
using Booking.Domain.Interfaces.Services;
using Booking.Domain.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Api.Controllers
{
    /// <summary>
    /// Часто задаваемые вопросы
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class FaqController : ControllerBase
    {
        private readonly IFaqService _faqService;

        /// <summary>
        /// 
        /// </summary>
        public FaqController(IFaqService faqService)
        {
            _faqService = faqService;
        }

        /// <summary>
        /// Удалить вопрос
        /// </summary>

        [HttpDelete()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult>> DeleteFaq(long id)
        {

            var response = await _faqService.DeleteFaq(id);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Добавить вопрос
        /// </summary>
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<long>>> CreateFaq(CreateFaqDto dto)
        {

            var response = await _faqService.CreateFaq(dto);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Изменить вопрос
        /// </summary>
        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<FaqFullDto>>> UpdateFaq(FaqFullDto dto)
        {

            var response = await _faqService.UpdateFaq(dto);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Получить вопрос по Id.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<FaqFullDto>>> GetFaq(long id)
        {

            var response = await _faqService.GetFaq(id);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Получить все вопросы по Id отеля 
        /// </summary>
        [HttpGet("hotel_faqs/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<FaqDto>>> GetFaqsByHotelId(long id)
        {
            var response = await _faqService.GetAllHotelFaqs(id);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
