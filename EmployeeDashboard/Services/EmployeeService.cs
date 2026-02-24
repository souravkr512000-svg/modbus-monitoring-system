using EmployeeDashboard.Models;

namespace EmployeeDashboard.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public async Task<DashboardViewModel> GetDashboardDataAsync(string? searchTerm, int? departmentId, EmploymentStatus? status, int page = 1, int pageSize = 10)
        {
            var totalRecords = await _repository.GetTotalCountAsync(searchTerm, departmentId, status);
            var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

            var allEmployees = await _repository.SearchAsync(searchTerm ?? string.Empty, departmentId, status);
            var employees = allEmployees.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var departments = (await _repository.GetAllDepartmentsAsync()).ToList();
            var statistics = await GetStatisticsAsync();

            return new DashboardViewModel
            {
                Employees = employees,
                Departments = departments,
                Statistics = statistics,
                SearchTerm = searchTerm,
                DepartmentFilter = departmentId,
                StatusFilter = status,
                CurrentPage = page,
                TotalPages = totalPages,
                PageSize = pageSize,
                TotalRecords = totalRecords
            };
        }

        public async Task<Employee?> GetEmployeeByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<DashboardStatistics> GetStatisticsAsync()
        {
            var employees = (await _repository.GetAllAsync()).ToList();
            var departments = (await _repository.GetAllDepartmentsAsync()).ToList();

            var statistics = new DashboardStatistics
            {
                TotalEmployees = employees.Count,
                ActiveEmployees = employees.Count(e => e.Status == EmploymentStatus.Active),
                OnLeaveEmployees = employees.Count(e => e.Status == EmploymentStatus.OnLeave),
                DepartmentsCount = departments.Count,
                AverageSalary = employees.Where(e => e.Salary.HasValue).Any() 
                    ? employees.Where(e => e.Salary.HasValue).Average(e => e.Salary!.Value) 
                    : 0
            };

            // Employees by Department
            foreach (var dept in departments)
            {
                var count = employees.Count(e => e.DepartmentId == dept.Id);
                if (count > 0)
                {
                    statistics.EmployeesByDepartment[dept.Name] = count;
                }
            }

            // Employees by Status
            foreach (EmploymentStatus status in Enum.GetValues(typeof(EmploymentStatus)))
            {
                var count = employees.Count(e => e.Status == status);
                if (count > 0)
                {
                    statistics.EmployeesByStatus[status.ToString()] = count;
                }
            }

            return statistics;
        }

        public async Task<IEnumerable<Department>> GetAllDepartmentsAsync()
        {
            return await _repository.GetAllDepartmentsAsync();
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task CreateEmployeeAsync(Employee employee)
        {
            await _repository.CreateAsync(employee);
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            await _repository.UpdateAsync(employee);
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
