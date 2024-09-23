using AutoMapper;
using Booking.Application.Resources;
using Booking.Domain.Dto;
using Booking.Domain.Dto.Room;
using Booking.Domain.Dto.User;
using Booking.Domain.Entity;
using Booking.Domain.Enum;
using Booking.Domain.Interfaces.Repositories;
using Booking.Domain.Interfaces.Services;
using Booking.Domain.Result;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Booking.Domain.Interfaces.UnitsOfWork;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Caching.Memory;
using Booking.Application.Cashe;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Org.BouncyCastle.Crypto.Parameters;

namespace Booking.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IBaseRepository<User> _userRepository = null!;
        private readonly ILogger _logger = null!;
        private readonly IMapper _mapper = null!;
        private readonly IBaseRepository<UserToken> _userTokenRepository = null!;
        private readonly ITokenService _tokenService;
        private readonly IBaseRepository<Role> _roleRepository = null!;
        private readonly IRoleUnitOfWork _unitOfWork = null!;
        private readonly IHashService _hashService = null!;
        private readonly IMemoryCache _memoryCache = null!;
        private readonly IEmailService _emailService = null!;

        public AuthService(IBaseRepository<User> userRepository, ILogger logger, IMapper mapper,
            IBaseRepository<UserToken> userTokenRepository, ITokenService tokenService,
            IBaseRepository<Role> roleRepository, IRoleUnitOfWork unitOfWork, IHashService hashService, 
            IMemoryCache memoryCache, IEmailService emailService )
        {
            _userRepository = userRepository;
            _logger = logger;
            _mapper = mapper;
            _userTokenRepository = userTokenRepository;
            _tokenService = tokenService;
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
            _hashService = hashService;
            _memoryCache = memoryCache;
            _emailService = emailService;
        }

        public async Task<BaseResult<UserDto>> Register(RegisterUserDto dto)
        {
           // throw new UnauthorizedAccessException("UnauthorizedAccessException");
            if(dto.Password != dto.PasswordConfirm)
            {
                return new BaseResult<UserDto>()
                {
                    ErrorMessage = ErrorMessage.PasswordNotEqualsPasswordConfirm,
                    ErrorCode = (int)ErrorCodes.PasswordNotEqualsPasswordConfirm,

                };
            }

            string pattern = "([a-zA-Z0-9._-]+@[a-zA-Z0-9._-]+\\.[a-zA-Z0-9_-]+)";
            if (!Regex.IsMatch(dto.Email, pattern, RegexOptions.IgnoreCase))
            {
                return new BaseResult<UserDto>()
                {
                    ErrorMessage = ErrorMessage.EmailIsNotCorrect,
                    ErrorCode = (int)ErrorCodes.EmailIsNotCorrect
                };
            }
            
 
           // var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.UserEmail == dto.Email);
            var user = await _unitOfWork.Users.GetAll().FirstOrDefaultAsync(x => x.UserEmail == dto.Email);

            if(user != null)
            {
                return new BaseResult<UserDto>()
                {
                    ErrorMessage = ErrorMessage.UserAlreadyExists,
                    ErrorCode = (int)ErrorCodes.UserAlreadyExists
                };
            }

            
            String salt = _hashService.HexString(Guid.NewGuid().ToString());
            String dk = _hashService.HexString(salt + dto.Password);

            string? confirmCode = CheckAndSetUserToCache(dto.Email, salt, dk, 10);
            if(confirmCode == null)
            {
                return new BaseResult<UserDto>()
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)ErrorCodes.InternalServerError
                };
            }

            await _emailService.SendConfirmationEmailAsync(dto.Email, confirmCode);

            return new BaseResult<UserDto>()
            {
                Data = new UserDto(dto.Email)
            };


            //var hashUserPassword = HashPassword(dto.Password);

            //using (var transaction = await _unitOfWork.BeginTransactionAsync())
            //{
            //    try
            //    {
            //        user = new User()
            //        {
            //            UserEmail = dto.Email,
            //            PasswordDk = hashUserPassword,
            //            PasswordSalt = hashUserPassword,
            //        };
            //        await _unitOfWork.Users.CreateAsync(user);
            //        await _unitOfWork.SaveChangesAsync();

            //        var role = await _roleRepository.GetAll().FirstOrDefaultAsync(x => x.RoleName == nameof(Roles.User));
            //        if (role == null)
            //        {
            //            return new BaseResult<UserDto>()
            //            {
            //                ErrorMessage = ErrorMessage.RoleNotFound,
            //                ErrorCode = (int)ErrorCodes.RoleNotFound
            //            };
            //        }

            //        UserRole userRole = new UserRole()
            //        {
            //            UserId = user.Id,
            //            RoleId = role.Id
            //        };

            //        await _unitOfWork.UserRoles.CreateAsync(userRole);

            //        await _unitOfWork.SaveChangesAsync();

            //        await transaction.CommitAsync();

            //    }
            //    catch (Exception ex)
            //    {
            //        await transaction.RollbackAsync();

            //        _logger.Error(ex, ex.Message);
            //        return new BaseResult<UserDto>()
            //        {
            //            ErrorMessage = ErrorMessage.UserWasntCreated,
            //            ErrorCode = (int)ErrorCodes.UserWasNotCreated
            //        };
            //    }
            //}

            //return new BaseResult<UserDto>()
            //{
            //    Data = _mapper.Map<UserDto>(user)
            //};

        }
        public async Task<BaseResult<UserDto>> ConfirmRegister(ConfirmRegisterDto dto)
        {
            _memoryCache.TryGetValue(dto.confirmCode, out RegisterUserCacheDto? userCache);

            if (userCache == null)
            {
                return new BaseResult<UserDto>()
                {
                    ErrorMessage = ErrorMessage.RegistrationCodeNotFound,
                    ErrorCode = (int)ErrorCodes.RegistrationCodeNotFound
                };
            }
            if(userCache.UserEmail != dto.email)
            {
                return new BaseResult<UserDto>()
                {
                    ErrorMessage = ErrorMessage.UserIsNotMatched,
                    ErrorCode = (int)ErrorCodes.UserIsNotMatched
                };
            }

            var user = await _unitOfWork.Users.GetAll().FirstOrDefaultAsync(x => x.UserEmail == dto.email);

            if (user != null)
            {
                return new BaseResult<UserDto>()
                {
                    ErrorMessage = ErrorMessage.UserAlreadyExists,
                    ErrorCode = (int)ErrorCodes.UserAlreadyExists
                };
            }

            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    user = new User()
                    {
                        UserEmail = dto.email,
                        PasswordDk = userCache.PasswordDk,
                        PasswordSalt = userCache.PasswordSalt,
                    };
                    await _unitOfWork.Users.CreateAsync(user);
                    await _unitOfWork.SaveChangesAsync();

                    var role = await _roleRepository.GetAll().FirstOrDefaultAsync(x => x.RoleName == nameof(Roles.User));
                    if (role == null)
                    {
                        return new BaseResult<UserDto>()
                        {
                            ErrorMessage = ErrorMessage.RoleNotFound,
                            ErrorCode = (int)ErrorCodes.RoleNotFound
                        };
                    }

                    UserRole userRole = new UserRole()
                    {
                        UserId = user.Id,
                        RoleId = role.Id
                    };

                    await _unitOfWork.UserRoles.CreateAsync(userRole);

                    await _unitOfWork.SaveChangesAsync();

                    await transaction.CommitAsync();

                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();

                    _logger.Error(ex, ex.Message);
                    return new BaseResult<UserDto>()
                    {
                        ErrorMessage = ErrorMessage.UserWasntCreated,
                        ErrorCode = (int)ErrorCodes.UserWasNotCreated
                    };
                }
            }

            return new BaseResult<UserDto>()
            {
                Data = _mapper.Map<UserDto>(user)
            };
        }
        public async Task<BaseResult<TokenDto>> Login(LoginUserDto dto)
        {
 
            var user = await _userRepository.GetAll()
                .Include(x => x.Roles)
                .FirstOrDefaultAsync(x => x.UserEmail == dto.Email);
            if (user == null)
            {
                return new BaseResult<TokenDto>()
                {
                    ErrorMessage = ErrorMessage.UserNotFound,
                    ErrorCode = (int)ErrorCodes.UserNotFound
                };
            }
                
            if (!IsVerifyPassword(user.PasswordDk, user.PasswordSalt, dto.Password)) 
            {
                return new BaseResult<TokenDto>()
                {
                    ErrorMessage = ErrorMessage.PasswordIsWrong,
                    ErrorCode = (int)ErrorCodes.PasswordIsWrong
                };
            }

            var userToken = await _userTokenRepository.GetAll().FirstOrDefaultAsync(x => x.UserId == user.Id);

            var userRoles = user.Roles;
            var claims = userRoles.Select(x => new Claim(ClaimTypes.Role, x.RoleName)).ToList();
            claims.Add(new Claim(ClaimTypes.Email, dto.Email));
               
            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            if (userToken == null)
            {
                userToken = new UserToken()
                {
                    UserId = user.Id,
                    RefreshToken = refreshToken,
                    RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(10)
                };
                await _userTokenRepository.CreateAsync(userToken);
            }
            else
            {
                userToken.RefreshToken = refreshToken;
                userToken.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(10);

                _userTokenRepository.Update(userToken);
                await _userTokenRepository.SaveChangesAsync();
            }

            return new BaseResult<TokenDto>()
            {
                Data = new TokenDto()
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                }
            };
 
        }

        private string? CheckAndSetUserToCache( string email, string salt, string dk , int times)
        {
            string id = new Random().Next(100000, 999999).ToString();
            _memoryCache.TryGetValue(id, out RegisterUserCacheDto? userCache);

            if (userCache == null)
            {
                SetUserToCache(id, email, salt, dk);
                return id;
            }
            else
            {
                if (userCache.UserEmail == email)
                {
                    _memoryCache.Remove(id);
                    SetUserToCache(id, email, salt, dk);
                    return id;
                }
                else
                {
                    if (times == 0) return null;
                    times--;
                    return CheckAndSetUserToCache(email, salt, dk, times);
                }
            }
        }

        private void SetUserToCache(string id, string email, string salt, string dk)
        {
            RegisterUserCacheDto userCache = new RegisterUserCacheDto()
            {
                Id = id,
                UserEmail = email,
                PasswordSalt = salt,
                PasswordDk = dk
            };
            _memoryCache.Set(id, userCache, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
        }

        private bool IsVerifyPassword(string UserPasswordDk, string userSalt, string userPassword)
        {
            string dk = _hashService.HexString(userSalt + userPassword);
            return UserPasswordDk == dk;
        }
    }
}
