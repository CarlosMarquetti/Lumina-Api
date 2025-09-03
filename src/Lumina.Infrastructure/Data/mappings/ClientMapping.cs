using Lumina.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lumina.Infrastructure.Data.Mappings
{
    public class ClientMapping : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Username)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.FullName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.PasswordHash)
                .IsRequired();

            builder.Property(c => c.CpfCnpj)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(c => c.PhoneNumber)
                .HasMaxLength(20);

            builder.Property(c => c.CreatedAt)
                .IsRequired();

            builder.Property(c => c.LastLogin);

            builder.HasIndex(c => c.Email).IsUnique();
            builder.HasIndex(c => c.Username).IsUnique();

        }
    }
}