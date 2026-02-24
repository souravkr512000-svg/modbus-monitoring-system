# 🚀 START HERE - Quick Setup Guide

## ⚡ Fastest Way to Get Running

### Option 1: Automated Setup (Recommended)
1. **Right-click** `RUN_NOW.bat` → **Run as Administrator**
2. Follow the prompts
3. The script will:
   - Check for .NET SDK
   - Download and install if needed
   - Build the project
   - Run the application

### Option 2: Manual Setup (If Option 1 doesn't work)

#### Step 1: Install .NET SDK
- The download page should have opened automatically
- If not, go to: **https://dotnet.microsoft.com/download/dotnet/8.0**
- Download **".NET SDK 8.0"** (not just Runtime)
- Run the installer
- **Restart your computer** (or at least restart PowerShell)

#### Step 2: Run the Application
After installing .NET SDK:

**Double-click:** `AFTER_INSTALL_RUN.bat`

OR run these commands in PowerShell:
```powershell
cd "c:\Users\skumar\OneDrive - Rugged Monitoring\Desktop\MY profile\EmployeeDashboard"
dotnet restore
dotnet build
dotnet run
```

#### Step 3: Open Browser
- Go to: **https://localhost:5001**
- Or: **http://localhost:5000**

## ✅ What You'll See

Once running, you'll see:
- 📊 **Dashboard** with statistics cards
- 📈 **Charts** showing employee distribution
- 🔍 **Search & Filter** functionality
- 📋 **Employee Table** with 12 sample employees
- 👤 **Employee Details** pages

## 🆘 Troubleshooting

### "dotnet command not found"
- Restart your PowerShell/terminal window
- Restart your computer
- Verify installation at: `C:\Program Files\dotnet\`

### "Port already in use"
- Close other applications using port 5001
- Or change port in `Properties/launchSettings.json`

### "Build failed"
- Make sure you installed **.NET SDK** (not just Runtime)
- Run: `dotnet --version` to verify
- Should show: `8.0.xxx`

## 📁 File Guide

- `RUN_NOW.bat` - **Start here!** Automated setup
- `AFTER_INSTALL_RUN.bat` - Run after installing .NET SDK
- `AUTO_INSTALL_DOTNET.ps1` - PowerShell auto-installer
- `INSTALLATION_STEPS.md` - Detailed instructions
- `QUICK_FIX.md` - Quick troubleshooting

---

**Need help?** Check `INSTALLATION_STEPS.md` for detailed guide.
