using Codeping.Utils;
using System;
using System.Collections.Concurrent;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Codeping.Gink.Core
{
    internal class DefaultGinkSession : IGinkSession
    {
        private readonly ConcurrentDictionary<string, Link> _longs;
        private readonly ConcurrentDictionary<string, Link> _shorts;

        public DefaultGinkSession(ILogger<DefaultGinkSession> logger)
        {
            _longs = new ConcurrentDictionary<string, Link>(StringComparer.OrdinalIgnoreCase);
            _shorts = new ConcurrentDictionary<string, Link>(StringComparer.OrdinalIgnoreCase);

            logger.LogWarning("正在使用默认的 GoLinkSession 实现，数据不能持久化!!!");
            logger.LogWarning("正在使用默认的 GoLinkSession 实现，数据不能持久化!!!");
            logger.LogWarning("正在使用默认的 GoLinkSession 实现，数据不能持久化!!!");
        }

        public async Task<Result<Link>> AddAsync(Link link)
        {
            var result = new Result<Link>();

            await Task.Yield();

            if (_shorts.ContainsKey(link.Id))
            {
                return result.Fail("短地址已存在!");
            }

            _shorts.AddOrUpdate(link.Id, link, (x, y) => link);
            _longs.AddOrUpdate(link.LongUrl, link, (x, y) => link);

            return result.Ok(link);
        }

        public IQueryable<Link> Get(Predicate<Link> predicate)
        {
            return _shorts.Values.AsQueryable().Where(x => predicate(x));
        }

        public async Task<Result<Link>> GetAsync(string shortId)
        {
            var result = new Result<Link>();

            await Task.Yield();

            if (_shorts.TryGetValue(shortId, out Link link))
            {
                return result.Ok(link);
            }

            return result.Fail("短地址不存在!");
        }

        public IQueryable<Link> GetAsync(Predicate<Link> predicate)
        {
            return _shorts.Values.AsQueryable().Where(x => predicate(x));
        }

        public async Task<Result<Link>> HasAsync(string longUrl)
        {
            var result = new Result<Link>();

            await Task.Yield();

            if (_longs.TryGetValue(longUrl, out Link link))
            {
                return result.Ok(link);
            }

            return result.Fail("无该短地址映射");
        }

        public async Task<Result<Link>> RemoveAsync(string shortId)
        {
            var result = new Result<Link>();

            await Task.Yield();

            if (_shorts.TryRemove(shortId, out Link link))
            {
                _longs.TryRemove(link.LongUrl, out _);

                return result.Ok(link);
            }

            return result.Fail("短地址不存在!");
        }

        public async Task<Result<Link>> UpdateAsync(Link link)
        {
            var result = new Result<Link>();

            await Task.Yield();

            if (_shorts.TryGetValue(link.Id, out Link value))
            {
                _shorts.TryUpdate(link.Id, link, value);

                if (_longs.ContainsKey(link.LongUrl))
                {
                    _longs.TryUpdate(link.Id, link, value);

                    return result.Ok(link);
                }
            }

            return result.Fail("更新失败!");
        }
    }
}
