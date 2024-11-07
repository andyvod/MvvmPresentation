using MvvmPresentation.Core;

namespace MvvmPresentation.App.Console
{
    internal class CustomerOrdersView : BaseView
    {
        private const char RELOAD_COMMAND = 'r';
        private const char CUSTOMERS_COMMAND = 'c';
        private const char EXIT_COMMAND = 'e';
        private const char HELP_COMMAND = 'h';

        public CustomerOrdersView(CustomerOrdersViewModel viewModel) : base(viewModel)
        {
            
        }

        protected override void DisplayHelp()
        {
            string[] _helpInfo = [
                 "Назначение клавиш:",
                $"{RELOAD_COMMAND} - загрузка всех заказов",
                $"{CUSTOMERS_COMMAND} - фильтр по заказчику",
                $"{EXIT_COMMAND} - выход",
                $"{HELP_COMMAND} - помощь"
             ];
            Display(_helpInfo);
        }

        protected override void SetBindings()
        {
            SetTrigger(nameof(CustomerOrdersViewModel.OrderList), ShowOrderList);

            SetTrigger(nameof(CustomerOrdersViewModel.IsBusy), () => {
                if(((CustomerOrdersViewModel)ViewModelINotify).IsBusy == true)
                {
                    Display("Идет загрузка данных...");
                }
            });

            CommandEvent += CustomerOrdersView_CommandEvent;

            ViewLoaded += CustomerOrdersView_ViewLoaded;
        }

        private void CustomerOrdersView_ViewLoaded(object? sender, EventArgs e)
        {
            ((CustomerOrdersViewModel)ViewModelINotify).OnLoad().Wait();
            while (true)
            {
                //System.Console.Write("command:");
                var key = System.Console.ReadKey();

                if (key.KeyChar == EXIT_COMMAND)
                {
                    break;
                }

                System.Console.Write(System.Environment.NewLine);
                OnCommandEntered(key.KeyChar);
            }
        }

        private void CustomerOrdersView_CommandEvent(object? sender, CommandEventArgs e)
        {
            switch (e.Command)
            {
                case RELOAD_COMMAND:
                    ((CustomerOrdersViewModel)ViewModelINotify).RefreshData().Wait();
                    break;
                case HELP_COMMAND:
                    DisplayHelp();
                    System.Console.Write("command:");
                    break;
                case CUSTOMERS_COMMAND:
                    _selectCustomer();
                    break;
                default:
                    break;
            }
        }

        private void _selectCustomer()
        {
            var viewModel = (CustomerOrdersViewModel)ViewModelINotify;
            var customers = viewModel.Customers.ToList();

            Display(customers);
            while (true)
            {
                System.Console.Write("Введите номер клиента:");
                var idStr = System.Console.ReadLine();

                if(!int.TryParse(idStr, out int clientId))
                {
                    System.Console.WriteLine("Номер клиента должен быть целым числом");
                    continue;
                }

                var selectedCustomerInView = customers.FirstOrDefault(c => c.Id == clientId);

                if (selectedCustomerInView is null) {
                    System.Console.WriteLine($"Не найден клиент с идентификатором: {clientId}");
                    continue;
                }

                if(selectedCustomerInView == viewModel.SelectedCustomer)
                {
                    selectedCustomerInView = new CustomerNameViewModel(selectedCustomerInView.Id, selectedCustomerInView.FullName);
                }

                viewModel.SelectedCustomer = selectedCustomerInView;
                break;
            }
        }

        private void ShowOrderList()
        {
            var newOrders = ((CustomerOrdersViewModel)ViewModelINotify).OrderList.ToList();

            Display(newOrders);

            System.Console.Write("command:");
        }
    }
}
