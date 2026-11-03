using StoreBackend.DomainService;
using StoreBackend.Dto;
using StoreBackend.Exceptions;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public Task<List<User>> GetAllAsync()
    {
        return _userRepository.GetAllAsync();
    }

    public Task<User?> GetByIdAsync(Guid userId)
    {
        return _userRepository.GetByIdAsync(userId);
    }

    public Task<User> AddAsync(UserDto user)
    {
        var userEntity = new User
        {
            UserResourceId = user.UserResourceId,
            Name = user.Name,
            Email = user.Email
        };

        return _userRepository.AddAsync(userEntity);
    }

    public async Task DeleteAsync(Guid userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);

        if (user == null) throw new ResourceNotFoundException();

        await _userRepository.DeleteAsync(user);
    }
}
