using System;
using System.Linq;
using StoreBackend.Dto;

namespace StoreBackend.Facade.Mappers
{
    public class UserMapper
    {
        public static List<UserDto> ToDto(List<User> users)
        {
            return users.Select(u => ToDto(u)).ToList();
        }

        public static UserDto ToDto(User user)
        {
            return new UserDto
            {
                UserId = user.UserId,
                UserResourceId = user.UserResourceId,
                Name = user.Name,
                Email = user.Email
            };
        }
    }
}
