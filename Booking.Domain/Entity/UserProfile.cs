﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;


namespace Booking.Domain.Entity
{
    public class UserProfile
    {
        public long Id { get; set; }
        public string UserName { get; set; } = null!;
        public string UserSurname { get; set; } = null!;
        public string? Avatar { get; set; } = null!;
        public string? UserPhone { get; set; } = null!;
        public DateTime? DateOfBirth { get; set; }
        public bool? IsUserPet { get; set; } = null!;
        public long UserId { get; set; }
        public User User {  get; set; } = null!;
        public string? CurrencyCodeId { get; set; } = null!;
        public Currency? Currency { get; set; } = null!;
        public long? TravelReasonId {  get; set; }
        public TravelReason? TravelReason { get; set; } = null!;
        public long? CityId { get; set; }
        public City? City { get; set; } = null!;
        public List<UserProfileFacility>? UserProfileFacilities { get; set; } = null!;
        public List<UserProfileTopic>? UserProfileTopics { get; set; } = null!;
        public List<PayMethod>? PayMethods { get; set; } = null!;
    }
}
