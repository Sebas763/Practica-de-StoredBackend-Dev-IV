using StoreBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace StoreBackend.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<User> CreateAsync(User user)
    {
        _context.Users.Add(user);
        return user;

    }

    public async Task<bool> HasUserByUsernameAsync(string username)
    {
        return await _context.Users.AnyAsync(u => u.Username == username);
    }


    public async Task<bool> HasUserByEmailAsync(string email)
    {
        return await _context.Users.AnyAsync(u => u.Email == email);
    }
    public Task<User?> GetByUsername(string username) => _context.Users.FirstOrDefaultAsync(u => u.Username == username.ToUpperInvariant());

    // ---- Agregados para la pantalla de usuarios ----

    public async Task<List<User>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User?> GetByIdAsync(Guid userResourceId)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.UserResourceId == userResourceId);
    }

    public Task DeleteAsync(User user)
    {
        _context.Users.Remove(user);
        return Task.CompletedTask;
    }
}