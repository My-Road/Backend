using Microsoft.AspNetCore.Identity.Data;
using MyRoad.API.Identity.RequestsDto;
using MyRoad.Domain.Users;
using Riok.Mapperly.Abstractions;

namespace MyRoad.API.Identity;

[Mapper]
public static partial class IdentityMapper
{
    public static partial User ToDomainUser(this RegisterRequestDto registerRequest);
}