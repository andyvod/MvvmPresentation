
namespace MvvmPresentation.Core.Services
{
    public interface IOrderQueries
    {
        Task<IEnumerable<CustomerNameViewModel>> GetCustomers();
        Task<IEnumerable<CustomerOrderItemViewModel>> GetOrders(int customerId);
    }
}