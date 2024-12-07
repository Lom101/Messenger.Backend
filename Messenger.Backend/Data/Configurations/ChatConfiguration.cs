using Messenger.Backend.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Messenger.Backend.Data.Configurations;

public class ChatConfiguration : IEntityTypeConfiguration<Chat>
{
    public void Configure(EntityTypeBuilder<Chat> builder)
    {
        builder.ToTable("Chat");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.ChatName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.IsGroupChat)
            .IsRequired();

        builder.Property(c => c.CreatedAt)
            .IsRequired();
        
        builder.HasMany(c => c.ChatUsers)  
            .WithOne(cu => cu.Chat)
            .HasForeignKey(cu => cu.ChatId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.Messages)
            .WithOne(m => m.Chat)
            .HasForeignKey(m => m.ChatId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}