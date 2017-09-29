using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Models;
using Capstone.DAL;

namespace Capstone.Controllers
{
    public class RecipeController : HomeController
    {

        private string connectionString = @"Data Source=localhost\sqlexpress;Initial Catalog=MealPlanning;Integrated Security=True";

        public ActionResult AddRecipe()
        {
            ViewBag.Message = "Add a recipe to the database.";

            return View("AddRecipe");
        }

        [HttpPost]
        public ActionResult AddRecipe(RecipeModel recipe)
        {
            if (!ModelState.IsValid)
            {               
                ViewBag.Num = recipe.NumOfIngredients;
                return View("AddRecipe");
            }

            ViewBag.Message = "Recipe is being added!";
            RecipeSqlDAL recipeDAL = new RecipeSqlDAL(connectionString);
            recipeDAL.AddRecipe(recipe);

            return RedirectToAction("DisplayRecipes", "Recipe");
        }

        [HttpGet]
        public ActionResult EditRecipe(int id)
        {
            //ViewBag.Num = recipe.Ingredients.Count;
            RecipeSqlDAL recipeDAL = new RecipeSqlDAL(connectionString);
            RecipeModel recipe = recipeDAL.SelectOneRecipe(id);
            return View("EditRecipeDetail", recipe);

        }

        [HttpPost]
        public ActionResult EditRecipeDetails(RecipeModel recipe)
        {
            ViewBag.Num = recipe.Ingredients.Count;

            RecipeSqlDAL recipeDAL = new RecipeSqlDAL(connectionString);
            recipeDAL.EditRecipe(recipe);

            return RedirectToAction("UserLibrary", "Home");
        }



        public ActionResult DisplayRecipes()
        {
            RecipeSqlDAL recipeDAL = new RecipeSqlDAL(connectionString);

            List<RecipeModel> allRecipes = recipeDAL.SelectAllRecipes();

            return View("DisplayRecipes", allRecipes);
        }

        public ActionResult RecipeDetail(int id)
        {
            RecipeSqlDAL recipeDAL = new RecipeSqlDAL(connectionString);

            RecipeModel recipe = recipeDAL.SelectOneRecipe(id);

            return View("RecipeDetail", recipe);
        }

        public ActionResult CheckLoggedIn(RecipeModel recipe)
        {
            if (Session["userID"] != null)
            {
                //add recipe to user's library
                UserLibrariesDAL libraryDAL = new UserLibrariesDAL(connectionString);
               libraryDAL.AddRecipeCopy(int.Parse(Session["userID"].ToString()), recipe);

                return RedirectToAction("UserLibrary", "Home", recipe);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        
        public ActionResult DelRecFrmLib(RecipeModel recipe)
        {
            UserLibrariesDAL libraryDAL = new UserLibrariesDAL(connectionString);

            if (libraryDAL.UserLibaryContainsRecipe(recipe.Recipe_Id, int.Parse(Session["userID"].ToString())))
            {
                libraryDAL.DeleteRecipeFromLibrary(recipe.Recipe_Id, int.Parse(Session["userID"].ToString()));
            }
            return RedirectToAction("UserLibrary", "Home");
        }
    }
}