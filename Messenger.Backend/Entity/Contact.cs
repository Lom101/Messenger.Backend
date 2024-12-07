namespace Messenger.Backend.Entity;

public class Contact
{
    public Guid Id { get; set; }
    public Guid OwnerId { get; set; }
    public User Owner { get; set; }
    public Guid ContactUserId { get; set; }
    public User ContactUser { get; set; }
    public DateTime AddedAt { get; set; }
    public bool IsBlocked { get; set; }
}