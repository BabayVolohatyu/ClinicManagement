# Clinic Management System - User Manual

## Table of Contents
1. [Introduction](#introduction)
2. [Getting Started](#getting-started)
3. [Navigation](#navigation)
4. [Working with Entities](#working-with-entities)
5. [Predefined Queries](#predefined-queries)
6. [Query Manager](#query-manager)
7. [User Management & Permissions](#user-management--permissions)

---

## Introduction

Welcome to the Clinic Management System! This web-based application helps manage clinic operations, patient records, appointments, and medical data efficiently. This manual will guide you through all the features and functionalities of the system.

---

## Getting Started

### Login

1. Navigate to the login page
2. Enter your username and password
3. Click "Login" to access the system

**Note:** Your access level determines what features you can use in the system.

### Home Page

After logging in, you'll see the Home page which displays:
- **Predefined Queries**: Quick access to common search and analytics queries
- **Navigation Menu**: Access to different sections of the system

---

## Navigation

The navigation bar at the top provides access to all major sections:

### Home
- Return to the main dashboard with predefined queries

### Facilities
- **Cabinet**: Manage clinic cabinets and rooms
- **CabinetType**: Configure different types of cabinets (examination rooms, procedure rooms, etc.)

### Health
- **Diagnosis**: View and manage patient diagnoses
- **Procedure**: Manage medical procedures
- **Sickness**: Catalog of illnesses and conditions
- **SicknessProcedure**: Link sicknesses with related procedures
- **SicknessSymptom**: Associate symptoms with sicknesses
- **SicknessTreatment**: Link treatments with sicknesses
- **Symptom**: Manage symptoms database
- **Treatment**: Manage treatment options

### Humans
- **DistrictDoctor**: Manage district doctor assignments
- **Doctor**: View and manage doctor information
- **Patient**: Access patient records and information
- **Person**: Manage person records (base information)
- **Specialty**: Manage medical specialties

### Info
- **Address**: Manage address information
- **Appointment**: Schedule and view appointments
- **DoctorOnCallStatus**: Track doctors' on-call status
- **DoctorProcedure**: Link doctors with procedures they perform
- **Schedule**: Manage doctor schedules and availability

### Users (Admin Only)
- Manage system users and their permissions

---

## Working with Entities

### Viewing Data

1. Click on any menu item to view the list of records
2. The table displays all records with pagination support
3. Use the search/filter functionality (if available) to find specific records
4. Click on any row to view detailed information about that record

### Creating New Records

1. Navigate to the desired entity page
2. Click the **+** (Plus) button in the top-right toolbar
3. Fill in all required fields in the form
4. Click "Save" to add the new record

### Editing Records

1. Click on any row in the table to view the record details
2. Click on a field to edit it (the field becomes editable)
3. Modify the information as needed
4. Click "Save" to save your changes

### Deleting Records

1. View the record details page
2. Click the "Delete" button
3. Confirm the deletion in the confirmation dialog
4. The record will be permanently removed

**⚠️ Warning:** Deletions are permanent and cannot be undone!

### Downloading Data

1. Navigate to any entity page
2. Click the **↓** (Download) button in the top-right toolbar
3. The data will be exported as a CSV file for use in Excel or other tools

---

## Predefined Queries

The Home page provides quick access to commonly used queries:

### Available Queries

- **Reception Schedule**: View reception days and hours of doctors with corresponding cabinets
- **Doctor Details**: Get detailed information about doctors and issued certificates
- **Weekly Patient Count**: Count patients examined by a doctor per week (requires Doctor ID)
- **Search by Last Name**: Find patients by their last name
- **Search by Record Number**: Search patients by medical record number with full history
- **Search by Condition**: Find patients by health condition/sickness name
- **Search by Doctor**: Find all patients assigned to a specific doctor
- **Multiple Doctors**: Find patients who have been seen by multiple doctors
- **Angina Statistics**: View angina-related statistics
- **Doctor Schedule**: View a specific doctor's schedule
- **Doctors by Specialty**: List all doctors in a specific specialty
- **Home Call Patients**: View patients who require home visits
- **Home Calls Count**: Count total home calls
- **Procedures Total**: View total procedures statistics
- **Patients with Procedures**: List patients who have undergone procedures
- **Fluorography Patients**: Find patients who have had fluorography
- **Missed Vaccination**: Identify patients who have missed vaccinations
- **Physio Room Schedule**: View physiotherapy room schedule
- **Physio Room Doctors**: Count doctors using the physio room
- **Total Visits**: View total visit statistics
- **Visits by Specialty**: View visit statistics grouped by specialty

### Using Predefined Queries

1. Go to the Home page
2. Browse the query cards
3. Click **"Execute Query"** on any query card
4. If the query requires parameters, a dialog will appear:
   - Fill in all required fields
   - Click "Execute Query" to run it
5. View the results in the query results page

---

## Query Manager

For advanced users with proper permissions:

1. Click the **Terminal** (⚙️) icon in the top toolbar
2. Write your custom SQL query in the editor
3. Click "Execute" to run the query
4. View results in the table below

**⚠️ Important:** 
- Only users with "execute_raw_queries" permission can use this feature
- Be careful with SQL syntax to avoid errors
- Always test queries on non-critical data first

---

## User Management & Permissions

### Permission Levels

The system uses role-based access control:

- **Guest**: Limited read-only access
- **Authorized**: Can  create, edit, and delete records and execute predefined queries
- **Operator**: Can create, edit, delete records and write their own queries
- **Admin**: Full system access including user management

### Requesting Promotion

1. Click the **"Ask Promotion"** button (envelope with plus icon) in the toolbar
2. Fill in the promotion request form
3. Submit your request
4. An administrator will review and approve/deny your request

### Viewing Promotion Requests

Admins can:
1. Click the **"Promotion List"** button (checklist icon) in the toolbar
2. Review pending promotion requests
3. Approve or deny requests

### Password Change Requests

Admins can manage password change requests:
1. Click the **"Password Change Requests"** button (key icon) in the toolbar
2. Review and process password change requests

---


## Quick Reference

### Common Actions

| Action | Button/Icon | Location |
|--------|-------------|----------|
| Create New Record | **+** | Top toolbar on entity pages |
| Download CSV | **↓** | Top toolbar on entity pages |
| View Query Manager | **⚙️** | Top toolbar (with permission) |
| Request Promotion | Envelope with **+** | Top toolbar |
| View Promotions | Checklist icon | Top toolbar (Admin) |
| View README | GitHub icon | Bottom-right corner |
| View User Manual | **?** | Bottom-right corner |

