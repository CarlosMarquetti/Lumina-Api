using Lumina.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lumina.Infrastructure.Data.Mappings
{
    public class AdminMapping : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Username).IsRequired().HasMaxLength(50);
            builder.Property(a => a.Email).IsRequired().HasMaxLength(100);
            builder.Property(a => a.PasswordHash).IsRequired();
            builder.Property(a => a.CreatedAt).IsRequired();
            builder.Property(a => a.LastLogin);
        }
    }
}