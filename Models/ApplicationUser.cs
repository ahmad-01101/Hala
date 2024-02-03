using Microsoft.AspNetCore.Identity;

namespace Hala.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalId { get; set; }
        public string Gender { get; set; }
        public bool? IsApproved { get; set; }
    }
}