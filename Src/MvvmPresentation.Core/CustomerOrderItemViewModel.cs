using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MvvmPresentation.Core
{
    public class CustomerOrderItemViewModel
    {
        public CustomerOrderItemViewModel(int orderNumber, string customer, DateTime orderDate, decimal sum)
        {
            OrderNumber = orderNumber;
            Customer = customer;
            OrderDate = orderDate;
            Sum = sum;
        }

        [DisplayName("Заказ")]
        [Description("Номер заказа")]
        [Display(Order = 0)]
        public int OrderNumber { get; }

        [DisplayName("Покупатель")]
        [Description("ФИО покупателя")]
        [Display(Order = 1)]
        public string Customer { get; }

        [DisplayName("Дата покупки")]
        [Description("Дата/время покупки")]
        [Display(Order = 2)]
        public DateTime OrderDate { get; }

        [DisplayName("Сумма")]
        [Description("Сумма в рублях")]
        [Display(Order = 3)]
        public decimal Sum { get; }
    }
}
