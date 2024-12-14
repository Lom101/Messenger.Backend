namespace Messenger.Backend.Entity;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Username { get; set; }
    public string Email { get; set; } = "12345";
    public string PhoneNumber { get; set; }
    public string PasswordHash { get; set; }
    public string ProfilePictureUrl { get; set; }
    public string Status { get; set; }
    public DateTime LastActiveAt { get; set; } = DateTime.UtcNow;
    public ICollection<ChatUser> ChatUsers { get; set; } = new List<ChatUser>(); 
    public ICollection<Contact> Contacts { get; set; } = new List<Contact>();
}