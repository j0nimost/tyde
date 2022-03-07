﻿
using Tyde.Core.AuthHandler;
using Tyde.Core.Cache;
using Tyde.Shared.Configurations;

namespace Tyde.Core
{
    public class TydeHttpDelegatingHandler : DelegatingHandler
    {
        private readonly ITydeExtensionFactory _extensionFactory;
        private readonly ITydeCache _tydeCache;
        private readonly ITydeAuthHander _tydeAuthHandler;
        public TydeHttpDelegatingHandler(ITydeExtensionFactory extensionFactory, ITydeCache tydeCache, ITydeAuthHander tydeAuthHander)
        {
            _extensionFactory = extensionFactory;
            _tydeCache = tydeCache;
            _tydeAuthHandler = tydeAuthHander;
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
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if(!_tydeCache.IsSessionValid)
                _tydeAuthHandler.SendAuthRequestAsync().GetAwaiter().GetResult(); // refresh tokens


            if (TydeConfiguration.AuthenticationUrl == request.RequestUri)
                return base.SendAsync(request, cancellationToken); // avoid adding the headers


            foreach (KeyValuePair<string, string> val in TydeCache.CacheStorage)
            {
                request.Headers.Add(val.Key, val.Value); // Add the Authorization Headers
            }
            
            return base.SendAsync(request, cancellationToken);
        }

    }
}