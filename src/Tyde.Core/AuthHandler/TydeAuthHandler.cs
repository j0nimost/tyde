using Tyde.Shared.Configurations;
using System.Text.Json;
using System.Text;
using Tyde.Core.Cache;

namespace Tyde.Core.AuthHandler
{
    public class TydeAuthHandler: ITydeAuthHander
    {
        private readonly HttpClient _httpClient;
        private readonly ITydeExtensionFactory _extensionFactory;
        private readonly ITydeCache _tydeCache;

        public TydeAuthHandler(ITydeExtensionFactory tydeExtensionFactory, ITydeCache tydeCache)
        {
            _extensionFactory = tydeExtensionFactory;
            _tydeCache = tydeCache;
            _httpClient = _extensionFactory.HttpClientInstance ?? throw new ArgumentNullException("httpclient instance from IServices is Null");
        }

        public async Task<T> SendRequestAsync<T>()
        {
            throw new NotImplementedException();
        }

        public async Task SendRequestAsync()
        {
            var reqContent = JsonSerializer.Serialize<Dictionary<string, string>>(_extensionFactory.AuthorizingParams);
            var httpContent = new StringContent(reqContent, Encoding.UTF8, "application/json");
            try
            {
                var response = await _httpClient.PostAsync("", httpContent);

                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();

                Dictionary<string, string>? responseDict = JsonSerializer.Deserialize<Dictionary<string, string>>(responseBody);

                if(responseDict == null)
                {
                    throw new NullReferenceException("Authorizing API returned an empty response or an incorrect respose");
                }

                ResponseCacheAppend(responseDict);
            }
            catch (Exception)
            {

                throw;
            }
        }


        private void ResponseCacheAppend(Dictionary<string, string> responseDict)
        {
            foreach(KeyValuePair<string, string> res in responseDict)
            {
                if(_extensionFactory.SerializationConfig.ContainsKey(res.Key))
                {
                    if(String.IsNullOrEmpty(res.Value))
                        throw new ArgumentNullException($"{nameof(res.Key)} From Authorization API has an empty value");


                    if(TydeConfiguration.ExpiresInTagName.Equals(res.Key))
                    {
                        TimeSpan expiresIn = TimeSpan.Parse(res.Value);
                        _tydeCache.SetExpiresAt(expiresIn); // update only when Expires_In is not set
                        continue;
                    }

                    if(String.IsNullOrEmpty(_extensionFactory.SerializationConfig[res.Key]) || TydeConfiguration.TokenTagName == res.Key)
                        _tydeCache.AddSessionToken(res.Key, res.Value); // append the respective key
                    else
                        _tydeCache.AddSessionToken(res.Key, _extensionFactory.SerializationConfig[res.Key]); // if the customer has defined own values skip updating the value
                }

            }

        }
    }
}
