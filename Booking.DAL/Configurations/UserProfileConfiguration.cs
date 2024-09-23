using Booking.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Booking.DAL.Configurations
{
    internal class UserProfileConfiguration: IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.ToTable("user_profiles");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.UserName).HasMaxLength(100);
            builder.Property(x => x.UserSurname).HasMaxLength(100);
            builder.Property(x => x.Avatar).HasMaxLength(254);
            builder.Property(x => x.UserPhone).HasMaxLength(25);
            builder.Property(x => x.IsUserPet).HasDefaultValue(false);
            builder.Property(x => x.CurrencyCodeId).HasDefaultValue("UAH");
            builder.Property(x => x.UserId).IsRequired();

            builder.HasOne<User>(x => x.User)
              .WithOne(x => x.UserProfile)
              .HasForeignKey<UserProfile>(x => x.UserId);

            builder.HasMany(x => x.Facilities)
             .WithMany(x => x.UserProfiles)
             .UsingEntity<UserProfileFacility>(
             l => l.HasOne<Facility>().WithMany().HasForeignKey(x => x.FacilityId),
             l => l.HasOne<UserProfile>().WithMany().HasForeignKey(x => x.UserProfileId)
             );

            builder.HasData(new List<UserProfile>
        {
            new UserProfile
            {
                Id = 1,
                UserName = "Oleg",
                UserSurname = "Ivanov",
                Avatar = "avatar1.jpg",
                UserPhone = "+1234567890",
                UserId = 1,
                CityId = 189,
                PayMethodId = 1,
                CurrencyCodeId = "USD"
            },
            new UserProfile
            {
                Id = 2,
                UserName = "Alex",
                UserSurname = "Petrov",
                Avatar = "avatar2.jpg",
                UserPhone = "+0987654321",
                UserId = 2,
                CityId = 179,
                PayMethodId = 2,
                CurrencyCodeId = "GBP"

            },
            new UserProfile
            {
                Id = 3,
                UserName = "Dasha",
                UserSurname = "Kuznetsova",
                Avatar = "avatar3.jpg",
                UserPhone = "+1122334455",
                UserId = 3,
                CityId = 76,
            },
            new UserProfile
            {
                Id = 4,
                UserName = "Oleg",
                UserSurname = "Smirnov",
                Avatar = "avatar4.jpg",
                UserPhone = "+2233445566",
                UserId = 4,
                CityId = 70,
                PayMethodId = 3
            },
            new UserProfile
            {
                Id = 5,
                UserName = "Oleg",
                UserSurname = "Sokolov",
                Avatar = "avatar5.jpg",
                UserPhone = "+3344556677",
                UserId = 5,
                CityId = 188,
            }
        });
        }
    }
}
