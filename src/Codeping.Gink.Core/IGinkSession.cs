using Codeping.Utils;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Codeping.Gink.Core
{
    public interface IGinkSession
    {
        Task<Result<Link>> AddAsync(Link link);
        Task<Result<Link>> HasAsync(string longUrl);

        Task<Result<Link>> GetAsync(string shortId);
        IQueryable<Link> Get(Predicate<Link> predicate);

        Task<Result<Link>> UpdateAsync(Link link);
        Task<Result<Link>> RemoveAsync(string shortId);
    }
}
