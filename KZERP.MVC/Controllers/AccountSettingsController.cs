using System.Security.Claims;
using KZERP.Identity.AppUser;
using KZERP.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace KZERP.MVC.Controllers
{
    [Authorize]
    public class AccountSettingsController : Controller
    {

        private UserManager<ApplicationUser> _userManager;

        public AccountSettingsController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }


        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) { return NotFound(); }
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null) { return NotFound(); }

            var model = new AccountSettingsViewModel
            {
                UserName = user.UserName!,
                Email = user.Email!,
                PhoneNumber = user.PhoneNumber! ?? ""
            };

            return View(model);
        }

        // POST: /AccountSettings/UpdateProfile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfile(AccountSettingsViewModel model)
        {
            if (!ModelState.IsValid) { return View("Index", model); }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId!);

            if (user == null) { return NotFound(); }

            user.UserName = model.UserName;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View("Index", model);
            }

            ViewBag.Message = "Profile updated succesfully.";

            return View("Index", model);


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid) { return View("Index", model); }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId!);

            if (user == null) { return NotFound(); }
            if (model.NewPassword == null) { return View("Index",model); }

            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

            if (!result.Succeeded)
            {
                TempData["PasswordErrors"] = string.Join(" ", result.Errors.Select(e => e.Description));
            }
            else
            {
                TempData["PasswordSuccess"] = "Password changed successfully.";
            }


            return RedirectToAction("Index");
        }

    }



}