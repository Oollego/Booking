using AutoMapper;
using Booking.Application.Converters;
using Booking.Application.Mapping;
using Booking.Application.Services;
using Booking.Application.Validations;
using Booking.Application.Validations.FluentValidations;
using Booking.Domain.Dto.Room;
using Booking.Domain.Interfaces.Converters;
using Booking.Domain.Interfaces.Services;
using Booking.Domain.Interfaces.Validations;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Booking.Application.DependencyInjection
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            var emailOptions = configuration.GetSection(nameof(EmailService));

            string smtpServer = emailOptions["SmtpServer"] ?? "";
            int    smtpPort   = Convert.ToInt32(emailOptions["SmtpPort"]);
            bool   useSsl     = Convert.ToBoolean(emailOptions["UseSsl"]);
            string login      = emailOptions["Login"] ?? "";
            string password   = emailOptions["Password"] ?? "";

            var domainName = configuration.GetSection("DomainName").Value ?? "";

            services.AddScoped<IImageToLinkConverter, ImageToLinkConverter>(x => new ImageToLinkConverter(domainName));
            services.AddScoped<IUniqueCodeGenerator, UniqueCodeGenerator>();

            services.AddScoped<IEmailService, EmailService>( x => 
                    new EmailService(smtpServer, smtpPort, useSsl, login, password)
                );

            InitServices(services);
            InitMapping(services);
            InitValidators(services);

            

            //services.AddScoped<IGoogleAuthService, GoogleAuthService>();
            //services.Configure<GoogleAuthConfig>(configuration.GetSection("Google"));

        }

        public static void InitServices(this IServiceCollection services) 
        {

            services.AddScoped<ISocialAuthService, SocialAuthService>();
            services.AddScoped<IHashService, HashService>();

            services.AddScoped<IGoogleAuthService, GoogleAuthService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IHotelService, HotelService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IFacilityService, FacilityService>();
            services.AddScoped<IInfoCellService, InfoCellService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IPlaceService, PlaceService>();
            services.AddScoped<ITopicService, TopicService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IReasonService, ReasonService>();
            services.AddScoped<IUserTopicService, UserTopicService>();
            services.AddScoped<ICurrencyService, CurrencyService>();
            services.AddScoped<ICardTypeService, CardTypeService>();
            services.AddScoped<IPayMethodService, PayMethodService>();
            services.AddScoped<IUserProfileFacilityService, UserProfileFacilityService>();
            services.AddScoped<IUserProfileService, UserProfileService>();
            services.AddScoped<IFacilityGroupService, FacilityGroupService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IFaqService, FaqService>();
            services.AddScoped<IUserService, UserService>();
        }

        public static void InitMapping(this IServiceCollection services)
        {
            services.AddAutoMapper
                (
                    typeof(RoomMapping), 
                    typeof(HotelMapping), 
                    typeof(RoleMapping), 
                    typeof(UserMapping),
                    typeof(CountryMapping),
                    typeof(CityMapping),
                    typeof(ReasonMapping),
                    typeof(TopicMapping),
                    typeof(CurrencyMapping),
                    typeof(PayMethodMapping),
                    typeof(ReviewMapping)
                );
        }

        public static void InitValidators(this IServiceCollection services)
        {
            services.AddScoped<IRoomValidator, RoomValidator>();
            services.AddScoped<IHotelCreateUpdateValidator, HotelCreateUpdateValidator>();
            services.AddScoped<IValidator<CreateRoomDto>, CreateRoomValidator>();
            services.AddScoped<IValidator<UpdateRoomDto>, UpdateRoomValidator>();
            services.AddScoped<IUserProfileValidator, UserProfileValidator>();
            services.AddScoped<IReviewDtoValidator, ReviewDtoValidator>();
        }
    }

}
