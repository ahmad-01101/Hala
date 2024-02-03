using Hala.Models;
using Hala.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Hala.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public IActionResult Login() { 
        
            return View(); 
        }
        [HttpPost]
        public async Task<IActionResult> Login(SignInUser signInUser)
        {

            if (ModelState.IsValid)
            {
                var result = await _accountRepository.PasswordSignInAsync(signInUser);
                if (result == null)
                {
                    ModelState.AddModelError("", "Wait For Admin Approval");
                    return View(signInUser);
                }

                if (result.Succeeded)
                {
                    if (User.IsInRole("admin"))
                    {
                        return RedirectToAction("Employees", "Admin");
                    }
                    else
                    {
                        return RedirectToAction("Home", "Home");
                    }
                }
                ModelState.AddModelError("", "Invalid Credentials");

            }
            return View(signInUser);
            
        }

        public IActionResult Signup(bool isSuccess = false)
        {
            ViewBag.IsSuccess = isSuccess;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Signup(SignUpUser signUpUser)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountRepository.CreateUser(signUpUser);
                if (!result.Succeeded)
                {
                    foreach(var errorMessage in result.Errors)
                    {
                        ModelState.AddModelError("", errorMessage.Description);
                    }
                    return View(signUpUser);
                }
                ModelState.Clear();
                return RedirectToAction(nameof(Signup), new { isSuccess = true });
            }
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await _accountRepository.SignOutAsync();
            return RedirectToAction("Login", "account");
        }
    }
}
