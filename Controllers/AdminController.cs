using Hala.Data;
using Hala.Models;
using Hala.Models.Domain;
using Hala.Repository;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Hala.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly HalaDbContext halaDbContext;
        private readonly IAccountRepository _accountRepository;
        private readonly UserManager<ApplicationUser> _userManager;


        public AdminController(HalaDbContext halaDbContext, UserManager<ApplicationUser> userManager, IAccountRepository accountRepository)
        {
            this.halaDbContext = halaDbContext;
            _userManager = userManager;
            _accountRepository = accountRepository;
        }
        public async Task<IActionResult> Employees(string Id, bool isDeleteed = false, bool isAdded = false)
        {
            //ViewBag for successful message after deleting a user
            ViewBag.IsDeleteed = isDeleteed;
            //ViewBag for successful message after adding a user
            ViewBag.IsAdded = isAdded;
            //returns all users that are approved by the admin to use the system
            var Users = await _accountRepository.GetUsers();

            return View(Users);
        }

        public async Task<IActionResult> NotFound()
        {
            return View();
        }

        public async Task<IActionResult> GetEmployee(string Id, bool isUpdated = false)
        {
            //ViewBag for successful message after updating a user
            ViewBag.IsUpdated = isUpdated;
            //return a single user based on the id that is being passed
            var User = await _accountRepository.GetUser(Id);
            if (User == null)
            {
                //if there is no user with pass id return to not found view
                return RedirectToAction(nameof(NotFound));
            }
            return View(User);
        }

        public async Task<IActionResult> UpdateEmployee(string Id)
        {
            //return a single user based on the id that is being passed
            var User = await _accountRepository.GetUser(Id);
            if (User == null)
            {
                //if there is no user with pass id return to not found view
                return RedirectToAction(nameof(NotFound));
            }
            return View(User);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateEmployee(EmployeesVM employeesVM, string Id)
        {
            //update the employee details to the new passed details
            var User = await _userManager.Users.Where(m => m.NationalId == Id).SingleOrDefaultAsync();
            User.FirstName = employeesVM.FirstName;
            User.LastName = employeesVM.LastName;
            User.Email = employeesVM.Email;
            User.PhoneNumber = employeesVM.PhoneNumber;
            User.Gender = employeesVM.Gender;
            User.NationalId = employeesVM.NationalId;

            await _userManager.UpdateAsync(User);
            //Redirect the user to GetEmployee with a successful message
            return RedirectToAction(nameof(GetEmployee), new { Id = employeesVM.NationalId, isUpdated = true });
        }
        [HttpPost]
        public async Task<IActionResult> DeleteEmployee(EmployeesVM employeesVM)
        {
            if (employeesVM.NationalId == null)
            {
                //if there is no user with pass id return to not found view
                return RedirectToAction(nameof(NotFound));
            }
            //return a single user based on the id that is being passed
            var User = await _userManager.Users.Where(m => m.NationalId == employeesVM.NationalId).SingleOrDefaultAsync();
            await _userManager.DeleteAsync(User);
            //Redirect the user to Employee with a successful message
            return RedirectToAction(nameof(Employees), new { isDeleteed = true });
        }

        public async Task<IActionResult> AddEmployees()
        {
            //get all the users that is not approved by admin
            var Users = await _accountRepository.GetNewUsers();

            return View(Users);
        }

        public async Task<IActionResult> GetNewEmployee(string Id)
        {
            //get a single based on the id that is being passed and isn't approved by admin
            var User = await _accountRepository.GetNewEmployee(Id);
            if (User == null)
            {
                //if there is no user with pass id return to not found view
                return RedirectToAction(nameof(NotFound));
            }
            return View(User);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(EmployeesVM employeesVM)
        {
            //approve a new user by setting the IsApproved property to true to let the user log into the system
            var User = await _userManager.Users.Where(m => m.NationalId == employeesVM.NationalId).SingleOrDefaultAsync();
            User.NationalId = employeesVM.NationalId;
            User.IsApproved = employeesVM.IsApproved;
            await _userManager.UpdateAsync(User);
            //Redirect the user to Employee with a successful message
            return RedirectToAction(nameof(Employees), new { isAdded = true });
        }

        public async Task<IActionResult> Attendance()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Attendance(Attendance attendance)
        {

            //return a specific attendance for a specific employee  based on the id and date that is being passed
            var Attendance = await halaDbContext.Attendances.Include(m => m.ApplicationUser)
            .Where(m => m.ApplicationUser.NationalId == attendance.ApplicationUser.NationalId
            && m.Date.Date == attendance.Date.Date).SingleOrDefaultAsync();

            return View(Attendance);
        }

    }
}