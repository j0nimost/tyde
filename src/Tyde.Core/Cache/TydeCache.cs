﻿

using System.Collections.Concurrent;
using Tyde.Shared.Configurations;

namespace Tyde.Core.Cache
{
    public sealed class TydeCache : ITydeCache
    {
        public ConcurrentDictionary<string,string> _cache { get; private set; } = new ConcurrentDictionary<string,string>();
        public void AddSessionToken(string key, string value)
        {
            _cache.AddOrUpdate(key, value, (key, value) => value);
        }

        public string GetSessionToken(string key)
        {
            return _cache.TryGetValue(key, out string? token) ? token : string.Empty;
        }

        public void RemoveSessionToken(string key)
        {
            _cache.TryRemove(key, out string? token);
        }

        public DateTimeOffset ExpiresAt { get; private set; } = DateTime.MinValue;
        public bool IsSessionValid => ExpiresAt > DateTime.UtcNow;

        public void SetExpiresAt(TimeSpan expiresIn)
        {
            if (TydeConfiguration.Expires_In <= TimeSpan.Zero)
                ExpiresAt = DateTime.UtcNow.Add(expiresIn); // update with the json response expires in
            else if(ExpiresAt == DateTime.MinValue)
                ExpiresAt = DateTime.UtcNow.Add(TydeConfiguration.Expires_In); // update with the set config
        }

    }
}
