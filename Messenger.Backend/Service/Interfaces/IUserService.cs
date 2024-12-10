using Messenger.Backend.Entity;

namespace Messenger.Backend.Service.Interfaces;

public interface IUserService
{
    Task<User> Authenticate(string username, string password);
    void Register(string username, string password);
    Task<string> HashPassword(string password);
}