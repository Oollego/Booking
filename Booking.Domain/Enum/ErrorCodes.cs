﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Enum
{
    public enum ErrorCodes
    {
        InternalServerError = 0,

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

        FasilityNotFound = 61,

        InfoCellNotFound = 71,

        CountryNotFound = 81,
        CountryAlreadyExists = 82,

        CityNotFound = 91,
        CityAlreadyExists = 92,

        RoomsNotFound = 101,
        RoomNotFound = 102,
        RoomAlreadyExists = 103

    }
}