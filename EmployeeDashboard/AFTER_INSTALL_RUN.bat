@echo off
REM Run this AFTER installing .NET SDK
REM This will automatically build and run the application

echo ========================================
echo Employee Dashboard - Starting...
echo ========================================
echo.

cd /d "%~dp0"

echo [1/3] Checking .NET SDK...
dotnet --version >nul 2>&1
if %errorlevel% neq 0 (
    echo ERROR: .NET SDK not found!
    echo.
    echo Please:
    echo 1. Install .NET SDK 8.0 from https://dotnet.microsoft.com/download/dotnet/8.0
    echo 2. Restart this window after installation
    echo 3. Run this script again
    echo.
    pause
    exit /b 1
)

echo .NET SDK found!
dotnet --version
echo.

echo [2/3] Restoring and building...
dotnet restore
if %errorlevel% neq 0 (
    echo ERROR: Failed to restore packages
    pause
    exit /b 1
)

dotnet build
if %errorlevel% neq 0 (
    echo ERROR: Build failed
    pause
    exit /b 1
)

echo.
echo [3/3] Starting application...
echo.
echo ========================================
echo Application will open at:
echo   https://localhost:5001
echo   http://localhost:5000
echo ========================================
echo.
echo Press Ctrl+C to stop the server
echo.

dotnet run

pause
