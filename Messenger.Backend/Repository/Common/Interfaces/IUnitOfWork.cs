using Messenger.Backend.Entity;
using Messenger.Backend.Repository.Interfaces;

namespace Messenger.Backend.Repository.Common.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IUserRepository Users { get; }
    IRepository<Chat> Chats { get; }
    IRepository<Contact> Contacts { get; }
    IRepository<Message> Messages { get; }
    IRepository<Attachment> Attachments { get; }
    Task<int> SaveChangesAsync();
}