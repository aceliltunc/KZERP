using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using KZERP.Identity.Services;
using KZERP.MVC.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace KZERP.MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        // LOGIN 
        // LOGIN GET
        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identities != null && User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }


        // LOGIN POST
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var loginResult = await _authService.LoginAsync(new LoginDto
            {
                UsernameOrEmail = model.UsernameOrEmail,
                Password = model.Password
            });

            bool ok = loginResult.ok;
            string? token = loginResult.token;
            string? error = loginResult.error;


            if (!ok)
            {
                ModelState.AddModelError("", error ?? "Login failed");
                return View(model);
            }

            // Token decode
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token!);

            var claims = new List<Claim> { new Claim("AccessToken", token!) };
            claims.AddRange(jwt.Claims);

            var identity = new ClaimsIdentity(
                claims,
                "KZERP_Cookie",
                ClaimTypes.Name,
                ClaimTypes.Role);

            var principal = new ClaimsPrincipal(identity);

            // Cookie’ye ekle
            await HttpContext.SignInAsync("KZERP_Cookie", principal);

            return RedirectToAction("Index", "Home");
        }

        // REGISTER
        // REGISTER GET
        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // REGISTER POST
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var registerResult = await _authService.RegisterAsync(new RegisterDto
            {
                Username = model.Username,
                Email = model.Email,
                Password = model.Password,
                FullName = model.FullName,
                JobTitle = model.JobTitle,
                Department = model.Department
            });

            bool ok = registerResult.ok;
            var errors = registerResult.errors;

            if (!ok)
            {
                foreach (var e in errors)
                    ModelState.AddModelError("", e);
                return View(model);
            }

            return RedirectToAction("Login");
        }


        // LOGOUT
        // LOGOUT POST
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("KZERP_Cookie");
            return RedirectToAction("Login", "Auth");
        }



    }
    public class LoginResponse
    {
        public string? Token { get; set; }
    }
}