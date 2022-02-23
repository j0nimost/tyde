using Microsoft.Extensions.DependencyInjection;

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

        return AddTyde(services);
    }

    public static IServiceCollection AddTyde(this IServiceCollection services, List<Uri> urls)
    {
        if(urls == null)
            throw new ArgumentNullException(nameof(urls));

        // TODO: bind all listed Urls to the specific url

        return AddTyde(services);
    }
}
