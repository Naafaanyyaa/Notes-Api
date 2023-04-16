using FirstLab.Business.Interfaces;
using FirstLab.Business.Models.Request;
using FirstLab.Business.Models.Response;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web_App.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAuthenticateService _authenticateService;

        public UserController(IUserService userService, IAuthenticateService authenticateService)
        {
            _userService = userService;
            _authenticateService = authenticateService;
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterRequest userModel)
        {
            var result = await _userService.RegisterAsync(userModel);
            return RedirectToAction(nameof(UserController.RegisterComplete), result);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult RegisterComplete(RegisterResponse registerResponse)
        {
            return View(registerResponse);
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var claims = await _authenticateService.Login(loginRequest);

            //Initialize a new instance of the ClaimsIdentity with the claims and authentication scheme    
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            //Initialize a new instance of the ClaimsPrincipal with ClaimsIdentity    
            var principal = new ClaimsPrincipal(identity);
            //SignInAsync is a Extension method for Sign in a principal for the specified scheme.    
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //Редирект на главную страницу    
            return RedirectToAction(nameof(UserController.Unauthorized), "User");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Unauthorized()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserList()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction(nameof(UserController.Unauthorized), "User");
            var userList = await _userService.GetUserList();
            
            return View(userList);
        }

        [HttpGet]
        [Authorize]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest changePasswordRequest)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction(nameof(UserController.Unauthorized), "User");
            var claims = User.Claims;
            string[] leha_byv_tyt = new string[2];
            int i = 0;
            foreach (var claim in claims)
            {
                leha_byv_tyt[i] = claim.Value;
                i++;
            }
            var userList = await _userService.ChangePasswordAsync(changePasswordRequest, leha_byv_tyt[0], leha_byv_tyt[1]);

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

    }
}
