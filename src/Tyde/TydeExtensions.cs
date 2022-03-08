using Microsoft.Extensions.DependencyInjection;
using Tyde.Shared.Configurations;
using Tyde.Core.AuthHandler;
using Tyde.Core.Cache;
using Tyde.Core;

namespace Tyde;
public static class TydeExtensions
{

    public static IServiceCollection AddTyde(this IServiceCollection services)
    {
        if (services == null)
            throw new ArgumentNullException(nameof(services));
 
        services.AddScoped<ITydeExtensionFactory, TydeExtensionFactory>();
        services.AddScoped<ITydeAuthHander, TydeAuthHandler>();
        services.AddScoped<ITydeCache, TydeCache>();


        services.AddScoped<TydeHttpDelegatingHandler>();

        return services;
    }

}
