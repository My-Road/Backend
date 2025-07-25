﻿using MyRoad.API.Users.RequestDto;
using MyRoad.API.Users.ResponseDto;
using MyRoad.Domain.Users;
using Riok.Mapperly.Abstractions;

namespace MyRoad.API.Users
{
    [Mapper]
    public static partial class UserMapper
    {
        public static partial User ToDomainUser(this UpdateUserProfileDto dto);
        public static partial UserDto ToDomainUser(this User dto);
    }
}
