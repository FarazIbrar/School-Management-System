# School Management System

Welcome to the School Management System repository! This robust system is designed to efficiently manage various aspects of a school, including students, teachers, revenue, exams, and attendance. It supports multiple user roles, each with specific functionalities to streamline school operations.

## Table of Contents

- [Overview](#overview)
- [Technologies Used](#technologies-used)
- [Output Video](#output-video)
- [Setup Guide](#setup-guide)
- [Project Structure](#project-structure)
- [Database Configuration](#database-configuration)
- [Running the Application](#running-the-application)
- [Features and Modules](#features-and-modules)
- [User Roles](#user-roles)
- [Contributing](#contributing)
- [License](#license)

## Overview

The School Management System is a comprehensive solution aimed at simplifying the management of student and teacher information, financial transactions, examinations, and attendance tracking. The system is tailored to support the needs of administrators, teachers, and students with specific roles and capabilities.

## Technologies Used

- **Blazor WebAssembly**: For a dynamic and responsive frontend interface.
- **ASP.NET Core and C#**: Core technologies for backend development and business logic.
- **SQL Server**: Used for reliable data storage and management.

## Output Video

-**Output Video**[Output](https://github-production-user-asset-6210df.s3.amazonaws.com/95362670/341114258-9c3473f2-890e-48af-aa04-2df35cff394c.mp4?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=AKIAVCODYLSA53PQK4ZA%2F20240619%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20240619T143424Z&X-Amz-Expires=300&X-Amz-Signature=8f00a9f35fe5cf7c19e45a0d24b05a9428758f132ea4e5ea3b2f164810098d94&X-Amz-SignedHeaders=host&actor_id=95362670&key_id=0&repo_id=817129624)

## Setup Guide

To get started with the School Management System, follow these steps:

1. Clone the project using Git or download the ZIP file from the repository.
2. If you are using Visual Studio, copy the `.vs` folder from the downloaded project and paste it into the main project folder.
3. Import the `.bacpac` file into your SQL Server Management Studio (SSMS) to create the database for the project.
4. Obtain the connection string from your SQL Server instance and paste it into the `DBHelper.cs` file located in the project's backend folder.
5. Open the `SchoolManagementSystem.sln` solution file with Visual Studio. Ensure that you have the .NET 7.0 SDK installed, as the project is built on this version.

After completing these steps, you are ready to run the School Management System project in Visual Studio.


## Project Structure

The project is organized into several key sections:

- **Controllers**: Handle HTTP requests and interactions with the database.
- **Models**: Define the data structure for the application.
- **Views**: Present the user interface components using Blazor.
- **Services**: Include business logic and data processing operations.

## Database Configuration

Before running the project, set up a SQL Server database with the required tables. Update the connection string in the `DBHelper.cs` file under the Controllers.


```csharp
// DBHelper.cs
public class DBHelper
{
private SqlConnection connection = new SqlConnection("Your Connection String");
}
```

# Running the Application
1. Configure the database: Ensure your SQL Server database is properly set up and the connection string in DBHelper.cs is accurate.
2. Build and run the project: Use Visual Studio or your preferred IDE to build and run the solution.

## Features and Modules

### Student Management
- Manage Students: Create, read, update, and delete student records.
- Student Details: View detailed information about individual students.
- Admission: Register new students through an admission form.
- Promotion: Handle the promotion of students to the next grade level.

### Teacher Management
- Manage Teachers: Full CRUD operations for teacher records.
- Teacher Details: Access detailed information about teachers.
- Add Teacher: Register new teachers to the system.

### Revenue Management
- Student Fees: Add and view fee information for students.
- Teacher Salaries: Add and view salary details for teachers.

### Examination Management
- Manage Exams: Add and view exam information.

### Attendance Management
- Mark Attendance: Record attendance for students.
- View Attendance: Review attendance records by class.

## User Roles
- Admin: Full access to all system functionalities.
- Teacher: View personal information, manage attendance, and exam records.
- Student: View personal information and exam results.

## Contributing
We welcome contributions! If you have suggestions for improvements or encounter issues, please open an issue or submit a pull request.

## License
This project is licensed under the MIT License. For more details, refer to the [LICENSE](https://github.com/FarazIbrar/School-Management-System?tab=MIT-1-ov-file#readme) file.


