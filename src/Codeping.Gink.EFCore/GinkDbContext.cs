using System.Runtime.CompilerServices;
using Codeping.Gink.Core;
using Microsoft.EntityFrameworkCore;

namespace Codeping.Gink.EFCore
{
    public class GinkDbContext : DbContext
    {
        public GinkDbContext() : base()
        {
        }

        public GinkDbContext(DbContextOptions options) : base(options)
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
