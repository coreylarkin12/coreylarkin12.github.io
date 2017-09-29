using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Models;
using Capstone.DAL;


namespace Capstone.Controllers
{
    
    public class HomeController : Controller
    {
        private string connectionString = @"Data Source=localhost\sqlexpress;Initial Catalog=MealPlanning;Integrated Security=True";
        //private const string UsernameKey = "email";
        //private string id;
        SignUpSqlDAL signUpDal;
 

        public HomeController()
        {
            signUpDal = new SignUpSqlDAL(connectionString);
        }

        public ActionResult Index()
        {
            return View("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View("About");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View("Contact");
        }
        public ActionResult LoggedIn()
        {
            return View("LoggedIn");
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            ViewBag.Message = "Sign up for a Food Fox account.";

            return View("SignUp");
        }

        [HttpPost]
        public ActionResult SignUp(Register model)
        {
           
            if (ModelState.IsValid)
            {
                var checkUsers = signUpDal.GetUser(model.Email);
                if (checkUsers.Email != null)
                {
                    ViewBag.ErrorMessage = "This email is unavailable.";
                    return View("SignUp", model);
                }
                var newUser = new Register
                {
                    Email = model.Email,
                    Password = model.Password
                };

                signUpDal.CreateUser(newUser);

                return RedirectToAction("LogIn", "Home");
            }
            else
            {
                return View("SignUp");
            }
        }

        //public string CurrentUser
        //{
        //    get
        //    {
        //        string username = string.Empty;

        //        //Check to see if user cookie exists, if not create it
        //        if (Session[UsernameKey] != null)
        //        {
        //            username = (string)Session[UsernameKey];
        //        }

        //        return username;
        //    }
        //}

        //public bool IsAuthenticated
        //{
        //    get
        //    {
        //        return Session[UsernameKey] != null;
        //    }
        //}

        //public void LogUserIn(string username)
        //{
        //    Session[UsernameKey] = CurrentUser;
        //}

        [HttpGet]
        public ActionResult Login()
        {
          
                return View("LogIn");
        }

        [HttpPost]
        public ActionResult Login(Login model)
        {
            
            LoginSqlDAL dal = new LoginSqlDAL(connectionString);

            var currentUser = dal.GetUser(model.Email);

            if (ModelState.IsValid)
            {
                ViewBag.Message = "Log in to your Food Fox account.";

                if (currentUser.Email == null || currentUser.Password == null)
                {
                    ModelState.AddModelError("invalid-user", "The email and or password is not valid");
                    return View("Login");
                }
                else if (currentUser.Email != model.Email)
                {
                    ModelState.AddModelError("invalid-user", "The email and or password is not valid");
                    return View("Login");
                }
                else if (currentUser.Password != model.Password)
                {
                    ModelState.AddModelError("invalid-user", "The email and or password is not valid");
                    return View("Login");
                }
            }
            else
            {
                ViewBag.Message = "The email and or password is not valid";
                return View("Login");
            }

            Session["userID"] = currentUser.UserId;
            TempData["confirmation"] = "Success, you're logged in!";

            return RedirectToAction("Index");
        }
   
        public ActionResult LogOut()
        {
            Session.Clear();
            Session.Abandon();
            
            return View("Index");
        }

        public ActionResult UserLibrary()
        {
            UserLibrariesDAL userDAL = new UserLibrariesDAL(connectionString);

            List<RecipeModel> recipes = userDAL.GetAllUserRecipes(int.Parse(Session["userID"].ToString()));

            return View("UserLibrary", recipes);
        }


        //[ChildActionOnly]
        //public ActionResult GetAuthenticatedUser()
        //{
        //    Login model = null;
        //    LoginSqlDAL user = new LoginSqlDAL(connectionString);
        //    if (IsAuthenticated)
        //    {
        //        model = user.GetUser(CurrentUser);
        //    }

        //    return View("Index", model);
        //}

    }
}