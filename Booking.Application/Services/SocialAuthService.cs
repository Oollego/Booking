using Booking.Application.Resources;
using Booking.Domain.Dto;
using Booking.Domain.Dto.Review;
using Booking.Domain.Dto.Token;
using Booking.Domain.Entity;
using Booking.Domain.Enum;
using Booking.Domain.Interfaces.Services;
using Booking.Domain.Interfaces.Services.ServiceDto;
using Booking.Domain.Interfaces.UnitsOfWork;
using Booking.Domain.Result;
using Google.Apis.Auth;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Services
{
    public class SocialAuthService: ISocialAuthService
    {
        public readonly ILogger _logger;
        public readonly IGoogleAuthService _googleAuthService;
        public readonly IAuthUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        public readonly IFaceBookAuthService _faceBookAuthService;

        public SocialAuthService(ILogger logger, IGoogleAuthService googleAuthService, IAuthUnitOfWork unitOfWork,
            ITokenService tokenService, IFaceBookAuthService faceBookAuthService)
        {
            _logger = logger;
            _googleAuthService = googleAuthService;
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _faceBookAuthService = faceBookAuthService;
        }

        private async Task<BaseResult<TokenDto>> SocialAuth(UserAuth userAuth)
        {

            var user = await _unitOfWork.Users.GetAll().Include(u => u.Roles)
                        .FirstOrDefaultAsync(u => u.UserEmail == userAuth.Email);

            if (user == null)
            {
                using (var transaction = await _unitOfWork.BeginTransactionAsync())
                {

                    var role = await _unitOfWork.Roles.GetAll().FirstOrDefaultAsync(r => r.RoleName == nameof(Roles.User));

                    if (role == null)
                    {
                        return new BaseResult<TokenDto>
                        {
                            ErrorMessage = ErrorMessage.RoleNotFound,
                            ErrorCode = (int)ErrorCodes.RoleNotFound
                        };
                    }

                    user = new User
                    {
                        UserEmail = userAuth.Email,
                        RegisteredAt = DateTime.UtcNow,
                        Roles = new List<Role> { role }
                    };

                    user = await _unitOfWork.Users.CreateAsync(user);
                    await _unitOfWork.Users.SaveChangesAsync();

                    UserProfile userProfile = new UserProfile()
                    {
                        UserName = userAuth.Name,
                        UserSurname = userAuth.Surname,
                        Avatar = userAuth.AvatarUrl,
                        UserId = user.Id
                    };

                    await _unitOfWork.UserProfiles.CreateAsync(userProfile);

                    await _unitOfWork.SaveChangesAsync();

                    await transaction.CommitAsync();
                }
            }
            else
            {
                var userProfile = await _unitOfWork.UserProfiles.GetAll()
                    .Include(up => up.User)
                    .Where(up => up.User.UserEmail ==  userAuth.Email)
                    .FirstOrDefaultAsync();

                if (userProfile == null)
                {
                    userProfile = new UserProfile
                    {
                        UserId = user.Id,
                        UserName = userAuth.Name,
                        UserSurname = userAuth.Surname,
                        Avatar = userAuth.AvatarUrl,
                    };

                    _ = await _unitOfWork.UserProfiles.CreateAsync(userProfile);
                }
                else
                {
                    userProfile.UserName = userAuth.Name;
                    userProfile.UserSurname = userAuth.Surname;
                    userProfile.Avatar = userAuth.AvatarUrl;

                    _ = _unitOfWork.UserProfiles.Update(userProfile);
                }

                await _unitOfWork.SaveChangesAsync();
            }

            var claims = user.Roles.Select(r => new Claim(ClaimTypes.Role, r.RoleName)).ToList();
            claims.Add(new Claim(ClaimTypes.Email, user.UserEmail));

            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            var userToken = await _unitOfWork.UserTokens.GetAll().FirstOrDefaultAsync(t => t.UserId == user.Id);
            if (userToken == null)
            {
                userToken = new UserToken
                {
                    UserId = user.Id,
                    RefreshToken = refreshToken,
                    RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(10)
                };
                await _unitOfWork.UserTokens.CreateAsync(userToken);
            }
            else
            {
                userToken.RefreshToken = refreshToken;
                userToken.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(10);
                _unitOfWork.UserTokens.Update(userToken);
            }

            await _unitOfWork.UserTokens.SaveChangesAsync();

            return new BaseResult<TokenDto>
            {
                Data = new TokenDto
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                }
            };
        }

        public async Task<BaseResult<TokenDto>> SignInWithFaceBook(SocialTokenDto dto)
        {
            if (dto == null || dto.TokenId == null || dto.TokenId.Length == 0)
            {
                return new BaseResult<TokenDto>()
                {
                    ErrorMessage = ErrorMessage.InvalidParameters,
                    ErrorCode = (int)ErrorCodes.InvalidParameters
                };
            }

            BaseResult<UserAuth> faceBookResult = await _faceBookAuthService.GetUserFromFacebookTokenAsync(dto.TokenId);

            if (!faceBookResult.IsSuccess)
            {
                return new BaseResult<TokenDto>
                {
                    ErrorCode = faceBookResult.ErrorCode,
                    ErrorMessage = faceBookResult.ErrorMessage
                };
            }

            return await SocialAuth(faceBookResult.Data!);
        }

        public async Task<BaseResult<TokenDto>> SignInWithGoogle(SocialTokenDto dto)
        {
            if (dto == null || dto.TokenId == null || dto.TokenId.Length == 0)
            {
                return new BaseResult<TokenDto>()
                {
                    ErrorMessage = ErrorMessage.InvalidParameters,
                    ErrorCode = (int)ErrorCodes.InvalidParameters
                };
            }

            BaseResult<UserAuth> googleResult = await _googleAuthService.ValidateIdTokenAsync(dto.TokenId);

            if (!googleResult.IsSuccess)
            {
                return new BaseResult<TokenDto>
                {
                    ErrorCode = googleResult.ErrorCode,
                    ErrorMessage = googleResult.ErrorMessage
                };
            }

            return await SocialAuth(googleResult.Data!);

        }

    }
}
