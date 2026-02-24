# 🚨 Quick Fix: Connection Refused Error

## The Problem
You're seeing `ERR_CONNECTION_REFUSED` because **.NET SDK is not installed**.

## The Solution (3 Steps)

### 1️⃣ Install .NET 8.0 SDK
- **Download**: https://dotnet.microsoft.com/download/dotnet/8.0
- **Install**: Run the downloaded `.exe` file
- **Restart**: Close and reopen your terminal/PowerShell

### 2️⃣ Verify Installation
Open PowerShell and type:
```powershell
dotnet --version
```
Should show: `8.0.xxx`

### 3️⃣ Run the App
Double-click: `INSTALL_AND_RUN.bat`

OR run these commands:
```powershell
cd "c:\Users\skumar\OneDrive - Rugged Monitoring\Desktop\MY profile\EmployeeDashboard"
dotnet restore
dotnet run
```

## That's It! 🎉

After installing .NET, the app will start and you can access it at:
- **https://localhost:5001**

---

**Need more help?** See `INSTALLATION_STEPS.md` for detailed instructions.
