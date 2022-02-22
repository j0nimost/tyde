
using Microsoft.Extensions.DependencyInjection;
using System.Collections;

namespace tyde.src.Tyde
{
    public static class TydeExtensions
    {
        public static IServiceCollection AddTyde(this IServiceCollection services)
        {
            if(services == null)
                throw new ArgumentNullException(nameof(services));
            
            // TODO: add all the necessary DI
            return services;
        }
    }
}
