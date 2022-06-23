using Microsoft.Extensions.DependencyInjection;
using PostService.Application.Services;
using PostService.Inerfaces;
using PostService.Repository;
using System.Reflection;

namespace PostService.Application
{
    public static class IoC
    {
        public static void AddAppServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUnitOfWorkService, UnitOfWorkService>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
