using Microsoft.Extensions.DependencyInjection;
using Tyde.Shared.Configurations;


namespace Tyde;
public static class TydeExtensions
{

    public static IServiceCollection AddTyde(this IServiceCollection services, Action<TydeConfiguration> configureTyde)
    {
        if (services == null)
            throw new ArgumentNullException(nameof(services));

        if (configureTyde == null)
            throw new ArgumentNullException(nameof(configureTyde));

        // TODO: implement

        return services;
    }

}
