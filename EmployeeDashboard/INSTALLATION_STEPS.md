# Installation Steps - Employee Dashboard

## ❌ Current Issue: .NET SDK Not Installed

The error `ERR_CONNECTION_REFUSED` occurs because the .NET SDK is not installed on your system. The application cannot run without it.

## ✅ Solution: Install .NET 8.0 SDK

### Step 1: Download .NET SDK

1. **Visit**: https://dotnet.microsoft.com/download/dotnet/8.0
2. **Download**: Click "Download .NET SDK x64" (for Windows)
3. **File**: You'll get a file like `dotnet-sdk-8.0.xxx-win-x64.exe`

### Step 2: Install .NET SDK

1. **Run the installer** you just downloaded
2. **Follow the installation wizard**:
   - Accept the license agreement
   - Choose installation location (default is fine)
   - Click "Install"
3. **Wait for installation** to complete (may take a few minutes)
4. **Restart your computer** (recommended) or restart your terminal/PowerShell

### Step 3: Verify Installation

Open **PowerShell** or **Command Prompt** and run:

```powershell
dotnet --version
```

**Expected output**: `8.0.xxx` (some version number)

If you see an error, try:
1. Restart your terminal/PowerShell window
2. Restart your computer
3. Check if .NET was added to PATH (usually automatic)

### Step 4: Run the Application

**Option A: Using the Batch File (Easiest)**
1. Navigate to: `EmployeeDashboard` folder
2. Double-click: `INSTALL_AND_RUN.bat`
3. The script will:
   - Check .NET installation
   - Restore packages
   - Build the project
   - Run the application

**Option B: Using Command Line**
```powershell
cd "c:\Users\skumar\OneDrive - Rugged Monitoring\Desktop\MY profile\EmployeeDashboard"
dotnet restore
dotnet build
dotnet run
```

**Option C: Using Visual Studio**
1. Install Visual Studio 2022 (Community Edition is free)
2. Open `EmployeeDashboard.sln`
3. Press F5 to run

### Step 5: Access the Dashboard

Once running, open your browser and go to:
- **https://localhost:5001** (preferred)
- **http://localhost:5000** (alternative)

## 🔧 Troubleshooting

### Issue: "dotnet: command not found" after installation
**Solution**: 
- Restart your terminal/PowerShell
- Restart your computer
- Manually add to PATH if needed:
  - Usually installed at: `C:\Program Files\dotnet\`
  - Add to System Environment Variables → Path

### Issue: Port 5001 already in use
**Solution**: 
- Close other applications using port 5001
- Or change port in `Properties/launchSettings.json`:
  ```json
  "applicationUrl": "http://localhost:5002;https://localhost:5003"
  ```

### Issue: SSL Certificate Error
**Solution**: 
- Click "Advanced" → "Proceed to localhost" in your browser
- Or use `http://localhost:5000` instead

### Issue: Build Errors
**Solution**:
```powershell
dotnet clean
dotnet restore
dotnet build
```

## 📋 Quick Checklist

- [ ] .NET 8.0 SDK downloaded
- [ ] .NET 8.0 SDK installed
- [ ] Computer/Terminal restarted
- [ ] `dotnet --version` shows version number
- [ ] Navigated to project folder
- [ ] Ran `dotnet restore`
- [ ] Ran `dotnet build` (no errors)
- [ ] Ran `dotnet run`
- [ ] Browser opened to localhost:5001

## 🎯 What to Expect After Installation

Once .NET is installed and you run the application:

1. **Terminal Output**: You'll see:
   ```
   info: Microsoft.Hosting.Lifetime[14]
         Now listening on: https://localhost:5001
         Now listening on: http://localhost:5000
   ```

2. **Browser**: Should automatically open, or manually navigate to the URLs above

3. **Dashboard**: You'll see:
   - Statistics cards (Total Employees, Active, Departments, Average Salary)
   - Two charts (Department distribution, Status distribution)
   - Search and filter controls
   - Employee table with 12 sample employees

## 💡 Alternative: Use Visual Studio

If you prefer a GUI:

1. **Download Visual Studio 2022 Community** (free):
   - https://visualstudio.microsoft.com/downloads/
   - During installation, select ".NET desktop development" workload

2. **Open Project**:
   - File → Open → Project/Solution
   - Select `EmployeeDashboard.sln`

3. **Run**:
   - Press F5
   - Or click the green "Run" button

Visual Studio includes .NET SDK, so you don't need to install it separately!

## 📞 Still Having Issues?

1. Check Windows Event Viewer for .NET installation errors
2. Ensure you have administrator rights for installation
3. Disable antivirus temporarily during installation
4. Check if Windows updates are pending

---

**Once .NET is installed, the application will run perfectly!** 🚀
