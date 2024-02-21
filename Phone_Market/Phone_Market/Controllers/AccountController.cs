using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Phone_Market.Enums;
using System.Security.Claims;
using Phone_Market.Models;
using Phone_Market.Services;
using Phone_Market.Code;
using Phone_Market.DTO;

namespace Phone_Market.Controllers
{
    public class AccountController : BaseController
    {
        private readonly Account_Service account_Service;

        public AccountController(ControllerDependencies dependencies, Account_Service account_Service) : base(dependencies) {
            this.account_Service = account_Service;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login(string returnUrl = "/")
        {
            return View(new LoginModel { ReturnUrl = returnUrl });

        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var user = account_Service.GetUserByEmailAndPassword(model);
            if (user == null)
            {
                return View(model);
            }
            
            var claims = new List<Claim>();

            claims.AddRange(new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Role, user.RoleId.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            });

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);


            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
            new AuthenticationProperties { IsPersistent = model.RememberLogin });

            return LocalRedirect(model.ReturnUrl);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserModel model)
        {
            var credential = account_Service.IsAccount(model.Email);
            if (credential != false)
            {
                ModelState.AddModelError(nameof(model.Email), "Email already exists");
                return View(model);
            }
            account_Service.RegisterUser(model);
            return Redirect("/");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }

        [Authorize(Policy = "User")]
        public IActionResult MyFavourites()
        {
            var favourites = account_Service.GetMyFavourites(CurrentUser.Id);
            return View(favourites);
        }

        [Authorize(Policy = "User")]
        [HttpGet]
        public IActionResult MyUserProfile(Guid userId)
        {
            if (userId == CurrentUser.Id)
            {
                var user = account_Service.GetMyUserProfileModelById(userId);
                return View(user);
            }
            throw new UnauthorizedAccessException();

        }
        [Authorize(Policy = "User")]
        [HttpPost]
        public IActionResult MyUserProfile(MyUserProfileModel model)
        {

            if (model.Id == CurrentUser.Id)
            {
                if (model.CurrentPassword != null && account_Service.IsUserPasswordCorrect(model.Id, model.CurrentPassword))
                {
                    account_Service.UpdateMyUserProfile(model);
                    return Redirect("/");
                }
                ModelState.AddModelError(nameof(model.CurrentPassword), "Password Incorect");
                return View(model);

            }
            throw new UnauthorizedAccessException();
        }
    }
}
