using Ardalis.GuardClauses;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProductStoreAsp.Models;
using ProductStoreAsp.Models.ViewModels;

namespace ProductStoreAsp.Controllers
{
    public class AuthController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthController(SignInManager<AppUser> signInManager,UserManager<AppUser> userManager,RoleManager<IdentityRole> roleManager) { 
            _signInManager = Guard.Against.Null(signInManager);
            _userManager = Guard.Against.Null(userManager);
            _roleManager = Guard.Against.Null(roleManager);
        }

        public IActionResult Login()
        {
            if (User!.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index", "Product");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is not null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Product");
                    }
                    ModelState.AddModelError("password", "Password is not valid");
                }
                else
                    ModelState.AddModelError("email", "Email is not valid");

            }
            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var oldUser = await _userManager.FindByEmailAsync(model.Email);
                if (oldUser is null)
                {
                    var newUser = model.Adapt<AppUser>();

                    if (!await _roleManager.RoleExistsAsync("User"))
                    {
                        var resultIdentity = await _roleManager.CreateAsync(new IdentityRole("User"));
                        if (!resultIdentity.Succeeded) throw new Exception(resultIdentity.Errors.First().Description);
                    }

                    var result = await _userManager.CreateAsync(newUser, model!.Password);

                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(newUser, "User");
                        return RedirectToAction("Index", "Product");
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> LogOutAsync()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
