using Microsoft.Extensions.DependencyInjection;
using Tyde.Shared.Configurations;

namespace Tyde.Extensions.HttpClient;
public static class TydeHttpExtensions
{

    public static IHttpClientBuilder AddTydeHttpClientExtension(this IHttpClientBuilder httpClientBuilder, Action<TydeConfiguration> options)
    {
        if (httpClientBuilder == null)
            throw new ArgumentNullException(nameof(httpClientBuilder));

        if (options == null)
            throw new ArgumentNullException(nameof(options));

        // TODO: implement
        httpClientBuilder.Services.Configure(options);
        return httpClientBuilder;
    }

    public static IHttpClientBuilder AddTydeHttpClientExtension<T>(this IHttpClientBuilder httpClientBuilder, Action<TydeOptions> options)
    {
        if (httpClientBuilder == null)
            throw new ArgumentNullException(nameof(httpClientBuilder));

        if (options == null)
            throw new ArgumentNullException(nameof(options));

        // TODO: implement
        httpClientBuilder.Services.Configure(options);
        
        TydeJsonProperties.JsonDeserailizatingToType = typeof(T);

        return httpClientBuilder;
    }
}
