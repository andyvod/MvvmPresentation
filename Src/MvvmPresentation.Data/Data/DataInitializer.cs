using Microsoft.Extensions.DependencyInjection;
using MvvmPresentation.Data.Data.Model;
using MvvmPresentation.Data.EF;
using MvvmPresentation.Data.EF.Model;

namespace MvvmPresentation.Data.Data
{
    public class DataInitializer
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public DataInitializer(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task InitDataIfNeed(JsonData jsonData)
        {
            using var scope = _serviceScopeFactory.CreateScope();

            var _context = scope.ServiceProvider.GetRequiredService<OrdersDbContext>();


            var justCreated = _context.Database.EnsureCreated();

            if (justCreated)
            {
                IEnumerable<OrderEntity> orders = _getOrders(jsonData);

                _context.Orders!.AddRange(orders);

                await _context.SaveChangesAsync();
            }

        }

        private IEnumerable<OrderEntity> _getOrders(JsonData jsonData)
        {
            var customerDtoItems = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<CustomerDTO>>(jsonData.CustomersJson);

            var customerEntities = customerDtoItems!.Select(c => new CustomerEntity(c.Id, c.FullName)).ToArray();

            var orderDtoItems = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<OrderDTO>>(jsonData.OrdersJson);

            var orderEntities = orderDtoItems!.Select(o => new OrderEntity(o.Id,
                                                                           o.Created,
                                                                           o.Sum,
                                                                           customerEntities.Single(c => c.Id == o.CustomerId)))
                .ToArray();

            return orderEntities;
        }
    }
}
