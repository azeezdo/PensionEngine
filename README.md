# PensionEngine
Pension system technical assessment : This describe Project setup instruction, API documentation, Architecture overview, Design decisions explanation

A **.NET 8 Web API** for managing member pension contributions, benefits, and background financial processing.  
The system implements **Clean Architecture**, **DDD principles**, and uses **Hangfire** for background jobs such as contribution validation, interest calculation, and benefit eligibility updates.

 Architecture Overview

This project follows **Clean Architecture / Onion Architecture**, ensuring clear separation of concerns and testability.

```
src/
│
├── Pension.Domain/           # Entities, Value Objects, Domain Events
│   └── Member.cs, Contribution.cs, Benefit.cs, Employer.cs
│
├── Pension.Application/      # Business logic, Use Cases, Interfaces
│   └── Services, DTOs, Validators, Interfaces
│
├── Pension.Infrastructure/   # EF Core, Repository Implementations, Data Access
│   └── ApplicationDbContext.cs, Repository.cs, Hangfire Services
│
├── Pension.API/              # Presentation Layer (Controllers, Middleware)
│   └── Controllers, Swagger, DI setup, Exception Handling
│
└── tests/
    └── Pension.Tests/        # Unit and Integration Tests (NUnit)
```

## Key Components

| Layer | Responsibility | Technologies |
|-------|----------------|---------------|
| **Domain** | Core entities & business rules | POCO classes |
| **Application** | Business logic, interfaces, validation | FluentValidation, AutoMapper |
| **Infrastructure** | Database, background jobs | EF Core, SQL Server, Hangfire, Serilog |
| **API** | RESTful endpoints, DI, middleware | ASP.NET Core Web API, Swagger |

---

## Project Setup Instructions

## Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or VS Code
- [Hangfire Dashboard](https://www.hangfire.io/)
- [Postman](https://www.postman.com/) for API testing

## Steps to Run the Project

1. **Clone Repository**
   ```bash
   git clone https://github.com/azeezdo/PensionEngine.git
   cd pension-contribution-system
   ```

2. **Set Up Database Connection**
   In `appsettings.json` (inside `Pension.API`), update:
   ```json
   "ConnectionStrings": {
       "DefaultConnection": "Server = .;Database = PensionDb; Integrated Security = true; TrustServerCertificate = True;"
   }
   ```

3. **Apply Migrations**
   ```bash
   cd src/Pension.Infrastructure
   dotnet ef database update
   ```

4. **Run the Application**
   ```bash
   cd ../Pension.API
   dotnet run
   ```

5. **Access the Application**
   - Swagger UI → `https://localhost:7133/swagger`
   - Hangfire Dashboard → `https://localhost:5001/hangfire`

---

## API Documentation

## Member Management
| Method | Endpoint | Description |
|--------|-----------|-------------|
| `POST` | `/api/members` | Register a new member |
| `GET` | `/api/members/{id}` | Retrieve member details |
| `PUT` | `/api/members/{id}` | Update member information |
| `DELETE` | `/api/members/{id}` | Soft delete a member |

**Example Request (POST):**
```json
{
  "firstName": "Azeez",
  "lastName": "Dauda",
  "email": "azeez@example.com",
  "dateOfBirth": "1985-02-15",
  "phoneNumber": "08012345678"
}
```

---

## Contribution Management
| Method | Endpoint | Description |
|--------|-----------|-------------|
| `POST` | `/api/contributions` | Add new contribution (Monthly/Voluntary) |
| `GET` | `/api/contributions/member/{memberId}` | Get all contributions by member |
| `GET` | `/api/contributions/statement/{memberId}` | Generate contribution statement |

**Example Request (POST):**
```json
{
  "memberId": "95a12336-00b3-4c7e-8095-253e94419f4b",
  "contributionType": "Monthly",
  "amount": 5000,
  "contributionDate": "2025-11-01",
  "referenceNumber": "REF-001"
}
```

---

## Benefit Management
| Method | Endpoint | Description |
|--------|-----------|-------------|
| `GET` | `/api/benefits/{memberId}` | Retrieve member benefit details |
| `POST` | `/api/benefits/calculate` | Calculate member benefits |

**Business Rules**
- Monthly contributions limited to **one per month**
- Voluntary contributions **anytime**
- Benefits eligible after **12 months of contribution**
- Minimum age for membership: **18 years**

---

## Background Jobs (Hangfire)
| Job Name | Schedule | Description |
|-----------|-----------|-------------|
| `monthly-contribution-validation` | Monthly | Validates one contribution per month |
| `benefit-eligibility-update` | Daily | Updates member eligibility status |
| `monthly-interest-calculation` | Monthly | Adds monthly interest to contributions |
| `generate-member-statements` | Monthly | Creates member statements |

---

## Design Decisions

### 1. Clean Architecture
Used to ensure scalability, testability, and independent evolution of each layer.

### 2. Entity Framework Core
Chosen for data access to support LINQ, migrations, and SQL Server integration.

### 3. Repository Pattern
Encapsulates data logic, reducing code duplication and improving testability.

### 4. Hangfire
Selected for background processing (interest calculation, validation, reports).

### 5. FluentValidation
Used for declarative, maintainable validation of entities (Member, Contribution, Employer).

### 6. Serilog
Used for structured logging and tracking system activity, integrated with Hangfire job logs.

### 7. Testing
Implemented **NUnit** for unit testing and **Moq** for mocking repository dependencies.

---

## Testing

### Run Tests
```bash
cd tests/Pension.Tests
dotnet test
```

### Coverage Goals
- Unit Tests: ≥ 70%
- Integration Tests: API + Database operations

---

## Technology Stack

| Category | Tool |
|-----------|------|
| Framework | .NET 8 Web API |
| ORM | Entity Framework Core |
| Database | SQL Server |
| Background Jobs | Hangfire |
| Validation | FluentValidation |
| Logging | Serilog |
| Testing | NUnit, Moq |
| Documentation | Swagger / OpenAPI |
