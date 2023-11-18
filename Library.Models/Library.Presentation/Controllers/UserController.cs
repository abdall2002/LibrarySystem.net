using Library.Presentation.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using Library.Models;

namespace Library.Presentation.Controllers
{
    public class UserController : Controller
    {
        UserManager<User> UserManager;
        SignInManager<User> SignInManager;
        RoleManager<IdentityRole> RoleManager;
        public UserController(UserManager<User> _UserManager,
            SignInManager<User> _SignInManager,
            RoleManager<IdentityRole> roleManager)
        {
            UserManager = _UserManager;

            SignInManager = _SignInManager;
            RoleManager = roleManager;
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(UserCreateModel model)
        {
            if (ModelState.IsValid == false)
                return View(); 
            else
            {
                User user = new User()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = model.UserName,
                    Email = model.Email
                };
                IdentityResult result
                      = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded == false)
                {
                    //List<string> errors = new List<string>();
                    //foreach (var error in result.Errors)
                    //{
                    //    errors.Add(error.Description);
                    //}
                    //foreach(var err in errors)
                    //{
                    //    ModelState.AddModelError("", err);
                    //}
                    result.Errors.ToList().ForEach(i =>
                    {
                        ModelState.AddModelError("", i.Description);
                    });
                    return View();
                }
                else
                {
                    return RedirectToAction("Index","Home");
                }
                
                
                    
                
            }
        }
        [HttpGet]
        public IActionResult SignIn(string ReturnUrl = null)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(LoginModel model)
        {
            if (ModelState.IsValid == false)
                return View();
            else
            {                
                var result
                    = await SignInManager.PasswordSignInAsync
                        (model.UserName, model.Password, model.RememberMe,
                            true);
                if (result.Succeeded == false)
                {
                    ModelState.AddModelError("", "Invalid User Name Of Password");
                    return View();
                }
                else if (result.IsLockedOut == true)
                {
                    ModelState.AddModelError("", "You're Locked Out Please Try Again After 20 Minute");
                    return View();
                }
                else
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl))
                        return LocalRedirect(model.ReturnUrl);
                    else
                        return RedirectToAction("Index", "Home");
                }
            }
        }
        [HttpGet]
        //signout
        public new async Task<IActionResult> SignOut()
        {
            //threating 
            //Sign out
            //Get User Using Cookies
            await SignInManager.SignOutAsync();
            return RedirectToAction("SignIn", "User");

        }
    }
}
