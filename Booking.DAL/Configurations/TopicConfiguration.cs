using Booking.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.DAL.Configurations
{
    internal class TopicConfiguration : IEntityTypeConfiguration<Topic>
    {
        public void Configure(EntityTypeBuilder<Topic> builder)
        {
            builder.ToTable("topics");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.TopicTitel).HasMaxLength(254).IsRequired();
            builder.Property(x => x.TopicText).HasMaxLength(1500).HasDefaultValue("");
            builder.Property(x => x.TopicPhoto).HasMaxLength(254).HasDefaultValue("");

            //builder.HasMany<UserProfile>(x => x.UserProfiles)
            //   .WithMany(x => x.Topics)
            //   .UsingEntity(x => x.ToTable("UsersTopics"));
            builder.HasMany(x => x.UserProfiles)
            .WithMany(x => x.Topics)
            .UsingEntity<UserProfileTopic>(
            l => l.HasOne<UserProfile>().WithMany().HasForeignKey(x => x.UserProfileId),
            l => l.HasOne<Topic>().WithMany().HasForeignKey(x => x.TopicId)
            );

            builder.HasData( new List<Topic>
            {
                new Topic()
                {
                    Id = 1,
                    TopicTitel = "Seasonal offers",
                    TopicText = "Discover the finest offers for every season. Every season has its own uniqueness, and we are here to assist you in finding the most exceptional deals",
                    TopicPhoto = "2b03bec6abec3c61cab333df168d2505.jpg"
                },
                new Topic()
                {
                    Id = 2,
                    TopicTitel = "Favorite cities",
                    TopicText = "We curate a collection of top-rated hotels in the cities you frequent the most, so you don't have to spend time searching for great deals. Let us take care of it for you!",
                    TopicPhoto = "96553d3f240dce709b128b5c98cfba21.jpg"
                },
                new Topic()
                {
                    Id = 3,
                    TopicTitel = "Across the world",
                    TopicText = "Are you a frequent traveler across the world? Let us help you find the best international deals! Enjoy your vacation at a beautiful resort without breaking the bank.",
                    TopicPhoto = "d475159dac94c27664a92ebb2db692e5.jpg"
                },
                new Topic()
                {
                    Id = 4,
                    TopicTitel = "Affordable travel",
                    TopicText = "Looking for affordable travel options? Let us find the best budget deals for you in your preferred country. You will be amazed at how easy it is to travel comfortably now!",
                    TopicPhoto = "2ddd638db07764dcd5c0d0918f1e5b72.jpg"
                }
            });
        }
    }
}
