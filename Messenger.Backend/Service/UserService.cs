using System.Security.Cryptography;
using System.Text;
using Messenger.Backend.Entity;
using Messenger.Backend.Repository.Common;
using Messenger.Backend.Repository.Common.Interfaces;
using Messenger.Backend.Service.Interfaces;

namespace Messenger.Backend.Service;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUnitOfWork unitOfWork)
    {
        
    }

    public async Task<User> Authenticate(string username, string password)
    {
        var hashedPassword = HashPassword(password);
        return await _unitOfWork.Users.GetByUsernameAndPasswordHashAsync(username, password);
    }

    public async void Register(string username, string password)
    {
        var hashedPassword = await HashPassword(password);
        var user = new User { Username = username, PasswordHash = hashedPassword };
        await _unitOfWork.Users.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<string> HashPassword(string password)
    {
        // Пример простого хэширования
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
    }
}