using Microsoft.EntityFrameworkCore;
using Booking.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MySql.EntityFrameworkCore.Extensions;

namespace Booking.DAL.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.UserEmail).HasMaxLength(254);
            builder.Property(x => x.PasswordSalt).HasMaxLength(128);
            builder.Property(x => x.PasswordDk).HasMaxLength(128);
            builder.Property(x => x.UpdatedBy).HasDefaultValue(0);

            builder.HasMany(x => x.Roles)
                .WithMany(x => x.Users)
                .UsingEntity<UserRole>(
                l => l.HasOne<Role>().WithMany().HasForeignKey(x => x.RoleId),
                l => l.HasOne<User>().WithMany().HasForeignKey(x => x.UserId)
                );
            builder.HasMany<Book>(x => x.Books)
               .WithOne(x => x.User)
               .HasForeignKey(x => x.UserId)
               .HasPrincipalKey(x => x.Id);
            builder.HasMany<Review>(x => x.Reviews)
               .WithOne(x => x.User)
               .HasForeignKey(x => x.UserId)
               .HasPrincipalKey(x => x.Id);

            builder.HasData(new List<User> 
            {
                new User
                {
                    Id = 1,
                    UserEmail = "oleg@com",
                    PasswordDk = "0D1285F72551DCB2F40D715CC0F7703C",
                    PasswordSalt = "67B8E358D171BEFC2A23A7308AB84A7C",
                    RegisteredAt = DateTime.Now,
                },
                 new User
                {
                    Id = 2,
                    UserEmail = "alex@com",
                    PasswordDk = "0D1285F72551DCB2F40D715CC0F7703C",
                    PasswordSalt = "67B8E358D171BEFC2A23A7308AB84A7C",
                    RegisteredAt = DateTime.Now,
                },
                 new User
                {
                    Id = 3,
                    UserEmail = "dasha@com",
                    PasswordDk = "0D1285F72551DCB2F40D715CC0F7703C",
                    PasswordSalt = "67B8E358D171BEFC2A23A7308AB84A7C",
                    RegisteredAt = DateTime.Now,
                },
                 new User
                {
                    Id = 4,
                    UserEmail = "oleg12@com",
                    PasswordDk = "0D1285F72551DCB2F40D715CC0F7703C",
                    PasswordSalt = "67B8E358D171BEFC2A23A7308AB84A7C",
                    RegisteredAt = DateTime.Now,
                },
                 new User
                {
                    Id = 5,
                    UserEmail = "oleg123@com",
                    PasswordDk = "0D1285F72551DCB2F40D715CC0F7703C",
                    PasswordSalt = "67B8E358D171BEFC2A23A7308AB84A7C",
                    RegisteredAt = DateTime.Now,
                }

            });
        }
    }
}
