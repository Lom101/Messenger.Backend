﻿using Microsoft.EntityFrameworkCore;
using Messenger.Backend.Entity;

namespace Messenger.Backend.Data;

public class MessengerDbContext : DbContext
{
    public DbSet<Attachment> Attachments { get; set; }
    public DbSet<Chat> Chats { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<User> Users { get; set; }

    public MessengerDbContext(DbContextOptions<MessengerDbContext> options)
        :base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}