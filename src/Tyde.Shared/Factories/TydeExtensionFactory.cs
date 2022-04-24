using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Tyde.Shared.Configurations;

namespace Tyde.Shared.Factories
{
    public class TydeExtensionFactory : ITydeExtensionFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Dictionary<string, string> _SerializationConfig;
        private readonly Dictionary<string, string> _AuthorizingParams;
        private readonly TydeConfiguration _tydeConfiguration;

        private HttpClient? _httpClient;


        public TydeExtensionFactory(IServiceProvider serviceProvider, IOptions<TydeConfiguration> tydeConfiguration)
        {
            _tydeConfiguration = tydeConfiguration.Value;
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _AuthorizingParams = _tydeConfiguration.AuthorizingParameters ?? throw new ArgumentNullException(nameof(TydeConfiguration.AuthorizingParameters));

            _SerializationConfig = new Dictionary<string, string>
            {
                {_tydeConfiguration.TokenTagName, "" }, // allows us to deserialize irregardless of the value passed

            };


            AppendAdditionalHeaders();

        }

        private void AppendAdditionalHeaders()
        {
            if (_tydeConfiguration.AdditionalHeaders == null)
                return;

            foreach (KeyValuePair<string, string> items in _tydeConfiguration.AdditionalHeaders)
            {
                _SerializationConfig.Add(items.Key, items.Value);
            }

            if (_tydeConfiguration.HasRefreshToken)
                _SerializationConfig.Add(_tydeConfiguration.RefreshTokenTagName, ""); // append Refresh Tokens if Exists
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
        /// <summary>
        /// Contains http param body to get the JWT token
        /// </summary>
        public Dictionary<string, string> AuthorizingParams => _AuthorizingParams;
    }
}
