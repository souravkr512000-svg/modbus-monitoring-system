using Microsoft.AspNetCore.Identity;

namespace EmployeeDashboard.Models
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public string? FirstName { get; set; }

        [PersonalData]
        public string? LastName { get; set; }

        [PersonalData]
        public string? ProfilePicture { get; set; }

        [PersonalData]
        public DateTime? CreatedDate { get; set; } = DateTime.Now;

        [PersonalData]
        public bool IsActive { get; set; } = true;

        public string FullName => $"{FirstName} {LastName}".Trim();
    }
}
