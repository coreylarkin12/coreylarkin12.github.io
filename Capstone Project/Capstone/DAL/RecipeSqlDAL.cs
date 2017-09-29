using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Capstone.Models;

namespace Capstone.DAL
{
    public class RecipeSqlDAL
    {
        private readonly string databaseConnectionString;
        private const string SQL_AddRecipe = "INSERT into recipes VALUES (1, @recipe_name, @description, @preparation);";
        private const string SQL_AddRecipeIng = "INSERT into ingred_by_recipe VALUES ((SELECT ingredient_id FROM ingredients WHERE ingredient_name = @ingredient), (SELECT TOP 1 recipe_id FROM recipes WHERE recipe_name = @recipe_name ORDER BY recipe_id DESC), @amount);";
        private const string SQL_AddIngredient = "INSERT into ingredients VALUES (@ingredient);";
        private const string SQL_CheckForIngredient = "SELECT ingredient_id FROM ingredients WHERE ingredient_name = @ingredient;";
        private const string SQL_SelectAllRecipes = "SELECT recipe_id, recipe_name, description, preparation FROM recipes WHERE is_master = 1 ORDER BY recipe_name ASC;";
        private const string SQL_SelectOneRecipe = "SELECT recipe_name, description, preparation, recipe_id FROM recipes WHERE recipe_id = @recipe_id;";
        private const string SQL_GetIngredients = "SELECT i.ingredient_name, ir.amount FROM ingred_by_recipe ir JOIN ingredients i on ir.ingredient_id = i.ingredient_id WHERE recipe_id = @recipe_id;";
        private const string SQL_CheckRecipeForIngredient = "SELECT ingred_by_recipe.ingredient_id FROM ingred_by_recipe JOIN ingredients ON ingred_by_recipe.ingredient_id = ingredients.ingredient_id WHERE ingredients.ingredient_name = @ingredient AND ingred_by_recipe.recipe_id = @recipe;";

        private const string SQL_EditUserRecipe = "UPDATE recipes SET recipe_name = @recipe_name, description = @description, preparation = @preparation WHERE recipe_id = @recipe_id;";
        private const string SQL_DeleteLibraryIngred = "DELETE FROM ingred_by_recipe WHERE recipe_id = @recipe_id;";


        public RecipeSqlDAL(string connectionString)
        {
            databaseConnectionString = connectionString;
        }

        public bool AddRecipe(RecipeModel recipe)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(databaseConnectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(SQL_AddRecipe, connection);
                    cmd.Parameters.AddWithValue("@recipe_name", recipe.Name);
                    cmd.Parameters.AddWithValue("@description", recipe.Description);
                    cmd.Parameters.AddWithValue("@preparation", recipe.Preparation);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    for(int x = 0; x < recipe.Ingredients.Count; x++)
                    {
                        if(rowsAffected > 0 && recipe.Ingredients[x] != "")
                        {

                            cmd = new SqlCommand(SQL_CheckForIngredient, connection);
                            cmd.Parameters.AddWithValue("@ingredient", recipe.Ingredients[x].ToLower());                     

                            if(cmd.ExecuteScalar()==null)
                            {
                                cmd = new SqlCommand(SQL_AddIngredient, connection);
                                cmd.Parameters.AddWithValue("@ingredient", recipe.Ingredients[x].ToLower());

                                cmd.ExecuteNonQuery();
                            
                            }

                            cmd = new SqlCommand(SQL_AddRecipeIng, connection);
                            cmd.Parameters.AddWithValue("@ingredient", recipe.Ingredients[x].ToLower());
                            cmd.Parameters.AddWithValue("@recipe_name", recipe.Name);
                            cmd.Parameters.AddWithValue("@amount", recipe.Amount[x]);

                            rowsAffected = cmd.ExecuteNonQuery();
                        }

                    }

                    return (rowsAffected > 0);           
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public RecipeModel SelectOneRecipe(int recipe_id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(databaseConnectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(SQL_SelectOneRecipe, connection);
                    cmd.Parameters.AddWithValue("@recipe_id", recipe_id);

                    SqlDataReader reader = cmd.ExecuteReader();
                    RecipeModel thisRecipe = new RecipeModel();

                    while (reader.Read())
                    {
                        thisRecipe.Name = Convert.ToString(reader["recipe_name"]);
                        thisRecipe.Description = Convert.ToString(reader["description"]);
                        thisRecipe.Preparation = Convert.ToString(reader["preparation"]);
                        thisRecipe.Recipe_Id = Convert.ToInt32(reader["recipe_id"]);
                    }
                    reader.Close();

                    cmd = new SqlCommand(SQL_GetIngredients, connection);
                    cmd.Parameters.AddWithValue("@recipe_id", recipe_id);
                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        thisRecipe.Ingredients.Add(Convert.ToString(reader["ingredient_name"]));
                        thisRecipe.Amount.Add(Convert.ToString(reader["amount"]));
                    }

                    return thisRecipe;
                }
            }
            catch
            {
                throw;
            }
        }

        public List<RecipeModel> SelectAllRecipes()
        {
            List<RecipeModel> output = new List<RecipeModel>();
            try
            {
                using (SqlConnection connection = new SqlConnection(databaseConnectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(SQL_SelectAllRecipes, connection);

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
                }
            }
            catch
            {
                throw;
            }

            return output;
        }

        public bool EditRecipe(RecipeModel recipe)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(databaseConnectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(SQL_EditUserRecipe, connection);
                    cmd.Parameters.AddWithValue("@recipe_name", recipe.Name);
                    cmd.Parameters.AddWithValue("@description", recipe.Description);
                    cmd.Parameters.AddWithValue("@preparation", recipe.Preparation);
                    cmd.Parameters.AddWithValue("@recipe_id", recipe.Recipe_Id);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    SqlCommand cmd2 = new SqlCommand(SQL_DeleteLibraryIngred, connection);
                    cmd2.Parameters.AddWithValue("@recipe_id", recipe.Recipe_Id);
                    cmd2.ExecuteNonQuery();

                    for (int x = 0; x < recipe.Ingredients.Count; x++)
                    {
                        if (recipe.Ingredients[x] != "**delete**")
                        {
                            if (rowsAffected > 0 && recipe.Ingredients[x] != "")
                            {

                                cmd = new SqlCommand(SQL_CheckForIngredient, connection);
                                cmd.Parameters.AddWithValue("@ingredient", recipe.Ingredients[x].ToLower());

                                if (cmd.ExecuteScalar() == null)
                                {
                                    cmd = new SqlCommand(SQL_AddIngredient, connection);
                                    cmd.Parameters.AddWithValue("@ingredient", recipe.Ingredients[x].ToLower());

                                    cmd.ExecuteNonQuery();

                                }
                                SqlCommand cmdTest = new SqlCommand(SQL_CheckRecipeForIngredient, connection);
                                cmdTest.Parameters.AddWithValue("@ingredient", recipe.Ingredients[x].ToLower());
                                cmdTest.Parameters.AddWithValue("@recipe", recipe.Recipe_Id);

                                if (cmdTest.ExecuteScalar() == null)
                                {
                                    cmd = new SqlCommand(SQL_AddRecipeIng, connection);
                                    cmd.Parameters.AddWithValue("@ingredient", recipe.Ingredients[x].ToLower());
                                    cmd.Parameters.AddWithValue("@recipe_name", recipe.Name);
                                    cmd.Parameters.AddWithValue("@amount", recipe.Amount[x]);

                                    rowsAffected = cmd.ExecuteNonQuery();
                                }

                            }
                        }

                    }

                    return rowsAffected > 0;

                }
            }

            catch (SqlException ex)
            {
               throw;
            }
        }
    }
}