using Microsoft.Extensions.DependencyInjection;

namespace Tyde.Extensions.HttpClient;
public static class TydeHttpExtensions
{
    public static IHttpClientBuilder AddTyde(this IHttpClientBuilder httpClientBuilder)
    {
        if(httpClientBuilder == null)
            throw new ArgumentNullException(nameof(httpClientBuilder));

        // TODO: implement

        return httpClientBuilder;
    }
}
