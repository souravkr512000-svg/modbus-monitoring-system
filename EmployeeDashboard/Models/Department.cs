using System.ComponentModel.DataAnnotations;

namespace EmployeeDashboard.Models
{
    public class Department
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Department Name")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Display(Name = "Manager")]
        public int? ManagerId { get; set; }

        public Employee? Manager { get; set; }

        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
