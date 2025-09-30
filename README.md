# Onion Architecture with ASP.NET Core 8.0

A clean, scalable ASP.NET Core Web API implementation demonstrating **Onion Architecture** and **SOLID principles**. This project serves as a practical learning resource for building enterprise-level applications with proper separation of concerns.

## 📋 Overview

This is a Company-Employee management system that showcases how to structure a .NET application using Onion Architecture, where the domain layer sits at the core and dependencies flow inward, making the system maintainable, testable, and loosely coupled.

## 🏗️ Architecture

The project follows the **Onion Architecture** pattern with these layers:

```
┌─────────────────────────────────────────┐
│     Presentation Layer (API)            │  ← Controllers, HTTP concerns
├─────────────────────────────────────────┤
│     Service Layer (Business Logic)      │  ← Application services
├─────────────────────────────────────────┤
│     Repository Layer (Data Access)      │  ← EF Core, data operations
├─────────────────────────────────────────┤
│     Domain Layer (Entities)             │  ← Core business models
└─────────────────────────────────────────┘
         Infrastructure (Logging)          ← Cross-cutting concerns
```

### Layer Breakdown

#### 1. **Domain Layer** (`Entities/`)
- Contains core business entities: `Company` and `Employee`
- No dependencies on other layers
- Pure domain models with data annotations

#### 2. **Repository Layer** (`Repository/`, `Contracts/`)
- Implements the Repository pattern
- Generic `RepositoryBase<T>` for common CRUD operations
- Specific repositories: `CompanyRepository`, `EmployeeRepository`
- `RepositoryManager` implements Unit of Work pattern
- Entity Framework Core `DbContext` integration

#### 3. **Service Layer** (`Service/`, `Service.Contract/`)
- Contains business logic
- Service interfaces in `Service.Contract` for abstraction
- Implementations: `CompanyService`, `EmployeeService`
- `ServiceManager` coordinates multiple services

#### 4. **Presentation Layer** (`Presentation/`)
- RESTful API controllers
- Handles HTTP requests/responses
- Minimal logic, delegates to services

#### 5. **Infrastructure** (`LoggerService/`)
- Cross-cutting concerns
- NLog integration for structured logging

#### 6. **Composition Root** (`Onions/`)
- Main Web API project
- Dependency injection configuration
- Startup and middleware setup

## 🎯 Design Patterns & Principles

### SOLID Principles
- **Single Responsibility**: Each class has one clear purpose
- **Open/Closed**: Extensible through interfaces and abstractions
- **Liskov Substitution**: Interfaces allow substitutable implementations
- **Interface Segregation**: Focused, specific interfaces
- **Dependency Inversion**: High-level modules depend on abstractions

### Design Patterns
- **Repository Pattern**: Abstracts data access logic
- **Unit of Work**: Manages transactions across repositories
- **Dependency Injection**: Constructor injection throughout
- **Lazy Initialization**: Services and repositories loaded on-demand
- **Generic Repository**: Reusable CRUD operations

## 🚀 Features

- RESTful API endpoints for Company and Employee management
- Entity Framework Core with SQL Server
- Structured logging with NLog
- CORS support for cross-origin requests
- Tracking/non-tracking query options for performance
- Database migrations for version control
- Seed data initialization

## 🛠️ Tech Stack

- **Framework**: ASP.NET Core 8.0
- **ORM**: Entity Framework Core
- **Database**: SQL Server
- **Logging**: NLog
- **Architecture**: Onion Architecture
- **Patterns**: Repository, Unit of Work, Dependency Injection

## 📦 Project Structure

```
Onion/
├── Onions/                    # Main Web API project
│   ├── Extensions/            # Service configuration extensions
│   ├── Migrations/            # EF Core migrations
│   └── Program.cs             # Application entry point
├── Presentation/              # API Controllers
├── Service/                   # Business logic implementation
├── Service.Contract/          # Service interfaces
├── Repository/                # Data access implementation
│   ├── Configuration/         # EF Core entity configurations
│   └── RepositoryContext.cs   # DbContext
├── Contracts/                 # Repository interfaces
├── Entities/                  # Domain models
│   └── Models/
│       ├── Company.cs
│       └── Employee.cs
└── LoggerService/             # Logging infrastructure
```

## 🔧 Setup & Configuration

### Prerequisites
- .NET 8.0 SDK
- SQL Server
- Visual Studio 2022 or VS Code

### Configuration

1. **Database Connection**: Update connection string in `appsettings.json`:
```json
"ConnectionStrings": {
  "sqlConnection": "server=YOUR_SERVER; database=CompanyEmployee; Integrated Security=true;TrustServerCertificate=true;"
}
```

2. **Apply Migrations**:
```bash
dotnet ef database update
```

3. **Run the Application**:
```bash
dotnet run --project Onions
```

The API will be available at:
- HTTPS: `https://localhost:5001`
- HTTP: `http://localhost:5000`

## 📡 API Endpoints

### Companies
- `GET /api/companies` - Get all companies

### Employees
- *(To be implemented)*

## 🧪 Code Examples

### Repository Pattern Usage
```csharp
public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
{
    public CompanyRepository(RepositoryContext repositoryContext)
        : base(repositoryContext) { }

    public IEnumerable<Company> GetAllCompanies(bool trackChanges)
        => FindAll(trackChanges).OrderBy(c => c.Name).ToList();
}
```

### Service Layer
```csharp
public class CompanyService : ICompanyService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;

    public IEnumerable<Company> GetAllCompanies(bool trackChanges)
    {
        var companies = _repository.Company.GetAllCompanies(trackChanges);
        return companies;
    }
}
```

### Dependency Injection
```csharp
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureSqlContext(builder.Configuration);
```

## 🎓 Learning Objectives

This project demonstrates:
- How to implement Onion Architecture in .NET
- Proper separation of concerns across layers
- Dependency injection and inversion of control
- Repository and Unit of Work patterns
- Entity Framework Core best practices
- RESTful API design
- Structured logging
- SOLID principles in practice

## 🔮 Future Enhancements

- [ ] Complete CRUD operations for Companies and Employees
- [ ] Add AutoMapper for DTO mapping
- [ ] Implement authentication & authorization (JWT)
- [ ] Add validation with FluentValidation
- [ ] Unit and integration tests
- [ ] API versioning
- [ ] Pagination and filtering
- [ ] Global error handling middleware
- [ ] Swagger/OpenAPI documentation
- [ ] Caching layer (Redis)
- [ ] Docker containerization

## 📝 License

This project is for educational purposes and learning Onion Architecture implementation.

## 🤝 Contributing

This is a learning project. Feel free to fork and experiment with different architectural approaches!

---

**Note**: This is an ongoing project for learning and implementing clean architecture principles in .NET.