using Asp.Versioning;
using Booking.Application.Services;
using Booking.Domain.Dto.Facility;
using Booking.Domain.Dto.Hotel;
using Booking.Domain.Dto.HotelInfoCell;
using Booking.Domain.Dto.Review;
using Booking.Domain.Dto.Room;
using Booking.Domain.Dto.SearchFilter;
using Booking.Domain.Interfaces.Services;
using Booking.Domain.Result;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Booking.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class HotelController: ControllerBase
    {
        private IHotelService _hotelService;
        private IRoomService _roomService;
        private IReviewService _reviewService;
        private IFacilityService _facilityService;
        private IInfoCellService _infoCellService;

        /// <summary>
        /// 
        /// </summary>
        public HotelController(IHotelService hotelService, IRoomService roomService, IReviewService reviewService, 
            IFacilityService facilityService, IInfoCellService infoCellService)
        {
            _hotelService = hotelService;
            _roomService = roomService;
            _reviewService = reviewService;
            _facilityService = facilityService;
            _infoCellService = infoCellService;
        }

        /// <summary>
        /// Получить лучшие отели.
        /// </summary>
        [HttpGet("info/tophotels")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<TopHotelDto>>> GetTopHotel(int qty, int rating)
        {
            var response = await _hotelService.GetTopHotelsAsync(qty, rating);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Получить комнаты отеля по HotelId.
        /// </summary>
        [HttpGet("info/rooms/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<RoomDto>>> GetHotelRooms(long id)
        {
            var response = await _roomService.GetRoomsAsync(id);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }



        /// <summary>
        /// Поиск отелей.
        /// </summary>
        [HttpPatch("info/search_hotel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<HotelDto>>> SearchHotels([FromBody] SearchHotelDto dto)
        {

            var response = await _hotelService.SearchHotelAsync(dto);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Выводит данные для фильтрации отелей, после поиска. Вводим те же данные которые вводили для поиска отелей.
        /// </summary>
        [HttpPatch("info/search_filters")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<SearchFilterResponseDto>>> SearchFilters([FromBody] SearchFilterDto dto)
        {
 
            var response = await _hotelService.GetSearchFiltersAsync(dto);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        //[HttpPatch("info/hotels_by_city")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<ActionResult<BaseResult<InfoHotelDto>>> GetHotelsByCity(long id)
        //{

        //    var response = await _hotelService;

        //    if (response.IsSuccess)
        //    {
        //        return Ok(response);
        //    }
        //    return BadRequest(response);
            
        //}

        /// <summary>
        /// Получить отель по HotelId.
        /// </summary>
        [HttpGet("info/hotel/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<InfoHotelDto>>> GetHotelInfo(long id)
        {
            var user = HttpContext.User.Identity as ClaimsIdentity;
            string? email = null;

            if (user is not null && user.IsAuthenticated)
            {
                 email = user.Claims.First().Value;
            }

            var response = await _hotelService.GetHotelInfoAsync(id, email);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Поучить удобства отеля по HotelId.
        /// </summary>
        [HttpGet("info/facilities/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CollectionResult<FacilityInfoDto>>> GetHotelFacilitiesInfo(long id)
        {
            var response = await _facilityService.GetHotelFacilitiesAsync(id);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Поучить Important information отеля по HotelId.
        /// </summary>
        [HttpGet("info/cells/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CollectionResult<HotelInfoCellDto>>> GetHotelCellsInfo(long id)
        {
            var response = await _infoCellService.GetHotelInfoCellsAsync(id);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Получить отзывы отеля по HotelId.
        ///  </summary>
        [HttpGet("info/reviews/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CollectionResult<HotelReviewDto>>> GetHotelReviewsInfo(long id, int qty)
        {
           
            var response = await _reviewService.GetHotelReviewsAsync(id, qty);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Добавить отель. 
        /// </summary>
        [HttpPost("hotel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<InfoHotelDto>>> CreatHotel(CreateHotelDto dto)
        {
            var response = await _hotelService.CreateHotelAsync(dto);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Редактировать отель.
        /// </summary>
        [HttpPut("hotel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<InfoHotelDto>>> UpdateHotel(CreateUpdateHotelDto dto)
        {
            var response = await _hotelService.UpdateHotelAsync(dto);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
