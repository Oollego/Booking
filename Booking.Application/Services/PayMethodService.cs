using AutoMapper;
using Booking.Application.Resources;
using Booking.Domain.Dto.PayMethod;
using Booking.Domain.Entity;
using Booking.Domain.Enum;
using Booking.Domain.Interfaces.Repositories;
using Booking.Domain.Interfaces.Services;
using Booking.Domain.Result;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Booking.Application.Services
{
    public class PayMethodService : IPayMethodService
    {
        private readonly ILogger _logger = null!;
        private readonly IBaseRepository<PayMethod> _payMethodRepository;
        private readonly IBaseRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public PayMethodService(ILogger logger, IBaseRepository<PayMethod> payMethodRepository, IMapper mapper, IBaseRepository<User> userRepository)
        {
            _logger = logger;
            _payMethodRepository = payMethodRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<BaseResult<PayMethodDto>> CreatUserPayMethodAsync(CreatePayMethodDto dto, string? email)
        {

            if (dto.CardDate < DateTime.UtcNow || dto.CardNumber.Length != 16 || dto.CardTypeId < 0)
            {
                _logger.Warning(ErrorMessage.InvalidParameters);
                return new BaseResult<PayMethodDto>
                {
                    ErrorCode = (int)ErrorCodes.InvalidParameters,
                    ErrorMessage = ErrorMessage.InvalidParameters
                };
            }

           
            var userPayMethod = await _payMethodRepository.GetAll()
                .Include(pm => pm.UserProfile)
                    .ThenInclude(up => up.User)
                .Where(up => up.UserProfile.User.UserEmail == email && up.CardNumber == dto.CardNumber)
                .FirstOrDefaultAsync();

            if (userPayMethod != null)
            {
                _logger.Warning(ErrorMessage.PayMethodAlreadyExists);
                return new BaseResult<PayMethodDto>
                {
                    ErrorCode = (int)ErrorCodes.PayMethodAlreadyExists,
                    ErrorMessage = ErrorMessage.PayMethodAlreadyExists
                };
            }

            var user = await GetUserByEmailAsync(email);

            if (user == null)
            {
                _logger.Warning(ErrorMessage.AuthenticationRequired);
                return new BaseResult<PayMethodDto>
                {
                    ErrorCode = (int)ErrorCodes.AuthenticationRequired,
                    ErrorMessage = ErrorMessage.AuthenticationRequired
                };
            }
            
            var newPayMethod = new PayMethod()
            {
                CardNumber = dto.CardNumber,
                CardDate = dto.CardDate,
                CardTypeId = dto.CardTypeId,
                UserProfileId = user.UserProfile.Id
            };

            newPayMethod = await _payMethodRepository.CreateAsync(newPayMethod);
            await _payMethodRepository.SaveChangesAsync();

            return new BaseResult<PayMethodDto>()
            {
                Data = _mapper.Map<PayMethodDto>(newPayMethod)
            };
        }

        public async Task<BaseResult<PayMethodDto>> DeleteUserPayMethodAsync(long methodId, string? email)
        {
            if (methodId <= 0)
            {
                _logger.Warning(ErrorMessage.InvalidParameters);
                return new BaseResult<PayMethodDto>
                {
                    ErrorCode = (int)ErrorCodes.InvalidParameters,
                    ErrorMessage = ErrorMessage.InvalidParameters
                };
            }

            var user = await GetUserByEmailAsync(email);

            if (user == null)
            {
                _logger.Warning(ErrorMessage.AuthenticationRequired);
                return new BaseResult<PayMethodDto>
                {
                    ErrorCode = (int)ErrorCodes.AuthenticationRequired,
                    ErrorMessage = ErrorMessage.AuthenticationRequired
                };
            }

            var userPayMethod = await GetPayMethodAsync(methodId, email);

            if (userPayMethod == null)
            {
                _logger.Warning(ErrorMessage.PayMethodNotFound);
                return new BaseResult<PayMethodDto>
                {
                    ErrorCode = (int)ErrorCodes.PayMethodNotFound,
                    ErrorMessage = ErrorMessage.PayMethodNotFound
                };
            }

            userPayMethod = _payMethodRepository.Remove(userPayMethod);
            await _payMethodRepository.SaveChangesAsync();

            return new BaseResult<PayMethodDto>()
            {
                Data = _mapper.Map<PayMethodDto>(userPayMethod)
            };
        }

        public async Task<CollectionResult<PayMethodDto>> GetAllUserPayMethodAsync(string? email)
        {
            var payMethods = await _payMethodRepository.GetAll()
                .Include(pm => pm.UserProfile)
                    .ThenInclude(up => up.User)
                .Where(up => up.UserProfile.User.UserEmail == email)
                .Select(pm => new PayMethodDto()
                {
                    Id = pm.Id,
                    CardNumber = pm.CardNumber,
                    CardDate = pm.CardDate,
                    CardTypeId = pm.CardTypeId,
                })
                .ToListAsync();

            if (payMethods == null)
            {
                _logger.Warning(ErrorMessage.PayMethodNotFound);
                return new CollectionResult<PayMethodDto>
                {
                    ErrorCode = (int)ErrorCodes.PayMethodNotFound,
                    ErrorMessage = ErrorMessage.PayMethodNotFound
                };
            }

            return new CollectionResult<PayMethodDto>()
            {
                Count = payMethods.Count,
                Data = payMethods
            };

        }

        public async Task<BaseResult<PayMethodDto>> GetUserPayMethodByIdAsync(long methodId, string? email)
        {
            if (methodId <= 0)
            {
                _logger.Warning(ErrorMessage.InvalidParameters);
                return new BaseResult<PayMethodDto>
                {
                    ErrorCode = (int)ErrorCodes.InvalidParameters,
                    ErrorMessage = ErrorMessage.InvalidParameters
                };
            }

            var payMethod = await _payMethodRepository.GetAll()
               .Include(pm => pm.UserProfile)
                   .ThenInclude(up => up.User)
               .Where(pm => pm.Id == methodId)
               .Where(up => up.UserProfile.User.UserEmail == email)
               .Select(pm => new PayMethodDto()
               {
                   Id = pm.Id,
                   CardNumber = pm.CardNumber,
                   CardDate = pm.CardDate,
                   CardTypeId = pm.CardTypeId,
               })
               .FirstOrDefaultAsync();

            if (payMethod == null)
            {
                _logger.Warning(ErrorMessage.PayMethodNotFound);
                return new BaseResult<PayMethodDto>
                {
                    ErrorCode = (int)ErrorCodes.PayMethodNotFound,
                    ErrorMessage = ErrorMessage.PayMethodNotFound
                };
            }

            return new BaseResult<PayMethodDto>()
            {
                Data = payMethod
            };
        }

        public async Task<BaseResult<PayMethodDto>> UpdatePayMethodAsync(PayMethodDto dto, string? email)
        {
            if (dto.CardDate < DateTime.UtcNow || dto.CardNumber.Length != 16 || dto.CardTypeId < 0 || dto.Id < 0)
            {
                _logger.Warning(ErrorMessage.InvalidParameters);
                return new BaseResult<PayMethodDto>
                {
                    ErrorCode = (int)ErrorCodes.InvalidParameters,
                    ErrorMessage = ErrorMessage.InvalidParameters
                };
            }

            var user = await GetUserByEmailAsync(email);

            if (user == null)
            {
                _logger.Warning(ErrorMessage.AuthenticationRequired);
                return new BaseResult<PayMethodDto>
                {
                    ErrorCode = (int)ErrorCodes.AuthenticationRequired,
                    ErrorMessage = ErrorMessage.AuthenticationRequired
                };
            }

            var userPayMethod = await GetPayMethodAsync(dto.Id, email);

            if (userPayMethod == null)
            {
                _logger.Warning(ErrorMessage.PayMethodNotFound);
                return new BaseResult<PayMethodDto>
                {
                    ErrorCode = (int)ErrorCodes.PayMethodNotFound,
                    ErrorMessage = ErrorMessage.PayMethodNotFound
                };
            }

            userPayMethod.CardDate = dto.CardDate.Date;
            userPayMethod.CardTypeId = dto.CardTypeId;
            userPayMethod.CardNumber = dto.CardNumber;

            userPayMethod = _payMethodRepository.Update(userPayMethod);
            await _payMethodRepository.SaveChangesAsync();

            return new BaseResult<PayMethodDto>()
            {
                Data = _mapper.Map<PayMethodDto>(userPayMethod)
            };
        }

        private async Task<User?> GetUserByEmailAsync(string? email)
        {
            var user = await _userRepository.GetAll()
                .Include(ur => ur.UserProfile)
                .Where(u => u.UserEmail == email)
                .FirstOrDefaultAsync();

            return user;
        }

        private async Task<PayMethod?> GetPayMethodAsync( long methodId, string? email)
        {
            var userPayMethod = await _payMethodRepository.GetAll()
             .Include(pm => pm.UserProfile)
                 .ThenInclude(up => up.User)
             .Where(up => up.UserProfile.User.UserEmail == email)
             .Where(up => up.Id == methodId)
             .FirstOrDefaultAsync();

            return userPayMethod;
        }
    }
}
