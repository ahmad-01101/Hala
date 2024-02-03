using System.ComponentModel.DataAnnotations;

namespace Hala.Models
{
    public class EmployeesVM
    {

        [Required(ErrorMessage = "Please enter your First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter your Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter your phone number")]
        [RegularExpression(@"^(?:\+?0*?966)?0?(5[0-9]{8})$", ErrorMessage = "Please give a valid phone number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please enter your National Id")]
        public string NationalId { get; set; }

        [Required(ErrorMessage = "Please enter your specify your gender")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Please enter your email")]
        [Display(Name = "Email address")]
        [EmailAddress(ErrorMessage = "Please enter a valid email")]
        public string Email { get; set; }
        public bool? IsApproved { get; set; }
        public List<string> Claims { get; set;}
        public IList<string> Roles { get; set;}

    }
}
