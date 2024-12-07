namespace Messenger.Backend.Entity;


public class Chat
{
    public Guid Id { get; set; }
    public string ChatName { get; set; }
    public bool IsGroupChat { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<User> Users { get; set; } = new List<User>();
    public ICollection<Message> Messages { get; set; } = new List<Message>();
}