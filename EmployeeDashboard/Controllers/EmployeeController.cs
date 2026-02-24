using EmployeeDashboard.Models;
using EmployeeDashboard.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeDashboard.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public EmployeeController(
            IEmployeeService employeeService, 
            ILogger<EmployeeController> logger,
            UserManager<ApplicationUser> userManager)
        {
            _employeeService = employeeService;
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<IActionResult> Dashboard(string? search, int? department, EmploymentStatus? status, int page = 1)
        {
            try
            {
                var viewModel = await _employeeService.GetDashboardDataAsync(search, department, status, page, 10);
                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading dashboard");
                return View("Error");
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var employee = await _employeeService.GetEmployeeByIdAsync(id);
                if (employee == null)
                {
                    return NotFound();
                }
                return View(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading employee details");
                return View("Error");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin,HR")]
        public async Task<IActionResult> Create()
        {
            var departments = await _employeeService.GetAllDepartmentsAsync();
            ViewBag.Departments = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(departments, "Id", "Name");
            ViewBag.Managers = await _employeeService.GetAllEmployeesAsync();
            return View(new Employee());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,HR")]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _employeeService.CreateEmployeeAsync(employee);
                    TempData["SuccessMessage"] = "Employee created successfully!";
                    return RedirectToAction(nameof(Dashboard));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating employee");
                    ModelState.AddModelError("", "An error occurred while creating the employee.");
                }
            }

            var departments = await _employeeService.GetAllDepartmentsAsync();
            ViewBag.Departments = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(departments, "Id", "Name");
            ViewBag.Managers = await _employeeService.GetAllEmployeesAsync();
            return View(employee);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,HR")]
        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            var departments = await _employeeService.GetAllDepartmentsAsync();
            ViewBag.Departments = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(departments, "Id", "Name", employee.DepartmentId);
            ViewBag.Managers = await _employeeService.GetAllEmployeesAsync();
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,HR")]
        public async Task<IActionResult> Edit(int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _employeeService.UpdateEmployeeAsync(employee);
                    TempData["SuccessMessage"] = "Employee updated successfully!";
                    return RedirectToAction(nameof(Details), new { id = employee.Id });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error updating employee");
                    ModelState.AddModelError("", "An error occurred while updating the employee.");
                }
            }

            var departments = await _employeeService.GetAllDepartmentsAsync();
            ViewBag.Departments = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(departments, "Id", "Name", employee.DepartmentId);
            ViewBag.Managers = await _employeeService.GetAllEmployeesAsync();
            return View(employee);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,HR")]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        [Authorize(Roles = "Admin,HR")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _employeeService.DeleteEmployeeAsync(id);
                TempData["SuccessMessage"] = "Employee deleted successfully!";
                return RedirectToAction(nameof(Dashboard));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting employee");
                TempData["ErrorMessage"] = "An error occurred while deleting the employee.";
                return RedirectToAction(nameof(Details), new { id });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetStatistics()
        {
            try
            {
                var statistics = await _employeeService.GetStatisticsAsync();
                return Json(statistics);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading statistics");
                return StatusCode(500, new { error = "Failed to load statistics" });
            }
        }
    }
}
