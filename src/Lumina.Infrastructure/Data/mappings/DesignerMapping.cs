using Lumina.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class DesignerMapping : IEntityTypeConfiguration<Designer>
{
    public void Configure(EntityTypeBuilder<Designer> builder)
    {
        builder.HasKey(d => d.Id);

        builder.Property(d => d.FullName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(d => d.Username)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(d => d.CpfCnpj)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(d => d.DateOfBirth)
            .IsRequired();

        builder.Property(d => d.PhoneNumber)
            .HasMaxLength(20);

        builder.Property(d => d.Email)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(d => d.PasswordHash)
            .IsRequired();

        builder.Property(d => d.Address)
            .HasMaxLength(200);

        builder.Property(d => d.City).HasMaxLength(100);
        builder.Property(d => d.State).HasMaxLength(50);
        builder.Property(d => d.ZipCode).HasMaxLength(20);

        builder.Property(d => d.YearsOfExperience);

        builder.Property(d => d.CreatedAt).IsRequired();
        builder.Property(d => d.LastLogin);

        builder.HasIndex(d => d.Email).IsUnique();
        builder.HasIndex(d => d.Username).IsUnique();
    }
}
