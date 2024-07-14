using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MusicLibraryBLL
{
    public static class AutomapperRegistrator
    {
        public static void ConfigureAutomapper(this  IServiceCollection services)
        {
            services.AddAutoMapper(cfg => cfg.AddMaps(Assembly.GetExecutingAssembly()));
        }
    }
}
