using Booking.Application.Cashe;
using Booking.Application.Resources;
using Booking.Domain.Dto.Review;
using Booking.Domain.Dto.User;
using Booking.Domain.Entity;
using Booking.Domain.Enum;
using Booking.Domain.Interfaces.Repositories;
using Booking.Domain.Interfaces.Services;
using Booking.Domain.Result;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Booking.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IHashService _hashService;
        private readonly IMemoryCache _memoryCache;
        private readonly IEmailService _emailService;
        private readonly ILogger _logger;

        public UserService(IBaseRepository<User> userRepository, IHashService hashService, IMemoryCache memoryCache,
            IEmailService emailService, ILogger logger)
        {
            _userRepository = userRepository;
            _hashService = hashService;
            _memoryCache = memoryCache;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task<BaseResult> ChangeUserPasswordAsync(PassDto dto, string? email)
        {
            if(dto.Password != dto.RepeatPassword)
            {
                _logger.Warning(ErrorMessage.PasswordNotEqualsPasswordConfirm);
                return new BaseResult()
                {
                    ErrorMessage = ErrorMessage.PasswordNotEqualsPasswordConfirm,
                    ErrorCode = (int)ErrorCodes.PasswordNotEqualsPasswordConfirm,

                };
            }

            if(dto.Password == null || dto.Password.Length < 5 || dto.OldPassword == null || dto.OldPassword.Length < 5 )
            {
                _logger.Warning(ErrorMessage.InvalidParameters);
                return new BaseResult()
                {
                    ErrorMessage = ErrorMessage.InvalidParameters,
                    ErrorCode = (int)ErrorCodes.InvalidParameters,
                };
            }

            var user = _userRepository.GetAll().Where(u => u.UserEmail == email).FirstOrDefault();

            if (user == null)
            {
                _logger.Warning(ErrorMessage.UserNotFound);
                return new BaseResult<UserDto>()
                {
                    ErrorMessage = ErrorMessage.UserNotFound,
                    ErrorCode = (int)ErrorCodes.UserNotFound
                };
            }

            String salt = _hashService.HexString(Guid.NewGuid().ToString());
            String dk = _hashService.HexString(salt + dto.Password);

            user.PasswordDk = dk;
            user.PasswordSalt = salt;

            user = _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();

            return new BaseResult();
        }

        public async Task<BaseResult> ConfirmNewEmailAsync(ConfirmRegisterDto dto, string? oldEmail)
        {
            _memoryCache.TryGetValue(dto.confirmCode, out string? cacheEmail);

            if (cacheEmail == null)
            {
                _logger.Warning(ErrorMessage.RegistrationCodeNotFound);
                return new BaseResult<UserDto>()
                {
                    ErrorMessage = ErrorMessage.RegistrationCodeNotFound,
                    ErrorCode = (int)ErrorCodes.RegistrationCodeNotFound
                };
            }
            if(cacheEmail != dto.email)
            {
                _logger.Warning(ErrorMessage.UserIsNotMatched);
                return new BaseResult<UserDto>()
                {
                    ErrorMessage = ErrorMessage.UserIsNotMatched,
                    ErrorCode = (int)ErrorCodes.UserIsNotMatched
                };
            }


            var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.UserEmail == oldEmail);

            if (user == null)
            {
                _logger.Warning(ErrorMessage.UserNotFound);
                return new BaseResult<UserDto>()
                {
                    ErrorMessage = ErrorMessage.UserNotFound,
                    ErrorCode = (int)ErrorCodes.UserNotFound
                };
            }

            user.UserEmail = dto.email;

            user = _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();

            return new BaseResult();
        }

        public async Task<BaseResult> UpdateUserEmailAsync(string newEmail, string? email)
        {
            string pattern = "([a-zA-Z0-9._-]+@[a-zA-Z0-9._-]+\\.[a-zA-Z0-9_-]+)";
            if (!Regex.IsMatch(newEmail, pattern, RegexOptions.IgnoreCase))
            {
                return new BaseResult()
                {
                    ErrorMessage = ErrorMessage.EmailIsNotCorrect,
                    ErrorCode = (int)ErrorCodes.EmailIsNotCorrect
                };
            }



            var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.UserEmail == email);

            if (user == null)
            {
                return new BaseResult()
                {
                    ErrorMessage = ErrorMessage.UserNotFound,
                    ErrorCode = (int)ErrorCodes.UserNotFound
                };
            }

            string? confirmCode = CheckAndSetUserToCache(newEmail, 10);
            if (confirmCode == null)
            {
                return new BaseResult()
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)ErrorCodes.InternalServerError
                };
            }

            await _emailService.SendUpdatedConfirmationEmailAsync(newEmail, confirmCode);

            return new BaseResult();
        }

        private string? CheckAndSetUserToCache(string email, int times)
        {
            string id = new Random().Next(100000, 999999).ToString();
            _memoryCache.TryGetValue(id, out string? userEmail);
            
            if (userEmail == null)
            {
                SetEmailToCache(id, email);
                return id;
            }
            else
            {
                if (userEmail != email)
                {
                   
                    _memoryCache.Remove(id);
                    SetEmailToCache(id, email);
                    return id;
                }
                else
                {
                    if (times == 0) return null;
                    times--;
                    return CheckAndSetUserToCache(email, times);
                }
            }
        }

        private void SetEmailToCache(string id, string email)
        {
            _memoryCache.Set(id, email, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(Convert.ToInt32(AppSource.LifeOfEmailConfirmationCode))));
        }

    }
}
