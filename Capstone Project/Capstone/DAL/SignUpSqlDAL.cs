using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.DAL;
using Capstone.Models;
using System.Data.SqlClient;

namespace Capstone.DAL
{
    public class SignUpSqlDAL
    {
        private string connectionString = @"Data Source=localhost\sqlexpress;Initial Catalog=MealPlanning;Integrated Security=True";

        public SignUpSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool CreateUser(Register newUser)
        {
            try
            {
                string sql = "INSERT INTO users VALUES (@email, @password); SELECT CAST(SCOPE_IDENTITY() as int);";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@email", newUser.Email);
                    cmd.Parameters.AddWithValue("@password", newUser.Password);

                    int result = cmd.ExecuteNonQuery();

                    return result > 0;
                }
            }
            catch(SqlException ex)
            {
                throw;
            }
        }

        //public List<string> GetUsernames(string startsWith)
        //{
        //    List<string> usernames = new List<string>();

        //    try
        //    {
        //        string sql = "SELECT email FROM users WHERE email LIKE '@startsWith%';";

        //        using (SqlConnection conn = new SqlConnection(connectionString))
        //        {
        //            conn.Open();

        //            SqlCommand cmd = new SqlCommand(sql, conn);
        //            cmd.Parameters.AddWithValue("@startsWith", startsWith);

        //            SqlDataReader reader = cmd.ExecuteReader();
        //            while (reader.Read())
        //            {
        //                usernames.Add(Convert.ToString(reader["email"]));
        //            }
        //        }
        //    }
        //    catch (SqlException ex)
        //    {
        //        throw;
        //    }

        //    return usernames;
        //}

        public Register GetUser(string emailAddress)
        {
            Register output = new Register();


            try
            {
                string sql = "SELECT TOP (1) * FROM users WHERE email = @email;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.Parameters.AddWithValue("@email", emailAddress);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {

                        Register user = new Register();
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