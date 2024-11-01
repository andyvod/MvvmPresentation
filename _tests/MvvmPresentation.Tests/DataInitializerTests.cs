using Microsoft.Extensions.DependencyInjection;
using MvvmPresentation.Data.Data;
using MvvmPresentation.Data.Data.Model;

namespace MvvmPresentation.Tests
{
    public class DataInitializerTests
    {
        private readonly IServiceScopeFactory _scopeFactory;
        public DataInitializerTests()
        {
            IServiceProvider _sp = _buildServices();

            _scopeFactory = _sp.GetRequiredService<IServiceScopeFactory>();
        }       

        [Fact]
        public async Task init_data_test()
        {
            JsonData data = new(Properties.Resources.OrdersJson, Properties.Resources.CustomersJson);
            using var scope = _scopeFactory.CreateScope();

            var service = scope.ServiceProvider.GetRequiredService<DataInitializer>();

            await service.InitDataIfNeed(data);
        }

        private static IServiceProvider _buildServices()
        {
            ServiceCollection services = new();

            services.AddScoped<DataInitializer>();

            services.AddApplicationDb("Data Source = DataInitializerTests.db");

            var sp = services.BuildServiceProvider();

            return sp;
        }
    }
}