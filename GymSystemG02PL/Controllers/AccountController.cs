using GymSystemG02BLL.Services.Interfaces;
using GymSystemG02BLL.ViewModels.AccountViewModels;
using GymSystemG02DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GymSystemG02PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(IAccountService accountService,SignInManager<ApplicationUser> signInManager)
        {
            _accountService = accountService;
            _signInManager = signInManager;
        }
        #region Login Action
        //1. GET Login Action
        public ActionResult Login()
        {
            return View();
        }
        //2. POST Login Action
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if(!ModelState.IsValid) return View(model);

            var User = _accountService.ValidateUser(model);
            if (User == null)
            {
                ModelState.AddModelError("Invalid Model", "Invalid Email Or Password");
                return View(model);
            }
            //sign in if valid
            var Result = _signInManager.PasswordSignInAsync(User,model.Password,model.RememberMe,false).Result;

            if(Result.IsNotAllowed)
                ModelState.AddModelError("Not Allowed", "You Are Not Allowed To Sign In");
            if(Result.IsLockedOut)
                ModelState.AddModelError("Locked Out", "Your Account Is Locked Out");
            if(Result.Succeeded)
                return RedirectToAction("Index", "Home");

            return View(model);
        }

        #endregion

        #region Logout Action
        [HttpPost]
        public ActionResult Logout()
        {
            _signInManager.SignOutAsync().GetAwaiter().GetResult();
            return RedirectToAction("Login");
        }
        #endregion

        #region Access Denied Action
        public ActionResult AccessDenied()
        {
            return View();
        }
        #endregion
    }
}
