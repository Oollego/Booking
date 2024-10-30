using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Enum
{
    public enum ErrorCodes
    {
        InternalServerError = 0,
        StorageServerError = 1,
        AuthenticationRequired = 2,

        UserNotFound = 11,
        UserAlreadyExists = 12,
        UserUnauthorizedAccess = 13,
        UserAlreadyExistsThisRole = 14,
        UserWasNotCreated = 15,

        PasswordNotEqualsPasswordConfirm = 21,
        PasswordIsWrong = 22,
        EmailIsNotCorrect = 23,
        RegistrationCodeNotFound = 24,
        UserIsNotMatched = 25,
        InvalidParameters = 26,

        RoleAlreadyExists = 31,
        RoleNotFound = 32,
        
        HotelNotFound = 41,
        HotelAlreadyExists = 42,

        ReviewNotFound = 51,
        ReviewAlreadyExists = 52,
        InvalidScoreParametr = 53,

        FacilityNotFound = 61,
        FacilityAlreadyExists = 62,
        SomeOfFacilitiesNotFound = 63,

        InfoCellNotFound = 71,

        CountryNotFound = 81,
        CountryAlreadyExists = 82,

        CityNotFound = 91,
        CityAlreadyExists = 92,

        RoomsNotFound = 101,
        RoomNotFound = 102,
        RoomAlreadyExists = 103,

        NearPlaceNotFound = 111,

        ReasonAlreadyExists = 121,
        ReasonNotFound = 122,

        TopicAlreadyExists = 131,
        TopicNotFound = 132,

        CurrencyAlreadyExists = 141,
        CurrencyNotFound = 142,

        CardTypeAlreadyExists = 151,
        CardTypeNotFound = 152,

        PayMethodAlreadyExists = 161,
        PayMethodNotFound = 162,

        UserProfileAlreadyExists = 171,
        UserProfileNotFound = 172,

        FacilityGroupAlreadyExists = 181,
        FacilityGroupNotFound = 182,

        BookingAlreadyExists = 191,
        BookingNotFound = 192,
        NoAvailableRooms = 193,

        FaqAlreadyExists = 201,
        FaqNotFound = 202
    }
}
