using Codeping.Utils;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Codeping.GoLink.Core
{
    public interface IGoLinkService
    {
        Task<Result<string>> ToShortAsync(string longUrl);
        Task<Result<string>> ToLongAsync(string shortId);
        Task<Result> RemoveAsync(string shortId);
        IQueryable<Link> Where(Predicate<Link> predicate);
    }
}
