namespace Tyde.Core.Cache
{
    public interface ITydeCache
    {
        string GetSessionToken(string key);
        void AddSessionToken(string key, string token);
        void RemoveSessionToken(string key);

    }
}
