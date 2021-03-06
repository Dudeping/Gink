﻿using Codeping.Utils;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Codeping.Gink.Core
{
    public interface IGinkService
    {
        Task<Result<string>> ToShortAsync(string longUrl);
        Task<Result<string>> ToLongAsync(string shortId);
        Task<Result> RemoveAsync(string shortId);
        IQueryable<Link> Where(Predicate<Link> predicate);
    }
}
