using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Models;
using System.Data.SqlClient;

namespace Capstone.DAL
{
    public class MealPlanSqlDAL
    {
        //WHERE meal_plan_id = @mealPlanId ORDER BY day ASC, meal ASC;
        private string connectionString = @"Data Source=localhost\sqlexpress;Initial Catalog=MealPlanning;Integrated Security=True";
        private const string SQL_GetAllUserRecipes = "SELECT * FROM recipe_by_user JOIN recipes ON recipe_by_user.recipe_id = recipes.recipe_id WHERE user_id = @user_id AND recipes.is_master = 0 ORDER BY recipe_name ASC;";
        private const string GetSql = "SELECT * FROM meal_plan_by_user WHERE user_id = @userID;";
        private const string AddSql = "INSERT INTO meal_plan_by_user VALUES (@user_id, @plan_name); SELECT TOP 1 meal_plan_id FROM meal_plan_by_user ORDER BY meal_plan_id DESC;";
        private const string PlanDetailSql = "SELECT * FROM meal_plans JOIN recipes ON meal_plans.recipe_id = recipes.recipe_id WHERE meal_plan_id = @mealPlanId ORDER BY meal;";
        private const string GrocerySql = "SELECT count(ingredient_name) as total, ingredient_name, amount FROM ingredients JOIN ingred_by_recipe ON ingredients.ingredient_id = ingred_by_recipe.ingredient_id JOIN recipes ON ingred_by_recipe.recipe_id = recipes.recipe_id JOIN meal_plans ON recipes.recipe_id = meal_plans.recipe_id WHERE meal_plans.meal_plan_id = @mealPlanId GROUP BY ingredients.ingredient_name, amount;";
        private const string SQL_AddMealToPlan = "INSERT INTO meal_plans VALUES(@MealId, @DayNumber, @MealNumber, @RecipeId);";
        private const string SQL_DeleteMealFromPlan = "DELETE FROM meal_plans WHERE meal_plan_index = @id;";



        public MealPlanSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<MealPlan> GetPlans(int userID)
        {
            List<MealPlan> output = new List<MealPlan>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(GetSql, connection);
                    cmd.Parameters.AddWithValue("@userID", userID);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {

                        MealPlan user = new MealPlan();
                        user.UserId = Convert.ToInt32(reader["user_id"]);
                        user.MealPlanId = Convert.ToInt32(reader["meal_plan_id"]);
                        user.PlanName = Convert.ToString(reader["meal_plan_name"]);

                        output.Add(user);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            return output;
        }

        public int AddPlan(MealPlan newPlan, int userId)
        {
            try
            {


                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(AddSql, conn);
                    cmd.Parameters.AddWithValue("@user_id", userId);
                    cmd.Parameters.AddWithValue("@plan_name", newPlan.PlanName);

                    int newId = int.Parse(cmd.ExecuteScalar().ToString());

                    return (newId);
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }


        public List<MealPlan> GetPlanDetails(int mealPlanId)
        {
            List<MealPlan> output = new List<MealPlan>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(PlanDetailSql, connection);
                    cmd.Parameters.AddWithValue("@mealPlanId", mealPlanId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        MealPlan planDetails = new MealPlan();

                        planDetails.MealPlanId = Convert.ToInt32(reader["meal_plan_id"]);
                        planDetails.DayNumber = Convert.ToInt32(reader["day"]);
                        planDetails.MealNumber = Convert.ToInt32(reader["meal"]);
                        planDetails.RecipeId = Convert.ToInt32(reader["recipe_id"]);
                        planDetails.RecipeName = Convert.ToString(reader["recipe_name"]);
                        planDetails.MealPlanIndex = Convert.ToInt32(reader["meal_plan_index"]);

                        output.Add(planDetails);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            return output;

        }

        public List<RecipeModel> GetRecipes(int userID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    List<RecipeModel> output = new List<RecipeModel>();

                    connection.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetAllUserRecipes, connection);
                    cmd.Parameters.AddWithValue("@user_id", userID);

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

        public bool AddMealToPlan(MealPlan thisMeal)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(SQL_AddMealToPlan, connection);

                        cmd.Parameters.AddWithValue("@MealId", thisMeal.MealPlanId);
                        cmd.Parameters.AddWithValue("@DayNumber", thisMeal.DayNumber);
                        cmd.Parameters.AddWithValue("@MealNumber", thisMeal.MealNumber);
                        cmd.Parameters.AddWithValue("@RecipeId", thisMeal.RecipeId);
                   
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return (rowsAffected > 0);
                }             
            }

            catch (SqlException ex)
            {
                throw;
            }
        }

        public bool DeleteMealFromPlan(int id)
        {

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(SQL_DeleteMealFromPlan, connection);

                    cmd.Parameters.AddWithValue("@id", id);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return (rowsAffected > 0);
                }
            }

            catch (SqlException ex)
            {
                throw;
            }
        }

        public List<MealPlan> GroceryList(int mealPlanId)
        {
            List<MealPlan> output = new List<MealPlan>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(GrocerySql, connection);
                    cmd.Parameters.AddWithValue("@mealPlanId", mealPlanId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {

                        MealPlan grocery = new MealPlan();
                        grocery.Ingredient = Convert.ToString(reader["ingredient_name"]);
                        grocery.Count = Convert.ToInt32(reader["total"]);
                        grocery.Amount = Convert.ToString(reader["amount"]);

                        output.Add(grocery);
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


