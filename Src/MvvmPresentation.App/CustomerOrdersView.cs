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

            //Привязка комбобокса к коллекции в модели представления
            fluent.SetBinding(comboBox1, c => c.DataSource, vm => vm.Customers);

            // Поскольку к позициям комбобокса привязывается объект CustomerNameViewModel,
            // то указываем, чтобы в тексте позиции отображалось полное имя заказчика
            comboBox1.DisplayMember = nameof(CustomerNameViewModel.FullName);

            //привязка выбранной позиции комбобокса к свойству SelectedCustomer вью модели
            fluent.WithEvent<ComboBox, EventArgs>(comboBox1, nameof(ComboBox.SelectedIndexChanged))
                .SetBinding(vm => vm.SelectedCustomer, args => (CustomerNameViewModel)comboBox1.SelectedItem, (c, vm) => { });

            //привязка грида к коллекции заказов в модели преставления
            fluent.SetBinding(gridControl1, g => g.DataSource, vm => vm.OrderList);

            //Привязка выбранной позиции грида к свойству CurrentOrder модели представления
            fluent.WithEvent<ColumnView, FocusedRowObjectChangedEventArgs>(gridView1, nameof(gridView1.FocusedRowObjectChanged))
                .SetBinding(vm => vm.CurrentOrder,
                    args => gridView1.GetRow(args.RowHandle) as CustomerOrderItemViewModel,
                    (gView, entity) =>
                    {
                        gView.FocusedRowHandle = gView.FindRow(entity);
                        gView.ClearSelection();
                        gView.SelectRow(gView.FocusedRowHandle);
                    });
            //Привязка свойства LoadingPanelVisible грида к свойству IsBusy модели представления
            //Когда IsBusy == true, на гриде отображается "колесико" прогресса 
            fluent.SetBinding(gridView1, g => g.LoadingPanelVisible, vm => vm.IsBusy);

            //Привязка кнопки к методу вью модели 
            fluent.BindCommand(simpleButton1, vm => vm.RefreshData);

            //После загрузки формы, вызвать комманду загрузки данных в модели представления
            Load += (s, e) => fluent.ViewModel.OnLoad();
        }
    }
}
