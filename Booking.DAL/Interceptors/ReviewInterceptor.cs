using Booking.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Mysqlx.Session;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Booking.DAL.Interceptors
{
    internal class ReviewInterceptor : SaveChangesInterceptor
    {
        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            var context = eventData.Context;
            if (context == null) 
            {
                return await base.SavingChangesAsync(eventData, result, cancellationToken);
            }

            var addedReviews = context.ChangeTracker.Entries<Review>()
                     .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
                     .Select(e => e.Entity)
                     .GroupBy(r => r.HotelId)
                     .ToList();

            foreach (var reviewGroup in addedReviews)
            {
                var hotelId = reviewGroup.Key;

                var reviewData = await context.Set<Review>()
                    .Where(r => r.HotelId == hotelId)
                    .GroupBy(r => r.HotelId)
                    .Select(g => new
                    {
                        reviewCount = g.Count(),
                        Rating = Math.Round(g.Average(r => (r.CleanlinessScore + r.StaffScore + r.ComfortScore + r.FacilityScore + r.LocationScore + r.ValueScore) / 6), 1)
                    })
                    .FirstOrDefaultAsync(cancellationToken);

                var hotelData = await context.Set<HotelData>()
                    .Where(hd => hd.HotelId == hotelId)
                    .FirstOrDefaultAsync(cancellationToken);

                if (hotelData == null || reviewData == null)
                {
                    continue;
                }

                hotelData.ReviewCount = reviewData.reviewCount;
                hotelData.Rating = reviewData.Rating;
            }

            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
