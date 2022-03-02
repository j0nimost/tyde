using Tyde.Shared.Configurations;


namespace Tyde.Core.AuthHandler
{
    public class TydeAuthHandler: ITydeAuthHander
    {
        private readonly HttpClient? _httpClient;
        private readonly ITydeExtensionFactory _extensionFactory;

        public TydeAuthHandler(ITydeExtensionFactory tydeExtensionFactory)
        {
            _extensionFactory = tydeExtensionFactory;
            _httpClient = _extensionFactory.HttpClientInstance ?? throw new ArgumentNullException("httpclient instance from IServices is Null");
        }

        public async Task<T> SendRequestAsync<T>()
        {
            throw new NotImplementedException();
        }

        public async Task SendRequestAsync()
        {
            throw new NotImplementedException();
        }
    }
}
