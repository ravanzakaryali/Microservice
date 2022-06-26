using FileService.API.Services.Abstractions.Storage;

namespace FileService.API.Services
{
    public static class ServiceRegistration
    {
        public static void AddStorage<T>(this IServiceCollection services) where T : class, IStorage
        {
            services.AddScoped<IStorage, T>();
        }
    }
}
