using EmployeeDashboard.Models;

namespace EmployeeDashboard.Services
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee?> GetByIdAsync(int id);
        Task<IEnumerable<Employee>> SearchAsync(string searchTerm, int? departmentId, EmploymentStatus? status);
        Task<int> GetTotalCountAsync(string? searchTerm, int? departmentId, EmploymentStatus? status);
        Task<IEnumerable<Department>> GetAllDepartmentsAsync();
        Task CreateAsync(Employee employee);
        Task UpdateAsync(Employee employee);
        Task DeleteAsync(int id);
    }
}
