using EmployeeDashboard.Models;

namespace EmployeeDashboard.Services
{
    public interface IEmployeeService
    {
        Task<DashboardViewModel> GetDashboardDataAsync(string? searchTerm, int? departmentId, EmploymentStatus? status, int page = 1, int pageSize = 10);
        Task<Employee?> GetEmployeeByIdAsync(int id);
        Task<DashboardStatistics> GetStatisticsAsync();
        Task<IEnumerable<Department>> GetAllDepartmentsAsync();
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        Task CreateEmployeeAsync(Employee employee);
        Task UpdateEmployeeAsync(Employee employee);
        Task DeleteEmployeeAsync(int id);
    }
}
