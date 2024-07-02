using Dapper;
using ProductAPI.Data;
using ProductAPI.Models;

namespace ProductAPI.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DapperContext _context;

        public ProductRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var query = "SELECT * FROM Products";
            using (var connection = _context.CreateConnection())
            {
                var products = await connection.QueryAsync<Product>(query);
                return products.ToList();
            }
        }

        public async Task<Product> GetProductById(int id)
        {
            var query = "SELECT * FROM Products WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<Product>(query, new { Id = id });
            }
        }

        public async Task AddProduct(Product product)
        {
            var query = "INSERT INTO Products (Name, Price) VALUES (@Name, @Price)";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { product.Name, product.Price });
            }
        }

        public async Task UpdateProduct(Product product)
        {
            var query = "UPDATE Products SET Name = @Name, Price = @Price WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { product.Name, product.Price, product.Id });
            }
        }

        public async Task DeleteProduct(int id)
        {
            var query = "DELETE FROM Products WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { Id = id });
            }
        }
    }
}
