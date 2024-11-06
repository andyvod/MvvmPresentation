using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using MvvmPresentation.Core.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MvvmPresentation.Core
{
    [POCOViewModel()]
    public class CustomerOrdersViewModel
    {
        //Все ок. DevExpress MVVM сам это разруливает
        protected virtual IMessageBoxService _messageBoxService => throw new NotImplementedException();

        private readonly IOrderQueries _orderQueries;

        private Task? _dataLoadingTask;


        /****************************************************************************/

        //We recommend that you not use public constructors to prevent creating the View Model without the ViewModelSource
        protected CustomerOrdersViewModel()
        { 
        }

        //This is a helper method that uses the ViewModelSource class for creating a LoginViewModel instance
        public static CustomerOrdersViewModel Create()
        {
            return ViewModelSource.Create(() => new CustomerOrdersViewModel());
        }

        public static CustomerOrdersViewModel Create(IOrderQueries orderQueries)
        {
            var factory = ViewModelSource.Factory((IOrderQueries orderQueries) => new CustomerOrdersViewModel(orderQueries));
            return factory(orderQueries);
        }

        /***************************************************************************/

        public CustomerOrdersViewModel(IOrderQueries orderQueries)
        {
            _orderQueries = orderQueries;
        }

        public virtual ObservableCollection<CustomerOrderItemViewModel> OrderList { get; protected set; } = [];

        public virtual CustomerOrderItemViewModel? CurrentOrder { get; set; }

        public virtual List<CustomerNameViewModel> Customers { get; protected set; } = [];

        public virtual CustomerNameViewModel? SelectedCustomer { get; set; }


        public virtual bool IsBusy { get; protected set; }

        protected void OnIsBusyChanged()
        {
            this.RaiseCanExecuteChanged(vm => vm.RefreshData());
        }

        public void OnLoad() {
            _loadCustomersFilter();
        }


        [Command(CanExecuteMethodName = "CanRefreshData", Name = "RefreshDataCommand", UseCommandManager = true)]
        public void RefreshData()
        {
            _loadCustomersFilter();
        }

        public bool CanRefreshData() { 
            return !IsBusy;
        }


        private void _loadCustomersFilter1()
        {
            CustomerNameViewModel _allCust = new(0, "Все");
            List<CustomerNameViewModel> _newCustomerList = [_allCust];

            IsBusy = true;
            Task<IEnumerable<CustomerNameViewModel>> _task = _orderQueries.GetCustomers();
            IEnumerable<CustomerNameViewModel> res = _task.Result;

            _newCustomerList.AddRange(res);
            Customers = _newCustomerList;

            IsBusy = false;
            SelectedCustomer = _allCust;
            _dataLoadingTask = null;
        }

        private void _loadOrdersForCustomer1(int customerId = default)
        {
            OrderList.Clear();

            IsBusy = true;
            Task<IEnumerable<CustomerOrderItemViewModel>> _task = _orderQueries.GetOrders(customerId);

            _dataLoadingTask = _task;
            _task.Result.ToList().ForEach(order => OrderList.Add(order));
            CurrentOrder = OrderList.FirstOrDefault();
            _dataLoadingTask = null;
            IsBusy = false;
        }


        private void _loadCustomersFilter()
        {
            IsBusy = true;
            Task<IEnumerable<CustomerNameViewModel>> _task = _orderQueries.GetCustomers();

            _dataLoadingTask = _task;

            _task.ContinueWith(task => {
                if (task.IsFaulted) { 
                    var ex = task.Exception!.InnerExceptions.First();
                    _messageBoxService.ShowMessage(ex.Message, "Error", MessageButton.OK, MessageIcon.Error);
                    _dataLoadingTask = null;
                    IsBusy = false;
                    return;
                }
                CustomerNameViewModel _allCust = new(0, "Все");
                List<CustomerNameViewModel> _newCustomerList = [_allCust];

                _newCustomerList.AddRange(_task.Result);
                Customers = _newCustomerList;

                //IsBusy = false;
                SelectedCustomer = _allCust;
                _dataLoadingTask = null;                
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void _loadOrdersForCustomer(int customerId = default)
        {
            IsBusy = true;
            Task<IEnumerable<CustomerOrderItemViewModel>> _task = _orderQueries.GetOrders(customerId);

            _dataLoadingTask = _task;

            _task.ContinueWith(task => {
                if (task.IsFaulted)
                {
                    var ex = task.Exception!.InnerExceptions.First();
                    _messageBoxService.ShowMessage(ex.Message, "Error", MessageButton.OK, MessageIcon.Error);
                    _dataLoadingTask = null;
                    IsBusy = false;
                    return;
                }
                
                OrderList.Clear();
                task.Result.ToList().ForEach(order => OrderList.Add(order));
                CurrentOrder = OrderList.FirstOrDefault();
                _dataLoadingTask = null;
                IsBusy = false;
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        protected void OnSelectedCustomerChanged() {
            if (SelectedCustomer == null) {
                return;
            }

            _loadOrdersForCustomer(SelectedCustomer.Id);
        }
    }
}
