using Microsoft.EntityFrameworkCore;

namespace RXDigital.Api.Context.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddContextConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            services.AddDbContext<RxDigitalContext>(options => {
                options.UseSqlServer(configuration.GetConnectionString("RXDigitalServiceConnection"));
                options.EnableDetailedErrors(true);
            });
            services.AddTransient<IRxDigitalContext, RxDigitalContext>(); ;
        }
    }
}
