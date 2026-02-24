using EmployeeDashboard.Data;
using EmployeeDashboard.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDashboard.Services
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.Manager)
                .ToListAsync();
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.Manager)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Employee>> SearchAsync(string searchTerm, int? departmentId, EmploymentStatus? status)
        {
            var query = _context.Employees
                .Include(e => e.Department)
                .Include(e => e.Manager)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                query = query.Where(e =>
                    e.FirstName.ToLower().Contains(searchTerm) ||
                    e.LastName.ToLower().Contains(searchTerm) ||
                    e.Email.ToLower().Contains(searchTerm) ||
                    e.EmployeeId.ToLower().Contains(searchTerm) ||
                    e.Position.ToLower().Contains(searchTerm));
            }

            if (departmentId.HasValue)
            {
                query = query.Where(e => e.DepartmentId == departmentId.Value);
            }

            if (status.HasValue)
            {
                query = query.Where(e => e.Status == status.Value);
            }

            return await query.OrderBy(e => e.LastName).ThenBy(e => e.FirstName).ToListAsync();
        }

        public async Task<int> GetTotalCountAsync(string? searchTerm, int? departmentId, EmploymentStatus? status)
        {
            var query = _context.Employees.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                query = query.Where(e =>
                    e.FirstName.ToLower().Contains(searchTerm) ||
                    e.LastName.ToLower().Contains(searchTerm) ||
                    e.Email.ToLower().Contains(searchTerm) ||
                    e.EmployeeId.ToLower().Contains(searchTerm) ||
                    e.Position.ToLower().Contains(searchTerm));
            }

            if (departmentId.HasValue)
            {
                query = query.Where(e => e.DepartmentId == departmentId.Value);
            }

            if (status.HasValue)
            {
                query = query.Where(e => e.Status == status.Value);
            }

            return await query.CountAsync();
        }

        public async Task<IEnumerable<Department>> GetAllDepartmentsAsync()
        {
            return await _context.Departments.OrderBy(d => d.Name).ToListAsync();
        }

        public async Task CreateAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            }
        }
    }
}
