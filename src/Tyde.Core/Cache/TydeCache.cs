using Microsoft.Extensions.Options;
using System.Collections.Concurrent;
using Tyde.Shared.Configurations;

namespace Tyde.Core.Cache
{
    public sealed class TydeCache : ITydeCache
    {
        internal static ConcurrentDictionary<string,string> CacheStorage { get; private set; } = new ConcurrentDictionary<string,string>();
        private readonly TydeConfiguration _tydeconfiguration;
        public TydeCache(IOptions<TydeConfiguration> tydeConfiguration)
        {
            _tydeconfiguration=tydeConfiguration.Value;
        }

        public void AddSessionToken(string key, string value)
        {
            CacheStorage.AddOrUpdate(key, value, (key, value) => value);
        }

        public string GetSessionToken(string key)
        {
            return CacheStorage.TryGetValue(key, out string? token) ? token : null;
        }

        public void RemoveSessionToken(string key)
        {
            CacheStorage.TryRemove(key, out string? token);
        }

        public DateTime ExpiresAt { get; private set; } = DateTime.MinValue;
        public bool IsSessionValid() => ExpiresAt > DateTime.Now.AddSeconds(5); // add an overhead of maybe request taking too long

        public void SetExpiresAt(TimeSpan expiresIn)
        {
            if (_tydeconfiguration.Expires_In <= TimeSpan.Zero)
                ExpiresAt = DateTime.Now.AddSeconds(expiresIn.TotalSeconds); // update with the json response expires in
            else if(ExpiresAt == DateTime.MinValue)
                ExpiresAt = DateTime.Now.AddSeconds(_tydeconfiguration.Expires_In.TotalSeconds); // update with the set config
        }

    }
}
