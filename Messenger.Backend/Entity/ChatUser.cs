﻿namespace Messenger.Backend.Entity;

public class ChatUser
{
    public Guid ChatId { get; set; }
    public Chat Chat { get; set; }
    
    public Guid UserId { get; set; }
    public User User { get; set; }

    public DateTime JoinedAt { get; set; }
}
