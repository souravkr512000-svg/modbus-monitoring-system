@echo off
echo ========================================
echo Employee Dashboard - Setup and Run
echo ========================================
echo.

REM Check if .NET SDK is installed
echo [1/3] Checking .NET SDK installation...
dotnet --version >nul 2>&1
if %errorlevel% neq 0 (
    echo.
    echo ERROR: .NET SDK is not installed!
    echo.
    echo Please install .NET 8.0 SDK from:
    echo https://dotnet.microsoft.com/download/dotnet/8.0
    echo.
    echo After installation:
    echo 1. Close and reopen this window
    echo 2. Run this script again
    echo.
    pause
    exit /b 1
)

echo .NET SDK found!
dotnet --version
echo.

REM Navigate to project directory
cd /d "%~dp0"
echo [2/3] Restoring NuGet packages...
dotnet restore
if %errorlevel% neq 0 (
    echo ERROR: Failed to restore packages
    pause
    exit /b 1
)
echo Packages restored successfully!
echo.

echo [3/3] Building project...
dotnet build
if %errorlevel% neq 0 (
    echo ERROR: Build failed
    pause
    exit /b 1
)
echo Build successful!
echo.

echo ========================================
echo Starting application...
echo ========================================
echo.
echo The application will open in your browser at:
echo   - https://localhost:5001
echo   - http://localhost:5000
echo.
echo Press Ctrl+C to stop the server
echo.

dotnet run

pause
