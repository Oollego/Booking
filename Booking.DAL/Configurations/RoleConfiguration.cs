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
    internal class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("roles");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.RoleName).IsRequired().HasMaxLength(50);
            builder.HasData(new List<Role>()
            {
                new Role()
                {
                    Id = 1,
                    RoleName = "User"
                },
                new Role()
                {
                    Id = 2,
                    RoleName = "Admin"
                },
                new Role()
                {
                    Id = 3,
                    RoleName = "Moderator"
                }
            });
        }
    }
}
