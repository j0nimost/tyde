using Microsoft.Extensions.DependencyInjection;


namespace Tyde.Shared.Configurations
{
    public class TydeExtensionFactory : ITydeExtensionFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private HttpClient? _httpClient;

        public TydeExtensionFactory(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null)
                throw new ArgumentNullException(nameof(serviceProvider));


            _serviceProvider = serviceProvider;

        }

        public HttpClient HttpClientInstance {
           get
            {
                // re-use the application defined HttpClient
                if (_httpClient == null)
                {
                    var httpInstance = _serviceProvider.GetRequiredService<HttpClient>();

                    if(httpInstance == null)
                        throw new ArgumentNullException(nameof(_httpClient));

                    _httpClient = httpInstance;
                }
                return _httpClient;
            }
         }
    }
}
