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
    internal class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
    {
        public void Configure(EntityTypeBuilder<UserToken> builder)
        {
            builder.ToTable("user_tokens");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.RefreshToken).IsRequired();
            builder.Property(x => x.RefreshTokenExpiryTime).IsRequired();
            builder.Property(x => x.UserId).IsRequired();

            //builder.HasData(new List<UserToken>() 
            //{
            //    new UserToken()
            //    {
            //        Id = 1,
            //        RefreshToken = "hbwfbbcisbciusiswcib",
            //        RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7),
            //        UserId = 1
            //    }
            
            //});
        }
    }
}
