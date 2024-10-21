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
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.UserProfileId).IsRequired();
            builder.Property(x => x.FacilityId).IsRequired();

            builder.HasData(AddFasility());
        }

        private List<UserProfileFacility> AddFasility()
        {
            var list = new List<UserProfileFacility>();

            for (int i = 1; i < 36; i++)
            {
                list.Add(new UserProfileFacility { Id = i, FacilityId = i, UserProfileId = 1 });
            }

            return list;
        }
    }
}
