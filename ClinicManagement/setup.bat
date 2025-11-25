@echo off
setlocal enabledelayedexpansion

echo ========================================
echo Clinic Management System - Setup Script
echo ========================================
echo.
echo This script will help you set up the project.
echo.

REM Check if .NET SDK is installed
echo [1/6] Checking .NET SDK installation...
dotnet --version >nul 2>&1
if %errorlevel% neq 0 (
    echo ERROR: .NET SDK is not installed or not in PATH.
    echo Please install .NET 8.0 SDK from: https://dotnet.microsoft.com/download/dotnet/8.0
    pause
    exit /b 1
)
for /f "tokens=1 delims=." %%a in ('dotnet --version') do set DOTNET_MAJOR=%%a
if !DOTNET_MAJOR! LSS 8 (
    echo WARNING: .NET SDK version 8.0 or higher is required.
    echo Current version: 
    dotnet --version
    echo.
    set /p continue="Continue anyway? (y/n): "
    if /i not "!continue!"=="y" exit /b 1
) else (
    echo ✓ .NET SDK found:
    dotnet --version
)
echo.

REM Check if dotnet-ef tool is installed
echo [2/6] Checking Entity Framework Core tools...
dotnet ef --version >nul 2>&1
if %errorlevel% neq 0 (
    echo Entity Framework Core tools not found. Installing...
    dotnet tool install --global dotnet-ef
    if %errorlevel% neq 0 (
        echo ERROR: Failed to install dotnet-ef tool.
        pause
        exit /b 1
    )
    echo ✓ Entity Framework Core tools installed.
) else (
    echo ✓ Entity Framework Core tools found.
)
echo.

REM Check PostgreSQL connection
echo [3/6] Checking PostgreSQL connection...
echo Please ensure PostgreSQL is installed and running.
echo.
set /p db_password="Enter your PostgreSQL 'postgres' user password (or press Enter to skip): "
if not "!db_password!"=="" (
    echo Testing connection...
    set PGPASSWORD=!db_password!
    psql -U postgres -c "SELECT 1;" >nul 2>&1
    if %errorlevel% neq 0 (
        echo WARNING: Could not connect to PostgreSQL. Please check:
        echo   - PostgreSQL service is running
        echo   - Password is correct
        echo   - psql is in your PATH
        echo.
        set /p continue="Continue anyway? (y/n): "
        if /i not "!continue!"=="y" exit /b 1
    ) else (
        echo ✓ PostgreSQL connection successful.
    )
    set PGPASSWORD=
) else (
    echo Skipping PostgreSQL connection test.
    echo Please make sure PostgreSQL is running and the database 'clinic' exists.
)
echo.

REM Update connection string if password provided
if not "!db_password!"=="" (
    echo [4/6] Updating database connection string...
    if exist "appsettings.json" (
        REM Create backup
        copy "appsettings.json" "appsettings.json.backup" >nul 2>&1
        
        REM Update password in connection string using PowerShell
        REM Use a temporary file to pass the password safely
        echo !db_password! > temp_pwd.txt
        powershell -Command "$pwd = Get-Content 'temp_pwd.txt' -Raw; $pwd = $pwd.Trim(); $content = Get-Content 'appsettings.json' -Raw; $content = $content -replace 'Password=[^;]+', ('Password=' + $pwd); Set-Content 'appsettings.json' -Value $content -NoNewline"
        del temp_pwd.txt >nul 2>&1
        if %errorlevel% equ 0 (
            echo ✓ Connection string updated (password changed).
            echo   Note: If your username is not 'postgres', please edit appsettings.json manually.
        ) else (
            echo WARNING: Could not automatically update connection string.
            echo Please edit appsettings.json manually and update the password.
        )
    ) else (
        echo WARNING: appsettings.json not found. Please create it manually.
    )
) else (
    echo [4/6] Skipping connection string update.
    echo Please update appsettings.json manually with your PostgreSQL password.
)
echo.

REM Create database if it doesn't exist
if not "!db_password!"=="" (
    echo [5/6] Creating database (if it doesn't exist)...
    set PGPASSWORD=!db_password!
    psql -U postgres -c "SELECT 1 FROM pg_database WHERE datname='clinic'" 2>nul | findstr /C:"1" >nul
    if %errorlevel% neq 0 (
        echo Creating 'clinic' database...
        psql -U postgres -c "CREATE DATABASE clinic;" 2>nul
        if %errorlevel% equ 0 (
            echo ✓ Database 'clinic' created.
        ) else (
            echo WARNING: Could not create database automatically.
            echo Please create it manually using pgAdmin or psql.
        )
    ) else (
        echo ✓ Database 'clinic' already exists.
    )
    set PGPASSWORD=
    echo.
)

REM Restore packages
echo [5/6] Restoring NuGet packages...
dotnet restore
if %errorlevel% neq 0 (
    echo ERROR: Failed to restore packages.
    pause
    exit /b 1
)
echo ✓ Packages restored.
echo.

REM Apply migrations
echo [6/6] Applying database migrations...
dotnet ef database update
if %errorlevel% neq 0 (
    echo.
    echo ERROR: Failed to apply migrations.
    echo Please check:
    echo   1. PostgreSQL is running
    echo   2. Database 'clinic' exists
    echo   3. Connection string in appsettings.json is correct
    echo.
    pause
    exit /b 1
)
echo ✓ Database migrations applied successfully.
echo.

echo ========================================
echo Setup completed successfully!
echo ========================================
echo.
echo Next steps:
echo   1. If you want sample data, run: populate-sample-data.bat
echo   2. To start the application, run: dotnet run
echo   3. Open your browser to: https://localhost:5001
echo.
echo Default login credentials:
echo   - Email: admin@clinic.local
echo   - Password: 0000
echo.
set /p run_app="Would you like to start the application now? (y/n): "
if /i "!run_app!"=="y" (
    echo.
    echo Starting application...
    echo Press Ctrl+C to stop the server.
    echo.
    dotnet run
) else (
    echo.
    echo Setup complete! Run 'dotnet run' when you're ready to start the application.
    pause
)

