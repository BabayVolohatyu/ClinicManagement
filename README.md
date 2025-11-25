# Clinic Management System

A comprehensive web-based clinic management system built with ASP.NET Core 8.0 and PostgreSQL. This application helps manage patients, doctors, appointments, diagnoses, and other clinic operations.

## Table of Contents

- [Prerequisites](#prerequisites)
- [Quick Start (Automated Setup)](#quick-start-automated-setup)
- [Getting Started](#getting-started)
- [Database Setup](#database-setup)
- [Running the Application](#running-the-application)
- [Populating Sample Data (Optional)](#populating-sample-data-optional)
- [Default Login Credentials](#default-login-credentials)
- [Project Structure](#project-structure)
- [Automation Scripts](#automation-scripts)
- [Troubleshooting](#troubleshooting)

## Prerequisites

- **.NET 8.0 SDK**: [Download](https://dotnet.microsoft.com/download/dotnet/8.0)
  - Verify: `dotnet --version` (should be 8.0.x or higher)
- **PostgreSQL**: [Download](https://www.postgresql.org/download/)
  - Note the `postgres` user password during installation
  - Ensure PostgreSQL service is running
- **Git** (optional): [Download](https://git-scm.com/downloads)
- **Code Editor** (optional): Visual Studio 2022 or Visual Studio Code

## Quick Start (Automated Setup)

Windows users can use automated setup scripts:

1. Navigate to project directory:
   ```bash
   cd ClinicManagement/ClinicManagement/ClinicManagement
   ```

2. Run setup script:
   ```bash
   setup.bat
   ```

   Script performs:
   - .NET SDK verification
   - Entity Framework Core tools installation
   - PostgreSQL connection test
   - Database connection string update
   - Database creation
   - NuGet package restoration
   - Database migrations
   - Optional application start

3. Populate sample data (optional):
   ```bash
   populate-sample-data.bat
   ```

4. Start application:
   ```bash
   dotnet run
   ```

For manual setup or non-Windows systems, see sections below.

## Getting Started

### Clone Repository

```bash
git clone git@github.com:BabayVolohatyu/ClinicManagement.git
cd ClinicManagement/ClinicManagement
```

Navigate to the innermost `ClinicManagement` folder where the `.csproj` file is located.

## Database Setup

### Create Database

**Using pgAdmin:**
- Right-click "Databases" → "Create" → "Database"
- Name: `clinic`

**Using psql:**
```bash
psql -U postgres
CREATE DATABASE clinic;
\q
```

### Update Connection String

Edit `appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Database=clinic;Username=postgres;Password=YOUR_PASSWORD;Persist Security Info=True"
}
```

Update `Password`, `Username`, and `Host` as needed.

### Apply Migrations

```bash
dotnet ef database update
```

If `dotnet ef` is not found:
```bash
dotnet tool install --global dotnet-ef
```

## Running the Application

```bash
dotnet restore
dotnet build
dotnet run
```
Ignore SSL certificate warnings in local development.

## Populating Sample Data (Optional)

**Warning**: This will delete all existing data.

**Using pgAdmin:**
1. Connect to PostgreSQL server
2. Right-click `clinic` database → "Query Tool"
3. Open `sample_data.sql`
4. Execute (F5)

**Using psql:**
```bash
psql -U postgres -d clinic -f sample_data.sql
```

**Using automated script (Windows):**
```bash
populate-sample-data.bat
```

## Default Login Credentials

| Email | Password | Role |
|-------|----------|------|
| `admin@clinic.local` | `0000` | Admin |
| `authorized@clinic.local` | `0000` | Authorized User |
| `operator@clinic.local` | `0000` | Operator |

Change these credentials in production.

## Project Structure

```
ClinicManagement/
├── Controllers/          # Handles HTTP requests and responses
│   ├── Auth/            # Authentication and user management
│   ├── Facilities/      # Cabinet and facility management
│   ├── Health/          # Medical records (symptoms, sicknesses, treatments)
│   ├── Humans/          # People (patients, doctors, persons)
│   └── Info/            # Appointments, schedules, home calls
├── Data/                 # Database context and seed data
├── Models/               # Data models (entities)
├── Services/             # Business logic layer
├── Views/                # Razor views (HTML templates)
├── Middleware/           # Custom middleware (JWT authentication)
├── Migrations/           # Database migration files
├── Validators/           # Input validation
├── Helpers/              # Utility functions
├── Configurations/       # Entity Framework configurations
├── wwwroot/              # Static files (CSS, JavaScript, images)
├── Program.cs            # Application entry point and configuration
├── appsettings.json      # Application settings (database connection, etc.)
└── sample_data.sql       # Sample data script
```

## Automation Scripts

### `setup.bat`
Automated project setup:
- Verifies .NET SDK
- Installs EF Core tools
- Tests PostgreSQL connection
- Updates connection string
- Creates database
- Restores packages
- Applies migrations
- Optionally starts application

```bash
setup.bat
```

### `populate-sample-data.bat`
Populates database with sample data:
- Confirms before execution
- Tests database connection
- Executes `sample_data.sql`

```bash
populate-sample-data.bat
```

### `reset-database.bat`
Resets database and migrations (development only):
- Drops database
- Deletes migration files
- Creates new initial migration
- Applies migration

```bash
reset-database.bat
```

Warning: Deletes all database data.

## Troubleshooting

**"dotnet: command not found"**
- Install .NET 8.0 SDK and restart terminal

**Database connection errors**
- Verify PostgreSQL service is running
- Check password in `appsettings.json`
- Verify `clinic` database exists
- Check PostgreSQL is listening on `localhost:5432`

**"dotnet ef: command not found"**
```bash
dotnet tool install --global dotnet-ef
```

**Migration errors**
- Verify database exists
- Check connection string in `appsettings.json`
- Reset database:
  ```bash
  dotnet ef database drop --force
  dotnet ef database update
  ```

**Port already in use**
- Stop conflicting application
- Change ports in `Properties/launchSettings.json`

**SSL/Certificate warnings**
- Normal for local development. Proceed anyway.

**Sample data script fails**
- Ensure migrations are applied: `dotnet ef database update`
- Verify database connection
- Check SQL script file integrity

## Additional Resources

- [.NET Documentation](https://docs.microsoft.com/dotnet/)
- [Entity Framework Core Documentation](https://docs.microsoft.com/ef/core/)
- [PostgreSQL Documentation](https://www.postgresql.org/docs/)
- [ASP.NET Core Documentation](https://docs.microsoft.com/aspnet/core/)

