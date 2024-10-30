using Amazon.S3.Model;
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
    internal class FaqConfiguration: IEntityTypeConfiguration<Faq>
    {
        public void Configure(EntityTypeBuilder<Faq> builder)
        {
            builder.ToTable("faqs");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.HotelId).IsRequired();
            builder.Property(x => x.Question).IsRequired();

            builder.HasData(MakeData());
        }
       
        private List<Faq> MakeData()
        {
            List<FaqType> faqTypeData = new List<FaqType>
            {

                new FaqType { Question = "What is the check-in time?", Answer = "Check-in is from 3 PM." },
                new FaqType { Question = "Is breakfast included?", Answer = "Yes, breakfast is included." },
                new FaqType { Question = "Is there free parking?", Answer = "Yes, we offer free parking." },
                new FaqType { Question = "Do you have a swimming pool?", Answer = "Yes, we have an outdoor pool." },
                new FaqType { Question = "Are pets allowed?", Answer = "Pets are allowed with an additional fee." },
                new FaqType { Question = "Is room service available?", Answer = "Room service is available 24/7." },
                new FaqType { Question = "What time is check-out?", Answer = "Check-out is at 11 AM." },
                new FaqType { Question = "Do rooms have Wi-Fi?", Answer = "Yes, free Wi-Fi is available in all rooms." },
                new FaqType { Question = "Are airport transfers available?", Answer = "Yes, airport transfers can be arranged." },
                new FaqType { Question = "Is there a gym?", Answer = "Yes, the hotel has a fitness center." },
                new FaqType { Question = "Can I request an extra bed?", Answer = "Yes, extra beds are available upon request." },
                new FaqType { Question = "Is late check-out available?", Answer = "Late check-out is available for a fee." },
                new FaqType { Question = "Is breakfast available?", Answer = "Yes, continental breakfast is provided." },
                new FaqType { Question = "Do rooms have an ocean view?", Answer = "Some rooms offer an ocean view." },
                new FaqType { Question = "Are there accessible rooms?", Answer = "Yes, accessible rooms are available." },
                new FaqType { Question = "Can I bring my pet?", Answer = "Only small pets are allowed." },
                new FaqType { Question = "What payment methods are accepted?", Answer = "We accept all major credit cards." },
                new FaqType { Question = "Do you offer laundry services?", Answer = "Yes, laundry services are available." },
                new FaqType { Question = "What is included in the room rate?", Answer = "Rates include breakfast and Wi-Fi." },
                new FaqType { Question = "Is there a business center?", Answer = "Yes, we have a 24/7 business center." },
                new FaqType { Question = "Are there conference rooms available?", Answer = "Yes, conference rooms can be booked." },
                new FaqType { Question = "Is housekeeping daily?", Answer = "Daily housekeeping is provided." },
                new FaqType { Question = "Can I request a non-smoking room?", Answer = "Yes, non-smoking rooms are available." },
                new FaqType { Question = "Do rooms have balconies?", Answer = "Select rooms come with balconies." },
                new FaqType { Question = "Is breakfast buffet style?", Answer = "Yes, we offer a buffet breakfast." },
                new FaqType { Question = "Do you have a shuttle service?", Answer = "Yes, a shuttle is available to nearby areas." },
                new FaqType { Question = "Is there a spa?", Answer = "Yes, our hotel features a full-service spa." },
                new FaqType { Question = "Is there a cancellation policy?", Answer = "Cancellations are allowed up to 24 hours before check-in." },
                new FaqType { Question = "Are there activities for children?", Answer = "Yes, we have a kids club and pool." },
                new FaqType { Question = "Are rooms soundproof?", Answer = "Yes, all rooms are soundproofed." },
            };

            List<Faq> faqs = new List<Faq>();

            int faqId = 1;

            Random rnd = new Random();

            for(int i = 1; i <= 15; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    int type = rnd.Next(0, 29);
                    faqs.Add(new Faq { Id = faqId, HotelId = i, Question = faqTypeData[type].Question, Answer = faqTypeData[type].Answer });
                    faqId++;
                }
            }

            return faqs;
    
        }

    }

    class FaqType
    {
        public string Question { get; set; } = null!;
        public string Answer { get; set;} = null!;
    }
}
