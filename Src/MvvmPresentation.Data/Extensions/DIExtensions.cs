using Microsoft.EntityFrameworkCore;
using MvvmPresentation.Data.EF;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DIExtensions
    {
        public static IServiceCollection AddApplicationDb(this IServiceCollection services, string connectionString) 
        {
            services.AddDbContext<OrdersDbContext>(options => { 
                options.UseSqlite(connectionString);
            });

            return services;
        }
    }
}
