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

        httpClientBuilder.Services.Configure(options);
        return httpClientBuilder;
    }

    public static IHttpClientBuilder AddTydeHttpClientExtension<T>(this IHttpClientBuilder httpClientBuilder, Action<TydeConfiguration> options, Action<TydeJsonOptions> Jsonoptions)
    {
        if (httpClientBuilder == null)
            throw new ArgumentNullException(nameof(httpClientBuilder));

        if (options == null)
            throw new ArgumentNullException(nameof(options));

        httpClientBuilder.Services.Configure(options);
        httpClientBuilder.Services.Configure(Jsonoptions);

        return httpClientBuilder;
    }
}
