using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Booking.Application.Resources;
using Booking.Domain.Dto.Country;
using Booking.Domain.Dto.Reason;
using Booking.Domain.Entity;
using Booking.Domain.Enum;
using Booking.Domain.Interfaces.Repositories;
using Booking.Domain.Interfaces.Services;
using Booking.Domain.Result;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Booking.Application.Services
{
    public class ReasonService : IReasonService
    {
        private readonly IBaseRepository<TravelReason> _reasonRepository = null!;
        private readonly ILogger _logger = null!;
        private readonly IMapper _mapper = null!;

        public ReasonService(IBaseRepository<TravelReason> reasonRepository, ILogger logger, IMapper mapper)
        {
            _reasonRepository = reasonRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<BaseResult<ReasonDto>> CreatReasonAsync(string reason)
        {
            if (reason == null || reason == "")
            {
                _logger.Warning(ErrorMessage.InvalidParameters);
                return new BaseResult<ReasonDto>
                {
                    ErrorCode = (int)ErrorCodes.InvalidParameters,
                    ErrorMessage = ErrorMessage.InvalidParameters
                };
            }

            var reasonResult =  await _reasonRepository.GetAll().Where(r => r.Reason == reason).FirstOrDefaultAsync();

            if(reasonResult != null)
            {
                _logger.Warning(ErrorMessage.ReasonAlreadyExists);
                return new BaseResult<ReasonDto>
                {
                    ErrorCode = (int)ErrorCodes.ReasonAlreadyExists,
                    ErrorMessage = ErrorMessage.ReasonAlreadyExists
                };
            }

            var newReason = new TravelReason
            {
                Reason = reason
            };

            newReason = await _reasonRepository.CreateAsync(newReason);
            await _reasonRepository.SaveChangesAsync();

            return new BaseResult<ReasonDto>
            {
                Data = _mapper.Map<ReasonDto>(newReason)
            };
        }

        public async Task<BaseResult<ReasonDto>> DeleteReasonAsync(long id)
        {
            if (id <= 0)
            {
                _logger.Warning(ErrorMessage.InvalidParameters);
                return new BaseResult<ReasonDto>
                {
                    ErrorCode = (int)ErrorCodes.InvalidParameters,
                    ErrorMessage = ErrorMessage.InvalidParameters
                };
            }

            var reason = await _reasonRepository.GetAll().Where(r => r.Id == id).FirstOrDefaultAsync();

            if (reason == null)
            {
                _logger.Warning(ErrorMessage.ReasonNotFound);
                return new BaseResult<ReasonDto>
                {
                    ErrorCode = (int)ErrorCodes.ReasonNotFound,
                    ErrorMessage = ErrorMessage.ReasonNotFound
                };
            }

            reason = _reasonRepository.Remove(reason);
            await _reasonRepository.SaveChangesAsync();

            return new BaseResult<ReasonDto>
            {
                Data = _mapper.Map<ReasonDto>(reason)
            };
        }

        public async Task<CollectionResult<ReasonDto>> GetAllReasonsAsync()
        {
            var reasons = await _reasonRepository.GetAll().Select(r => new ReasonDto
            {
                Id = r.Id,
                Reason = r.Reason,
            }).ToListAsync();

            if (reasons == null || reasons.Count == 0)
            {
                _logger.Warning(ErrorMessage.ReasonNotFound);
                return new CollectionResult<ReasonDto>
                {
                    ErrorCode = (int)ErrorCodes.ReasonNotFound,
                    ErrorMessage = ErrorMessage.ReasonNotFound
                };
            }

            return new CollectionResult<ReasonDto>
            {
                Data = reasons
            };
        }

        public async Task<BaseResult<ReasonDto>> GetReasonByIdAsync(long id)
        {
            if (id <= 0)
            {
                _logger.Warning(ErrorMessage.InvalidParameters);
                return new BaseResult<ReasonDto>
                {
                    ErrorCode = (int)ErrorCodes.InvalidParameters,
                    ErrorMessage = ErrorMessage.InvalidParameters
                };
            }

            var reason = await _reasonRepository.GetAll().Where(r => r.Id == id).Select(r => new ReasonDto
            {
                Id = id,
                Reason = r.Reason
            }).FirstOrDefaultAsync();

            if (reason == null )
            {
                _logger.Warning(ErrorMessage.ReasonNotFound);
                return new BaseResult<ReasonDto>
                {
                    ErrorCode = (int)ErrorCodes.ReasonNotFound,
                    ErrorMessage = ErrorMessage.ReasonNotFound
                };
            }

            return new BaseResult<ReasonDto>
            {
                Data = reason
            };
        }

        public async Task<BaseResult<ReasonDto>> UpdateReasonAsync(ReasonDto dto)
        {
            if (dto.Id <= 0 || dto.Reason == null || dto.Reason == "")
            {
                _logger.Warning(ErrorMessage.InvalidParameters);
                return new BaseResult<ReasonDto>
                {
                    ErrorCode = (int)ErrorCodes.InvalidParameters,
                    ErrorMessage = ErrorMessage.InvalidParameters
                };
            }

            var reason = await _reasonRepository.GetAll().Where(r => r.Id == dto.Id).FirstOrDefaultAsync();

            if (reason == null)
            {
                _logger.Warning(ErrorMessage.ReasonNotFound);
                return new BaseResult<ReasonDto>
                {
                    ErrorCode = (int)ErrorCodes.ReasonNotFound,
                    ErrorMessage = ErrorMessage.ReasonNotFound
                };
            }

            reason.Reason = dto.Reason;

            var newReason = _reasonRepository.Update(reason);
            await _reasonRepository.SaveChangesAsync();

            return new BaseResult<ReasonDto>
            {
                Data = _mapper.Map<ReasonDto>(newReason)
            };
        }
    }
}
