using Amazon;
using Amazon.Extensions.NETCore.Setup;
using Amazon.S3;
using Booking.DAL.Interceptors;
using Booking.DAL.Repositories;
using Booking.DAL.UnitOfWork;
using Booking.Domain.Entity;
using Booking.Domain.Interfaces.Repositories;
using Booking.Domain.Interfaces.UnitsOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Booking.DAL.DependencyInjection
{
    public static class DependencyInjection
    {
        public static void AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbMySQL(configuration);
            services.AddSingleton<DateInterceptor>();
            services.AddAws3S(configuration);
            services.InitRepositories();
        }

        public static void InitRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBaseRepository<User>, BaseRepository<User>>();
            services.AddScoped<IBaseRepository<UserToken>, BaseRepository<UserToken>>();
            services.AddScoped<IBaseRepository<Role>, BaseRepository<Role>>();
            services.AddScoped<IBaseRepository<UserRole>, BaseRepository<UserRole>>();

            services.AddScoped<IBaseRepository<Hotel>, BaseRepository<Hotel>>();
            services.AddScoped<IBaseRepository<Room>, BaseRepository<Room>>();
            services.AddScoped<IBaseRepository<Review>, BaseRepository<Review>>();
            services.AddScoped<IBaseRepository<City>, BaseRepository<City>>();
            services.AddScoped<IBaseRepository<Country>, BaseRepository<Country>>();
            services.AddScoped<IBaseRepository<NearObject>, BaseRepository<NearObject>>();
            services.AddScoped<IBaseRepository<HotelData>, BaseRepository<HotelData>>();
            services.AddScoped<IBaseRepository<Facility>, BaseRepository<Facility>>();
            services.AddScoped<IBaseRepository<NearPlace>,  BaseRepository<NearPlace>>();
            services.AddScoped<IBaseRepository<TravelReason>, BaseRepository<TravelReason>>();
            services.AddScoped<IBaseRepository<Topic>,  BaseRepository<Topic>>();
            services.AddScoped<IBaseRepository<UserProfileTopic>, BaseRepository<UserProfileTopic>>();
            services.AddScoped<IBaseRepository<UserProfile>, BaseRepository<UserProfile>>();
            services.AddScoped<IBaseRepository<Currency>, BaseRepository<Currency>>();
            services.AddScoped<IBaseRepository<CardType>, BaseRepository<CardType>>();
            services.AddScoped<IBaseRepository<PayMethod>, BaseRepository<PayMethod>>();
            services.AddScoped<IBaseRepository<UserProfileFacility>,  BaseRepository<UserProfileFacility>>();
            services.AddScoped<IBaseRepository<Facility>, BaseRepository<Facility>>();


            services.AddScoped<IRoleUnitOfWork, RoleUnitOfWork>();
            services.AddScoped<IHotelUnitOfWork, HotelUnitOfWork>();

            services.AddScoped<IS3BucketRepository, S3BucketRepository>();
        }

        private static void AddDbMySQL(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MySQL") ?? "";

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseMySQL(connectionString);
            });
        }

        private static void AddAws3S(this IServiceCollection services, IConfiguration configuration)
        {
            var awsOption = configuration.GetSection("AWS");
            var accessKey = awsOption["AccessKey"];
            var secretKey = awsOption["SecretKey"];
            var region = awsOption["Region"];

            // Configure AWS options with the access key, secret key, and region
            var awsOptions = new AWSOptions
            {
                Credentials = new Amazon.Runtime.BasicAWSCredentials(accessKey, secretKey),
                Region = RegionEndpoint.GetBySystemName(region)
            };

            // Register AWS services with the configured AWS options
            services.AddDefaultAWSOptions(awsOptions);
            services.AddAWSService<IAmazonS3>();
        }
    }
}
