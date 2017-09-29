using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Models;
using Capstone.DAL;


namespace Capstone.Controllers
{
    public class MealPlanController : HomeController
    {
        // GET: MealPlan
        private string connectionString = @"Data Source=localhost\sqlexpress;Initial Catalog=MealPlanning;Integrated Security=True";

       
        public ActionResult DisplayPlans()
        {
            MealPlanSqlDAL dal = new MealPlanSqlDAL(connectionString);

            
            List<MealPlan> model = dal.GetPlans(int.Parse(Session["userID"].ToString()));

            return View("DisplayPlans", model);
        }


        public ActionResult AddPlan(string id)
        {
            MealPlan newPlan = new MealPlan();

            newPlan.PlanName = id;

            MealPlanSqlDAL add = new MealPlanSqlDAL(connectionString);

            add.AddPlan(newPlan, int.Parse(Session["userID"].ToString()));

            return RedirectToAction("DisplayPlans");
        }

        [HttpGet]
        public ActionResult EditPlan(int id)
        {
            UserLibrariesDAL dal = new UserLibrariesDAL(connectionString);
            List<RecipeModel> dalList = dal.GetAllUserRecipes(int.Parse(Session["userID"].ToString()));

            if(dalList.Count == 0)
            {
                return RedirectToAction("DisplayRecipes", "Recipe");
            }

            List<SelectListItem> recipes = new List<SelectListItem>();
            foreach (RecipeModel recipe in dalList)
            {
                recipes.Add(new SelectListItem { Selected = false, Text = recipe.Name, Value = recipe.Recipe_Id.ToString() });
            }
            ViewBag.List = recipes;

            List<SelectListItem> DayNums = new List<SelectListItem>();
            DayNums.Add(new SelectListItem { Value = "1", Text = "Monday" });
            DayNums.Add(new SelectListItem { Value = "2", Text = "Tuesday" });
            DayNums.Add(new SelectListItem { Value = "3", Text = "Wednesday" });
            DayNums.Add(new SelectListItem { Value = "4", Text = "Thursday" });
            DayNums.Add(new SelectListItem { Value = "5", Text = "Friday" });
            DayNums.Add(new SelectListItem { Value = "6", Text = "Saturday" });
            DayNums.Add(new SelectListItem { Value = "7", Text = "Sunday" });
            ViewBag.Days = DayNums;

            List<SelectListItem> MealNums = new List<SelectListItem>();
            MealNums.Add(new SelectListItem { Value = "1", Text = "Breakfast" });
            MealNums.Add(new SelectListItem { Value = "2", Text = "Lunch" });
            MealNums.Add(new SelectListItem { Value = "3", Text = "Dinner" });
            ViewBag.MealNumbers = MealNums;

            MealPlan thisPlan = new MealPlan();
            thisPlan.MealPlanId = id;
            return View("EditPlan", thisPlan);
        }

        [HttpPost] 
        public ActionResult AddMealToPlan(MealPlan thisMeal)
        {
            MealPlanSqlDAL dal = new MealPlanSqlDAL(connectionString);
            dal.AddMealToPlan(thisMeal);
            int id = thisMeal.MealPlanId;

            return RedirectToAction("PlanDetails",  new { id = id });
        }

        [HttpGet]
        public ActionResult DeleteMealPlan(int id)
        {
            //code here that deletes entire meal plan based off of the int id (the id is the meal plan id)

            MealPlanSqlDAL dal = new MealPlanSqlDAL(connectionString);
            List<MealPlan> model = dal.GetPlans(int.Parse(Session["userID"].ToString()));

            return RedirectToAction("DisplayPlans", model);
        }

        public ActionResult PlanDetails(int id)
        {
            MealPlanSqlDAL planDetails = new MealPlanSqlDAL(connectionString);
            //List<MealPlan> model = planDetails.GetPlanDetails(int.Parse(Session["userID"].ToString()));
            List<MealPlan> model = planDetails.GetPlanDetails(id);
            ViewBag.Item = id;

            return View("PlanDetails", model);
        }

        public ActionResult DeleteMealFromPlan(int id, int mealPlanId)
        {
            MealPlanSqlDAL dal = new MealPlanSqlDAL(connectionString);
            dal.DeleteMealFromPlan(id);

            return RedirectToAction("PlanDetails", new { id = mealPlanId });       
        }

        public ActionResult GroceryList(int id)
        {
            MealPlanSqlDAL grocery = new MealPlanSqlDAL(connectionString);
            List<MealPlan> list = grocery.GroceryList(id);
            return View("GroceryList", list);
        }
    }
}