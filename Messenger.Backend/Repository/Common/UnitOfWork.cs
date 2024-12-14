using Messenger.Backend.Data;
using Messenger.Backend.Entity;
using Messenger.Backend.Repository.Common.Interfaces;
using Messenger.Backend.Repository.Interfaces;

namespace Messenger.Backend.Repository.Common;

public class UnitOfWork : IUnitOfWork
{
    private readonly MessengerDbContext _context;
    private IUserRepository _users;
    private IRepository<Chat> _chats;
    private IRepository<Contact> _contacts;
    private IRepository<Message> _messages;
    private IRepository<Attachment> _attachments;

    public UnitOfWork(MessengerDbContext context)
    {
        _context = context;
    }

    public IUserRepository Users => _users ??= new UserRepository(_context);
    public IRepository<Chat> Chats => _chats ??= new Repository<Chat>(_context);
    public IRepository<Contact> Contacts => _contacts ??= new Repository<Contact>(_context);
    public IRepository<Message> Messages => _messages ??= new Repository<Message>(_context);
    public IRepository<Attachment> Attachments => _attachments ??= new Repository<Attachment>(_context);

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

}