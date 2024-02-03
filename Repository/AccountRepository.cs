using Hala.Models;
using Hala.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;

namespace Hala.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserService _userService;


        public AccountRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;

        }
        public async Task<IdentityResult> CreateUser(SignUpUser UserDetails)
        {
            //create a new object from the application user class to register a new user
            var user = new ApplicationUser()
            {
                FirstName = UserDetails.FirstName,
                LastName = UserDetails.LastName,
                NationalId = UserDetails.NationalId,
                Gender = UserDetails.Gender,
                UserName = UserDetails.Email,
                Email = UserDetails.Email,
                PhoneNumber = UserDetails.PhoneNumber
            };
            //pass user details to the CreateAsync method to create new user
            var result = await _userManager.CreateAsync(user, UserDetails.Password);
            return result;
        }

        public async Task<SignInResult> PasswordSignInAsync(SignInUser signInUser)
        {
            //if the user is approved by the admin then he is allowed to login
            var user = await _userManager.Users.Where(m => m.Email == signInUser.Email && m.IsApproved == null).SingleOrDefaultAsync();
            if (user != null)
            {
                return null;
            }
            return await _signInManager.PasswordSignInAsync(signInUser.Email, signInUser.Password, signInUser.RememberMe, false);
        }

        public async Task<List<EmployeesVM>> GetUsers()
        {
            //returns all users that are approved by the admin to use the system
            return await _userManager.Users.Select(user => new EmployeesVM()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                NationalId = user.NationalId,
                Gender = user.Gender,
                IsApproved = user.IsApproved
            }).Where(m => m.IsApproved == true).ToListAsync();
        }

        public async Task<EmployeesVM?> GetUser(string Id)
        {
            //return a single user based on the id that is being passed
            var user = await _userManager.Users.Where(m => m.NationalId == Id).SingleOrDefaultAsync();
            if (user == null)
            {
                return null;
            }
            var userClaims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            var model = new EmployeesVM
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                NationalId = user.NationalId,
                Gender = user.Gender,
                Claims = userClaims.Select(m => m.Value).ToList(),
                Roles = userRoles
            };
            return model;
        }

        public async Task<List<EmployeesVM>> GetNewUsers()
        {
            //returns all users that aren't approved by the admin to use the system
            return await _userManager.Users.Select(user => new EmployeesVM()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                NationalId = user.NationalId,
                Gender = user.Gender,
                IsApproved = user.IsApproved
            }).Where(m => m.IsApproved == null).ToListAsync();
        }

        public async Task<EmployeesVM?> GetNewEmployee(string Id)
        {
            //return a single user based on the id that is being passed and isn't approved by the admin
            var user = await _userManager.Users.Where(m => m.IsApproved == null).SingleOrDefaultAsync();
            if (user == null)
            {
                return null;
            }
            var userClaims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            var model = new EmployeesVM
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                NationalId = user.NationalId,
                Gender = user.Gender,
                Claims = userClaims.Select(m => m.Value).ToList(),
                Roles = userRoles
            };
            return model;
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}