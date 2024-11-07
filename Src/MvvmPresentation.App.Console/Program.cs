using DevExpress.Mvvm.POCO;
using Microsoft.Extensions.DependencyInjection;
using MvvmPresentation.Core;
using MvvmPresentation.Core.Services;
using MvvmPresentation.Data.Data;
using MvvmPresentation.Data.Services;
using System.Configuration;

namespace MvvmPresentation.App.Console
{
    internal class Program
    {
        private static IServiceProvider? _rootServiceProvider;
        static async Task Main(string[] args)
        {
            _buildServiceProvider();
            await _initDataBaseIfNeed();
            using var scope = _rootServiceProvider?.CreateScope();
            var startupView = scope?.ServiceProvider.GetRequiredService<CustomerOrdersView>();
            startupView?.Show();
        }

        /// <summary>
        /// Первичная инициализация данных для приложения. 
        /// </summary>
        private static async Task _initDataBaseIfNeed()
        {
            var initializer = _rootServiceProvider!.GetRequiredService<DataInitializer>();
            Data.Data.Model.JsonData jsonData = new(Properties.Resources.OrdersJson, Properties.Resources.CustomersJson);
            await initializer.InitDataIfNeed(jsonData);
        }

        /// <summary>
        /// Регистрация сервисов для DependencyInjection
        /// </summary>
        private static void _buildServiceProvider()
        {
            ServiceCollection services = new();

            services.AddScoped<IOrderQueries, OrderQueries>();
            services.AddApplicationDb(ConfigurationManager.ConnectionStrings["ordersConnectionString"].ConnectionString);
            services.AddSingleton<DataInitializer>();
            services.AddSingleton(typeof(CustomerOrdersViewModel), ViewModelSource.GetPOCOType(typeof(CustomerOrdersViewModel)));
            services.AddSingleton<CustomerOrdersView>();

            _rootServiceProvider = services.BuildServiceProvider();
        }
    }
}
