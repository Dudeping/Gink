using Codeping.Utils;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Codeping.Gink.Core
{
    internal class GinkService : IGinkService
    {
        private readonly IGinkSession _session;

        public GinkService(IGinkSession session)
        {
            _session = session;
        }

        public async Task<Result<string>> ToLongAsync(string shortId)
        {
            var result = new Result<string>();

            var r = await _session.GetAsync(shortId);

            if (r.Succeeded)
            {
                var link = r.Value;

                if (link != null)
                {
                    link.Total++;

                    await _session.UpdateAsync(link);

                    return result.Ok(link.Id);
                }
            }

            return result.Merge(r);
        }

        public async Task<Result<string>> ToShortAsync(string longUrl)
        {
            var result = new Result<string>();

            var r = await _session.HasAsync(longUrl);

            int retry = 10;

            while (!r.Succeeded && retry > 0)
            {
                var shortId = RandomEx.GenerateString(6);

                var link = new Link { Id = shortId, LongUrl = longUrl };

                r = await _session.AddAsync(link);

                retry--;
            }

            return r.Succeeded ? result.Ok(r.Value.Id) : result.Merge(r);
        }

        public async Task<Result> RemoveAsync(string shortId)
        {
            return await _session.RemoveAsync(shortId);
        }

        public IQueryable<Link> Where(Predicate<Link> predicate)
        {
            return _session.Get(predicate);
        }
    }
}
