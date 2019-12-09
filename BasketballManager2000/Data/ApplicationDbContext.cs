using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BasketballManager2000.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Member>()
                .HasOne(o => o.User)
                .WithOne();

            builder.Entity<Game>()
                .HasOne(o => o.PaidByMember)
                .WithMany(o => o.PaidForGames)
                .HasForeignKey(o => o.PaidByMemberId);
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<Member> Members { get; set; }
    }
}
