# Simplified .NET SDK Download and Install Script
# This uses the official Microsoft download page

Write-Host "========================================" -ForegroundColor Cyan
Write-Host ".NET SDK 8.0 Installation Helper" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

# Check if already installed
try {
    $version = & dotnet --version 2>&1
    if ($LASTEXITCODE -eq 0) {
        $majorVersion = [int]$version.Split('.')[0]
        if ($majorVersion -ge 8) {
            Write-Host ".NET SDK 8.0+ is already installed! Version: $version" -ForegroundColor Green
            Write-Host ""
            Write-Host "Running the application now..." -ForegroundColor Green
            
            $scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
            Set-Location $scriptDir
            & dotnet restore
            & dotnet build
            & dotnet run
            exit 0
        }
    }
} catch {
    Write-Host ".NET SDK not found" -ForegroundColor Yellow
}

Write-Host "Opening .NET SDK download page..." -ForegroundColor Green
Write-Host ""
Write-Host "Please:" -ForegroundColor Yellow
Write-Host "1. Download the .NET SDK 8.0 installer" -ForegroundColor White
Write-Host "2. Run the installer" -ForegroundColor White
Write-Host "3. Restart this script after installation" -ForegroundColor White
Write-Host ""

Start-Process "https://dotnet.microsoft.com/download/dotnet/8.0"

Write-Host "Download page opened in your browser." -ForegroundColor Green
Write-Host ""
Write-Host "After installation, run this script again or use:" -ForegroundColor Cyan
Write-Host "  dotnet restore" -ForegroundColor White
Write-Host "  dotnet build" -ForegroundColor White
Write-Host "  dotnet run" -ForegroundColor White
Write-Host ""

Read-Host "Press Enter to exit"
