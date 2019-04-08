using Codeping.Gink.Core;
using Codeping.Utils;
using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Codeping.Gink.EFCore
{
    internal class EFCoreSqlServerGinkSession : IGinkSession
    {
        private readonly ILogger _logger;
        private readonly GinkDbContext _context;

        public EFCoreSqlServerGinkSession(IServiceProvider provider)
        {
            _logger = provider.GetService<ILogger<EFCoreSqlServerGinkSession>>();

            var options = provider.GetService<DbContextOptions>();

            if (options != null && !options.IsFrozen && options.ContextType != null)
            {
                _context = provider.GetService(options.ContextType).Cast<GinkDbContext>();
            }

            if (_context == null)
            {
                _logger.LogError("找不到 GoLinkDbContext 派生的数据库上下文!!!");
            }
        }

        public async Task<Result<Link>> AddAsync(Link link)
        {
            var result = new Result<Link>();

            try
            {
                var entry = await _context.Links.AddAsync(link);

                await _context.SaveChangesAsync();

                return result.Ok(entry.Entity);
            }
            catch (Exception ex)
            {
                return result.Fail(ex);
            }
        }

        public async Task<Result<Link>> GetAsync(string shortId)
        {
            var result = new Result<Link>();

            try
            {
                var entry = await _context.Links.FindAsync(shortId);

                if (entry == null)
                {
                    return result.Fail("找不到该短链接!");
                }

                return result.Ok(entry);
            }
            catch (Exception ex)
            {
                return result.Fail(ex);
            }
        }

        public IQueryable<Link> Get(Predicate<Link> predicate)
        {
            return _context.Links.Where(x => predicate(x));
        }

        public async Task<Result<Link>> HasAsync(string longUrl)
        {
            var result = new Result<Link>();

            try
            {
                var entry = await _context.Links.FirstOrDefaultAsync(
                    x => x.LongUrl.Equals(longUrl, StringComparison.OrdinalIgnoreCase));

                if (entry == null)
                {
                    return result.Fail("找不到该短链接!");
                }

                return result.Ok(entry);
            }
            catch (Exception ex)
            {
                return result.Fail(ex);
            }
        }

        public async Task<Result<Link>> RemoveAsync(string shortId)
        {
            var result = new Result<Link>();

            try
            {
                var entry = await _context.Links.FindAsync(shortId);

                if (entry == null)
                {
                    return result.Fail("找不到该短链接!");
                }

                _context.Links.Remove(entry);

                await _context.SaveChangesAsync();

                return result.Ok(entry);
            }
            catch (Exception ex)
            {
                return result.Fail(ex);
            }
        }

        public async Task<Result<Link>> UpdateAsync(Link link)
        {
            var result = new Result<Link>();

            try
            {
                var entry = await _context.Links.FindAsync(link);

                if (entry == null)
                {
                    return result.Fail("找不到该短链接!");
                }

                entry.Total = link.Total;

                _context.Links.Update(entry);

                await _context.SaveChangesAsync();

                return result.Ok(entry);
            }
            catch (Exception ex)
            {
                return result.Fail(ex);
            }
        }
    }
}
