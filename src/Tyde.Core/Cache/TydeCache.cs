

using System.Collections.Concurrent;

namespace Tyde.Core.Cache
{
    public sealed class TydeCache : ITydeCache
    {
        private readonly ConcurrentDictionary<string,string> _cache = new ConcurrentDictionary<string, string>();
        public void AddSessionToken(string key, string token)
        {
            _cache.AddOrUpdate(key, token, (key, token) => token);
        }

        public string GetSessionToken(string key)
        {
            return _cache.TryGetValue(key, out string? token) ? token : string.Empty;
        }

        public void RemoveSessionToken(string key)
        {
            _cache.TryRemove(key, out string? token);
        }
    }
}
