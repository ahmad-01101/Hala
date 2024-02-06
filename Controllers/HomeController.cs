using Hala.Data;
using Hala.Models.Domain;
using Hala.Service;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Hala.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly HalaDbContext halaDbContext;
        private readonly IUserService _userService;

        public HomeController(HalaDbContext halaDbContext, IUserService userService)
        {
            _userService = userService;
            this.halaDbContext = halaDbContext;
        }

        public async Task<IActionResult> Attendance()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Attendance(Attendance attendance)
        {
            var currentUserId = _userService.GetUserId();

            var Attendance = await halaDbContext.Attendances.Where(m => m.UserId == currentUserId &&
            m.Date.Date == attendance.Date.Date).SingleOrDefaultAsync();

            return View(Attendance);
        }

        public async Task<IActionResult> Home(bool isCheckedIn = false, bool isCheckedOut = false, bool isCheckOut_WithOut_CheckingIn = false)
        {
            //ViewBag for successful message when the user check-in successfully
            ViewBag.IsCheckedIn = isCheckedIn;
            //ViewBag for successful message when the user check-Out successfully
            ViewBag.IsCheckedOut = isCheckedOut;
            //ViewBag for error message when the user check-out before checking-in
            ViewBag.IsCheckOut_WithOut_CheckingIn = isCheckOut_WithOut_CheckingIn;
            //get the current logged in user id
            var currentUserId = _userService.GetUserId();
            // returns today's attendance of the current User
            var CheckUser = await halaDbContext.Attendances.Where(m => m.UserId == currentUserId && m.Date.Date == DateTime.Today).SingleOrDefaultAsync();

            return View(CheckUser);
        }

        // This method will get executed if the user click the check-in button any time between 07:00 AM to 08:30 AM
        public async Task<IActionResult> CheckIn(Attendance attendance)
        {
            //get the current logged in user id
            var currentUserId = _userService.GetUserId();
            //check if the user check-in only thru check-in button not URL
            if (attendance.Isvalid == true)
            {
                // returns today's attendance of the current User
                var CheckUser = await halaDbContext.Attendances.Where(m => m.UserId == currentUserId && m.Date.Date == DateTime.Today).SingleOrDefaultAsync();
                //if the user didn't check-in today yet to create a new record attendance for him
                if (CheckUser == null)
                {
                    //creating a new object from attendance model class to insert a new attendance
                    var NewCheckIn = new Attendance
                    {

                        CheckedIn_at = DateTime.UtcNow.AddHours(+3),
                        Date = DateTime.UtcNow.AddHours(+3),
                        //Means the user check-in only thru check-in button not a URL, this column is added for extra security
                        Isvalid = attendance.Isvalid,
                        // Present Not Valid (PRN) means the user checked-in on time and didn’t checked-out
                        Status = "PRN",
                        UserId = currentUserId
                    };
                    await halaDbContext.AddAsync(NewCheckIn);
                    await halaDbContext.SaveChangesAsync();
                    // if the user checked-in successfully then Redirect to home with a successful message
                    return RedirectToAction(nameof(Home), new { isCheckedIn = true });
                }
            }

            return RedirectToAction(nameof(Home), new { Isvalid = false });
        }

        //this method will get executed if the user click the Late check-in button any time between 08:30 AM to 02:00 PM
        public async Task<IActionResult> LateCheckIn(Attendance attendance)
        {
            //get the current logged in user id
            var currentUserId = _userService.GetUserId();
            //check if the user check-in only thru check-in button not URL
            if (attendance.Isvalid == true)
            {
                // returns today's attendance of the current User
                var CheckUser = await halaDbContext.Attendances.Where(m => m.UserId == currentUserId && m.Date.Date == DateTime.Today).SingleOrDefaultAsync();
                //if the user didn't check-in today yet to create a new record attendance for him
                if (CheckUser == null)
                {
                    //creating a new object from attendance model class to insert a new attendance
                    var NewCheckIn = new Attendance
                    {

                        CheckedIn_at = DateTime.UtcNow.AddHours(+3),
                        Date = DateTime.UtcNow.AddHours(+3),
                        LateCheckedIn_reason = attendance.LateCheckedIn_reason,
                        //Means the user check-in only thru check-in button not a URL, this column is added for extra security
                        Isvalid = attendance.Isvalid,
                        // Late Not Valid (LAN) means the user checked-in late and didn’t checked-out
                        Status = "LAN",
                        UserId = currentUserId
                    };
                    await halaDbContext.AddAsync(NewCheckIn);
                    await halaDbContext.SaveChangesAsync();
                    // if the user checked-in successfully then Redirect to home with a successful message
                    return RedirectToAction(nameof(Home), new { isCheckedIn = true });
                }
            }

            return RedirectToAction(nameof(Home), new { Isvalid = false });
        }

        //this method will get executed if the user click the check-out button any time between 03:00 PM to 05:00 PM
        public async Task<IActionResult> CheckOut(Attendance attendance)
        {
            //get the current logged in user id
            var currentUserId = _userService.GetUserId();
            // returns today's attendance of the current User
            var CheckUser = await halaDbContext.Attendances.Where(m => m.UserId == currentUserId && m.Date.Date == DateTime.Today).SingleOrDefaultAsync();
            //check if the user check-out only thru check-out button not URL
            if (attendance.Isvalid == true)
            {
                //if checkuser is null that means he didn't check in yet and he will be prompted with a message saying you have to check-in first before checking-out
                if (CheckUser != null)
                {
                    if (CheckUser.Status == "PRN")
                    {
                        // Present Valid (PRV) means the user checked-in on time and checked-out on time
                        CheckUser.Status = "PRV";
                        CheckUser.CheckedOut_at = DateTime.UtcNow.AddHours(+3);
                        halaDbContext.Update(CheckUser);
                        await halaDbContext.SaveChangesAsync();
                        // if the user checked-out successfully then Redirect to home with a successful message
                        return RedirectToAction(nameof(Home), new { isCheckedOut = true });
                    }
                    else if (CheckUser.Status == "LAN")
                    {
                        // Late Valid (LAV) means the user checked-in late and checked-out on time
                        CheckUser.Status = "LAV";
                        CheckUser.CheckedOut_at = DateTime.UtcNow.AddHours(+3);
                        halaDbContext.Update(CheckUser);
                        await halaDbContext.SaveChangesAsync();
                        // if the user checked-out successfully then Redirect to home with a successful message
                        return RedirectToAction(nameof(Home), new { isCheckedOut = true });
                    }
                    else
                        return RedirectToAction(nameof(Home), new { isCheckedOut = false });
                }
                else
                {
                    return RedirectToAction(nameof(Home), new { isCheckOut_WithOut_CheckingIn = true });
                }
            }
            else
                return RedirectToAction(nameof(Home), new { Isvalid = false });
        }

        //this method will get executed if the user click the Early check-out button any time between 07:00 AM to 02:59 PM
        public async Task<IActionResult> EarlyCheckOut(Attendance attendance)
        {
            //get the current logged in user id
            var currentUserId = _userService.GetUserId();
            // returns today's attendance of the current User
            var CheckUser = await halaDbContext.Attendances.Where(m => m.UserId == currentUserId && m.Date.Date == DateTime.Today).SingleOrDefaultAsync();
            //check if the user check-out only thru check-out button not URL
            if (attendance.Isvalid == true)
            {
                //if checkuser is null that means he didn't check in yet and he will be prompted with a message saying you have to check-in first before checking-out
                if (CheckUser != null)
                {
                    if (CheckUser.Status == "PRN")
                    {
                        // Early Out Valid (EAV) means the user checked-in on time and checked-out early
                        CheckUser.Status = "EAV";
                        CheckUser.EarlyCheckedOut_reason = attendance.EarlyCheckedOut_reason;
                        CheckUser.CheckedOut_at = DateTime.UtcNow.AddHours(+3);
                        halaDbContext.Update(CheckUser);
                        await halaDbContext.SaveChangesAsync();
                        // if the user checked-out successfully then Redirect to home with a successful message
                        return RedirectToAction(nameof(Home), new { isCheckedOut = true });
                    }
                    else if (CheckUser.Status == "LAN")
                    {
                        // Late-In, Early-Out (LL) means the user checked-in late and checked-out early
                        CheckUser.Status = "LL";
                        CheckUser.EarlyCheckedOut_reason = attendance.EarlyCheckedOut_reason;
                        CheckUser.CheckedOut_at = DateTime.UtcNow.AddHours(+3);
                        halaDbContext.Update(CheckUser);
                        await halaDbContext.SaveChangesAsync();
                        // if the user checked-out successfully then Redirect to home with a successful message
                        return RedirectToAction(nameof(Home), new { isCheckedOut = true });
                    }
                    else
                        return RedirectToAction(nameof(Home), new { isCheckedOut = false });
                }
                else
                {
                    return RedirectToAction(nameof(Home), new { isCheckOut_WithOut_CheckingIn = true });
                }
            }
            return RedirectToAction(nameof(Home), new { Isvalid = false });
        }
    }
}
