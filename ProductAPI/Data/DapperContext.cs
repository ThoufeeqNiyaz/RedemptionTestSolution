using Microsoft.Data.SqlClient;
using System.Data;

namespace ProductAPI.Data
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DapperContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Data Source=Thoufeeq_Niyaz;Initial Catalog=RedemptiontestU,O,P;Integrated Security=True;TrustServerCertificate=True")
                                ?? throw new ArgumentNullException(nameof(configuration));
        }


        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
