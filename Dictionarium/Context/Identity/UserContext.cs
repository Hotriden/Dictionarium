using Dictionarium.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dictionarium.Context
{
    public class UserContext : IdentityDbContext<IdentityUser>
    {
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
        }

        public DbSet<IdentityUser> IdentityUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityUser>()
                .ToTable("User");
            modelBuilder.Entity<IdentityUser>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<IdentityUser>()
                .Property(u => u.PasswordHash)
                .HasMaxLength(500);
            modelBuilder.Entity<IdentityUser>()
                .Property(u => u.PhoneNumber)
                .HasMaxLength(50);
        }
    }
}
