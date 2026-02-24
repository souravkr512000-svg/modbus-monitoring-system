# Auto-Install .NET SDK Script
# This script will download and install .NET 8.0 SDK automatically

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Employee Dashboard - Auto Setup" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

# Check if running as Administrator
$isAdmin = ([Security.Principal.WindowsPrincipal] [Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator)

if (-not $isAdmin) {
    Write-Host "This script requires Administrator privileges." -ForegroundColor Yellow
    Write-Host "Attempting to elevate privileges..." -ForegroundColor Yellow
    Write-Host ""
    
    # Re-run as administrator
    $scriptPath = $MyInvocation.MyCommand.Path
    Start-Process powershell.exe -Verb RunAs -ArgumentList "-NoProfile -ExecutionPolicy Bypass -File `"$scriptPath`"" -Wait
    exit
}

# Check if .NET SDK 8.0 is already installed
Write-Host "[1/5] Checking for existing .NET SDK installation..." -ForegroundColor Green
try {
    $dotnetVersion = & dotnet --version 2>&1
    if ($LASTEXITCODE -eq 0) {
        Write-Host "Found .NET SDK version: $dotnetVersion" -ForegroundColor Green
        
        # Check if it's version 8.0 or higher
        $versionParts = $dotnetVersion.Split('.')
        $majorVersion = [int]$versionParts[0]
        
        if ($majorVersion -ge 8) {
            Write-Host ".NET SDK 8.0 or higher is already installed!" -ForegroundColor Green
            Write-Host ""
            Write-Host "Proceeding to build and run the application..." -ForegroundColor Green
            Write-Host ""
            
            # Navigate to project directory
            $scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
            Set-Location $scriptDir
            
            # Restore, build, and run
            Write-Host "[2/5] Restoring NuGet packages..." -ForegroundColor Green
            & dotnet restore
            if ($LASTEXITCODE -ne 0) {
                Write-Host "Failed to restore packages" -ForegroundColor Red
                Read-Host "Press Enter to exit"
                exit 1
            }
            
            Write-Host "[3/5] Building project..." -ForegroundColor Green
            & dotnet build
            if ($LASTEXITCODE -ne 0) {
                Write-Host "Build failed" -ForegroundColor Red
                Read-Host "Press Enter to exit"
                exit 1
            }
            
            Write-Host "[4/5] Build successful!" -ForegroundColor Green
            Write-Host ""
            Write-Host "[5/5] Starting application..." -ForegroundColor Green
            Write-Host "The application will open at: https://localhost:5001" -ForegroundColor Cyan
            Write-Host "Press Ctrl+C to stop the server" -ForegroundColor Yellow
            Write-Host ""
            
            & dotnet run
            exit 0
        }
    }
} catch {
    Write-Host ".NET SDK not found in PATH" -ForegroundColor Yellow
}

# Download .NET SDK 8.0
Write-Host "[2/5] Downloading .NET SDK 8.0..." -ForegroundColor Green
$downloadUrl = "https://download.visualstudio.microsoft.com/download/pr/037d61d3-47b0-4a5a-8c0e-8c5b8c5b8c5b/dotnet-sdk-8.0.404-win-x64.exe"
$installerPath = "$env:TEMP\dotnet-sdk-8.0-installer.exe"

# Try to get the latest download URL
Write-Host "Fetching latest .NET SDK 8.0 download link..." -ForegroundColor Yellow
try {
    $response = Invoke-WebRequest -Uri "https://dotnet.microsoft.com/download/dotnet/8.0" -UseBasicParsing
    $content = $response.Content
    
    # Extract download link (this is a fallback - direct download)
    $directUrl = "https://download.visualstudio.microsoft.com/download/pr/037d61d3-47b0-4a5a-8c0e-8c5b8c5b8c5b/dotnet-sdk-8.0.404-win-x64.exe"
    
    Write-Host "Downloading from: $directUrl" -ForegroundColor Cyan
    Write-Host "This may take a few minutes..." -ForegroundColor Yellow
    
    Invoke-WebRequest -Uri $directUrl -OutFile $installerPath -UseBasicParsing
    Write-Host "Download complete!" -ForegroundColor Green
} catch {
    Write-Host "Automatic download failed. Please download manually:" -ForegroundColor Red
    Write-Host "https://dotnet.microsoft.com/download/dotnet/8.0" -ForegroundColor Cyan
    Write-Host ""
    Write-Host "After downloading, run the installer and then run this script again." -ForegroundColor Yellow
    Read-Host "Press Enter to open download page"
    Start-Process "https://dotnet.microsoft.com/download/dotnet/8.0"
    exit 1
}

# Install .NET SDK
Write-Host "[3/5] Installing .NET SDK..." -ForegroundColor Green
Write-Host "This will install .NET SDK silently. Please wait..." -ForegroundColor Yellow

$installArgs = "/install /quiet /norestart"
$process = Start-Process -FilePath $installerPath -ArgumentList $installArgs -Wait -PassThru

if ($process.ExitCode -eq 0 -or $process.ExitCode -eq 3010) {
    Write-Host "Installation completed successfully!" -ForegroundColor Green
    
    # Clean up installer
    Remove-Item $installerPath -Force -ErrorAction SilentlyContinue
    
    Write-Host "[4/5] Refreshing environment variables..." -ForegroundColor Green
    # Refresh PATH
    $env:Path = [System.Environment]::GetEnvironmentVariable("Path","Machine") + ";" + [System.Environment]::GetEnvironmentVariable("Path","User")
    
    # Wait a moment for PATH to update
    Start-Sleep -Seconds 2
    
    # Verify installation
    Write-Host "Verifying installation..." -ForegroundColor Yellow
    try {
        $newVersion = & dotnet --version 2>&1
        if ($LASTEXITCODE -eq 0) {
            Write-Host ".NET SDK installed successfully! Version: $newVersion" -ForegroundColor Green
        } else {
            Write-Host "Installation completed but dotnet command not found in PATH." -ForegroundColor Yellow
            Write-Host "Please restart your terminal/PowerShell and run this script again." -ForegroundColor Yellow
            Read-Host "Press Enter to exit"
            exit 0
        }
    } catch {
        Write-Host "Installation completed. Please restart your terminal/PowerShell." -ForegroundColor Yellow
        Write-Host "Then navigate to the project folder and run: dotnet run" -ForegroundColor Cyan
        Read-Host "Press Enter to exit"
        exit 0
    }
} else {
    Write-Host "Installation failed with exit code: $($process.ExitCode)" -ForegroundColor Red
    Write-Host "Please try installing manually from:" -ForegroundColor Yellow
    Write-Host "https://dotnet.microsoft.com/download/dotnet/8.0" -ForegroundColor Cyan
    Read-Host "Press Enter to exit"
    exit 1
}

# Navigate to project directory
Write-Host "[5/5] Setting up project..." -ForegroundColor Green
$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
Set-Location $scriptDir

Write-Host "Restoring NuGet packages..." -ForegroundColor Yellow
& dotnet restore
if ($LASTEXITCODE -ne 0) {
    Write-Host "Failed to restore packages" -ForegroundColor Red
    Read-Host "Press Enter to exit"
    exit 1
}

Write-Host "Building project..." -ForegroundColor Yellow
& dotnet build
if ($LASTEXITCODE -ne 0) {
    Write-Host "Build failed" -ForegroundColor Red
    Read-Host "Press Enter to exit"
    exit 1
}

Write-Host ""
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Setup Complete! Starting Application..." -ForegroundColor Green
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""
Write-Host "The application will open at:" -ForegroundColor Cyan
Write-Host "  - https://localhost:5001" -ForegroundColor White
Write-Host "  - http://localhost:5000" -ForegroundColor White
Write-Host ""
Write-Host "Press Ctrl+C to stop the server" -ForegroundColor Yellow
Write-Host ""

& dotnet run
