@echo off
REM Quick script to run the Employee Dashboard
REM This bypasses the OneDrive permission issue by running the DLL directly

echo ========================================
echo Starting Employee Dashboard...
echo ========================================
echo.

cd /d "%~dp0\bin\Debug\net8.0"

if not exist "EmployeeDashboard.dll" (
    echo ERROR: Application not built yet!
    echo.
    echo Please run: dotnet build
    echo Then run this script again.
    echo.
    pause
    exit /b 1
)

echo Application is starting...
echo.
echo The dashboard will open at:
echo   - http://localhost:5000
echo   - https://localhost:5001
echo.
echo Press Ctrl+C to stop the server
echo.

dotnet EmployeeDashboard.dll --urls "http://localhost:5000;https://localhost:5001"

pause
