# Employee Records Dashboard

A comprehensive, modern employee management dashboard built with ASP.NET Core MVC. This application demonstrates professional .NET development practices including clean architecture, repository pattern, dependency injection, and modern UI/UX design.

## Features

### 🎯 Core Functionality
- **Employee Dashboard**: Centralized view of all employee records
- **Advanced Search & Filtering**: Search by name, email, ID, or position; filter by department and status
- **Employee Details**: Comprehensive employee profile pages with all information
- **Statistics & Analytics**: Real-time statistics with visual charts
- **Pagination**: Efficient data pagination for large datasets
- **Responsive Design**: Works seamlessly on desktop, tablet, and mobile devices

### 📊 Dashboard Features
- **Statistics Cards**: Total employees, active employees, departments count, and average salary
- **Interactive Charts**: 
  - Pie chart showing employees by department
  - Bar chart showing employees by employment status
- **Employee Table**: Sortable, searchable table with key employee information
- **Quick Actions**: View detailed employee information with one click

### 🏗️ Architecture
- **Clean Architecture**: Separation of concerns with Models, Views, Controllers, Services, and Data layers
- **Repository Pattern**: Abstracted data access layer for maintainability
- **Dependency Injection**: Proper IoC container usage
- **Entity Framework Core**: ORM for data access (using In-Memory database for demo)
- **Service Layer**: Business logic separated from controllers

## Technology Stack

- **.NET 8.0**: Latest .NET framework
- **ASP.NET Core MVC**: Web framework
- **Entity Framework Core**: ORM
- **Bootstrap 5**: Responsive UI framework
- **Chart.js**: Interactive charts and graphs
- **Font Awesome**: Icon library
- **jQuery**: JavaScript library for DOM manipulation

## Prerequisites

Before running this application, ensure you have:

1. **.NET 8.0 SDK** installed
   - Download from: https://dotnet.microsoft.com/download/dotnet/8.0
   - Verify installation: `dotnet --version`

2. **Visual Studio 2022** (recommended) or **Visual Studio Code**
   - Visual Studio: https://visualstudio.microsoft.com/
   - VS Code: https://code.visualstudio.com/

## Getting Started

### 1. Clone or Navigate to Project Directory
```bash
cd "c:\Users\skumar\OneDrive - Rugged Monitoring\Desktop\MY profile"
```

### 2. Restore Dependencies
```bash
dotnet restore
```

### 3. Build the Project
```bash
dotnet build
```

### 4. Run the Application
```bash
dotnet run --project EmployeeDashboard
```

Or using Visual Studio:
- Open `EmployeeDashboard.sln`
- Press F5 or click "Run"

### 5. Access the Application
- Navigate to: `https://localhost:5001` or `http://localhost:5000`
- The dashboard will automatically load with sample employee data

## Project Structure

```
EmployeeDashboard/
├── Controllers/
│   └── EmployeeController.cs      # Main controller handling requests
├── Data/
│   ├── ApplicationDbContext.cs    # EF Core DbContext
│   └── SeedData.cs                # Initial data seeding
├── Models/
│   ├── Employee.cs                # Employee entity model
│   ├── Department.cs              # Department entity model
│   └── DashboardViewModel.cs      # View model for dashboard
├── Services/
│   ├── IEmployeeService.cs        # Service interface
│   ├── EmployeeService.cs         # Service implementation
│   ├── IEmployeeRepository.cs    # Repository interface
│   └── EmployeeRepository.cs      # Repository implementation
├── Views/
│   ├── Employee/
│   │   ├── Dashboard.cshtml       # Main dashboard view
│   │   └── Details.cshtml        # Employee details view
│   └── Shared/
│       ├── _Layout.cshtml         # Main layout
│       └── Error.cshtml           # Error page
├── wwwroot/
│   ├── css/
│   │   └── site.css              # Custom styles
│   └── js/
│       └── site.js                # Custom JavaScript
├── Program.cs                     # Application entry point
└── appsettings.json              # Configuration
```

## Key Features Explained

### Search & Filter
- **Search**: Enter any text to search across employee names, emails, IDs, or positions
- **Department Filter**: Filter employees by specific department
- **Status Filter**: Filter by employment status (Active, On Leave, Terminated, Retired)
- **Combined Filters**: All filters work together for precise results

### Employee Details
Each employee detail page includes:
- Personal information (name, email, phone, DOB)
- Employment details (department, position, status, hire date, salary)
- Address information
- Skills and competencies
- Manager information
- Years of service calculation

### Statistics
Real-time statistics include:
- Total employee count
- Active employees count
- Number of departments
- Average salary calculation
- Visual representation via charts

## Database

This application uses **Entity Framework Core In-Memory Database** for demonstration purposes. The database is seeded with sample data on application startup.

### To Use SQL Server Instead:

1. Update `Program.cs`:
```csharp
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
```

2. Add connection string to `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=EmployeeDashboard;Trusted_Connection=True;"
  }
}
```

3. Create migration:
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

## Customization

### Adding New Fields
1. Update `Employee.cs` model
2. Update `SeedData.cs` to include new field in sample data
3. Update views to display new field
4. Update search logic if field should be searchable

### Styling
- Modify `wwwroot/css/site.css` for custom styles
- Bootstrap classes can be used directly in views
- Color scheme defined in CSS variables

## Best Practices Demonstrated

✅ **Separation of Concerns**: Clear separation between layers  
✅ **Dependency Injection**: Proper IoC usage  
✅ **Repository Pattern**: Abstracted data access  
✅ **Async/Await**: Asynchronous operations throughout  
✅ **Error Handling**: Try-catch blocks with logging  
✅ **Responsive Design**: Mobile-first approach  
✅ **Accessibility**: Semantic HTML and ARIA labels  
✅ **Performance**: Efficient queries with pagination  
✅ **Security**: Input validation and sanitization  

## Future Enhancements

Potential improvements:
- [ ] CRUD operations (Create, Update, Delete)
- [ ] Export to Excel/PDF
- [ ] Advanced reporting
- [ ] User authentication and authorization
- [ ] Real-time updates with SignalR
- [ ] Integration with external HR systems
- [ ] Employee photo uploads
- [ ] Performance reviews tracking
- [ ] Leave management
- [ ] Document management

## Troubleshooting

### Issue: "dotnet command not found"
**Solution**: Install .NET 8.0 SDK from https://dotnet.microsoft.com/download

### Issue: Port already in use
**Solution**: Change port in `Properties/launchSettings.json` or kill the process using the port

### Issue: Dependencies not restoring
**Solution**: Run `dotnet restore` and ensure you have internet connection

### Issue: Charts not displaying
**Solution**: Ensure Chart.js CDN is accessible and check browser console for errors

## License

This project is created for demonstration purposes.

## Author

Built as a portfolio project demonstrating .NET development skills.

---

**Note**: This dashboard is designed to showcase professional .NET development practices and can serve as a foundation for a production employee management system.
