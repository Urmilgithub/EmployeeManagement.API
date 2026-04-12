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