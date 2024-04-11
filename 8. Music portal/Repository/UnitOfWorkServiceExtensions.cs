using _8._Music_portal.Models;
using _8._Music_portal.NewFolder;

namespace _8._Music_portal.Repository
{
    public static class UnitOfWorkServiceExtensions
    {
        public static void AddUnitOfWorkService(this IServiceCollection services)
        {
            services.AddScoped<IRepository, MyRepository>();
            services.AddScoped<IRepository<GenreModel>, GenreRepositori>();
            services.AddScoped<IRepository<PerformerModel>, PerformerRepositori>();

        }
    }
}
