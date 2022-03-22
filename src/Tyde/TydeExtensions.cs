using Microsoft.Extensions.DependencyInjection;
using Tyde.Shared.Configurations;
using Tyde.Core.AuthHandler;
using Tyde.Core.Cache;
using Tyde.Core;
using Tyde.Shared.Factories;

namespace Tyde;
public static class TydeExtensions
{

    public static IHttpClientBuilder AddTyde(this IHttpClientBuilder httpClientBuilder, Action<TydeConfiguration> options)
    {
        if (httpClientBuilder == null)
            throw new ArgumentNullException(nameof(httpClientBuilder));
        if (options == null)
            throw new ArgumentNullException(nameof(options));

        httpClientBuilder.Services.AddSingleton<ITydeExtensionFactory, TydeExtensionFactory>();
        httpClientBuilder.Services.AddSingleton<ITydeAuthHander, TydeAuthHandler>();
        httpClientBuilder.Services.AddSingleton<ITydeCache, TydeCache>();

        httpClientBuilder.Services.AddSingleton<TydeHttpDelegatingHandler>();

        httpClientBuilder.Services.Configure(options); // add options


        return httpClientBuilder;
    }

    public static IHttpClientBuilder AddTyde(this IHttpClientBuilder httpClientBuilder, Action<TydeConfiguration> options, Action<TydeJsonOptions> Jsonoptions)
    {
        if (httpClientBuilder == null)
            throw new ArgumentNullException(nameof(httpClientBuilder));

        if (options == null)
            throw new ArgumentNullException(nameof(options));

        httpClientBuilder.AddTyde(options);

        httpClientBuilder.Services.Configure(Jsonoptions);

        return httpClientBuilder;
    }

}
