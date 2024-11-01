using DevExpress.Mvvm.POCO;
using MvvmPresentation.Core.Services;
using System.Collections.ObjectModel;

namespace MvvmPresentation.Core
{
    public class CustomerOrdersViewModel
    {
        private readonly IOrderQueries _orderQueries;

        private Task? _dataLoadingTask;

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

        public void RefreshData()
        {
            _loadCustomersFilter();
        }

        public bool CanRefreshData() { 
            return !IsBusy;
        }

        private void _loadCustomersFilter()
        {
            IsBusy = true;
            Task<IEnumerable<CustomerNameViewModel>> _task = _orderQueries.GetCustomers();

            _dataLoadingTask = _task;

            _task.ContinueWith(task => {
                CustomerNameViewModel _allCust = new(0, "Все");
                List<CustomerNameViewModel> _newCustomerList = [_allCust];

                _newCustomerList.AddRange(_task.Result);
                Customers = _newCustomerList;

                IsBusy = false;
                SelectedCustomer = _allCust;
                _dataLoadingTask = null;                
            });
        }

        private void _loadOrdersForCustomer(int customerId = default)
        {
            IsBusy = true;
            Task<IEnumerable<CustomerOrderItemViewModel>> _task = _orderQueries.GetOrders(customerId);

            _dataLoadingTask = _task;

            _task.ContinueWith(task => {
                IsBusy = false;
                OrderList.Clear();

                task.Result.ToList().ForEach(order => OrderList.Add(order));
                CurrentOrder = OrderList.FirstOrDefault();
                _dataLoadingTask = null;
            });
        }

        protected void OnSelectedCustomerChanged() {
            if (SelectedCustomer == null) {
                return;
            }

            _loadOrdersForCustomer();
        }
    }
}
