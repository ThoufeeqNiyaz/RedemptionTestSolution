using Dapper;
using UserAPI.Data;
using UserAPI.Models;

namespace UserAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperContext _context;

        public UserRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var query = "SELECT * FROM Users";
            using (var connection = _context.CreateConnection())
            {
                var users = await connection.QueryAsync<User>(query);
                return users.ToList();
            }
        }

        public async Task<User> GetUserById(int id)
        {
            var query = "SELECT * FROM Users WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<User>(query, new { Id = id });
            }
        }

        public async Task AddUser(User user)
        {
            var query = "INSERT INTO Users (Name, Email) VALUES (@Name, @Email)";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { user.Name, user.Email });
            }
        }

        public async Task UpdateUser(User user)
        {
            var query = "UPDATE Users SET Name = @Name, Email = @Email WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { user.Name, user.Email, user.Id });
            }
        }

        public async Task DeleteUser(int id)
        {
            var query = "DELETE FROM Users WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { Id = id });
            }
        }
    }
}
