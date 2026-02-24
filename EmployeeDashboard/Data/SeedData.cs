using EmployeeDashboard.Models;
using Microsoft.AspNetCore.Identity;

namespace EmployeeDashboard.Data
{
    public static class SeedData
    {
        public static async Task Initialize(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Create roles
            string[] roleNames = { "Admin", "HR", "Manager", "Employee" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Create default admin user
            if (!userManager.Users.Any())
            {
                var adminUser = new ApplicationUser
                {
                    UserName = "admin@company.com",
                    Email = "admin@company.com",
                    FirstName = "Admin",
                    LastName = "User",
                    EmailConfirmed = true,
                    IsActive = true,
                    CreatedDate = DateTime.Now
                };

                var result = await userManager.CreateAsync(adminUser, "Admin@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }

                // Create HR user
                var hrUser = new ApplicationUser
                {
                    UserName = "hr@company.com",
                    Email = "hr@company.com",
                    FirstName = "HR",
                    LastName = "Manager",
                    EmailConfirmed = true,
                    IsActive = true,
                    CreatedDate = DateTime.Now
                };

                result = await userManager.CreateAsync(hrUser, "Hr@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(hrUser, "HR");
                }

                // Create regular employee user
                var empUser = new ApplicationUser
                {
                    UserName = "employee@company.com",
                    Email = "employee@company.com",
                    FirstName = "John",
                    LastName = "Employee",
                    EmailConfirmed = true,
                    IsActive = true,
                    CreatedDate = DateTime.Now
                };

                result = await userManager.CreateAsync(empUser, "Emp@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(empUser, "Employee");
                }
            }

            if (context.Departments.Any())
            {
                return; // Database has been seeded
            }

            // Create Departments
            var departments = new[]
            {
                new Department { Id = 1, Name = "Engineering", Description = "Software Development and IT" },
                new Department { Id = 2, Name = "Human Resources", Description = "HR and Talent Management" },
                new Department { Id = 3, Name = "Sales", Description = "Sales and Business Development" },
                new Department { Id = 4, Name = "Marketing", Description = "Marketing and Communications" },
                new Department { Id = 5, Name = "Finance", Description = "Finance and Accounting" },
                new Department { Id = 6, Name = "Operations", Description = "Operations and Administration" }
            };

            context.Departments.AddRange(departments);
            context.SaveChanges();

            // Create Employees
            var employees = new[]
            {
                new Employee
                {
                    Id = 1,
                    EmployeeId = "EMP001",
                    FirstName = "John",
                    LastName = "Smith",
                    Email = "john.smith@company.com",
                    Phone = "+1-555-0101",
                    DateOfBirth = new DateTime(1985, 5, 15),
                    DepartmentId = 1,
                    Position = "Senior Software Engineer",
                    Salary = 120000,
                    HireDate = new DateTime(2020, 1, 15),
                    Status = EmploymentStatus.Active,
                    Address = "123 Tech Street",
                    City = "San Francisco",
                    State = "CA",
                    ZipCode = "94102",
                    Country = "USA",
                    Skills = "C#, .NET, ASP.NET Core, SQL Server, Azure",
                    Notes = "Excellent team player"
                },
                new Employee
                {
                    Id = 2,
                    EmployeeId = "EMP002",
                    FirstName = "Sarah",
                    LastName = "Johnson",
                    Email = "sarah.johnson@company.com",
                    Phone = "+1-555-0102",
                    DateOfBirth = new DateTime(1990, 8, 22),
                    DepartmentId = 2,
                    Position = "HR Manager",
                    Salary = 95000,
                    HireDate = new DateTime(2019, 3, 10),
                    Status = EmploymentStatus.Active,
                    Address = "456 HR Avenue",
                    City = "New York",
                    State = "NY",
                    ZipCode = "10001",
                    Country = "USA",
                    Skills = "Recruitment, Employee Relations, HRIS",
                    ManagerId = null
                },
                new Employee
                {
                    Id = 3,
                    EmployeeId = "EMP003",
                    FirstName = "Michael",
                    LastName = "Chen",
                    Email = "michael.chen@company.com",
                    Phone = "+1-555-0103",
                    DateOfBirth = new DateTime(1988, 12, 5),
                    DepartmentId = 1,
                    Position = "Lead Developer",
                    Salary = 140000,
                    HireDate = new DateTime(2018, 6, 1),
                    Status = EmploymentStatus.Active,
                    Address = "789 Code Lane",
                    City = "Seattle",
                    State = "WA",
                    ZipCode = "98101",
                    Country = "USA",
                    Skills = "Full Stack Development, Architecture, Team Leadership",
                    ManagerId = null
                },
                new Employee
                {
                    Id = 4,
                    EmployeeId = "EMP004",
                    FirstName = "Emily",
                    LastName = "Davis",
                    Email = "emily.davis@company.com",
                    Phone = "+1-555-0104",
                    DateOfBirth = new DateTime(1992, 3, 18),
                    DepartmentId = 3,
                    Position = "Sales Executive",
                    Salary = 85000,
                    HireDate = new DateTime(2021, 2, 1),
                    Status = EmploymentStatus.Active,
                    Address = "321 Sales Drive",
                    City = "Chicago",
                    State = "IL",
                    ZipCode = "60601",
                    Country = "USA",
                    Skills = "B2B Sales, CRM, Negotiation"
                },
                new Employee
                {
                    Id = 5,
                    EmployeeId = "EMP005",
                    FirstName = "David",
                    LastName = "Wilson",
                    Email = "david.wilson@company.com",
                    Phone = "+1-555-0105",
                    DateOfBirth = new DateTime(1987, 7, 30),
                    DepartmentId = 1,
                    Position = "Software Engineer",
                    Salary = 110000,
                    HireDate = new DateTime(2020, 8, 15),
                    Status = EmploymentStatus.Active,
                    Address = "654 Dev Boulevard",
                    City = "Austin",
                    State = "TX",
                    ZipCode = "78701",
                    Country = "USA",
                    Skills = "JavaScript, React, Node.js, Python",
                    ManagerId = 3
                },
                new Employee
                {
                    Id = 6,
                    EmployeeId = "EMP006",
                    FirstName = "Lisa",
                    LastName = "Anderson",
                    Email = "lisa.anderson@company.com",
                    Phone = "+1-555-0106",
                    DateOfBirth = new DateTime(1991, 11, 12),
                    DepartmentId = 4,
                    Position = "Marketing Manager",
                    Salary = 98000,
                    HireDate = new DateTime(2019, 9, 5),
                    Status = EmploymentStatus.Active,
                    Address = "987 Marketing Way",
                    City = "Los Angeles",
                    State = "CA",
                    ZipCode = "90001",
                    Country = "USA",
                    Skills = "Digital Marketing, SEO, Content Strategy"
                },
                new Employee
                {
                    Id = 7,
                    EmployeeId = "EMP007",
                    FirstName = "Robert",
                    LastName = "Taylor",
                    Email = "robert.taylor@company.com",
                    Phone = "+1-555-0107",
                    DateOfBirth = new DateTime(1986, 4, 25),
                    DepartmentId = 5,
                    Position = "Financial Analyst",
                    Salary = 88000,
                    HireDate = new DateTime(2020, 4, 1),
                    Status = EmploymentStatus.Active,
                    Address = "147 Finance Street",
                    City = "Boston",
                    State = "MA",
                    ZipCode = "02101",
                    Country = "USA",
                    Skills = "Financial Analysis, Excel, SQL"
                },
                new Employee
                {
                    Id = 8,
                    EmployeeId = "EMP008",
                    FirstName = "Jennifer",
                    LastName = "Martinez",
                    Email = "jennifer.martinez@company.com",
                    Phone = "+1-555-0108",
                    DateOfBirth = new DateTime(1993, 9, 8),
                    DepartmentId = 1,
                    Position = "Junior Developer",
                    Salary = 75000,
                    HireDate = new DateTime(2022, 1, 10),
                    Status = EmploymentStatus.Active,
                    Address = "258 Junior Road",
                    City = "Denver",
                    State = "CO",
                    ZipCode = "80201",
                    Country = "USA",
                    Skills = "C#, .NET, HTML, CSS",
                    ManagerId = 3
                },
                new Employee
                {
                    Id = 9,
                    EmployeeId = "EMP009",
                    FirstName = "James",
                    LastName = "Brown",
                    Email = "james.brown@company.com",
                    Phone = "+1-555-0109",
                    DateOfBirth = new DateTime(1989, 6, 14),
                    DepartmentId = 6,
                    Position = "Operations Manager",
                    Salary = 92000,
                    HireDate = new DateTime(2018, 11, 20),
                    Status = EmploymentStatus.Active,
                    Address = "369 Operations Avenue",
                    City = "Phoenix",
                    State = "AZ",
                    ZipCode = "85001",
                    Country = "USA",
                    Skills = "Process Improvement, Project Management"
                },
                new Employee
                {
                    Id = 10,
                    EmployeeId = "EMP010",
                    FirstName = "Amanda",
                    LastName = "White",
                    Email = "amanda.white@company.com",
                    Phone = "+1-555-0110",
                    DateOfBirth = new DateTime(1994, 2, 28),
                    DepartmentId = 2,
                    Position = "HR Specialist",
                    Salary = 68000,
                    HireDate = new DateTime(2021, 7, 1),
                    Status = EmploymentStatus.OnLeave,
                    Address = "741 HR Lane",
                    City = "Portland",
                    State = "OR",
                    ZipCode = "97201",
                    Country = "USA",
                    Skills = "Benefits Administration, Onboarding",
                    ManagerId = 2
                },
                new Employee
                {
                    Id = 11,
                    EmployeeId = "EMP011",
                    FirstName = "Christopher",
                    LastName = "Lee",
                    Email = "christopher.lee@company.com",
                    Phone = "+1-555-0111",
                    DateOfBirth = new DateTime(1984, 10, 3),
                    DepartmentId = 1,
                    Position = "DevOps Engineer",
                    Salary = 125000,
                    HireDate = new DateTime(2019, 5, 15),
                    Status = EmploymentStatus.Active,
                    Address = "852 DevOps Drive",
                    City = "San Jose",
                    State = "CA",
                    ZipCode = "95101",
                    Country = "USA",
                    Skills = "Docker, Kubernetes, AWS, CI/CD",
                    ManagerId = 3
                },
                new Employee
                {
                    Id = 12,
                    EmployeeId = "EMP012",
                    FirstName = "Michelle",
                    LastName = "Garcia",
                    Email = "michelle.garcia@company.com",
                    Phone = "+1-555-0112",
                    DateOfBirth = new DateTime(1990, 1, 20),
                    DepartmentId = 3,
                    Position = "Sales Manager",
                    Salary = 105000,
                    HireDate = new DateTime(2017, 8, 1),
                    Status = EmploymentStatus.Active,
                    Address = "963 Sales Street",
                    City = "Miami",
                    State = "FL",
                    ZipCode = "33101",
                    Country = "USA",
                    Skills = "Team Management, Strategic Planning"
                }
            };

            context.Employees.AddRange(employees);
            context.SaveChanges();
        }
    }
}
