using StoreBackend.Domain.Entities;
using StoreBackend.Dto;

namespace StoreBackend.DomainService;

public interface IUserService
{
    Task<User> CreateAsync(CreateUserDto user);

    // Agregados para la pantalla de usuarios (listar, ver detalle y eliminar)
    Task<List<User>> GetAllAsync();
    Task<User?> GetByIdAsync(Guid userResourceId);
    Task DeleteAsync(Guid userResourceId);
}