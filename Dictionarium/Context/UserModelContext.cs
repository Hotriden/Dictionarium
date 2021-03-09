using Dictionarium.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dictionarium.Context
{
    public class UserModelContext:DbContext
    {
        public UserModelContext(DbContextOptions<UserModelContext> options)
            : base(options)
        {
        }

        public DbSet<Word> Words { get; set; }
        public DbSet<UserModel> UserModels { get; set; }
        public DbSet<WordUserModel> WordUserModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<WordUserModel>()
                .HasKey(bc => new { bc.UserId, bc.WordId });
            modelBuilder.Entity<WordUserModel>()
                .HasOne(bc => bc.Word)
                .WithMany(b => b.WordUserModels)
                .HasForeignKey(c => c.WordId);
            modelBuilder.Entity<WordUserModel>()
                .HasOne(bc => bc.UserModel)
                .WithMany(b => b.WordUserModels)
                .HasForeignKey(c => c.UserId);
        }
    }
}
