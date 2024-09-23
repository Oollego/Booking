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
    internal class UserProfileFacilityConfiguration : IEntityTypeConfiguration<UserProfileFacility>
    {
        public void Configure(EntityTypeBuilder<UserProfileFacility> builder)
        {
            builder.ToTable("user_profile_facilities");
            builder.Property(x => x.UserProfileId).IsRequired();
            builder.Property(x => x.FacilityId).IsRequired();
        }
    }
}
