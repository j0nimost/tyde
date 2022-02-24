using Tyde.Shared.Configurations;


namespace Tyde.Core.AuthHandler
{
    public class TydeAuthHandler
    {
        private readonly HttpClient? _httpClient;
        private readonly ITydeExtensionFactory _extensionFactory;

        public TydeAuthHandler(ITydeExtensionFactory tydeExtensionFactory)
        {
            _extensionFactory = tydeExtensionFactory;
        }

        
    }
}
