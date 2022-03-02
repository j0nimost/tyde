using Microsoft.Extensions.DependencyInjection;


namespace Tyde.Shared.Configurations
{
    public class TydeExtensionFactory : ITydeExtensionFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Dictionary<string, string> _SerializationConfig;

        private HttpClient? _httpClient;


        public TydeExtensionFactory(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null)
                throw new ArgumentNullException(nameof(serviceProvider));


            _serviceProvider = serviceProvider;

            _SerializationConfig = new Dictionary<string, string>
            {
                {TydeConfiguration.TokenTagName, "" }, // allows us to deserialize irregardless of the value passed
                {TydeConfiguration.ExpiresInTagName, "" },
                

            };

            AppendAdditionalHeaders();
        }

        private void AppendAdditionalHeaders()
        {
            if (TydeConfiguration.AdditionalHeaders == null)
                return;

            foreach (KeyValuePair<string, string> items in TydeConfiguration.AdditionalHeaders)
            {
                _SerializationConfig.Add(items.Key, items.Value);
            }
        }


        public HttpClient HttpClientInstance {
           get
            {
                // re-use the application defined HttpClient
                if (_httpClient == null)
                {
                    var httpInstance = _serviceProvider.GetRequiredService<HttpClient>();

                    if(httpInstance == null)
                        throw new ArgumentNullException(nameof(HttpClient));

                    _httpClient = httpInstance;
                }
                return _httpClient;
            }
         }

        /// <summary>
        /// Contains all the parameters needed to process the Authorization JSON response,
        /// Default values will not be overwritten. <br/>
        /// Token and Expires_In will always overwritten when session expires.
        /// </summary>
        public Dictionary<string, string> SerializationConfig => _SerializationConfig ?? throw new ArgumentNullException(nameof(SerializationConfig));
    }
}
