using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tyde.Shared.Configurations;

namespace Tyde.Core
{
    public partial class TydeHttpDelegatingHandler : DelegatingHandler
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
                if (String.IsNullOrEmpty(val.Value))
                    return false;
            }

            return true;
        }
        // add session tokens and other headers
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // TODO: Check against Authentication Url
            return base.SendAsync(request, cancellationToken);
        }

        protected override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return base.Send(request, cancellationToken);
        }
    }
}
