using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Capstone.Models;

namespace Capstone.DAL
{
    public class UserLibrariesDAL
    {
        private readonly string databaseConnectionString;
        private const string SQL_AddRecipeCopy = "INSERT into recipes VALUES (0, @recipe_name, @description, @preparation);SELECT CAST(SCOPE_IDENTITY() as int);";
        private const string SQL_AddRecipeToLibrary = "INSERT INTO recipe_by_user VALUES (@user_id, @recipe_id);";
        private const string SQL_AddRecipeIngCopy = "INSERT INTO ingred_by_recipe (recipe_id, ingredient_id, amount) SELECT @copy_id, ingredient_id, amount FROM ingred_by_recipe Where recipe_id = @original_id;";
        private const string SQL_GetAllUserRecipes = "SELECT * FROM recipe_by_user JOIN recipes ON recipe_by_user.recipe_id = recipes.recipe_id WHERE user_id = @user_id AND recipes.is_master = 0 ORDER BY recipe_name ASC;";
        private const string SQL_CheckUserLibraryForRecipe = "SELECT recipe_id FROM recipe_by_user where recipe_id = @recipe_id AND user_id = @user_id";
        private const string SQL_DeleteRecipeFromLibrary = "DELETE FROM ingred_by_recipe WHERE recipe_id = @recipe_id; DELETE FROM recipe_by_user WHERE user_id = @user_id AND recipe_id = @recipe_id; DELETE from meal_plans WHERE recipe_id = @recipe_id; DELETE from recipes WHERE recipe_id = @recipe_id;";

        public UserLibrariesDAL(string connectionString)
        {
            databaseConnectionString = connectionString;
        }

        public bool AddRecipeCopy(int user_id, RecipeModel recipe)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(databaseConnectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(SQL_AddRecipeCopy, connection);
                    cmd.Parameters.AddWithValue("@recipe_name", recipe.Name);
                    cmd.Parameters.AddWithValue("@description", recipe.Description);
                    cmd.Parameters.AddWithValue("@preparation", recipe.Preparation);

                    int copy_id = (int)cmd.ExecuteScalar();

                    SqlCommand cmd2 = new SqlCommand(SQL_AddRecipeToLibrary, connection);
                    cmd2.Parameters.AddWithValue("@recipe_id", copy_id);
                    cmd2.Parameters.AddWithValue("@user_id", user_id);
                    int rowsAffected = cmd2.ExecuteNonQuery();

                    SqlCommand cmd3 = new SqlCommand(SQL_AddRecipeIngCopy, connection);
                    cmd3.Parameters.AddWithValue("@copy_id", copy_id);
                    cmd3.Parameters.AddWithValue("@original_id", recipe.Recipe_Id);
                    rowsAffected = cmd3.ExecuteNonQuery();
                    return rowsAffected > 0;
                    
                }
            }

            catch (SqlException ex)
            {
                throw;
            }
        }

        public bool UserLibaryContainsRecipe(int recipe_id, int user_id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(databaseConnectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(SQL_CheckUserLibraryForRecipe, connection);
                    cmd.Parameters.AddWithValue("@recipe_id", recipe_id);
                    cmd.Parameters.AddWithValue("@user_id", user_id);

                    if (cmd.ExecuteScalar() == null)
                    {
                        return false;
                    }

                    else
                    {
                        return true;
                    }

                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }


        public List<RecipeModel> GetAllUserRecipes(int user_id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(databaseConnectionString))
                {
                    List<RecipeModel> output = new List<RecipeModel>();

                    connection.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetAllUserRecipes, connection);
                    cmd.Parameters.AddWithValue("@user_id", user_id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        RecipeModel thisRecipe = new RecipeModel()
                        {
                            Name = Convert.ToString(reader["recipe_name"]),
                            Description = Convert.ToString(reader["description"]),
                            Preparation = Convert.ToString(reader["preparation"]),
                            Recipe_Id = Convert.ToInt32(reader["recipe_id"])
                        };
                        output.Add(thisRecipe);
                    }
                    return output;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public bool DeleteRecipeFromLibrary(int recipe_id, int user_id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(databaseConnectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(SQL_DeleteRecipeFromLibrary, connection);
                    cmd.Parameters.AddWithValue("@recipe_id", recipe_id);
                    cmd.Parameters.AddWithValue("@user_id", user_id);

                    return (cmd.ExecuteNonQuery() > 0);
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

    }
}