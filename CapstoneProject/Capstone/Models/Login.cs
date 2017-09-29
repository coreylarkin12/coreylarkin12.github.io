using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;



namespace Capstone.Models
{
    public class Login
    {

        public int UserId { get; set; }

        [Required(ErrorMessage = "Please enter a valid email address.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }



        [Required(ErrorMessage = "Password must be at least 8 characters.")]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
       
    } 
}
