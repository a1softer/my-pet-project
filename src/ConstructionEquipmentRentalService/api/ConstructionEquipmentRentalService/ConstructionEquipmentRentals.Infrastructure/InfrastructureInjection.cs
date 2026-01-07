using ConstructionEquipmentRentals.Infrastructure.Common;
using ConstructionEquipmentRentals.Infrastructure.Common.Configurations;
using Microsoft.Extensions.DependencyInjection;

namespace ConstructionEquipmentRentals.Infrastructure
{
    public static class InfrastructureInjection
    {
        public static void Inject(this IServiceCollection services)
        {
            services.AddOptions<PostgresConnectionOptions>().BindConfiguration("PostgresConnectionOptions");
            services.AddScoped<ApplicationDbContext>();
        }
    }
}
