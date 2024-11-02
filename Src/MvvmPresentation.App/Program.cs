using DevExpress.Utils.MVVM;
using Microsoft.Extensions.DependencyInjection;
using MvvmPresentation.Core.Services;
using MvvmPresentation.Data.Data;
using MvvmPresentation.Data.Services;
using System.Configuration;

namespace MvvmPresentation.App
{
    internal static class Program
    {
        private static IServiceProvider? _rootServiceProvider;
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static async Task Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            _buildServiceProvider();
            await _initDataBaseIfNeed();
            _addMVVMSupport();
            Application.Run(new CustomerOrdersView());
        }

        /// <summary>
        /// Первичная инициализация данных для приложения. 
        /// </summary>
        private static async Task _initDataBaseIfNeed()
        {
            var initializer = _rootServiceProvider!.GetRequiredService<DataInitializer>();
            Data.Data.Model.JsonData jsonData = new(Properties.Resources.OrdersJson,Properties.Resources.CustomersJson);
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
            
            _rootServiceProvider = services.BuildServiceProvider();
        }

        /// <summary>
        /// Поддержка DependencyInjection в моделях представления
        /// </summary>
        private static void _addMVVMSupport()
        {
            //See: https://docs.devexpress.com/WindowsForms/119492/build-an-application/winforms-mvvm/concepts/viewmodel-management
            MVVMContextCompositionRoot.ViewModelCreate += (s, e) =>
            {

                using var scope = _rootServiceProvider!.CreateScope();

                var constructors = e.ViewModelType.GetConstructors();

                var selectedConstructor = constructors.OrderByDescending(c => c.GetParameters().Length).First();

                var parameters = selectedConstructor.GetParameters().Select(p => scope.ServiceProvider.GetRequiredService(p.ParameterType)).ToArray();

                var viewModel = e.ViewModelSource.Create(e.ViewModelType, parameters);

                e.ViewModel = viewModel;
                return;
            };
        }
    }
}