using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.DAL;

namespace Capstone.Models
{

    public class MealPlan
    {
 
        public int RecipeId { get; set; }
        public string PlanName { get; set; }
        public int DayNumber { get; set; }
        public int MealNumber { get; set; }
        //this meal Plan ID stays the same for every mealplan item in the Meal Plan
        public int MealPlanId { get; set; }
        public int UserId { get; set; }
        public int MealPlanIndex { get; set; }     
        public string RecipeName { get; set; }
        public string Ingredient { get; set; }
        public int Count { get; set; }
        public string Amount { get; set; }
        public string GetDays()
        {
            if (DayNumber == 1)
            {
                return "Monday";
            }
            else if (DayNumber == 2)
            {
                return "Tuesday";
            }
            else if (DayNumber == 3)
            {
                return "Wednesday";
            }
            else if (DayNumber == 4)
            {
                return "Thursday";
            }
            else if (DayNumber == 5)
            {
                return "Friday";
            }
            else if (DayNumber == 6)
            {
                return "Saturday";
            }
            else
            {
                return "Sunday";
            }
        }

        public string GetMealNumber()
        {
            if (MealNumber == 1)
            {
                return "Breakfast";
            }
            else if (MealNumber == 2)
            {
                return "Lunch";
            }
            else
            {
                return "Dinner";
            }
        }

    }
}
