using Microsoft.Data.SqlClient;
using System.Data;

namespace OrderAPI.Data
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("OrderDatabase")
                                ?? throw new InvalidOperationException("Connection string 'OrderDatabase' not found.");
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
