using System;
using Microsoft.EntityFrameworkCore;

namespace PadLabN1.Entities
{
    public class PadLabN1DbContext : DbContext
    {
        public PadLabN1DbContext(DbContextOptions<PadLabN1DbContext> options)
                   : base(options)
        {
            Database.Migrate();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Sub> Subscriptions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sub>()
                .HasKey(s => new { s.SubId, s.SubOnId });
        }
    }
}
