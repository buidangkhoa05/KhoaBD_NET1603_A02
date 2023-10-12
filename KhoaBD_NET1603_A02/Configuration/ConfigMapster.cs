using Mapster;
using MapsterMapper;
using System.Reflection;

namespace RazorPage.Configuration
{
    public static class ConfigMapster
    {
        public static void RegisterMapster(this IServiceCollection serviceCollection)
        {
            //scan all assemblies finding for IRegister
            var config = TypeAdapterConfig.GlobalSettings;
            var assemblies = new Assembly[]
            {
                Assembly.GetExecutingAssembly()
            };
            config.Scan(assemblies);

            serviceCollection.AddSingleton(config);
            serviceCollection.AddScoped(typeof(IMapper), typeof(ServiceMapper));
        }
    }
}
