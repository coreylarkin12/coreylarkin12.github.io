using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Capstone.Models;

namespace Capstone.DAL
{
    public class LoginSqlDAL
    {
        private string connectionString = @"Data Source=localhost\sqlexpress;Initial Catalog=MealPlanning;Integrated Security=True";

        public LoginSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

     
        public Login GetUser(string emailAddress)
        {
            Login output = new Login();

           
            try
            {
                
                string sql = "SELECT TOP (1) * FROM users WHERE email = @email; SELECT CAST(SCOPE_IDENTITY() as int);";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.Parameters.AddWithValue("@email", emailAddress);
                    //cmd.Parameters.AddWithValue("@password", password);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {

                        Login user = new Login();
                        user.UserId = Convert.ToInt32(reader["user_id"]);
                        user.Email = Convert.ToString(reader["email"]);
                        user.Password = Convert.ToString(reader["password"]);

                        output = user;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            return output;
        }
    }
}