@echo off
echo ========================================
echo Resetting Database and Migrations
echo ========================================
echo.

echo Step 1: Dropping database...
dotnet ef database drop --force
if %errorlevel% neq 0 (
    echo ERROR: Failed to drop database
    pause
    exit /b %errorlevel%
)
echo Database dropped successfully.
echo.

echo Step 2: Deleting migrations folder...
if exist "Migrations" (
    rmdir /s /q "Migrations"
    echo Migrations folder deleted.
) else (
    echo Migrations folder does not exist.
)
echo.

echo Step 3: Creating initial migration...
dotnet ef migrations add InitialMigration
if %errorlevel% neq 0 (
    echo ERROR: Failed to create migration
    pause
    exit /b %errorlevel%
)
echo Migration created successfully.
echo.

echo Step 4: Applying migration to database...
dotnet ef database update
if %errorlevel% neq 0 (
    echo ERROR: Failed to update database
    pause
    exit /b %errorlevel%
)
echo Database updated successfully.
echo.

echo ========================================
echo Database reset completed successfully!
echo ========================================
pause

