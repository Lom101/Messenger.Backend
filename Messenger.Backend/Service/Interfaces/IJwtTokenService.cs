using Messenger.Backend.Entity;

namespace Messenger.Backend.Service.Interfaces;

public interface IJwtTokenService
{
    string GenerateToken(User user);
}