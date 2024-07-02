using Dapper;
using OrderAPI.Data;
using OrderAPI.Models;

namespace OrderAPI.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DapperContext _context;

        public OrderRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            var query = "SELECT * FROM Orders";
            using (var connection = _context.CreateConnection())
            {
                var orders = await connection.QueryAsync<Order>(query);
                return orders.ToList();
            }
        }

        public async Task<Order> GetOrderById(int id)
        {
            var query = "SELECT * FROM Orders WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<Order>(query, new { Id = id });
            }
        }

        public async Task AddOrder(Order order)
        {
            var query = "INSERT INTO Orders (UserId, ProductId, OrderDate) VALUES (@UserId, @ProductId, @OrderDate)";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { order.UserId, order.ProductId, order.OrderDate });
            }
        }

        public async Task UpdateOrder(Order order)
        {
            var query = "UPDATE Orders SET UserId = @UserId, ProductId = @ProductId, OrderDate = @OrderDate WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { order.UserId, order.ProductId, order.OrderDate, order.Id });
            }
        }

        public async Task DeleteOrder(int id)
        {
            var query = "DELETE FROM Orders WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { Id = id });
            }
        }
    }
}
