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
        _unitOfWork = unitOfWork;
    }

    public async Task<User> Authenticate(string username, string password)
    {
        var hashedPassword = await HashPassword(password);
        return await _unitOfWork.Users.GetByUsernameAndPasswordHashAsync(username, hashedPassword);
    }

    public async Task<bool> Register(string username, string password)
    {
        // var hashedPassword = await HashPassword(password);
        // var user = new User { Username = username, PasswordHash = hashedPassword };
        // await _unitOfWork.Users.AddAsync(user);
        // await _unitOfWork.SaveChangesAsync();
        
        // Проверка на существование пользователя с таким именем
        var existingUser = await _unitOfWork.Users.GetByUsernameAsync(username);
        if (existingUser != null)
        {
            // Пользователь с таким именем уже существует
            return false;
        }
        
        // Хэширование пароля
        var hashedPassword = await HashPassword(password);

        // Создание нового пользователя
        var user = new User { Username = username, PasswordHash = hashedPassword };

        // Добавление пользователя в базу данных
        await _unitOfWork.Users.AddAsync(user);

        // Сохранение изменений
        await _unitOfWork.SaveChangesAsync();

        return true;  // Возвращаем успех регистрации
    }

    public async Task<string> HashPassword(string password)
    {
        // Пример простого хэширования
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
    }
}