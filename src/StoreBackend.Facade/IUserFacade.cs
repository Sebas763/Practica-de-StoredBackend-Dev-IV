using StoreBackend.Dto;

namespace StoreBackend.Facade;

public interface IUserFacade
{
    Task<UserDto> CreateAsync(CreateUserDto user);

    // Agregados para la pantalla de usuarios (listar, ver detalle y eliminar)
    Task<List<UserDto>> GetAllAsync();
    Task<UserDto> GetByIdAsync(Guid userResourceId);
    Task DeleteAsync(Guid userResourceId);
}