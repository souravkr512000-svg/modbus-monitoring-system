# Verify Deployment Readiness Script
Write-Host "🔍 Checking deployment readiness..." -ForegroundColor Cyan
Write-Host ""

$errors = 0

# Check Dockerfile
Write-Host "Checking Dockerfile..." -ForegroundColor Yellow
if (Test-Path "EmployeeDashboard\Dockerfile") {
    Write-Host "  ✅ Dockerfile exists" -ForegroundColor Green
} else {
    Write-Host "  ❌ Dockerfile not found!" -ForegroundColor Red
    $errors++
}

# Check render.yaml
Write-Host "Checking render.yaml..." -ForegroundColor Yellow
if (Test-Path "render.yaml") {
    Write-Host "  ✅ render.yaml exists" -ForegroundColor Green
} else {
    Write-Host "  ❌ render.yaml not found!" -ForegroundColor Red
    $errors++
}

# Check .csproj
Write-Host "Checking .csproj..." -ForegroundColor Yellow
if (Test-Path "EmployeeDashboard\EmployeeDashboard.csproj") {
    Write-Host "  ✅ .csproj exists" -ForegroundColor Green
} else {
    Write-Host "  ❌ .csproj not found!" -ForegroundColor Red
    $errors++
}

# Check Program.cs
Write-Host "Checking Program.cs..." -ForegroundColor Yellow
if (Test-Path "EmployeeDashboard\Program.cs") {
    $programContent = Get-Content "EmployeeDashboard\Program.cs" -Raw
    if ($programContent -match "PORT") {
        Write-Host "  ✅ Program.cs configured for PORT" -ForegroundColor Green
    } else {
        Write-Host "  ⚠️  Program.cs may not handle PORT env variable" -ForegroundColor Yellow
    }
} else {
    Write-Host "  ❌ Program.cs not found!" -ForegroundColor Red
    $errors++
}

# Check GitHub Actions workflows
Write-Host "Checking GitHub Actions..." -ForegroundColor Yellow
if (Test-Path ".github\workflows\ci-cd.yml") {
    Write-Host "  ✅ CI/CD workflow exists" -ForegroundColor Green
} else {
    Write-Host "  ⚠️  CI/CD workflow not found" -ForegroundColor Yellow
}

if (Test-Path ".github\workflows\deploy-render.yml") {
    Write-Host "  ✅ Render deployment workflow exists" -ForegroundColor Green
} else {
    Write-Host "  ⚠️  Render deployment workflow not found" -ForegroundColor Yellow
}

# Try to build (if dotnet is available)
Write-Host ""
Write-Host "Checking .NET SDK..." -ForegroundColor Yellow
$dotnetVersion = & dotnet --version 2>&1
if ($LASTEXITCODE -eq 0) {
    Write-Host "  ✅ .NET SDK installed: $dotnetVersion" -ForegroundColor Green
    
    Write-Host ""
    Write-Host "Testing build..." -ForegroundColor Yellow
    Push-Location "EmployeeDashboard"
    & dotnet restore 2>&1 | Out-Null
    if ($LASTEXITCODE -eq 0) {
        Write-Host "  ✅ Restore successful" -ForegroundColor Green
        & dotnet build --configuration Release 2>&1 | Out-Null
        if ($LASTEXITCODE -eq 0) {
            Write-Host "  ✅ Build successful" -ForegroundColor Green
        } else {
            Write-Host "  ❌ Build failed!" -ForegroundColor Red
            $errors++
        }
    } else {
        Write-Host "  ❌ Restore failed!" -ForegroundColor Red
        $errors++
    }
    Pop-Location
} else {
    Write-Host "  ⚠️  .NET SDK not found (optional for verification)" -ForegroundColor Yellow
}

# Summary
Write-Host ""
Write-Host "========================================" -ForegroundColor Cyan
if ($errors -eq 0) {
    Write-Host "✅ All checks passed! Ready for deployment!" -ForegroundColor Green
    Write-Host ""
    Write-Host "Next steps:" -ForegroundColor Cyan
    Write-Host "1. Push to GitHub: git push origin master" -ForegroundColor White
    Write-Host "2. Go to https://render.com" -ForegroundColor White
    Write-Host "3. Connect repository and deploy!" -ForegroundColor White
} else {
    Write-Host "❌ Found $errors error(s). Please fix before deploying." -ForegroundColor Red
}
Write-Host "========================================" -ForegroundColor Cyan

exit $errors
