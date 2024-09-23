using Booking.Domain.Entity;
using Booking.Domain.Interfaces.Repositories;
using Booking.Domain.Interfaces.UnitsOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.DAL.UnitOfWork
{
    public class HotelUnitOfWork: IHotelUnitOfWork
    {
        public IBaseRepository<Hotel> Hotels { get; set; } = null!;
        public IBaseRepository<HotelData> HotelsData { get; set; } = null!;
        
        private readonly ApplicationDbContext _context;

        public HotelUnitOfWork(ApplicationDbContext context, IBaseRepository<Hotel> hotels,
            IBaseRepository<HotelData> hotelData)
        {
            _context = context;
            Hotels = hotels;
            HotelsData = hotelData;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
