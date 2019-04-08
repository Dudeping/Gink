using Codeping.Gink.EFCore;
using Microsoft.EntityFrameworkCore;

namespace Codeping.Gink.Web
{
    public class GinkWebContext : GinkDbContext
    {
        public GinkWebContext() : base()
        {
        }

        public GinkWebContext(DbContextOptions options) : base(options)
        {

        }
    }
}
