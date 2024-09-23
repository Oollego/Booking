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
    internal class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("books");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.BookComment).HasMaxLength(1500).HasDefaultValue("");
            builder.Property(x => x.IsPhoneCall).HasDefaultValue(false);
            builder.Property(x => x.IsEmail).HasDefaultValue(false);
            builder.Property(x => x.RoomBookPrice).HasPrecision(10, 2);
            builder.Property(x => x.RoomId).IsRequired();

            builder.HasData(new List<Book>
            {
                new Book
                {
                    Id = 1,
                    BookComment = "I would like a room with a sea view.",
                    CheckIn = new DateTime(2025, 03, 2),
                    CheckOut = new DateTime(2025, 03, 7),
                    RoomBookPrice = 1200,
                    RoomId = 1,
                    UserId = 1,
                    IsPhoneCall = true,
                    IsEmail = false
                },
                new Book
                {
                    Id = 2,
                    BookComment = "Please reserve a room on the second floor.",
                    CheckIn = new DateTime(2025, 03, 4),
                    CheckOut = new DateTime(2025, 03, 5),
                    RoomBookPrice = 3000,
                    RoomId = 3,
                    UserId = 2,
                    IsPhoneCall = false,
                    IsEmail = true
                },
                new Book
                {
                    Id = 3,
                    BookComment = "I would like an early check-in.",
                    CheckIn = new DateTime(2025, 03, 1),
                    CheckOut = new DateTime(2025, 03, 6),
                    RoomBookPrice = 1800,
                    RoomId = 2,
                    UserId = 3,
                    IsPhoneCall = true,
                    IsEmail = true
                },
                new Book
                {
                    Id = 4,
                    BookComment = "Please ensure a quiet room.",
                    CheckIn = new DateTime(2025, 03, 8),
                    CheckOut = new DateTime(2025, 03, 12),
                    RoomBookPrice = 1500,
                    RoomId = 6,
                    UserId = 4,
                    IsPhoneCall = false,
                    IsEmail = false
                },
                new Book
                {
                    Id = 5,
                    BookComment = "I would like a room with a mountain view.",
                    CheckIn = new DateTime(2025, 03, 5),
                    CheckOut = new DateTime(2025, 03, 10),
                    RoomBookPrice = 2000,
                    RoomId = 8,
                    UserId = 5,
                    IsPhoneCall = true,
                    IsEmail = false
                },
                new Book
                {
                    Id = 6,
                    BookComment = "Need a room close to the elevator.",
                    CheckIn = new DateTime(2025, 03, 15),
                    CheckOut = new DateTime(2025, 03, 20),
                    RoomBookPrice = 2200,
                    RoomId = 4,
                    UserId = 1,
                    IsPhoneCall = true,
                    IsEmail = false
                },
                new Book
                {
                    Id = 7,
                    BookComment = "Please arrange for extra towels.",
                    CheckIn = new DateTime(2025, 03, 12),
                    CheckOut = new DateTime(2025, 03, 16),
                    RoomBookPrice = 2500,
                    RoomId = 5,
                    UserId = 2,
                    IsPhoneCall = false,
                    IsEmail = true
                },
                new Book
                {
                    Id = 8,
                    BookComment = "I would like a room on a higher floor.",
                    CheckIn = new DateTime(2025, 03, 18),
                    CheckOut = new DateTime(2025, 03, 22),
                    RoomBookPrice = 3500,
                    RoomId = 7,
                    UserId = 3,
                    IsPhoneCall = false,
                    IsEmail = false
                },
                new Book
                {
                    Id = 9,
                    BookComment = "Prefer a room with no carpet.",
                    CheckIn = new DateTime(2025, 03, 20),
                    CheckOut = new DateTime(2025, 03, 25),
                    RoomBookPrice = 1400,
                    RoomId = 22,
                    UserId = 4,
                    IsPhoneCall = true,
                    IsEmail = false
                },
                new Book
                {
                    Id = 10,
                    BookComment = "I would like a room with a river view.",
                    CheckIn = new DateTime(2025, 03, 24),
                    CheckOut = new DateTime(2025, 03, 28),
                    RoomBookPrice = 1600,
                    RoomId = 14,
                    UserId = 5,
                    IsPhoneCall = false,
                    IsEmail = true
                },
                new Book
                {
                    Id = 11,
                    BookComment = "I prefer a room on the ground floor.",
                    CheckIn = new DateTime(2025, 03, 28),
                    CheckOut = new DateTime(2025, 04, 02),
                    RoomBookPrice = 2000,
                    RoomId = 18,
                    UserId = 1,
                    IsPhoneCall = true,
                    IsEmail = true
                },
                new Book
                {
                    Id = 12,
                    BookComment = "Please reserve a room with a balcony.",
                    CheckIn = new DateTime(2025, 04, 1),
                    CheckOut = new DateTime(2025, 04, 5),
                    RoomBookPrice = 2400,
                    RoomId = 10,
                    UserId = 2,
                    IsPhoneCall = false,
                    IsEmail = true
                },
                new Book
                {
                    Id = 13,
                    BookComment = "I would like a room with a city view.",
                    CheckIn = new DateTime(2025, 04, 3),
                    CheckOut = new DateTime(2025, 04, 7),
                    RoomBookPrice = 2900,
                    RoomId = 23,
                    UserId = 3,
                    IsPhoneCall = true,
                    IsEmail = false
                },
                new Book
                {
                    Id = 14,
                    BookComment = "Need a room with a Jacuzzi.",
                    CheckIn = new DateTime(2025, 04, 6),
                    CheckOut = new DateTime(2025, 04, 11),
                    RoomBookPrice = 1500,
                    RoomId = 30,
                    UserId = 4,
                    IsPhoneCall = true,
                    IsEmail = true
                },
                new Book
                {
                    Id = 15,
                    BookComment = "I would like a suite with a Jacuzzi.",
                    CheckIn = new DateTime(2025, 04, 10),
                    CheckOut = new DateTime(2025, 04, 15),
                    RoomBookPrice = 3500,
                    RoomId = 29,
                    UserId = 5,
                    IsPhoneCall = false,
                    IsEmail = true
                },
                new Book
                {
                    Id = 16,
                    BookComment = "Please arrange for a baby crib in the room.",
                    CheckIn = new DateTime(2025, 04, 12),
                    CheckOut = new DateTime(2025, 04, 18),
                    RoomBookPrice = 1700,
                    RoomId = 26,
                    UserId = 1,
                    IsPhoneCall = true,
                    IsEmail = false
                },
                new Book
                {
                    Id = 17,
                    BookComment = "I would like a late check-out, if possible.",
                    CheckIn = new DateTime(2025, 04, 15),
                    CheckOut = new DateTime(2025, 04, 20),
                    RoomBookPrice = 1900,
                    RoomId = 16,
                    UserId = 2,
                    IsPhoneCall = false,
                    IsEmail = true
                },
                new Book
                {
                    Id = 18,
                    BookComment = "Please reserve an adjoining room.",
                    CheckIn = new DateTime(2025, 04, 20),
                    CheckOut = new DateTime(2025, 04, 25),
                    RoomBookPrice = 2000,
                    RoomId = 28,
                    UserId = 3,
                    IsPhoneCall = true,
                    IsEmail = false
                },
                new Book
                {
                    Id = 19,
                    BookComment = "I would like a room with soundproofing.",
                    CheckIn = new DateTime(2025, 04, 22),
                    CheckOut = new DateTime(2025, 04, 26),
                    RoomBookPrice = 1500,
                    RoomId = 6,
                    UserId = 4,
                    IsPhoneCall = true,
                    IsEmail = true
                },
                new Book
                {
                    Id = 20,
                    BookComment = "Please provide an extra bed in the room.",
                    CheckIn = new DateTime(2025, 04, 25),
                    CheckOut = new DateTime(2025, 04, 30),
                    RoomBookPrice = 2500,
                    RoomId = 5,
                    UserId = 5,
                    IsPhoneCall = false,
                    IsEmail = true
                },
                new Book
                {
                    Id = 21,
                    BookComment = "I would like a room with a garden view.",
                    CheckIn = new DateTime(2025, 04, 28),
                    CheckOut = new DateTime(2025, 05, 02),
                    RoomBookPrice = 1600,
                    RoomId = 14,
                    UserId = 1,
                    IsPhoneCall = true,
                    IsEmail = false
                },
                new Book
                {
                    Id = 22,
                    BookComment = "Please arrange for airport pickup service.",
                    CheckIn = new DateTime(2025, 05, 02),
                    CheckOut = new DateTime(2025, 05, 06),
                    RoomBookPrice = 1800,
                    RoomId = 24,
                    UserId = 2,
                    IsPhoneCall = false,
                    IsEmail = true
                },
                new Book
                {
                    Id = 23,
                    BookComment = "I prefer a room with modern decor.",
                    CheckIn = new DateTime(2025, 05, 05),
                    CheckOut = new DateTime(2025, 05, 10),
                    RoomBookPrice = 3200,
                    RoomId = 29,
                    UserId = 3,
                    IsPhoneCall = true,
                    IsEmail = false
                },
                new Book
                {
                    Id = 24,
                    BookComment = "Please reserve a corner room with a view.",
                    CheckIn = new DateTime(2025, 05, 07),
                    CheckOut = new DateTime(2025, 05, 12),
                    RoomBookPrice = 1300,
                    RoomId = 12,
                    UserId = 4,
                    IsPhoneCall = false,
                    IsEmail = true
                },
                new Book
                {
                    Id = 25,
                    BookComment = "I would like a room with a private terrace.",
                    CheckIn = new DateTime(2025, 05, 10),
                    CheckOut = new DateTime(2025, 05, 15),
                    RoomBookPrice = 2900,
                    RoomId = 23,
                    UserId = 5,
                    IsPhoneCall = true,
                    IsEmail = false
                },
                new Book
                {
                    Id = 26,
                    BookComment = "Please arrange for a room with a fireplace.",
                    CheckIn = new DateTime(2025, 05, 14),
                    CheckOut = new DateTime(2025, 05, 18),
                    RoomBookPrice = 3000,
                    RoomId = 25,
                    UserId = 1,
                    IsPhoneCall = false,
                    IsEmail = true
                },
                new Book
                {
                    Id = 27,
                    BookComment = "I prefer a room with vintage furniture.",
                    CheckIn = new DateTime(2025, 05, 17),
                    CheckOut = new DateTime(2025, 05, 22),
                    RoomBookPrice = 1400,
                    RoomId = 22,
                    UserId = 2,
                    IsPhoneCall = true,
                    IsEmail = true
                },
                new Book
                {
                    Id = 28,
                    BookComment = "Please reserve a suite with a grand piano.",
                    CheckIn = new DateTime(2025, 05, 21),
                    CheckOut = new DateTime(2025, 05, 26),
                    RoomBookPrice = 4000,
                    RoomId = 21,
                    UserId = 3,
                    IsPhoneCall = true,
                    IsEmail = false
                },
                new Book
                {
                    Id = 29,
                    BookComment = "I would like a room with a hot tub.",
                    CheckIn = new DateTime(2025, 05, 25),
                    CheckOut = new DateTime(2025, 05, 30),
                    RoomBookPrice = 2000,
                    RoomId = 18,
                    UserId = 4,
                    IsPhoneCall = false,
                    IsEmail = true
                },
                new Book
                {
                    Id = 30,
                    BookComment = "Please arrange for a room with a large desk.",
                    CheckIn = new DateTime(2025, 05, 30),
                    CheckOut = new DateTime(2025, 06, 04),
                    RoomBookPrice = 2000,
                    RoomId = 8,
                    UserId = 5,
                    IsPhoneCall = true,
                    IsEmail = false
                },
                new Book
                {
                    Id = 31,
                    BookComment = "I would like a room with a minibar.",
                    CheckIn = new DateTime(2025, 06, 03),
                    CheckOut = new DateTime(2025, 06, 08),
                    RoomBookPrice = 2500,
                    RoomId = 5,
                    UserId = 1,
                    IsPhoneCall = false,
                    IsEmail = true
                },
                new Book
                {
                    Id = 32,
                    BookComment = "Please reserve a room with a soaking tub.",
                    CheckIn = new DateTime(2025, 06, 07),
                    CheckOut = new DateTime(2025, 06, 12),
                    RoomBookPrice = 1800,
                    RoomId = 13,
                    UserId = 2,
                    IsPhoneCall = true,
                    IsEmail = false
                },
                new Book
                {
                    Id = 33,
                    BookComment = "I prefer a room with a reading nook.",
                    CheckIn = new DateTime(2025, 06, 10),
                    CheckOut = new DateTime(2025, 06, 15),
                    RoomBookPrice = 2600,
                    RoomId = 31,
                    UserId = 3,
                    IsPhoneCall = false,
                    IsEmail = true
                },
                new Book
                {
                    Id = 34,
                    BookComment = "Please reserve a room with an ocean view.",
                    CheckIn = new DateTime(2025, 06, 14),
                    CheckOut = new DateTime(2025, 06, 19),
                    RoomBookPrice = 2200,
                    RoomId = 4,
                    UserId = 4,
                    IsPhoneCall = true,
                    IsEmail = true
                },
                 new Book
                {
                    Id = 35,
                    BookComment = "I would like a room with a walk-in closet.",
                    CheckIn = new DateTime(2025, 06, 18),
                    CheckOut = new DateTime(2025, 06, 23),
                    RoomBookPrice = 2300,
                    RoomId = 7,
                    UserId = 5,
                    IsPhoneCall = true,
                    IsEmail = false
                },
                new Book
                {
                    Id = 36,
                    BookComment = "Please arrange for a room with blackout curtains.",
                    CheckIn = new DateTime(2025, 06, 22),
                    CheckOut = new DateTime(2025, 06, 27),
                    RoomBookPrice = 1700,
                    RoomId = 15,
                    UserId = 1,
                    IsPhoneCall = false,
                    IsEmail = true
                },
                new Book
                {
                    Id = 37,
                    BookComment = "I prefer a room with an eco-friendly design.",
                    CheckIn = new DateTime(2025, 06, 25),
                    CheckOut = new DateTime(2025, 06, 30),
                    RoomBookPrice = 2400,
                    RoomId = 9,
                    UserId = 2,
                    IsPhoneCall = true,
                    IsEmail = false
                },
                new Book
                {
                    Id = 38,
                    BookComment = "Please reserve a room with a rain shower.",
                    CheckIn = new DateTime(2025, 06, 29),
                    CheckOut = new DateTime(2025, 07, 04),
                    RoomBookPrice = 3000,
                    RoomId = 11,
                    UserId = 3,
                    IsPhoneCall = false,
                    IsEmail = true
                },
                new Book
                {
                    Id = 39,
                    BookComment = "I would like a room with a panoramic window.",
                    CheckIn = new DateTime(2025, 07, 03),
                    CheckOut = new DateTime(2025, 07, 08),
                    RoomBookPrice = 3200,
                    RoomId = 17,
                    UserId = 4,
                    IsPhoneCall = true,
                    IsEmail = false
                },
                new Book
                {
                    Id = 40,
                    BookComment = "Please arrange for a room with high-speed Wi-Fi.",
                    CheckIn = new DateTime(2025, 07, 07),
                    CheckOut = new DateTime(2025, 07, 12),
                    RoomBookPrice = 2100,
                    RoomId = 20,
                    UserId = 5,
                    IsPhoneCall = false,
                    IsEmail = true
                }

            });
        }
    }
}
