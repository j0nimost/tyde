using Microsoft.Extensions.DependencyInjection;


namespace Tyde.Shared.Configurations
{
    public class TydeExtensionFactory : ITydeExtensionFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Dictionary<string, string>? _SerializationConfig;

        private HttpClient? _httpClient;


        public TydeExtensionFactory(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null)
                throw new ArgumentNullException(nameof(serviceProvider));


            _serviceProvider = serviceProvider;

            _SerializationConfig = new Dictionary<string, string>
            {
                {TydeConfiguration.TagName, "" } // allows us to deserialize irregardless of the value passed
            };

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

        public Dictionary<string, string> SerializationConfig => _SerializationConfig ?? throw new ArgumentNullException(nameof(SerializationConfig));
    }
}
