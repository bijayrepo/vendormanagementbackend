# Profile Backend API

> **Profile Backend** — An ASP.NET Core backend implementing user/profile management with both **Entity Framework Core (EF Core)** and **ADO.NET** examples. The project exposes a **Web API** for use by clients (mobile/web) and a simple **ASP.NET Core MVC** admin interface for managing users.

---

## Table of contents

* [Overview](#overview)
* [Tech stack](#tech-stack)
* [Features](#features)
* [Architecture & Folder Structure](#architecture--folder-structure)
* [Getting started (Local development)](#getting-started-local-development)

  * [Prerequisites](#prerequisites)
  * [Configuration / Environment variables](#configuration--environment-variables)
  * [Database setup & migrations](#database-setup--migrations)
  * [Run the app](#run-the-app)
* [API Reference (selected endpoints)](#api-reference-selected-endpoints)

  * [Authentication](#authentication)
  * [User / Profile CRUD](#user--profile-crud)
* [Using EF Core and ADO.NET in the same project](#using-ef-core-and-adonet-in-the-same-project)

  * [Why both?](#why-both)
  * [Examples](#examples)
* [Testing](#testing)
* [Deployment](#deployment)
* [Contributing](#contributing)
* [License](#license)

---

## Overview

This repository contains a sample **Profile Backend** that demonstrates best practices for building a modern .NET backend for user/profile management. It shows:

* A RESTful **Web API** built with ASP.NET Core.
* A small **MVC admin portal** for basic user management pages.
* Data access using **EF Core** (recommended for majority of operations) and **ADO.NET** (for high-performance or raw SQL scenarios).
* Example of authentication (JWT) and role-based protection for API and MVC controllers.

This is intended as a starter template you can adapt to your product.

---

## Tech stack

* .NET SDK: **.NET 8+** (adjust to your target runtime)
* ASP.NET Core Web API
* ASP.NET Core MVC (Razor views) for admin pages
* Entity Framework Core (EF Core) for ORM
* ADO.NET (System.Data.*) for raw SQL operations
* SQL Server (or any RDBMS supported by EF Core)
* Authentication: **JWT Bearer tokens** (sample)
* Testing: xUnit / MSTest (recommended)

---

## Features

* User registration, email (placeholder) verification flow
* Login / JWT token issuance
* User profile CRUD (create, read, update, delete)
* Role-based authorization (Admin / User)
* Sample MVC-admin pages to list and edit users
* Demonstration code for using EF Core and ADO.NET side-by-side

---

## Architecture & Folder Structure

```
/src
  /ProfileApi                 # ASP.NET Core Web API project
    /Controllers
    /DTOs
    /Services
    /Data                      # DbContext, EF migrations
    /Repositories              # Repos that use EF Core
    /Ado                       # classes using ADO.NET (IDbConnection etc.)
    /Models
    /Migrations
  /ProfileAdmin               # ASP.NET Core MVC project (Razor)
  /Tests                      # Unit & integration tests

/docs
README.md

```

Guiding principles:

* Keep controllers thin — move logic to Services.
* Use DTOs for API contracts (avoid returning EF entities directly).
* Use repository or query/service patterns for data access.

---

## Getting started (Local development)

### Prerequisites

* .NET SDK 8+ installed: `dotnet --version`
* SQL Server / LocalDB / Docker for SQL
* Optional: Visual Studio 2022/2023 or VS Code

### Configuration / Environment variables

Create a `appsettings.Development.json` (or use secrets) with keys like:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=ProfileDb;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "Jwt": {
    "Key": "super_secret_dev_key_change_me",
    "Issuer": "ProfileApi",
    "Audience": "ProfileClients",
    "ExpiresMinutes": 60
  }
}
```

Store production secrets in a secure store (Azure Key Vault, environment variables, etc.).

### Database setup & migrations

Using EF Core CLI:

```bash
# from /src/ProfileApi
dotnet ef migrations add InitialCreate
dotnet ef database update
```

If you prefer Docker, run a SQL Server container and update the connection string accordingly.

### Run the app

From the solution root:

```bash
cd src/ProfileApi
dotnet run
# App will run at https://localhost:5001 by default
```

Open the MVC admin project similarly to use the Razor UI.

---

## API Reference (selected endpoints)

### Authentication

* `POST /api/auth/register` — Register a new user (body: RegisterDto)
* `POST /api/auth/login` — Log in (body: LoginDto) → returns JWT

### User / Profile CRUD

* `GET /api/users` — Get list of users (Admin)
* `GET /api/users/{id}` — Get user by id (Admin or owner)
* `POST /api/users` — Create user (Admin)
* `PUT /api/users/{id}` — Update user (Admin or owner)
* `DELETE /api/users/{id}` — Delete user (Admin)

Request/response DTOs are in `/DTOs` folder. Controllers are in `/Controllers`.

---

## Using EF Core and ADO.NET in the same project

### Why both?

* **EF Core**: Productivity, change tracking, LINQ queries, migrations.
* **ADO.NET**: Raw performance, bulk operations, or when you want fine-grained control over SQL queries.

This repo demonstrates both to show when each is appropriate.

### Examples

**EF Core (typical repository/service)**

```csharp
public class UserRepository : IUserRepository
{
    private readonly ProfileDbContext _db;
    public UserRepository(ProfileDbContext db) => _db = db;

    public async Task<UserDto> GetByIdAsync(int id)
    {
        var user = await _db.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
        return user == null ? null : user.ToDto();
    }
}
```

**ADO.NET (example helper for a bulk or raw query)**

```csharp
public class UserAdoRepository
{
    private readonly string _connStr;
    public UserAdoRepository(IConfiguration config) => _connStr = config.GetConnectionString("DefaultConnection");

    public async Task<int> CountUsersAsync()
    {
        using var conn = new SqlConnection(_connStr);
        using var cmd = new SqlCommand("SELECT COUNT(1) FROM Users", conn);
        await conn.OpenAsync();
        var result = await cmd.ExecuteScalarAsync();
        return Convert.ToInt32(result);
    }
}
```

Notes:

* Use parameterized queries in ADO.NET to prevent SQL injection.
* Keep ADO.NET usage isolated behind a repository or service interface for testability.

---

## Authentication & Authorization

This sample uses JWT Bearer tokens for the API and cookie-based authentication for the MVC admin area (optional). Protect endpoints with `[Authorize]` and roles like `[Authorize(Roles = "Admin")]`.

Token issuance example (simplified):

```csharp
var claims = new[] { new Claim(ClaimTypes.Name, user.Email), new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) };
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));
var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
var token = new JwtSecurityToken(jwtSettings.Issuer, jwtSettings.Audience, claims, expires: DateTime.UtcNow.AddMinutes(jwtSettings.ExpiresMinutes), signingCredentials: creds);

return new JwtSecurityTokenHandler().WriteToken(token);
```

---

## Testing

* Unit tests for services and repositories using xUnit and Moq.
* Integration tests can use the in-memory provider for EF Core or a Testcontainer / Docker SQL instance for realistic tests.

Run tests:

```bash
cd src/Tests
dotnet test
```

---

## Deployment

* Build and publish: `dotnet publish -c Release`
* Dockerize the app (sample Dockerfile included)
* Use CI/CD pipelines (GitHub Actions / Azure DevOps) to run tests, build images, and deploy to your environment.

Sample `Dockerfile` snippet:

```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . ./
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish ./
ENTRYPOINT ["dotnet", "ProfileApi.dll"]
```

---

## Contributing

1. Fork the repo
2. Create a feature branch
3. Commit & push
4. Open a Pull Request with clear description and tests

---

## Notes & Next steps

* Add email service integration (SendGrid, SMTP) for verification.
* Add refresh tokens for longer sessions.
* Add file/photo upload for user avatars (Azure Blob / S3).

---

If you want, I can:

* Generate controller/service/repository code snippets for user CRUD in EF Core.
* Add ADO.NET examples for advanced queries (bulk insert / stored proc).
* Create a sample Postman collection for testing the API.
