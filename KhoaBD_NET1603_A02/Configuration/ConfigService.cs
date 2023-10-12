using Application.Repository;
using System.Runtime.CompilerServices;


namespace RazorPage.Configuration
{
    public static class ConfigService
    {
        public static void AddServices(this IServiceCollection service)
        {
            service.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            service.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
