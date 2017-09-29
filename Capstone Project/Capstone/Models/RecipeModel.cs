using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Capstone.Models
{
    public class RecipeModel
    {
        [Required(ErrorMessage = "Please Enter a Recipe Name")]
        [StringLength(maximumLength: 100, ErrorMessage = "Recipe Name is too long;")]
        public string Name
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Please Enter a Description")]
        [StringLength(maximumLength:1000, ErrorMessage = "Description is too long;")]
        public string Description
        {
            get;
            set;
        }

        //[Required(ErrorMessage = "Please Enter at Least One Ingredient")]
        public List<string> Ingredients
        {
            get;
            set;
        } = new List<string>() { };

        [Range(minimum:1, maximum: 100, ErrorMessage = "Please Enter at Least One Ingredient")]
        public int NumOfIngredients
        {
            get
            {
                return Ingredients.Count(s => s.Any());
            }
        }

        public List<string> Amount
        {
            get;
            set;
        } = new List<string>() { };

        [Required(ErrorMessage = "Please Enter Preparation Steps")]
        [StringLength(maximumLength: 500, ErrorMessage = "Preparation Steps are too long;")]
        public string Preparation
        {
            get;
            set;
        }

        public int Recipe_Id
        {
            get;
            set;
        }

        public int Is_Master
        {
            get;
            set;
        }
    }
}