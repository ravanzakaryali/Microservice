using Microsoft.Extensions.DependencyInjection;
using PostService.Application.Interfaces.Storage;
using PostService.Application.Services;

namespace PostService.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddStorage<T>(this IServiceCollection services) where T : class, IStorage
        {
            services.AddScoped<IStorage, T>();
        }
    }
}
