using Booking.Domain.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.DAL.Configurations
{
    internal class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("user_rols");
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.RoleId).IsRequired();

            builder.HasData(new List<UserRole>
            {
                new UserRole {UserId = 1, RoleId = 3},
                new UserRole {UserId = 2, RoleId = 2},
                new UserRole {UserId = 3, RoleId = 2},
                new UserRole {UserId = 4, RoleId = 2},
                new UserRole {UserId = 5, RoleId = 2}
            });

        }
    }
}
