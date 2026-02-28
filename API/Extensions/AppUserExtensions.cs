using System;
using API.DTOs;
using API.Entities;
using API.Interfaces;

namespace API.Extensions;

public static class AppUserExtensions
{
public static UserDto ToDto(this AppUser user,ITokenService tokenService)
{
    
    return new UserDto
    {
        ID=user.Id,
        DisplayName=user.DisplayName,
        Email=user.Email,
        Token=tokenService.CreateToken(user)
       
    };
}
}