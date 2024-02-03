using Hala.Models;
using Hala.Models.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Hala.Data
{
    public class HalaDbContext : IdentityDbContext<ApplicationUser>
    {
        public HalaDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Attendance> Attendances { get; set; }
    }
}