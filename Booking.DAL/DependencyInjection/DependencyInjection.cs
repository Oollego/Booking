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
   
            var connectionString = configuration.GetConnectionString("MySQL") ?? "";

            services.AddSingleton<DateInterceptor>();

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseMySQL(connectionString);
            });
 
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

            services.AddScoped<IRoleUnitOfWork, RoleUnitOfWork>();
            services.AddScoped<IHotelUnitOfWork, HotelUnitOfWork>();
        }
    }
}
