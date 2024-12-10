using Messenger.Backend.Data;
using Messenger.Backend.Entity;
using Messenger.Backend.Repository.Common;
using Messenger.Backend.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Backend.Repository;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(MessengerDbContext context) : base(context) { }

    
    public async Task<User> GetByEmailAsync(string email)
    {
        return await _context.Set<User>().FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User> GetByUsernameAndPasswordHashAsync(string username, string passwordHash)
    {
        return await _context.Set<User>().FirstOrDefaultAsync(u => u.Username == username && u.PasswordHash == passwordHash);
    }
}