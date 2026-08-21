using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server;

public class SnejkaDbContext(DbContextOptions<SnejkaDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(user => user.Id);

            entity.HasIndex(user => user.Username)
                .IsUnique();

            entity.Property(user => user.Username)
                .IsRequired()
                .HasMaxLength(32);

            entity.Property(user => user.PasswordHash)
                .IsRequired();
        });
    }
}
