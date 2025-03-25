using Microsoft.EntityFrameworkCore;
using Lumina.Domain.Entities;

namespace Lumina.Infrastructure.Data
{
    public class LuminaDbContext : DbContext
    {
        public LuminaDbContext(DbContextOptions<LuminaDbContext> options) : base(options) { }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Designer> Designers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LuminaDbContext).Assembly);
        }
    }
}