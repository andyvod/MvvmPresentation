using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvvmPresentation.Core;
using MvvmPresentation.Core.Services;
using MvvmPresentation.Data.EF;

namespace MvvmPresentation.Data.Services
{
    public class OrderQueries : IOrderQueries
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public OrderQueries(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public async Task<IEnumerable<CustomerNameViewModel>> GetCustomers()
        {
            //await Task.Delay(TimeSpan.FromSeconds(1));
            using var scope = _scopeFactory.CreateScope();
            var _dbContext = scope.ServiceProvider.GetRequiredService<OrdersDbContext>();

            var query = _dbContext.Customers!.AsNoTracking();

            var records = await query!.ToListAsync();

            var vmData = records.Select(r => new CustomerNameViewModel(r.Id, r.FullName)).ToArray();

            return vmData;
        }

        public async Task<IEnumerable<CustomerOrderItemViewModel>> GetOrders(int customerId)
        {
            //await Task.Delay(TimeSpan.FromSeconds(1));
            using var scope = _scopeFactory.CreateScope();
            var _dbContext = scope.ServiceProvider.GetRequiredService<OrdersDbContext>();

            var query = _dbContext.Orders!
            .Include(o => o.Customer)
                .Where(o => customerId == 0 ? true : o.Customer.Id == customerId)
                .AsNoTracking();

            var records = await query!.ToListAsync();

            var orderVmData = records.Select(r => new CustomerOrderItemViewModel(r.Id, r.Customer.FullName, r.Created, r.Sum)).ToArray();

            return orderVmData;
        }
    }
}
