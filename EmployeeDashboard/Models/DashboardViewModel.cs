namespace EmployeeDashboard.Models
{
    public class DashboardViewModel
    {
        public List<Employee> Employees { get; set; } = new();
        public List<Department> Departments { get; set; } = new();
        public DashboardStatistics Statistics { get; set; } = new();
        public string? SearchTerm { get; set; }
        public int? DepartmentFilter { get; set; }
        public EmploymentStatus? StatusFilter { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }
        public int PageSize { get; set; } = 10;
        public int TotalRecords { get; set; }
    }

    public class DashboardStatistics
    {
        public int TotalEmployees { get; set; }
        public int ActiveEmployees { get; set; }
        public int OnLeaveEmployees { get; set; }
        public int DepartmentsCount { get; set; }
        public decimal AverageSalary { get; set; }
        public Dictionary<string, int> EmployeesByDepartment { get; set; } = new();
        public Dictionary<string, int> EmployeesByStatus { get; set; } = new();
    }
}
