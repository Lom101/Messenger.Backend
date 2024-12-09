namespace Messenger.Backend.Entity;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Username { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string PasswordHash { get; set; }
    public string ProfilePictureUrl { get; set; }
    public string Status { get; set; }
    public DateTime LastActiveAt { get; set; }
    public ICollection<ChatUser> ChatUsers { get; set; } = new List<ChatUser>(); 
    public ICollection<Contact> Contacts { get; set; } = new List<Contact>();
}