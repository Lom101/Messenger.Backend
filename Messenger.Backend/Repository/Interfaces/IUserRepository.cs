using Messenger.Backend.Entity;
using Messenger.Backend.Repository.Common.Interfaces;

namespace Messenger.Backend.Repository.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User> GetByEmailAsync(string email);
}