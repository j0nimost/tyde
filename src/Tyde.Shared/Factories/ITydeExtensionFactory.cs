
namespace Tyde.Shared.Factories
{
    public interface ITydeExtensionFactory
    {
        HttpClient HttpClientInstance { get; }
        Dictionary<string, string> SerializationConfig { get; }
        Dictionary<string, string> AuthorizingParams { get; }
    }
}
