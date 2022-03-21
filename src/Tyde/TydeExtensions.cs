using Microsoft.Extensions.DependencyInjection;
using Tyde.Shared.Configurations;
using Tyde.Core.AuthHandler;
using Tyde.Core.Cache;
using Tyde.Core;
using Tyde.Shared.Factories;

namespace Tyde;
public static class TydeExtensions
{

    public static IServiceCollection AddTyde(this IServiceCollection services)
    {
        if (services == null)
            throw new ArgumentNullException(nameof(services));
 
        services.AddSingleton<ITydeExtensionFactory, TydeExtensionFactory>();
        services.AddSingleton<ITydeAuthHander, TydeAuthHandler>();
        services.AddSingleton<ITydeCache, TydeCache>();

        services.AddSingleton<TydeHttpDelegatingHandler>();

        return services;
    }

}
