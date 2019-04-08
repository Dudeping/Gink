using Codeping.Utils;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Codeping.Gink.Core
{
    internal class GinkService : IGinkService
    {
        private readonly GinkOptions _options;
        private readonly Lazy<IGinkSession> _session;

        public GinkService(Lazy<IGinkSession> session, GinkOptions options)
        {
            _session = session;
            _options = options;
        }

        public async Task<Result<string>> ToLongAsync(string shortId)
        {
            var result = new Result<string>();

            var r = await _session.Value.GetAsync(shortId);

            if (r.Succeeded)
            {
                var link = r.Value;

                if (link != null)
                {
                    link.Total++;

                    await _session.Value.UpdateAsync(link);

                    return result.Ok(link.Id);
                }
            }

            return result.Merge(r);
        }

        public async Task<Result<string>> ToShortAsync(string longUrl)
        {
            var result = new Result<string>();

            var r = await _session.Value.HasAsync(longUrl);

            int retry = _options.RetryNumWhenConfilict;

            while (!r.Succeeded && retry > 0)
            {
                var shortId = RandomEx.GenerateString(6);

                var link = new Link(shortId, longUrl);

                r = await _session.Value.AddAsync(link);

                retry--;
            }

            return r.Succeeded ? result.Ok(r.Value.Id) : result.Merge(r);
        }

        public async Task<Result> RemoveAsync(string shortId)
        {
            return await _session.Value.RemoveAsync(shortId);
        }

        public IQueryable<Link> Where(Predicate<Link> predicate)
        {
            return _session.Value.Get(predicate);
        }
    }
}
