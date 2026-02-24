# Quick Setup Guide

## Prerequisites Check

Before running the application, ensure you have:

1. **.NET 8.0 SDK** installed
   - Check: Open PowerShell/Command Prompt and run `dotnet --version`
   - Should display: `8.0.x` or higher
   - If not installed: Download from https://dotnet.microsoft.com/download/dotnet/8.0

## Step-by-Step Setup

### Option 1: Using Command Line (Recommended)

1. **Open Terminal/PowerShell**
   ```powershell
   cd "c:\Users\skumar\OneDrive - Rugged Monitoring\Desktop\MY profile"
   ```

2. **Restore NuGet Packages**
   ```powershell
   dotnet restore EmployeeDashboard
   ```

3. **Build the Project**
   ```powershell
   dotnet build EmployeeDashboard
   ```

4. **Run the Application**
   ```powershell
   dotnet run --project EmployeeDashboard
   ```

5. **Open Browser**
   - Navigate to: `https://localhost:5001` or `http://localhost:5000`
   - The application will automatically seed sample data

### Option 2: Using Visual Studio

1. **Open Visual Studio 2022**

2. **Open Solution**
   - File → Open → Project/Solution
   - Navigate to: `EmployeeDashboard.sln`

3. **Restore Packages**
   - Right-click solution → Restore NuGet Packages
   - Or: Build → Restore NuGet Packages

4. **Build Solution**
   - Press `Ctrl+Shift+B`
   - Or: Build → Build Solution

5. **Run Application**
   - Press `F5`
   - Or: Debug → Start Debugging
   - Browser will open automatically

## Verify Installation

After running, you should see:

✅ **Dashboard Page** with:
- 4 statistics cards (Total Employees, Active, Departments, Average Salary)
- 2 charts (Department distribution, Status distribution)
- Search and filter controls
- Employee table with 12 sample employees

✅ **Sample Data** includes:
- 12 employees across 6 departments
- Various employment statuses
- Complete employee profiles

## Troubleshooting

### Error: "dotnet: command not found"
**Solution**: Install .NET 8.0 SDK
- Download: https://dotnet.microsoft.com/download/dotnet/8.0
- Run installer and restart terminal

### Error: "Port 5000/5001 already in use"
**Solution**: Change port in `Properties/launchSettings.json`
```json
"applicationUrl": "http://localhost:5002;https://localhost:5003"
```

### Error: "Package restore failed"
**Solution**: 
1. Check internet connection
2. Run: `dotnet nuget locals all --clear`
3. Run: `dotnet restore` again

### Error: "Cannot find Chart.js"
**Solution**: The application uses CDN links, ensure you have internet connection for first load

## Next Steps

Once running successfully:

1. **Explore the Dashboard**
   - Try searching for employees
   - Filter by department or status
   - Click on employee rows to view details

2. **Review the Code**
   - Check `Controllers/EmployeeController.cs` for request handling
   - Review `Services/` folder for business logic
   - Examine `Views/` for UI implementation

3. **Customize**
   - Modify `Data/SeedData.cs` to add more sample data
   - Update `wwwroot/css/site.css` for custom styling
   - Add new features following the existing patterns

## Development Tips

- **Hot Reload**: Changes to views and CSS will reload automatically
- **Logging**: Check console output for application logs
- **Database**: Currently using In-Memory database (resets on restart)
- **Debugging**: Set breakpoints in Visual Studio for debugging

## Production Deployment

For production:
1. Change to SQL Server database (see README.md)
2. Configure connection strings
3. Run migrations
4. Set up proper authentication
5. Configure HTTPS
6. Set environment to Production

---

**Need Help?** Check the main `README.md` for detailed documentation.
