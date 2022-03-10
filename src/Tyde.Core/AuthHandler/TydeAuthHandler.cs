using Tyde.Shared.Configurations;
using System.Text.Json;
using System.Text;
using Tyde.Core.Cache;
using Microsoft.Extensions.Options;
using System.Reflection;
using System.Runtime.Remoting;

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


        private void ResponseCacheAppend(Dictionary<string, object> responseDict)
        {
            foreach(KeyValuePair<string, object> res in responseDict)
            {
                if(_extensionFactory.SerializationConfig.ContainsKey(res.Key))
                {
                    if(res.Value == null)
                        throw new ArgumentNullException($"{nameof(res.Key)} From Authorization API has an empty value");


                    if(_tydeConfiguration.ExpiresInTagName.Equals(res.Key))
                    {
                        TimeSpan expiresIn = (TimeSpan)res.Value;
                        _tydeCache.SetExpiresAt(expiresIn); // update only when Expires_In is not set
                        continue;
                    }

                    if(String.IsNullOrEmpty(_extensionFactory.SerializationConfig[res.Key]) || _tydeConfiguration.TokenTagName == res.Key)
                        _tydeCache.AddSessionToken(res.Key, Convert.ToString(res.Value)); // append the respective key
                    else
                        _tydeCache.AddSessionToken(res.Key, _extensionFactory.SerializationConfig[res.Key]); // if the customer has defined own values skip updating the value
                }
            }
        }


    }
}
