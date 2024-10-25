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
            builder.Property(x => x.RoomQuantity).IsRequired();
            builder.Property(x => x.Adult).IsRequired();
            builder.Property(x => x.Children).HasDefaultValue(0);
            builder.Property(x => x.IsPhoneCall).HasDefaultValue(false);
            builder.Property(x => x.IsEmail).HasDefaultValue(false);
            builder.Property(x => x.RoomPrice).HasPrecision(10, 2);
            builder.Property(x => x.RoomId).IsRequired();

            builder.HasData(new List<Book>
            {
                new Book
                {
                    Id = 1,
                    BookComment = "I would like a room with a sea view.",
                    CheckIn = new DateTime(2025, 03, 2),
                    CheckOut = new DateTime(2025, 03, 7),
                    RoomPrice = 1200,
                    Adult = 4,
                    RoomQuantity = 2,
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
                    RoomPrice = 3000,
                    Adult = 2,
                    RoomQuantity = 1,
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
                    RoomPrice = 1800,
                    Adult = 5,
                    RoomQuantity = 2,
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
                    RoomPrice = 1500,
                    Adult = 2,
                    Children = 1,
                    RoomQuantity = 1,
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
                    RoomPrice = 2000,
                    Adult = 4,
                    RoomQuantity = 2,
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
                    RoomPrice = 2200,
                    Adult = 1,
                    Children = 1,
                    RoomQuantity = 1,
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
                    RoomPrice = 2500,
                    Adult = 2,
                    Children = 0,
                    RoomQuantity = 1,
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
                    RoomPrice = 3500,
                    Adult = 2,
                    Children = 1,
                    RoomQuantity = 1,
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
                    RoomPrice = 1400,
                    Adult = 3,
                    Children = 1,
                    RoomQuantity = 2,
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
                    RoomPrice = 1600,
                    Adult = 1,
                    Children = 1,
                    RoomQuantity = 1,
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
                    RoomPrice = 2000,
                    Adult = 4,
                    Children = 1,
                    RoomQuantity = 2,
                    RoomId = 18,
                    UserId = 1,
                    IsPhoneCall = true,
                    IsEmail = true
                },
                new Book
                {
                    Id = 12,
                    BookComment = "Please reserve a room with a balcony.",
                    CheckIn = new DateTime(2025, 03, 1),
                    CheckOut = new DateTime(2025, 03, 5),
                    RoomPrice = 2400,
                    Adult = 3,
                    Children = 1,
                    RoomQuantity = 2,
                    RoomId = 10,
                    UserId = 2,
                    IsPhoneCall = false,
                    IsEmail = true
                },
                new Book
                {
                    Id = 13,
                    BookComment = "I would like a room with a city view.",
                    CheckIn = new DateTime(2025, 03, 3),
                    CheckOut = new DateTime(2025, 03, 7),
                    RoomPrice = 2900,
                    Adult = 1,
                    Children = 0,
                    RoomQuantity = 1,
                    RoomId = 23,
                    UserId = 3,
                    IsPhoneCall = true,
                    IsEmail = false
                },
                new Book
                {
                    Id = 14,
                    BookComment = "Need a room with a Jacuzzi.",
                    CheckIn = new DateTime(2025, 03, 6),
                    CheckOut = new DateTime(2025, 03, 11),
                    RoomPrice = 1500,
                    Adult = 5,
                    Children = 1,
                    RoomQuantity = 2,
                    RoomId = 30,
                    UserId = 4,
                    IsPhoneCall = true,
                    IsEmail = true
                },
                new Book
                {
                    Id = 15,
                    BookComment = "I would like a suite with a Jacuzzi.",
                    CheckIn = new DateTime(2025, 03, 10),
                    CheckOut = new DateTime(2025, 03, 15),
                    RoomPrice = 3500,
                    Adult = 2,
                    Children = 1,
                    RoomQuantity = 1,
                    RoomId = 29,
                    UserId = 5,
                    IsPhoneCall = false,
                    IsEmail = true
                },
                new Book
                {
                    Id = 16,
                    BookComment = "Please arrange for a baby crib in the room.",
                    CheckIn = new DateTime(2025, 03, 12),
                    CheckOut = new DateTime(2025, 03, 18),
                    RoomPrice = 1700,
                    Adult = 1,
                    Children = 1,
                    RoomQuantity = 1,
                    RoomId = 26,
                    UserId = 1,
                    IsPhoneCall = true,
                    IsEmail = false
                },
                new Book
                {
                    Id = 17,
                    BookComment = "I would like a late check-out, if possible.",
                    CheckIn = new DateTime(2025, 03, 15),
                    CheckOut = new DateTime(2025, 03, 20),
                    RoomPrice = 1900,
                    Adult = 2,
                    Children = 1,
                    RoomQuantity = 1,
                    RoomId = 16,
                    UserId = 2,
                    IsPhoneCall = false,
                    IsEmail = true
                },
                new Book
                {
                    Id = 18,
                    BookComment = "Please reserve an adjoining room.",
                    CheckIn = new DateTime(2025, 03, 20),
                    CheckOut = new DateTime(2025, 03, 25),
                    RoomPrice = 2000,
                    Adult = 2,
                    Children = 0,
                    RoomQuantity = 1,
                    RoomId = 28,
                    UserId = 3,
                    IsPhoneCall = true,
                    IsEmail = false
                },
                new Book
                {
                    Id = 19,
                    BookComment = "I would like a room with soundproofing.",
                    CheckIn = new DateTime(2025, 03, 22),
                    CheckOut = new DateTime(2025, 03, 26),
                    RoomPrice = 1500,
                    Adult = 1,
                    Children = 1,
                    RoomQuantity = 1,
                    RoomId = 6,
                    UserId = 4,
                    IsPhoneCall = true,
                    IsEmail = true
                },
                new Book
                {
                    Id = 20,
                    BookComment = "Please provide an extra bed in the room.",
                    CheckIn = new DateTime(2025, 03, 25),
                    CheckOut = new DateTime(2025, 03, 30),
                    RoomPrice = 2500,
                    Adult = 1,
                    Children = 0,
                    RoomQuantity = 1,
                    RoomId = 5,
                    UserId = 5,
                    IsPhoneCall = false,
                    IsEmail = true
                },
                new Book
                {
                    Id = 21,
                    BookComment = "I would like a room with a garden view.",
                    CheckIn = new DateTime(2025, 03, 28),
                    CheckOut = new DateTime(2025, 03, 02),
                    RoomPrice = 1600,
                    Adult = 3,
                    Children = 1,
                    RoomQuantity = 2,
                    RoomId = 14,
                    UserId = 1,
                    IsPhoneCall = true,
                    IsEmail = false
                },
                new Book
                {
                    Id = 22,
                    BookComment = "Please arrange for airport pickup service.",
                    CheckIn = new DateTime(2025, 03, 02),
                    CheckOut = new DateTime(2025, 03, 06),
                    RoomPrice = 1800,
                    Adult = 4,
                    Children = 0,
                    RoomQuantity = 2,
                    RoomId = 24,
                    UserId = 2,
                    IsPhoneCall = false,
                    IsEmail = true
                },
                new Book
                {
                    Id = 23,
                    BookComment = "I prefer a room with modern decor.",
                    CheckIn = new DateTime(2025, 03, 05),
                    CheckOut = new DateTime(2025, 03, 10),
                    RoomPrice = 3200,
                    Adult = 1,
                    Children = 1,
                    RoomQuantity = 1,
                    RoomId = 29,
                    UserId = 3,
                    IsPhoneCall = true,
                    IsEmail = false
                },
                new Book
                {
                    Id = 24,
                    BookComment = "Please reserve a corner room with a view.",
                    CheckIn = new DateTime(2025, 03, 07),
                    CheckOut = new DateTime(2025, 03, 12),
                    RoomPrice = 1300,
                    Adult = 2,
                    Children = 0,
                    RoomQuantity = 1,
                    RoomId = 12,
                    UserId = 4,
                    IsPhoneCall = false,
                    IsEmail = true
                },
                new Book
                {
                    Id = 25,
                    BookComment = "I would like a room with a private terrace.",
                    CheckIn = new DateTime(2025, 03, 10),
                    CheckOut = new DateTime(2025, 03, 15),
                    RoomPrice = 2900,
                    Adult = 2,
                    Children = 1,
                    RoomQuantity = 1,
                    RoomId = 23,
                    UserId = 5,
                    IsPhoneCall = true,
                    IsEmail = false
                },
                new Book
                {
                    Id = 26,
                    BookComment = "Please arrange for a room with a fireplace.",
                    CheckIn = new DateTime(2025, 03, 14),
                    CheckOut = new DateTime(2025, 03, 18),
                    RoomPrice = 3000,
                    Adult = 2,
                    Children = 0,
                    RoomQuantity = 1,
                    RoomId = 25,
                    UserId = 1,
                    IsPhoneCall = false,
                    IsEmail = true
                },
                new Book
                {
                    Id = 27,
                    BookComment = "I prefer a room with vintage furniture.",
                    CheckIn = new DateTime(2025, 03, 17),
                    CheckOut = new DateTime(2025, 03, 22),
                    RoomPrice = 1400,
                    Adult = 4,
                    Children = 0,
                    RoomQuantity = 2,
                    RoomId = 22,
                    UserId = 2,
                    IsPhoneCall = true,
                    IsEmail = true
                },
                new Book
                {
                    Id = 28,
                    BookComment = "Please reserve a suite with a grand piano.",
                    CheckIn = new DateTime(2025, 03, 21),
                    CheckOut = new DateTime(2025, 03, 26),
                    RoomPrice = 4000,
                    Adult = 2,
                    Children = 1,
                    RoomQuantity = 1,
                    RoomId = 21,
                    UserId = 3,
                    IsPhoneCall = true,
                    IsEmail = false
                },
                new Book
                {
                    Id = 29,
                    BookComment = "I would like a room with a hot tub.",
                    CheckIn = new DateTime(2025, 03, 25),
                    CheckOut = new DateTime(2025, 03, 30),
                    RoomPrice = 2000,
                    Adult = 1,
                    Children = 1,
                    RoomQuantity = 1,
                    RoomId = 18,
                    UserId = 4,
                    IsPhoneCall = false,
                    IsEmail = true
                },
                new Book
                {
                    Id = 30,
                    BookComment = "Please arrange for a room with a large desk.",
                    CheckIn = new DateTime(2025, 03, 30),
                    CheckOut = new DateTime(2025, 03, 04),
                    RoomPrice = 2000,
                    Adult = 2,
                    Children = 1,
                    RoomQuantity = 1,
                    RoomId = 8,
                    UserId = 5,
                    IsPhoneCall = true,
                    IsEmail = false
                },
                new Book
                {
                    Id = 31,
                    BookComment = "I would like a room with a minibar.",
                    CheckIn = new DateTime(2025, 03, 03),
                    CheckOut = new DateTime(2025, 03, 08),
                    RoomPrice = 2500,
                    Adult = 2,
                    Children = 0,
                    RoomQuantity = 1,
                    RoomId = 5,
                    UserId = 1,
                    IsPhoneCall = false,
                    IsEmail = true
                },
                new Book
                {
                    Id = 32,
                    BookComment = "Please reserve a room with a soaking tub.",
                    CheckIn = new DateTime(2025, 03, 07),
                    CheckOut = new DateTime(2025, 03, 12),
                    RoomPrice = 1800,
                    Adult = 1,
                    Children = 1,
                    RoomQuantity = 1,
                    RoomId = 13,
                    UserId = 2,
                    IsPhoneCall = true,
                    IsEmail = false
                },
                new Book
                {
                    Id = 33,
                    BookComment = "I prefer a room with a reading nook.",
                    CheckIn = new DateTime(2025, 04, 10),
                    CheckOut = new DateTime(2025, 04, 15),
                    RoomPrice = 2600,
                    Adult = 2,
                    Children = 0,
                    RoomQuantity = 1,
                    RoomId = 31,
                    UserId = 3,
                    IsPhoneCall = false,
                    IsEmail = true
                },
                new Book
                {
                    Id = 34,
                    BookComment = "Please reserve a room with an ocean view.",
                    CheckIn = new DateTime(2025, 04, 14),
                    CheckOut = new DateTime(2025, 04, 19),
                    RoomPrice = 2200,
                    Adult = 3,
                    Children = 1,
                    RoomQuantity = 2,
                    RoomId = 4,
                    UserId = 4,
                    IsPhoneCall = true,
                    IsEmail = true
                },
                 new Book
                {
                    Id = 35,
                    BookComment = "I would like a room with a walk-in closet.",
                    CheckIn = new DateTime(2025, 04, 18),
                    CheckOut = new DateTime(2025, 04, 23),
                    RoomPrice = 2300,
                    Adult = 1,
                    Children = 0,
                    RoomQuantity = 1,
                    RoomId = 7,
                    UserId = 5,
                    IsPhoneCall = true,
                    IsEmail = false
                },
                new Book
                {
                    Id = 36,
                    BookComment = "Please arrange for a room with blackout curtains.",
                    CheckIn = new DateTime(2025, 04, 22),
                    CheckOut = new DateTime(2025, 04, 27),
                    RoomPrice = 1700,
                    Adult = 2,
                    Children = 1,
                    RoomQuantity = 1,
                    RoomId = 15,
                    UserId = 1,
                    IsPhoneCall = false,
                    IsEmail = true
                },
                new Book
                {
                    Id = 37,
                    BookComment = "I prefer a room with an eco-friendly design.",
                    CheckIn = new DateTime(2025, 04, 25),
                    CheckOut = new DateTime(2025, 04, 30),
                    RoomPrice = 2400,
                    Adult = 2,
                    Children = 0,
                    RoomQuantity = 1,
                    RoomId = 9,
                    UserId = 2,
                    IsPhoneCall = true,
                    IsEmail = false
                },
                new Book
                {
                    Id = 38,
                    BookComment = "Please reserve a room with a rain shower.",
                    CheckIn = new DateTime(2025, 04, 29),
                    CheckOut = new DateTime(2025, 04, 04),
                    RoomPrice = 3000,
                    Adult = 2,
                    Children = 0,
                    RoomQuantity = 1,
                    RoomId = 11,
                    UserId = 3,
                    IsPhoneCall = false,
                    IsEmail = true
                },
                new Book
                {
                    Id = 39,
                    BookComment = "I would like a room with a panoramic window.",
                    CheckIn = new DateTime(2025, 03, 03),
                    CheckOut = new DateTime(2025, 03, 08),
                    RoomPrice = 3200,
                    Adult = 2,
                    Children = 1,
                    RoomQuantity = 1,
                    RoomId = 17,
                    UserId = 4,
                    IsPhoneCall = true,
                    IsEmail = false
                },
                new Book
                {
                    Id = 40,
                    BookComment = "Please arrange for a room with high-speed Wi-Fi.",
                    CheckIn = new DateTime(2025, 03, 07),
                    CheckOut = new DateTime(2025, 03, 12),
                    RoomPrice = 2100,
                    Adult = 2,
                    Children = 1,
                    RoomQuantity = 1,
                    RoomId = 20,
                    UserId = 5,
                    IsPhoneCall = false,
                    IsEmail = true
                }

            });
        }
    }
}
