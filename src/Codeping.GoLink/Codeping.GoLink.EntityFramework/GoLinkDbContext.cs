using System.Runtime.CompilerServices;
using Codeping.GoLink.Core;
using Microsoft.EntityFrameworkCore;

namespace Codeping.GoLink.EFCore
{
    public class GoLinkDbContext : DbContext
    {
        public GoLinkDbContext() : base()
        {
        }

        public GoLinkDbContext(DbContextOptions options) : base(options)
        {
        }

        internal DbSet<Link> Links { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Link>(x =>
            {
                x.HasIndex(x => x.LongUrl);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
