using System;
using System.Net.Http;
using Tyde.Shared.Configurations;

namespace Tyde.Shared.Handlers
{
    public class TydeHttpDelegatingHandler: DelegatingHandler
    {
        private readonly ITydeExtensionFactory _extensionFactory;

        public TydeHttpDelegatingHandler(ITydeExtensionFactory extensionFactory)
        {
            _extensionFactory = extensionFactory;
        }

        private bool ValidateIsExistingandIsValid()
        {
            if (_extensionFactory.HttpClientInstance == null)
                return false;

            foreach (KeyValuePair<string, string> val in _extensionFactory.SerializationConfig)
            {
                if(String.IsNullOrEmpty(val.Value))
                    return false;
            }



            return true;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return base.SendAsync(request, cancellationToken);
        }

        protected override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return base.Send(request, cancellationToken);
        }
    }
}
