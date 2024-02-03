using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hala.Models.Domain
{
    public class Attendance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public DateTime CheckedIn_at { get; set; }
        public string? LateCheckedIn_reason { get; set; }
        public DateTime CheckedOut_at { get; set; }
        public string? EarlyCheckedOut_reason { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public string? Address { get; set; }
        public string? JobTitle { get; set; }
        public bool Isvalid { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
