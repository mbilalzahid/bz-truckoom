# 🚚 Truckoom Maintenance Management System – Backend

This is the backend API for the Truckoom Maintenance Management System, developed with **ASP.NET Core 6.0**. It provides a secure and scalable solution to manage truck service schedules and associated maintenance tasks.

---

## 🧱 Architecture Overview

The project follows **Clean Architecture** principles:

- **Domain Layer** (`backend.Domain`): Contains core business models (Service, Task).
- **Application Layer** (`backend.Application`): Contains interfaces and business logic.
- **Infrastructure Layer** (`backend.Infrastructure`): Implements persistence with Entity Framework Core.
- **API Layer** (`backend`): ASP.NET Core Web API with JWT-based authentication.
- **Testing Layer** (`backend.Test`): MSTest project for unit testing service logic.

---

## ⚙️ Technologies

- **.NET 6.0**
- **ASP.NET Core Web API**
- **Entity Framework Core**
- **SQL Server (LocalDB)**
- **JWT Authentication**
- **MSTest**
- **Moq**

---

## 📦 Domain Models

### `Service`

| Property    | Type             | Description              |
| ----------- | ---------------- | ------------------------ |
| ServiceId   | int              | Primary key              |
| ServiceName | string (max 100) | Required name of service |
| ServiceDate | DateTime         | Required service date    |
| Tasks       | List< Task >     | Related tasks            |

### `Task`

| Property    | Type             | Description        |
| ----------- | ---------------- | ------------------ |
| TaskId      | int              | Primary key        |
| TaskName    | string (max 100) | Required task name |
| Description | string?          | Optional           |
| Remarks     | string?          | Optional           |

---

## 🔐 Authentication

The API uses **JWT Bearer Authentication** to protect all endpoints.

### Token Generation

JWTs are generated using the `JwtService` class:

```csharp
public string GenerateToken(string username)
```

### JWT Configuration in `appsettings.json`:

```json
"JwtSettings": {
    "SecretKey": "supersecretkey1234567890abcdef12345678",
    "Issuer": "TruckoomAPI",
    "Audience": "TruckoomClients",
    "ExpiryMinutes": 60
  }
```

---

## 🔗 API Endpoints

All endpoints are under the base route: `/api/services`

| Method | Endpoint | Description                   |
| ------ | -------- | ----------------------------- |
| GET    | `/`      | Retrieve all services + tasks |
| GET    | `/{id}`  | Get a service by ID           |
| POST   | `/`      | Create a new service          |
| PUT    | `/{id}`  | Update an existing service    |
| DELETE | `/{id}`  | Delete a service              |

> 🔒 All endpoints require a valid JWT Bearer token.

---

## 🗃️ Database

- **SQL Server LocalDB** used with **EF Core**
- Configured in `MaintenanceDbContext` under `backend.Infrastructure`

### Entity Relationship

- One-to-many relationship between `Service` and `Task`:

```csharp
modelBuilder.Entity<Service>()
    .HasMany(s => s.Tasks)
    .WithOne()
    .OnDelete(DeleteBehavior.Cascade);
```

---

## 🧪 Unit Testing

The `backend.Test` project contains unit tests for service logic using **MSTest** and **Moq**.

### Run All Tests:

```bash
dotnet test backend.Test
```

---

## 📁 Sample Request

file "truckoom_service_api.postman_collection.json" contains postman collection

```http
POST /api/services
Authorization: Bearer <token>
Content-Type: application/json

{
  "serviceName": "Oil Change",
  "serviceDate": "2025-05-28T00:00:00",
  "tasks": [
    {
      "taskName": "Change engine oil",
      "description": "Use 5W-30",
      "remarks": "Urgent"
    },
    {
      "taskName": "Replace oil filter"
    }
  ]
}
```

---

## 👨‍💻 Author

Developed by Bilal Zahid
© 2025 All rights reserved.

---
