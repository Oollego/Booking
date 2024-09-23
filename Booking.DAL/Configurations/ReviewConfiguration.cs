using Booking.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.DAL.Configurations
{
    internal class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("reviews");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.ReviewComment).HasMaxLength(1500).HasDefaultValue("");
            builder.Property(x => x.HotelId).IsRequired();
            builder.Property(x => x.FacilityScore).HasDefaultValue(0);
            builder.Property(x => x.StaffScore).HasDefaultValue(0);
            builder.Property(x => x.CleanlinessScore).HasDefaultValue(0);
            builder.Property(x => x.ComfortScore).HasDefaultValue(0);
            builder.Property(x => x.LocationScore).HasDefaultValue(0);
            builder.Property(x => x.ValueScore).HasDefaultValue(0);
            builder.ToTable(t => t.HasCheckConstraint("ValidFacilityScore", "FacilityScore >= 0 AND FacilityScore <= 10"));
            builder.ToTable(t => t.HasCheckConstraint("ValidStaffScore", "StaffScore >= 0 AND StaffScore <= 10"));
            builder.ToTable(t => t.HasCheckConstraint("ValidCleanlinessScore", "CleanlinessScore >= 0 AND CleanlinessScore <= 10"));
            builder.ToTable(t => t.HasCheckConstraint("ValidComfortScore", "ComfortScore >= 0 AND ComfortScore <= 10"));
            builder.ToTable(t => t.HasCheckConstraint("ValidLocationScore", "LocationScore >= 0 AND LocationScore <= 10"));
            builder.ToTable(t => t.HasCheckConstraint("ValidValueScore", "ValueScore >= 0 AND ValueScore <= 10"));

            builder.HasData(new List<Review> 
            {
                new Review { Id = 1, UserId = 1, HotelId = 1, ReviewComment = "Great place!", FacilityScore = 8, StaffScore = 9, CleanlinessScore = 7, ComfortScore = 8, LocationScore = 10, ValueScore = 9, ReviewDate = DateTime.Now.AddDays(-4) },
                new Review { Id = 2, UserId = 1, HotelId = 2, ReviewComment = "Good service.", FacilityScore = 7, StaffScore = 8, CleanlinessScore = 7, ComfortScore = 8, LocationScore = 8, ValueScore = 7, ReviewDate = DateTime.Now.AddDays(-6) },
                new Review { Id = 3, UserId = 1, HotelId = 3, ReviewComment = "Clean and comfortable.", FacilityScore = 9, StaffScore = 9, CleanlinessScore = 9, ComfortScore = 9, LocationScore = 9, ValueScore = 9, ReviewDate = DateTime.Now.AddDays(-4) },
                new Review { Id = 4, UserId = 2, HotelId = 1, ReviewComment = "Amazing experience!", FacilityScore = 10, StaffScore = 10, CleanlinessScore = 10, ComfortScore = 10, LocationScore = 10, ValueScore = 10, ReviewDate = DateTime.Now.AddDays(-5) },
                new Review { Id = 5, UserId = 2, HotelId = 2, ReviewComment = "Very nice place.", FacilityScore = 8, StaffScore = 8, CleanlinessScore = 8, ComfortScore = 8, LocationScore = 8, ValueScore = 8, ReviewDate = DateTime.Now.AddDays(-6) },
                new Review { Id = 6, UserId = 2, HotelId = 3, ReviewComment = "Solid experience.", FacilityScore = 7, StaffScore = 7, CleanlinessScore = 7, ComfortScore = 7, LocationScore = 7, ValueScore = 7, ReviewDate = DateTime.Now.AddDays(-6) },
                new Review { Id = 7, UserId = 3, HotelId = 1, ReviewComment = "Loved it!", FacilityScore = 9, StaffScore = 9, CleanlinessScore = 9, ComfortScore = 9, LocationScore = 9, ValueScore = 9, ReviewDate = DateTime.Now.AddDays(-5) },
                new Review { Id = 8, UserId = 3, HotelId = 3, ReviewComment = "Highly recommended.", FacilityScore = 9, StaffScore = 9, CleanlinessScore = 9, ComfortScore = 9, LocationScore = 9, ValueScore = 9, ReviewDate = DateTime.Now },
                new Review { Id = 9, UserId = 3, HotelId = 4, ReviewComment = "Nice ambiance.", FacilityScore = 8, StaffScore = 8, CleanlinessScore = 8, ComfortScore = 8, LocationScore = 8, ValueScore = 8, ReviewDate = DateTime.Now.AddDays(-4) },
                new Review { Id = 10, UserId = 4, HotelId = 1, ReviewComment = "Exceptional service.", FacilityScore = 10, StaffScore = 10, CleanlinessScore = 10, ComfortScore = 10, LocationScore = 10, ValueScore = 10, ReviewDate = DateTime.Now.AddDays(-4) },
                new Review { Id = 11, UserId = 4, HotelId = 2, ReviewComment = "Pretty decent.", FacilityScore = 7, StaffScore = 7, CleanlinessScore = 7, ComfortScore = 7, LocationScore = 7, ValueScore = 7, ReviewDate = DateTime.Now.AddDays(-6) },
                new Review { Id = 12, UserId = 4, HotelId = 5, ReviewComment = "Great for families.", FacilityScore = 8, StaffScore = 8, CleanlinessScore = 8, ComfortScore = 8, LocationScore = 8, ValueScore = 8, ReviewDate = DateTime.Now },
                new Review { Id = 13, UserId = 5, HotelId = 1, ReviewComment = "Perfect for vacation.", FacilityScore = 9, StaffScore = 9, CleanlinessScore = 9, ComfortScore = 9, LocationScore = 9, ValueScore = 9, ReviewDate = DateTime.Now.AddDays(-5) },
                new Review { Id = 14, UserId = 5, HotelId = 2, ReviewComment = "Good choice.", FacilityScore = 8, StaffScore = 8, CleanlinessScore = 8, ComfortScore = 8, LocationScore = 8, ValueScore = 8, ReviewDate = DateTime.Now.AddDays(-6) },
                new Review { Id = 15, UserId = 5, HotelId = 3, ReviewComment = "Satisfied overall.", FacilityScore = 8, StaffScore = 8, CleanlinessScore = 8, ComfortScore = 8, LocationScore = 8, ValueScore = 8, ReviewDate = DateTime.Now.AddDays(-4) },
                new Review { Id = 16, UserId = 5, HotelId = 4, ReviewComment = "Nice and clean.", FacilityScore = 9, StaffScore = 9, CleanlinessScore = 9, ComfortScore = 9, LocationScore = 9, ValueScore = 9, ReviewDate = DateTime.Now },
                new Review { Id = 17, UserId = 1, HotelId = 1, ReviewComment = "Would come again.", FacilityScore = 9, StaffScore = 9, CleanlinessScore = 9, ComfortScore = 9, LocationScore = 9, ValueScore = 9, ReviewDate = DateTime.Now.AddDays(-5) },
                new Review { Id = 18, UserId = 2, HotelId = 2, ReviewComment = "Enjoyed the stay.", FacilityScore = 8, StaffScore = 8, CleanlinessScore = 8, ComfortScore = 8, LocationScore = 8, ValueScore = 8, ReviewDate = DateTime.Now.AddDays(-4) },
                new Review { Id = 19, UserId = 3, HotelId = 5, ReviewComment = "Convenient location.", FacilityScore = 8, StaffScore = 8, CleanlinessScore = 8, ComfortScore = 8, LocationScore = 9, ValueScore = 8, ReviewDate = DateTime.Now },
                new Review { Id = 20, UserId = 4, HotelId = 2, ReviewComment = "Nice hotel.", FacilityScore = 7, StaffScore = 7, CleanlinessScore = 7, ComfortScore = 7, LocationScore = 7, ValueScore = 7, ReviewDate = DateTime.Now.AddDays(-6) },
                new Review { Id = 21, UserId = 2, HotelId = 3, ReviewComment = "Comfortable stay.", FacilityScore = 8, StaffScore = 8, CleanlinessScore = 8, ComfortScore = 8, LocationScore = 8, ValueScore = 8, ReviewDate = DateTime.Now },
                new Review { Id = 22, UserId = 3, HotelId = 4, ReviewComment = "Good experience.", FacilityScore = 8, StaffScore = 8, CleanlinessScore = 8, ComfortScore = 8, LocationScore = 8, ValueScore = 8, ReviewDate = DateTime.Now.AddDays(-4) },
                new Review { Id = 23, UserId = 1, HotelId = 5, ReviewComment = "Satisfied.", FacilityScore = 8, StaffScore = 8, CleanlinessScore = 8, ComfortScore = 8, LocationScore = 8, ValueScore = 8, ReviewDate = DateTime.Now },
                new Review { Id = 24, UserId = 4, HotelId = 3, ReviewComment = "Pleasant stay.", FacilityScore = 8, StaffScore = 8, CleanlinessScore = 8, ComfortScore = 8, LocationScore = 8, ValueScore = 8, ReviewDate = DateTime.Now.AddDays(-5) },
                new Review { Id = 25, UserId = 3, HotelId = 4, ReviewComment = "Very nice.", FacilityScore = 9, StaffScore = 9, CleanlinessScore = 9, ComfortScore = 9, LocationScore = 9, ValueScore = 9, ReviewDate = DateTime.Now },
                new Review { Id = 26, UserId = 2, HotelId = 5, ReviewComment = "Would recommend.", FacilityScore = 8, StaffScore = 8, CleanlinessScore = 8, ComfortScore = 8, LocationScore = 8, ValueScore = 8, ReviewDate = DateTime.Now.AddDays(-5) },
                new Review { Id = 27, UserId = 1, HotelId = 1, ReviewComment = "Great experience.", FacilityScore = 9, StaffScore = 9, CleanlinessScore = 9, ComfortScore = 9, LocationScore = 9, ValueScore = 9, ReviewDate = DateTime.Now.AddDays(-4) },
                new Review { Id = 28, UserId = 2, HotelId = 2, ReviewComment = "Good place.", FacilityScore = 8, StaffScore = 8, CleanlinessScore = 8, ComfortScore = 8, LocationScore = 8, ValueScore = 8, ReviewDate = DateTime.Now },
                new Review { Id = 29, UserId = 3, HotelId = 3, ReviewComment = "Enjoyed the stay.", FacilityScore = 8, StaffScore = 8, CleanlinessScore = 8, ComfortScore = 8, LocationScore = 8, ValueScore = 8, ReviewDate = DateTime.Now.AddDays(-6) },
                new Review { Id = 30, UserId = 2, HotelId = 4, ReviewComment = "Nice place.", FacilityScore = 8, StaffScore = 8, CleanlinessScore = 8, ComfortScore = 8, LocationScore = 8, ValueScore = 8, ReviewDate = DateTime.Now },
                new Review { Id = 31, UserId = 4, HotelId = 5, ReviewComment = "Good value.", FacilityScore = 8, StaffScore = 8, CleanlinessScore = 8, ComfortScore = 8, LocationScore = 8, ValueScore = 8, ReviewDate = DateTime.Now.AddDays(-5)},
                new Review { Id = 32, UserId = 1, HotelId = 1, ReviewComment = "Nice and clean.", FacilityScore = 9, StaffScore = 9, CleanlinessScore = 9, ComfortScore = 9, LocationScore = 9, ValueScore = 9, ReviewDate = DateTime.Now },
                new Review { Id = 33, UserId = 1, HotelId = 1, ReviewComment = "Great place!", FacilityScore = 8, StaffScore = 9, CleanlinessScore = 7, ComfortScore = 8, LocationScore = 10, ValueScore = 9, ReviewDate = DateTime.Now.AddDays(-4) },
                new Review { Id = 34, UserId = 1, HotelId = 15, ReviewComment = "Good service.", FacilityScore = 7, StaffScore = 8, CleanlinessScore = 7, ComfortScore = 8, LocationScore = 8, ValueScore = 7, ReviewDate = DateTime.Now },
                new Review { Id = 35, UserId = 2, HotelId = 13, ReviewComment = "Amazing experience!", FacilityScore = 10, StaffScore = 10, CleanlinessScore = 10, ComfortScore = 10, LocationScore = 10, ValueScore = 10, ReviewDate = DateTime.Now.AddDays(-6) },
                new Review { Id = 36, UserId = 2, HotelId = 14, ReviewComment = "Very nice place.", FacilityScore = 8, StaffScore = 8, CleanlinessScore = 8, ComfortScore = 8, LocationScore = 8, ValueScore = 8, ReviewDate = DateTime.Now },
                new Review { Id = 37, UserId = 3, HotelId = 9, ReviewComment = "Solid experience.", FacilityScore = 7, StaffScore = 7, CleanlinessScore = 7, ComfortScore = 7, LocationScore = 7, ValueScore = 7, ReviewDate = DateTime.Now.AddDays(-5) },
                new Review { Id = 38, UserId = 3, HotelId = 8, ReviewComment = "Highly recommended.", FacilityScore = 9, StaffScore = 9, CleanlinessScore = 9, ComfortScore = 9, LocationScore = 9, ValueScore = 9, ReviewDate = DateTime.Now.AddDays(-4) },
                new Review { Id = 39, UserId = 4, HotelId = 7, ReviewComment = "Loved it!", FacilityScore = 9, StaffScore = 9, CleanlinessScore = 9, ComfortScore = 9, LocationScore = 9, ValueScore = 9, ReviewDate = DateTime.Now },
                new Review { Id = 40, UserId = 4, HotelId = 8, ReviewComment = "Nice ambiance.", FacilityScore = 8, StaffScore = 8, CleanlinessScore = 8, ComfortScore = 8, LocationScore = 8, ValueScore = 8, ReviewDate = DateTime.Now.AddDays(-5) },
                new Review { Id = 41, UserId = 5, HotelId = 12, ReviewComment = "Perfect for vacation.", FacilityScore = 9, StaffScore = 9, CleanlinessScore = 9, ComfortScore = 9, LocationScore = 9, ValueScore = 9, ReviewDate = DateTime.Now },
                new Review { Id = 42, UserId = 5, HotelId = 11, ReviewComment = "Exceptional service.", FacilityScore = 10, StaffScore = 10, CleanlinessScore = 10, ComfortScore = 10, LocationScore = 10, ValueScore = 10, ReviewDate = DateTime.Now.AddDays(-4) },
                new Review { Id = 43, UserId = 3, HotelId = 10, ReviewComment = "Loved it!", FacilityScore = 9, StaffScore = 9, CleanlinessScore = 7, ComfortScore = 9, LocationScore = 9, ValueScore = 8, ReviewDate = DateTime.Now.AddDays(-8) },
                new Review { Id = 44, UserId = 4, HotelId = 10, ReviewComment = "Good place.", FacilityScore = 7, StaffScore = 7, CleanlinessScore = 8, ComfortScore = 8, LocationScore = 10, ValueScore = 8, ReviewDate = DateTime.Now.AddDays(-3) },
                new Review { Id = 45, UserId = 1, HotelId = 6, ReviewComment = "Pretty decent.", FacilityScore = 7, StaffScore = 8, CleanlinessScore = 8, ComfortScore = 7, LocationScore = 8, ValueScore = 7, ReviewDate = DateTime.Now.AddDays(-2) },
                new Review { Id = 46, UserId = 3, HotelId = 6, ReviewComment = "Would recommend.", FacilityScore = 7, StaffScore = 9, CleanlinessScore = 8, ComfortScore = 8, LocationScore = 7, ValueScore = 8, ReviewDate = DateTime.Now.AddDays(-5) },

            });
        }
    }
}
