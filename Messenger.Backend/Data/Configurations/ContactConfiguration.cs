using Messenger.Backend.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Messenger.Backend.Data.Configurations;

public class ContactConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.ToTable("Contact");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.AddedAt)
            .IsRequired();

        builder.HasOne(c => c.Owner)
            .WithMany(u => u.Contacts)
            .HasForeignKey(c => c.OwnerId);

        builder.HasOne(c => c.ContactUser)
            .WithMany()
            .HasForeignKey(c => c.ContactUserId);

    }
}