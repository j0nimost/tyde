using Tyde.Shared.Configurations;
using System.Text.Json;
using System.Text;
using Tyde.Core.Cache;
using Microsoft.Extensions.Options;
using System.Reflection;
using System.Runtime.Remoting;
using Tyde.Shared.Factories;

namespace Tyde.Core.AuthHandler
{
    public class TydeAuthHandler: ITydeAuthHander
    {
        private readonly HttpClient _httpClient;
        private readonly ITydeExtensionFactory _extensionFactory;
        private readonly ITydeCache _tydeCache;
        private readonly TydeConfiguration _tydeConfiguration;
        private readonly TydeJsonOptions _tydeJsonOptions;

        public TydeAuthHandler(ITydeExtensionFactory tydeExtensionFactory, ITydeCache tydeCache, IOptions<TydeConfiguration> tydeConfiguration, IOptions<TydeJsonOptions> jsonOptions)
        {
            _tydeConfiguration = tydeConfiguration.Value;
            _tydeJsonOptions = jsonOptions.Value;
            _extensionFactory = tydeExtensionFactory;
            _tydeCache = tydeCache;
            _httpClient = _extensionFactory.HttpClientInstance ?? throw new ArgumentNullException("httpclient instance from IServices is Null");
        }

        public async Task<T> SendRequestAsync<T>()
        {
            throw new NotImplementedException();
        }

        public async Task SendAuthRequestAsync()
        {
            var reqContent = JsonSerializer.Serialize<Dictionary<string, string>>(_extensionFactory.AuthorizingParams);
            var httpContent = new StringContent(reqContent, Encoding.UTF8, "application/json");
            try
            {
                var response = await _httpClient.PostAsync(_tydeConfiguration.AuthenticationUrl, httpContent);

                response.EnsureSuccessStatusCode();
                
                string responseBody = await response.Content.ReadAsStringAsync();

                Dictionary<string, object> responseObj = JsonSerializer.Deserialize<Dictionary<string, object>>(responseBody, _tydeJsonOptions.JsonSerializerOptions);


                if (responseObj == null)
                    throw new Exception("Incorrect deserialization of the response,\n either incorrect object was returned or an empty response");


                ResponseCacheAppend(responseObj);
            }
            catch (Exception)
            {

                throw;
            }
        }

        //private HttpContent SetHttpContent(Dictionary<string, string> dictionary)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    if(_tydeConfiguration.HasRefreshToken)
        //        sb.Append()
        //}

        private void ResponseCacheAppend(Dictionary<string, object> responseDict)
        {
            foreach(KeyValuePair<string, object> res in responseDict)
            {
                if(_extensionFactory.DeserializationConfig.ContainsKey(res.Key))
                {
                    if(res.Value == null)
                        throw new ArgumentNullException($"{nameof(res.Key)} From Authorization API has an empty value");

                    if(String.IsNullOrEmpty(_extensionFactory.DeserializationConfig[res.Key]) || _extensionFactory.DeserializationConfig.ContainsKey(res.Key))
                    {
                        _tydeCache.RemoveSessionToken(res.Key); // remove if exists
                        _tydeCache.AddSessionToken(res.Key, res.Value.ToString() ?? ""); // append the respective key

                    }
                    else
                        _tydeCache.AddSessionToken(res.Key, _extensionFactory.DeserializationConfig[res.Key]); // if the customer has defined own values skip updating the value
                }

                if (_tydeConfiguration.ExpiresInTagName.Equals(res.Key))
                {
                    double expiresIn = Convert.ToDouble(Convert.ToString(res.Value));

                    if (res.Value != null)
                        _tydeCache.SetExpiresAt(TimeSpan.FromSeconds(expiresIn));
                    else
                        _tydeCache.SetExpiresAt(_tydeConfiguration.Expires_In);
                }

            }
        }


    }
}
