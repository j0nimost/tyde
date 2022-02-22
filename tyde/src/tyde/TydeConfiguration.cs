
namespace tyde.src.Tyde
{
    public class TydeConfiguration
    {
        /// <summary>
        /// For a specific HttpClient Set the Authorization API Address
        /// </summary>
        public static bool IsAddressForAuthorization { get; set; }
        /// <summary>
        /// Provides the Timespan for the Token Expiry time
        /// </summary>
        public static TimeSpan Expire_In { get; set; } = TimeSpan.FromSeconds(3600);
        /// <summary>
        /// Exact Time Expiry Time(UTC) using the <param name="Expire_In"></param>
        /// </summary>
        public DateTimeOffset Expire_At_TimeStamp { get; private set; }
        /// <summary>
        /// Capture a Specific API Address for Authorization
        /// </summary>
        public static Uri? BaseAddressForAuthorization { get; private set; }
        /// <summary>
        /// All other Api Addresses using session tokens
        /// </summary>
        public static IList<Uri>? BaseAddresses { get; set; }
        /// <summary>
        /// Optional headers applying to all API endpoints
        /// </summary>
        public static KeyValuePair<string?, string?> GlobalHeaders { get; set; }
        /// <summary>
        /// Holds all related Urls sharing Authorization and Tokens
        /// </summary>
        public static Dictionary<string, List<string>> UrlBindings { get; private set; } = new Dictionary<string, List<string>>();

        private readonly HttpClient _httpClient;

        public TydeConfiguration(HttpClient httpClient, string Name)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            if(IsAddressForAuthorization)
            {
                BaseAddressForAuthorization = _httpClient.BaseAddress ?? throw new ArgumentNullException(nameof(_httpClient.BaseAddress));
                UrlBindings.TryAdd(BaseAddressForAuthorization.ToString(), new List<string>());
            }

            if (Expire_In.TotalMilliseconds > 0)
                Expire_At_TimeStamp = new DateTime().AddMilliseconds(Expire_In.TotalMilliseconds);

            if(Name == null)
                throw new ArgumentNullException(nameof(Name));

        }
    }
}
