using System;
using StoreBackend.Api.Models.Requests;
using StoreBackend.Api.Models.Responses;
using StoreBackend.Dto;

namespace StoreBackend.Api.Mappers;

public class UserMapper
{
    public static UserDto ToDto(CreateUserRequestModel model)
    {
        return new UserDto
        {
            UserResourceId = model.UserResourceId!.Value,
            Name = model.Name,
            Email = model.Email
        };
    }

    public static List<UserResponseModel> ToModel(List<UserDto> users)
    {
        return users.Select(u => ToModel(u)).ToList();
    }

    public static UserResponseModel ToModel(UserDto user)
    {
        return new UserResponseModel
        {
            UserId = user.UserId,
            UserResourceId = user.UserResourceId,
            Name = user.Name,
            Email = user.Email
        };
    }
}
