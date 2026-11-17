using StoreBackend.Domain.Entities;


namespace StoreBackend.Infrastructure.Repositories;

public interface IUserRepository
{
    Task<User> CreateAsync(User user);
    Task<bool> HasUserByUsernameAsync(string username);
    Task<bool> HasUserByEmailAsync(string email);
    Task<User?> GetByUsername(string username);

    // Agregados para la pantalla de usuarios (listar, ver detalle y eliminar)
    Task<List<User>> GetAllAsync();
    Task<User?> GetByIdAsync(Guid userResourceId);
    Task DeleteAsync(User user);
}