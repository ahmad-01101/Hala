using Hala.Models;
using Microsoft.AspNetCore.Identity;

namespace Hala.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> CreateUser(SignUpUser signUpUser);
        Task<List<EmployeesVM>> GetUsers();
        Task<EmployeesVM> GetUser(string Id);
        Task<List<EmployeesVM>> GetNewUsers();
        Task<EmployeesVM?> GetNewEmployee(string Id);
        Task<SignInResult> PasswordSignInAsync(SignInUser signInUser);
        Task SignOutAsync();

    }
}