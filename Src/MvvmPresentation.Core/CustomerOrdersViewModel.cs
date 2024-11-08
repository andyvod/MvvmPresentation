using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using MvvmPresentation.Core.Services;

namespace MvvmPresentation.Core
{
    public class CustomerOrdersViewModel
    {
        //Все ок. DevExpress MVVM сам это разруливает
        protected virtual IMessageBoxService _messageBoxService => throw new NotImplementedException();

        private readonly IOrderQueries _orderQueries;

        public CustomerOrdersViewModel(IOrderQueries orderQueries)
        {
            _orderQueries = orderQueries;
        }

        public virtual List<CustomerOrderItemViewModel> OrderList { get; protected set; } = [];

        public virtual CustomerOrderItemViewModel? CurrentOrder { get; set; }

        public virtual List<CustomerNameViewModel> Customers { get; protected set; } = [];

        public virtual CustomerNameViewModel? SelectedCustomer { get; set; }


        public virtual bool IsBusy { get; protected set; }

        protected void OnIsBusyChanged()
        {
            this.RaiseCanExecuteChanged(vm => vm.RefreshData());
        }

        public async Task OnLoad()
        {
            await _loadCustomersFilterAsync();
        }

        public async Task RefreshData()
        {
            await _loadCustomersFilterAsync();
        }

        public bool CanRefreshData()
        {
            return !IsBusy;
        }

        private async Task _loadCustomersFilterAsync()
        {
            IsBusy = true;
            CustomerNameViewModel _allCust = new(0, "Все");
            List<CustomerNameViewModel> _newCustomerList = [_allCust];

            try
            {
                var loadedCustomers = await _orderQueries.GetCustomers();
                _newCustomerList.AddRange(loadedCustomers);
                Customers = _newCustomerList;

                SelectedCustomer = _allCust;
            }
            catch (Exception ex)
            {
                _messageBoxService.ShowMessage(ex.Message, "Error", MessageButton.OK, MessageIcon.Error);
                IsBusy = false;
            }
            finally
            {
                //IsBusy = false;
            }
        }

        private async Task _loadOrdersForCustomerAsync(int customerId = default)
        {
            IsBusy = true;
            try
            {
                var newOrders = await _orderQueries.GetOrders(customerId);

                OrderList = newOrders.ToList();
                CurrentOrder = newOrders.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _messageBoxService.ShowMessage(ex.Message, "Error", MessageButton.OK, MessageIcon.Error);
            }
            finally
            {
                IsBusy = false;
            }
        }

        protected async void OnSelectedCustomerChanged()
        {
            if (SelectedCustomer == null)
            {
                return;
            }

            await _loadOrdersForCustomerAsync(SelectedCustomer.Id);
        }
    }
}
