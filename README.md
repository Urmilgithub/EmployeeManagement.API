# Employee Management API

A production-grade RESTful Web API built with ASP.NET Core 8 for managing
employee records, departments, and organizational hierarchy.

## Tech Stack

- **Backend:** ASP.NET Core 8, C#, Entity Framework Core
- **Database:** MS SQL Server
- **Auth:** JWT Bearer Authentication (Role-Based)
- **Architecture:** Repository Pattern, Dependency Injection
- **API Docs:** Swagger / OpenAPI with JWT support

## Features

- JWT Authentication with Admin and Employee roles
- Employee CRUD with filtering, sorting, and pagination
- Department, City, State, and Job management
- Role-based access control ([Authorize(Roles="Admin")])
- Soft delete (IsDeleted flag — records never hard deleted)
- Active/Inactive status management using IsActive
- Global exception handling with structured error responses
- Swagger UI with Bearer token input for easy testing

## API Endpoints

| Method | Route | Auth | Description |
|--------|-------|------|-------------|
| POST | /api/auth/login | Public | Login, returns JWT token |
| GET | /api/employees | Required | Get all employees (filter/sort/page) |
| GET | /api/employees/{id} | Required | Get employee by ID |
| POST | /api/employees | Admin | Create new employee |
| PUT | /api/employees/{id} | Admin | Update employee |
| DELETE | /api/employees/{id} | Admin | Soft delete employee |
| GET | /api/departments | Required | Get all departments |
| GET | /api/jobs | Required | Get all job roles |

## How to Run

1. Clone the repository
```
git clone https://github.com/Urmilgithub/EmployeeManagement.API.git
```
2. Update the connection string in `appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=EmployeeDB;Trusted_Connection=true;"
}
```
3. Apply database migrations:
```
dotnet ef database update
```
4. Run the application:
```
dotnet run
```
5. Open Swagger UI: `https://localhost:7xxx/swagger`


## Sample Login Request

```json
POST /api/auth/login
{
  "email": "admin@example.com",
  "password": "Admin@123"
}
```
Response:
```json
{
  "token": "eyJhbGciOiJIUzI1NiJ9...",
  "expiry": "2025-12-31T00:00:00"
}
```


## Architecture

This project follows a Repository Pattern with Dependency Injection for
clean separation of concerns. Each entity (Employee, Department, Job, City,
State) has its own repository interface and implementation, registered as
Scoped services in the DI container. JWT secrets and DB connection strings
are stored in appsettings.json (use Azure App Service Configuration or
environment variables in production).