using Booking.Application.Services;
using Booking.Domain.Dto.City;
using Booking.Domain.Dto.Facility;
using Booking.Domain.Dto.FacilityGroup;
using Booking.Domain.Interfaces.Services;
using Booking.Domain.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class FacilityController : ControllerBase
    {
        private readonly IFacilityGroupService _facilityGroupService;
        private readonly IFacilityService _facilityService;

        /// <summary>
        /// 
        /// </summary>
        public FacilityController(IFacilityService facilityService, IFacilityGroupService facilityGroupService)
        {
            _facilityService = facilityService;
            _facilityGroupService = facilityGroupService;
        }

        /// <summary>
        /// Получить удобство по Id.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<FacilityDto>>> GetFacility(long id)
        {
            var response = await _facilityService.GetFacilityByIdAsync(id);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Получить все удобства.
        /// </summary>
        [HttpGet("facilities")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CollectionResult<GroupFacilitiesDto>>> GetAllFacilities()
        {
            var response = await _facilityService.GetAllFacilities();

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Добавить удобство.
        /// </summary>
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<FacilityDto>>> CreateFacility(CreateFacilityDto dto)
        {
            var response = await _facilityService.CreatFacilityAsync(dto);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Изменить удобство.
        /// </summary>
        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<FacilityDto>>> UpdateFacility(FacilityDto dto)
        {
            var response = await _facilityService.UpdateFacilityAsync(dto);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Удалить удобство.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<FacilityDto>>> DeleteFacility(long id)
        {
            var response = await _facilityService.DeleteFacilityAsync(id);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Удалить группу удобств.
        /// </summary>
        [HttpDelete("facility_group")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<FacilityGroupDto>>> DeleteFacilityGroup(long id)
        {
            var response = await _facilityGroupService.DeleteFacilityGroupAsync(id);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Добавить группу удобств.
        /// </summary>
        [HttpPost("facility_group")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<FacilityGroupDto>>> CreateFacilityGroup(CreateFacilityGroupDto dto)
        {
            var response = await _facilityGroupService.CreatFacilityGroupAsync(dto);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Изменить группу удобств.
        /// </summary>
        [HttpPut("facility_group")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<FacilityGroupDto>>> UpdateFacilityGroup(UpdateFacilityGroupDto dto)
        {
            var response = await _facilityGroupService.UpdateFacilityGroupAsync(dto);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Получить группу удобств.
        /// </summary>
        [HttpGet("facility_group/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<FacilityGroupDto>>> GetFacilityGroup(long id)
        {
            var response = await _facilityGroupService.GetFacilityGroupByIdAsync(id);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Получить все группы удобств.
        /// </summary>
        [HttpGet("facility_groups/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CollectionResult<FacilityGroupDto>>> GetAllFacilityGroups()
        {
            var response = await _facilityGroupService.GetAllFacilityGroupsAsync();

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }


    }
}
