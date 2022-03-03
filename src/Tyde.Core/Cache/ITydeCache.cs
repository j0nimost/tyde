namespace Tyde.Core.Cache
{
    public interface ITydeCache
    {
        string GetSessionToken(string key);
        void AddSessionToken(string key, string value);
        void RemoveSessionToken(string key);
        void SetExpiresAt(TimeSpan expiresIn);
        DateTimeOffset ExpiresAt { get; }
        bool IsSessionValid { get; }
    }
}
