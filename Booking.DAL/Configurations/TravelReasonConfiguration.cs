using Booking.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.DAL.Configurations
{
    internal class TravelReasonConfiguration : IEntityTypeConfiguration<TravelReason>
    {
        public void Configure(EntityTypeBuilder<TravelReason> builder)
        {
            builder.ToTable("travel_reasons");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Reason).IsRequired().HasMaxLength(254);

            builder.HasMany<UserProfile>(x => x.UserProfiles)
               .WithOne(x => x.TravelReason)
               .HasForeignKey(x => x.TravelReasonId)
               .HasPrincipalKey(x => x.Id);

            builder.HasData(
                new TravelReason { Id = 1, Reason = "Business" },
                new TravelReason { Id = 2, Reason = "Vacation" },
                new TravelReason { Id = 3, Reason = "Family Visit" },
                new TravelReason { Id = 4, Reason = "Medical" },
                new TravelReason { Id = 5, Reason = "Education" },
                new TravelReason { Id = 6, Reason = "Work Assignment" },
                new TravelReason { Id = 7, Reason = "Relocation" },
                new TravelReason { Id = 8, Reason = "Other" }
            );
        }
    }
}
