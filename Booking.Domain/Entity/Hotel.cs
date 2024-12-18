﻿using Booking.Domain.Interfaces;

namespace Booking.Domain.Entity
{
    public class Hotel
    {
        public long Id { get; set; }
        public string HotelName { get; set; } = null!;
        public string HotelAddress { get; set; } = null!;
        public string HotelPhone { get; set; } = null!;
        public string HotelImage { get; set; } = null!;
        public string CityGuide { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Stars { get; set; }
        public bool IsPet { get; set; }
        public long OwnerProfileId { get; set; }
        public OwnerProfile OwnerProfile { get; set; } = null!;
        public long CityId { get; set; }
        public City City { get; set; } = null!;
        public long HotelTypeId { get; set; }
        public HotelType HotelType { get; set; } = null!;
        public long HotelChainId { get; set; }
        public HotelChain HotelChain { get; set; } = null!;
        public List<Room> Rooms { get; set; } = null!;
        public List<HotelInfoCell> HotelInfoCells { get; set; } = null!;
        public List<NearObject> NearObjects { get; set; } = null!;
        public List<NearPlace> NearPlaces { get; set; } = null!;
        public List<Faq> Faqs { get; set; } = null!;
        public List<Review> Reviews { get; set; } = null!;
        public List<Facility> Facilities { get; set; } = null!;
        public List<HotelLabelType> HotelLabelTypes { get; set; } = null!;
        public HotelData HotelData { get; set; } = null!;
    }
}
