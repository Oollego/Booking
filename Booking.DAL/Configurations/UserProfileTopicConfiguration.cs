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
    internal class UserProfileTopicConfiguration : IEntityTypeConfiguration<UserProfileTopic>
    {
        public void Configure(EntityTypeBuilder<UserProfileTopic> builder)
        {
            builder.ToTable("user_topics");

           
            builder.HasKey(x => x.Id);
            builder.Property(x => x.UserProfileId).IsRequired();
            builder.Property(x => x.TopicId).IsRequired();

            //builder.HasOne<UserProfile>()
            //   .WithMany(up => up.UserProfileTopics)
            //   .HasForeignKey(x => x.UserProfileId)
            //   .HasPrincipalKey(up => up.Id)
            //   .OnDelete(DeleteBehavior.Cascade);

            //builder.HasOne<Topic>()
            //       .WithMany(t => t.UserProfileTopics)
            //       .HasForeignKey(x => x.TopicId)
            //       .HasPrincipalKey(x => x.Id)
            //       .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
