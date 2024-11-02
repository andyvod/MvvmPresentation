using DevExpress.Utils.MVVM;
using Microsoft.Extensions.DependencyInjection;
using MvvmPresentation.Core.Services;
using MvvmPresentation.Data.Data;
using MvvmPresentation.Data.Services;
using System.Configuration;

namespace CSharp
{
    public class EntryPoint
    {
        private static IServiceProvider? _rootServiceProvider;
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            _buildServiceProvider();
            _initDataBaseIfNeed();
            _addMVVMSupport();
            var application = new System.Windows.Application();
            application.Run(new MvvmPresentation.App.Wpf.MainWindow());
        }

        /// <summary>
        /// Первичная инициализация данных для приложения. 
        /// </summary>
        private static void _initDataBaseIfNeed()
        {
            var initializer = _rootServiceProvider!.GetRequiredService<DataInitializer>();
            MvvmPresentation.Data.Data.Model.JsonData jsonData = new(MvvmPresentation.App.Wpf.Properties.Resources.OrdersJson, MvvmPresentation.App.Wpf.Properties.Resources.CustomersJson);
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