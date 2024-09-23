using Booking.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.DAL.Interceptors
{
    internal class RoomInterceptor: SaveChangesInterceptor
    {
        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            var saveResult = await base.SavingChangesAsync(eventData, result, cancellationToken);

            var context = eventData.Context;
            if (context == null)
            {
                return saveResult;
            }

            var addedRoomsByHotel = context.ChangeTracker.Entries<Room>()
           .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
           .Select(e => e.Entity)
           .GroupBy(r => r.Hotel)
           .ToList();

            foreach (var hotelRooms in addedRoomsByHotel)
            {
                var hotelId = hotelRooms.Key.Id;

                var roomMinPrice = await context.Set<Room>()
                    .Where(r => r.HotelId == hotelId)
                    .Select(r => (decimal?)r.RoomPrice)
                    .MinAsync(cancellationToken);

                if (roomMinPrice == null)
                {
                    roomMinPrice = hotelRooms.Key.Rooms?.First().RoomPrice ?? 0;
                }

                await context.Set<HotelData>()
                    .Where(hd => hd.HotelId == hotelId)
                    .ExecuteUpdateAsync(hd => hd.SetProperty(x => x.HotelMinRoomPrice, roomMinPrice), cancellationToken);
            }

            return saveResult;
        }
    }
}
