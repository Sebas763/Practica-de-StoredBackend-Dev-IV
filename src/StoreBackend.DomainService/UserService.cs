using StoreBackend.Domain.Entities;
using StoreBackend.Dto;
using StoreBackend.Exceptions;
using StoreBackend.Infrastructure.Repositories;

namespace StoreBackend.DomainService;

public class UserService : IUserService
{

    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<User> CreateAsync(CreateUserDto user)
    {
        if (await _userRepository.HasUserByUsernameAsync(user.Username))
        {
            throw new Exceptions.BadRequestResponseException("Username is already taken");
        }
        if (await _userRepository.HasUserByEmailAsync(user.Email))
        {
            throw new Exceptions.BadRequestResponseException("Email is already taken");
        }
        var entity = new User
        {
            UserResourceId = Guid.NewGuid(),
            Name = user.Name,
            Username = user.Username,
            Email = user.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password) //Aca se hashea la contraseña antes de guardarla en la base de datos
        };
        return await _userRepository.CreateAsync(entity);
    }
    // public async Task<User?> GetByUserAndPassword(AuthorizationRequestDto request)
    // {
    //     if (System.Text.Encoding.UTF8.GetByteCount(request.Password) > 72) return null;
    //     var user = await _userRepository.GetByUsername(request.Username);
    //     if (user == null)
    //     {
    //         return null;
    //     }

    //     if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
    //     {
    //         return null;
    //     }

    //     return user;
    // }

    // ---- Agregados para la pantalla de usuarios ----

    public Task<List<User>> GetAllAsync()
    {
        return _userRepository.GetAllAsync();
    }

    public Task<User?> GetByIdAsync(Guid userResourceId)
    {
        return _userRepository.GetByIdAsync(userResourceId);
    }

    public async Task DeleteAsync(Guid userResourceId)
    {
        var user = await _userRepository.GetByIdAsync(userResourceId);

        if (user == null) throw new ResourceNotFoundException();

        await _userRepository.DeleteAsync(user);
    }
}