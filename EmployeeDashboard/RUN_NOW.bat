@echo off
REM This script will attempt to install .NET SDK and run the application
REM Run this as Administrator for best results

echo ========================================
echo Employee Dashboard - Auto Setup
echo ========================================
echo.

REM Check if running as admin
net session >nul 2>&1
if %errorlevel% neq 0 (
    echo Requesting Administrator privileges...
    echo.
    powershell -Command "Start-Process '%~f0' -Verb RunAs"
    exit /b
)

echo Running PowerShell setup script...
echo.

powershell -ExecutionPolicy Bypass -File "%~dp0AUTO_INSTALL_DOTNET.ps1"

pause
