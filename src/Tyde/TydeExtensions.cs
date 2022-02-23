using Microsoft.Extensions.DependencyInjection;
using Tyde.Shared.Configurations;


namespace Tyde;
public static class TydeExtensions
{
    public static IServiceCollection AddTyde(this IServiceCollection services)
    {
        if(services == null)
            throw new ArgumentNullException(nameof(services));

        // TODO: implement
        return services;
    }


    public static IServiceCollection AddTyde(this IServiceCollection services, string Name)
    {
        if(String.IsNullOrEmpty(Name))
            throw new ArgumentNullException(nameof(Name));

        // TODO: try to capture name of Http Client to assign tyde instances to

        AddTyde(services);

        return services;
    }

    public static IServiceCollection AddTyde(this IServiceCollection services, List<Uri> urls)
    {
        if(urls == null)
            throw new ArgumentNullException(nameof(urls));

        // TODO: bind all listed Urls to the specific url
        AddTyde(services);

        return services;
    }

    public static IServiceCollection AddTyde(this IServiceCollection services, Action<TydeConfiguration> configureTyde)
    {
        if (services == null)
            throw new ArgumentNullException(nameof(services));

        if (configureTyde == null)
            throw new ArgumentNullException(nameof(configureTyde));

        // TODO: implement
        AddTyde(services);

        return services;
    }

    public static IServiceCollection AddTyde(this IServiceCollection services, string Name, Action<TydeConfiguration> configureTyde)
    {
        if (String.IsNullOrEmpty(Name))
            throw new ArgumentNullException(nameof(Name));

        if (configureTyde == null)
            throw new ArgumentNullException(nameof(configureTyde));
        // TODO: try to capture name of Http Client to assign tyde instances to

        AddTyde(services);

        return services;
    }
}
