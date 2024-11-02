using DevExpress.XtraGrid.Views.Base;
using MvvmPresentation.Core;

namespace MvvmPresentation.App
{
    public partial class CustomerOrdersView : Form
    {
        public CustomerOrdersView()
        {
            InitializeComponent();
            if (!mvvmContext1.IsDesignMode)
            {
                _initMvvmBindings();
            }
        }

        private void _initMvvmBindings()
        {
            var fluent = mvvmContext1.OfType<CustomerOrdersViewModel>();

            fluent.SetBinding(comboBox1, c => c.DataSource, vm => vm.Customers);
            comboBox1.DisplayMember = nameof(CustomerNameViewModel.FullName);

            fluent.WithEvent<ComboBox, EventArgs>(comboBox1, nameof(ComboBox.SelectedIndexChanged))
                .SetBinding(vm => vm.SelectedCustomer, args => (CustomerNameViewModel)comboBox1.SelectedItem, (c, vm) => { });

            
            fluent.SetBinding(gridControl1, g => g.DataSource, vm => vm.OrderList);
            fluent.WithEvent<ColumnView, FocusedRowObjectChangedEventArgs>(gridView1, nameof(gridView1.FocusedRowObjectChanged))
                .SetBinding(vm => vm.CurrentOrder,
                    args => gridView1.GetRow(args.RowHandle) as CustomerOrderItemViewModel,
                    (gView, entity) =>
                    {
                        gView.FocusedRowHandle = gView.FindRow(entity);
                        gView.ClearSelection();
                        gView.SelectRow(gView.FocusedRowHandle);
                    });
            fluent.SetBinding(gridView1, g => g.LoadingPanelVisible, vm => vm.IsBusy);

            fluent.BindCommand(simpleButton1, vm => vm.RefreshData);

            Load += (s, e) => fluent.ViewModel.OnLoad();
        }
    }
}
