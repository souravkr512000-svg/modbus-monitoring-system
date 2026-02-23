@echo off
echo ========================================
echo Modbus Monitoring System - Quick Start
echo ========================================
echo.

echo [1/3] Installing backend dependencies...
call npm install
if errorlevel 1 (
    echo Backend installation failed!
    pause
    exit /b 1
)

echo.
echo [2/3] Installing frontend dependencies...
cd client
call npm install
if errorlevel 1 (
    echo Frontend installation failed!
    pause
    exit /b 1
)
cd ..

echo.
echo [3/3] Starting servers...
echo.
echo Backend will start on http://localhost:3001
echo Frontend will start on http://localhost:3000
echo.
echo Press Ctrl+C to stop both servers
echo.

start "Backend Server" cmd /k "npm start"
timeout /t 3 /nobreak >nul
start "Frontend Server" cmd /k "cd client && npm start"

echo.
echo Servers are starting in separate windows...
echo Dashboard will open automatically in your browser.
echo.
pause
