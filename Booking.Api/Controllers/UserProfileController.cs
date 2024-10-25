using Booking.Application.Services;
using Booking.Domain.Dto.Facility;
using Booking.Domain.Dto.PayMethod;
using Booking.Domain.Dto.Topic;
using Booking.Domain.Dto.UserProfile;
using Booking.Domain.Dto.UserTopicDto;
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
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileService _userProfileService;
        private readonly IUserTopicService _userTopicService;
        private readonly IUserProfileFacilityService _facilityService;
        private readonly IPayMethodService _payMethodService;

        /// <summary>
        /// 
        /// </summary>
        public UserProfileController(IUserProfileService userProfileService, IUserTopicService userTopicService,
            IUserProfileFacilityService facilityService, IPayMethodService payMethodService)
        {
            _userProfileService = userProfileService;
            _userTopicService = userTopicService;
            _facilityService = facilityService;
            _payMethodService = payMethodService;
        }

        /// <summary>
        /// Добавить профиль пользователя. (Аватар добавлять не обязательно, обязательные поля Имя и Фамилия.)
        /// </summary>
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<BaseResult<UserProfileDto>>> CreateUserProfile(FileUserProfileDto dto)
        {
            var email = GetUserEmail();

            if (email == null)
            {
                return Unauthorized();
            }

            var response = await _userProfileService.CreateUserProfileAsync(dto, email);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Изменить профиль пользователя. Аватар добавлять не обязательно. Обязательные поля Имя и Фамилия. Если какое-то поле будет null, то в базе будет null (Кроме аватар, если аватар будет null – то в базе будет старый. 
        /// </summary>
        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<BaseResult<UserProfileDto>>> UpdateUserProfile(FileUserProfileDto dto)
        {
            var email = GetUserEmail();

            if (email == null)
            {
                return Unauthorized();
            }

            var response = await _userProfileService.UpdateUserProfileAsync(dto, email);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Получить профиль пользователя.
        /// </summary>
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<BaseResult<UserProfileDto>>> GetUserProfile()
        {
            var email = GetUserEmail();

            if (email == null)
            {
                return Unauthorized();
            }

            var response = await _userProfileService.GetUserProfileAsync(email);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Добавить новостную тему для пользователя.
        /// </summary>
        [HttpPost("info/user_topic")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<BaseResult<UserTopicDto>>> CreatUserTopic(CreateUserTopicDto dto)
        {
            var email = GetUserEmail();

            if (email == null)
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
        /// Удалить новостную тему.
        /// </summary>
        [HttpDelete("info/user_topic/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<BaseResult<UserTopicDto>>> DeleteUserTopic(long id)
        {
            var email = GetUserEmail();

            if (email == null)
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
        /// Получить все новостные темы пользователя.
        /// </summary>
        [HttpGet("info/user_topics")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<CollectionResult<TopicDto>>> GetAllUserTopic()
        {
            var email = GetUserEmail();

            if (email == null)
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
        /// Получить новостную тему.
        /// </summary>
        [HttpGet("info/user_topic")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<BaseResult<TopicDto>>> GetUserTopic(long id)
        {
            var email = GetUserEmail();

            if (email == null)
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

        /// <summary>
        /// Получить удобства пользователя.
        /// </summary>
        [HttpGet("info/user_facilities")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<CollectionResult<GroupFacilitiesDto>>> GetAllUserFacilities()
        {
            var email = GetUserEmail();

            if (email == null)
            {
                return Unauthorized();
            }

            var response = await _facilityService.GetAllUserFacilitiesAsync(email);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Получить удобство по Id.
        /// </summary>
        [HttpGet("info/user_facility/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<BaseResult<GroupFacilitiesDto>>> GetUserFacility(long id)
        {
            var email = GetUserEmail();

            if (email == null)
            {
                return Unauthorized();
            }

            var response = await _facilityService.GetUserFacilityByIdAsync(id, email);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Удалить удобства у пользователя.
        /// </summary>
        [HttpDelete("info/user_facility")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<CollectionResult<UserFacilityDto>>> DeleteUserFacility([FromForm] IdFacilityDto dto)
        {
            var email = GetUserEmail();

            if (email == null)
            {
                return Unauthorized();
            }

            var response = await _facilityService.DeleteRangeUserFacilitAsync(dto, email);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Добавить удобства для пользователя.
        /// </summary>
        [HttpPost("info/user_facility")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<CollectionResult<UserFacilityDto>>> CreateUserFacility([FromForm] IdFacilityDto dto)
        {
            var email = GetUserEmail();

            if (email == null)
            {
                return Unauthorized();
            }

            var response = await _facilityService.CreateRangeUserFacilityAsync(dto, email);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Получить все методы оплаты пользователя.
        /// </summary>
        [HttpGet("info/pay_methods")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<BaseResult<PayMethodDto>>> GetAllUserPayMethods()
        {
            var email = GetUserEmail();

            if (email == null)
            {
                return Unauthorized();
            }

            var response = await _payMethodService.GetAllUserPayMethodAsync(email);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Получить метод оплаты пользователя по Id.
        /// </summary>
        [HttpGet("info/pay_method/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<BaseResult<PayMethodDto>>> GetUserPayMethodById(long id)
        {
            var email = GetUserEmail();

            if (email == null)
            {
                return Unauthorized();
            }

            var response = await _payMethodService.GetUserPayMethodByIdAsync(id, email);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Добавить метод оплаты.
        /// </summary>
        [HttpPost("info/pay_method")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<BaseResult<PayMethodDto>>> CreateUserPayMethod(CreatePayMethodDto dto)
        {
            var email = GetUserEmail();

            if (email == null)
            {
                return Unauthorized();
            }

            var response = await _payMethodService.CreatUserPayMethodAsync(dto, email);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Изменить метод оплаты.
        /// </summary>
        [HttpPut("info/pay_method")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<BaseResult<PayMethodDto>>> UpdateUserPayMethod(PayMethodDto dto)
        {
            var email = GetUserEmail();

            if (email == null)
            {
                return Unauthorized();
            }

            var response = await _payMethodService.UpdatePayMethodAsync(dto, email);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Удалить метод оплаты.
        /// </summary>
        [HttpDelete("info/pay_method/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<BaseResult<PayMethodDto>>> DeleteUserPayMethod(long id)
        {
            var email = GetUserEmail();

            if (email == null)
            {
                return Unauthorized();
            }

            var response = await _payMethodService.DeleteUserPayMethodAsync(id, email);

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
