namespace Messenger.Backend.Entity;

public class Attachment
{
    public Guid Id { get; set; }
    public Guid MessageId { get; set; }
    public Message Message { get; set; }
    public string FileName { get; set; }
    public string FileUrl { get; set; }
    public string FileType { get; set; }
    public DateTime UploadedAt { get; set; }
}
