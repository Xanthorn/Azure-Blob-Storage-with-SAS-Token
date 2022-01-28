using Dapper;
using Exercise1.DataLayer.Models;
using System.Data;
using System.Data.SqlClient;

namespace Exercise1.DataLayer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private IDbConnection _dbConnection;

        public UserRepository(string connectionString)
        {
            _dbConnection = new SqlConnection(connectionString);
        }

        public int AddUser(User user)
        {
            var sql = "INSERT INTO Users (FirstName, LastName, Email, Age, AvatarUrl) VALUES(@FirstName, @LastName, @Email, @Age, @AvatarUrl);" +
                "SELECT CAST(SCOPE_IDENTITY() as int);";

            int id = _dbConnection.Query<int>(sql, user).Single();
            return id;
        }

        public User GetUser(int id) => _dbConnection.Query<User>("SELECT * FROM Users WHERE Id = @Id", new { id }).SingleOrDefault();

        public List<User> GetUsers() => _dbConnection.Query<User>("SELECT * FROM Users").ToList();

        public int UpdateUser(User user)
        {
            var sql = "UPDATE Users SET " +
                "FirstName = @FirstName, " +
                "LastName = @LastName, " +
                "Email = @Email, " +
                "Age = @Age, " +
                "AvatarUrl = @AvatarUrl, " +
                "WHERE Id = @Id;";
            _dbConnection.Query(sql, user);
            return user.Id;
        }
    }
}
