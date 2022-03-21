
using Microsoft.Extensions.Options;
using Tyde.Core.AuthHandler;
using Tyde.Core.Cache;
using Tyde.Shared.Configurations;
using Tyde.Shared.Factories;

namespace Tyde.Core
{
    public class TydeHttpDelegatingHandler : DelegatingHandler
    {
        private readonly ITydeExtensionFactory _extensionFactory;
        private readonly ITydeCache _tydeCache;
        private readonly ITydeAuthHander _tydeAuthHandler;
        private readonly TydeConfiguration _tydeConfigurations;
        public TydeHttpDelegatingHandler(ITydeExtensionFactory extensionFactory, ITydeCache tydeCache, ITydeAuthHander tydeAuthHander, IOptions<TydeConfiguration> tydeConfiguration)
        {
            _extensionFactory = extensionFactory;
            _tydeCache = tydeCache;
            _tydeAuthHandler = tydeAuthHander;
            _tydeConfigurations = tydeConfiguration.Value;
        }

        private bool ValidateIsExistingandIsValid()
        {
            if (_extensionFactory.HttpClientInstance == null)
                return false;

            foreach (KeyValuePair<string, string> val in _extensionFactory.SerializationConfig)
            {
                if (String.IsNullOrEmpty(val.Value))
                    return false;
            }

            return true;
        }
        // add session tokens and other headers
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if(!_tydeCache.IsSessionValid())
                await _tydeAuthHandler.SendAuthRequestAsync(); // refresh tokens
            


            foreach (KeyValuePair<string, string> val in TydeCache.CacheStorage)
            {
                if(val.Key == "token")
                {
                    request.Headers.Add("Authorization",$"Bearer {val.Value}"); // set auth header
                    continue;
                }
                request.Headers.Add(val.Key, val.Value); // Add the Authorization Headers
            }
            
            return await base.SendAsync(request, cancellationToken);
        }

    }
}
