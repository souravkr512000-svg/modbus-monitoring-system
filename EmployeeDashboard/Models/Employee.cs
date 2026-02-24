using System.ComponentModel.DataAnnotations;

namespace EmployeeDashboard.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Employee ID")]
        public string EmployeeId { get; set; } = string.Empty;

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Phone")]
        public string? Phone { get; set; }

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [Required]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        public Department? Department { get; set; }

        [Required]
        [Display(Name = "Position")]
        public string Position { get; set; } = string.Empty;

        [Display(Name = "Salary")]
        [DataType(DataType.Currency)]
        public decimal? Salary { get; set; }

        [Display(Name = "Hire Date")]
        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; } = DateTime.Now;

        [Display(Name = "Status")]
        public EmploymentStatus Status { get; set; } = EmploymentStatus.Active;

        [Display(Name = "Address")]
        public string? Address { get; set; }

        [Display(Name = "City")]
        public string? City { get; set; }

        [Display(Name = "State")]
        public string? State { get; set; }

        [Display(Name = "Zip Code")]
        public string? ZipCode { get; set; }

        [Display(Name = "Country")]
        public string? Country { get; set; } = "USA";

        [Display(Name = "Manager")]
        public int? ManagerId { get; set; }

        public Employee? Manager { get; set; }

        [Display(Name = "Skills")]
        public string? Skills { get; set; }

        [Display(Name = "Notes")]
        public string? Notes { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }

    public enum EmploymentStatus
    {
        Active,
        OnLeave,
        Terminated,
        Retired
    }
}
