@echo off
setlocal enabledelayedexpansion

echo ========================================
echo Populating Database with Sample Data
echo ========================================
echo.
echo WARNING: This will DELETE all existing data and replace it with sample data.
echo.

set /p confirm="Are you sure you want to continue? (yes/no): "
if /i not "!confirm!"=="yes" (
    echo Operation cancelled.
    pause
    exit /b 0
)

echo.
echo Checking if sample_data.sql exists...
if not exist "sample_data.sql" (
    echo ERROR: sample_data.sql file not found in the current directory.
    echo Please make sure you're running this script from the project root.
    pause
    exit /b 1
)
echo ✓ sample_data.sql found.
echo.

echo Checking PostgreSQL connection...
set /p db_password="Enter your PostgreSQL 'postgres' user password: "

echo.
echo Testing connection...
set PGPASSWORD=%db_password%
psql -U postgres -d clinic -c "SELECT 1;" >nul 2>&1
if %errorlevel% neq 0 (
    echo ERROR: Could not connect to PostgreSQL database 'clinic'.
    echo Please check:
    echo   - PostgreSQL service is running
    echo   - Database 'clinic' exists
    echo   - Password is correct
    echo   - psql is in your PATH
    set PGPASSWORD=
    pause
    exit /b 1
)
echo ✓ Connection successful.
echo.

echo Executing sample_data.sql...
echo This may take a few moments...
echo.

psql -U postgres -d clinic -f sample_data.sql
set PGPASSWORD=
if %errorlevel% neq 0 (
    echo.
    echo ERROR: Failed to execute sample_data.sql
    echo Please check the error messages above.
    pause
    exit /b 1
)

echo.
echo ========================================
echo Sample data populated successfully!
echo ========================================
echo.
echo The database now contains sample:
echo   - Addresses, Specialties, Cabinet Types
echo   - Doctors and Patients
echo   - Appointments and Schedules
echo   - Medical records and diagnoses
echo.
echo You can now start the application with: dotnet run
echo.
pause

