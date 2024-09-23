using Booking.Domain.Entity;
using Booking.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Interfaces.UnitsOfWork
{
    public interface IHotelUnitOfWork : IStateSaveChanges
    {
        IBaseRepository<Hotel> Hotels { get; set; }
        IBaseRepository<HotelData> HotelsData { get; set; }
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}
