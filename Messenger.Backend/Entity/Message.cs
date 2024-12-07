namespace Messenger.Backend.Entity;

public class Message
{
    public Guid Id { get; set; }
    public Guid ChatId { get; set; }
    public Chat Chat { get; set; }
    public Guid SenderId { get; set; }
    public User Sender { get; set; }
    public string Text { get; set; }
    public ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();
    public DateTime SentAt { get; set; }
    public bool IsRead { get; set; }
}