
namespace Tyde.Shared.Factories
{
    public interface ITydeExtensionFactory
    {
        HttpClient HttpClientInstance { get; }
        Dictionary<string, string> DeserializationConfig { get; }
        Dictionary<string, string> AuthorizingParams { get; }
    }
}
