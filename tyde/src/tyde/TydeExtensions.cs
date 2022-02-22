
using Microsoft.Extensions.DependencyInjection;
using System.Collections;

namespace tyde.src.Tyde
{
    public static class TydeExtensions
    {
        public static IHttpClientBuilder AddTyde(this IHttpClientBuilder httpClientBuilderService)
        {
            if(httpClientBuilderService == null)
                throw new ArgumentNullException(nameof(httpClientBuilderService));

            // TODO: add all the necessary DI
            
            return httpClientBuilderService;
        }
    }
}
