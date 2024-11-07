using DevExpress.Mvvm.POCO;
using Microsoft.Extensions.DependencyInjection;
using MvvmPresentation.Core;
using MvvmPresentation.Core.Services;
using MvvmPresentation.Data.Data;
using MvvmPresentation.Data.Services;
using System.Configuration;
using System.Windows;

namespace MvvmPresentation.App.Wpf
{
    public partial class App : System.Windows.Application
    {
        private static IServiceProvider? _rootServiceProvider;
        private readonly Func<Type?, object?> Resolve = (type) => type == null ? null : _rootServiceProvider?.GetService(type);

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _buildServiceProvider();
            _initDataBaseIfNeed();

            DISource.Resolver = Resolve;
        }
        

        /// <summary>
        /// Первичная инициализация данных для приложения. 
        /// </summary>
        private static void _initDataBaseIfNeed()
        {
            var initializer = _rootServiceProvider!.GetRequiredService<DataInitializer>();
            Data.Data.Model.JsonData jsonData = new(Wpf.Properties.Resources.OrdersJson, customersJson: Wpf.Properties.Resources.CustomersJson);
            initializer.InitDataIfNeed(jsonData).Wait();
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
            services.AddScoped(typeof(CustomerOrdersViewModel), ViewModelSource.GetPOCOType(typeof(CustomerOrdersViewModel)));

            _rootServiceProvider = services.BuildServiceProvider();
        }
    }
}
