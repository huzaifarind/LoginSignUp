using LoginSignUp.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace LoginSignUp.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        public DbSet<Users> Users { get; set; }

        public void RegisterUser(string userName, string userEmail, string userPassword)
        {
            using (var connection = Database.GetDbConnection())
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "UserOperation";
                    command.CommandType = CommandType.StoredProcedure;

                    var operationIdParam = new SqlParameter("@OperationID", 1); // 1 for register
                    var userNameParam = new SqlParameter("@UserName", userName);
                    var userEmailParam = new SqlParameter("@UserEmail", userEmail);
                    var userPasswordParam = new SqlParameter("@UserPassword", userPassword);

                    command.Parameters.Add(operationIdParam);
                    command.Parameters.Add(userNameParam);
                    command.Parameters.Add(userEmailParam);
                    command.Parameters.Add(userPasswordParam);

                    command.ExecuteNonQuery();
                }
            }
        }

        public string LoginUser(string userName, string userPassword)
        {
            using (var connection = Database.GetDbConnection())
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "UserOperation";
                    command.CommandType = CommandType.StoredProcedure;

                    var operationIdParam = new SqlParameter("@OperationID", 2); // 2 for login
                    var userNameParam = new SqlParameter("@UserName", userName);
                    var userPasswordParam = new SqlParameter("@UserPassword", userPassword);

                    command.Parameters.Add(operationIdParam);
                    command.Parameters.Add(userNameParam);
                    command.Parameters.Add(userPasswordParam);

                    var result = command.ExecuteScalar();
                    if (result != null)
                    {
                        return result.ToString();
                    }
                    else
                    {
                        return null; // Login failed
                    }
                }
            }
        }
    }
}
