using Lumina.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lumina.Infrastructure.Data.Mappings
{
    public class DesignerMapping : IEntityTypeConfiguration<Designer>
    {
        public void Configure(EntityTypeBuilder<Designer> builder)
        {
            builder.HasKey(d => d.Id);
            builder.Property(d => d.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(d => d.LastName).IsRequired().HasMaxLength(50);
            builder.Property(d => d.Email).IsRequired().HasMaxLength(100);
            builder.Property(d => d.PasswordHash).IsRequired();
            builder.Property(d => d.PhoneNumber).HasMaxLength(20);
            builder.Property(d => d.Address).HasMaxLength(200);
            builder.Property(d => d.City).HasMaxLength(100);
            builder.Property(d => d.State).HasMaxLength(50);
            builder.Property(d => d.ZipCode).HasMaxLength(20);
            builder.Property(d => d.Portfolio).HasMaxLength(500);
            builder.Property(d => d.Specialization).HasMaxLength(100);
            builder.Property(d => d.YearsOfExperience);
            builder.Property(d => d.CreatedAt).IsRequired();
            builder.Property(d => d.LastLogin);
        }
    }
}