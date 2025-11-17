# PostOfficeProject
[![Lint Status](https://github.com/Alisky1223/PostOfficeProject/workflows/Lint/badge.svg)](https://github.com/Alisky1223/PostOfficeProject/actions/workflows/ci.yml)

This is a backend project built with .NET Core to demonstrate over 2 years of experience in developing scalable APIs. It simulates a post office management system, handling users, customers, postmen, post offices, products, transports, and authentication/authorization.

## Table of Contents
- [Features](#features)
- [Technologies](#technologies)
- [Project Structure](#project-structure)
- [Setup and Installation](#setup-and-installation)
- [Running the Project](#running-the-project)
- [API Endpoints](#api-endpoints)
- [Testing](#testing)
- [CI/CD](#cicd)
- [Future Improvements](#future-improvements)
- [License](#license)

## Features
- User registration, login, and role-based authentication.
- CRUD operations for post offices, customers, postmen, products, and transports.
- Database migrations for schema management.
- Data transfer objects (DTOs) for secure and efficient data handling.
- Unit testing for core components.

## Technologies
- .NET Core 8 
- Entity Framework Core for ORM and migrations
- ASP.NET Core for API development
- GitHub Actions for CI
- JWT for auth
- Blazor server side .Net 8
- Mud Blazor 9

## Project Structure
- **MainAPI**: Core API solution with the backend project, including controllers, services, and configurations.
  - `PostOfficeBackendProject`: Main application with src (Controllers, Models, Services), Program.cs, and appsettings.
  - `PostOfficeProject.Tests`: Unit tests.
- **Common**: Shared DLL with DTOs for entities like CustomerDto, ProductDto, etc.
- **AccountingAuthenticationAuthorization** (AAA): Handles auth, accounting, and user management with EF migrations.
- **Frontend**: Placeholder for frontend integration (e.g., React/Angular—not fully implemented).
- **.github/workflows**: CI pipeline for builds and tests.

## Setup and Installation
1. Clone the repository:

git clone https://github.com/Alisky1223/PostOfficeProject.git

2. Navigate to the project root.
3. Restore NuGet packages:

dotnet restore

4. Update database (if using EF):

dotnet ef database update --project AccountingAuthenticationAuthorization/AAA

Prerequisites:
- .NET SDK 8+
- SQL Server (or your DB provider)
- Visual Studio or VS Code

## Running the Project
1. Build the solution:

dotnet build

2. Run the main API:

dotnet run --project MainAPI/PostOfficeBackendProject/PostOfficeBackendProject

3. Access the API at `https://localhost:5001` (check launchSettings.json for ports).

## API Endpoints
(Example—replace with actual)
- `POST /api/auth/login`: User login.
- `GET /api/postoffices`: List post offices.
- `POST /api/products`: Create a product.
(Integrate Swagger: Navigate to `/swagger` after running.)

## Testing
Run unit tests:

dotnet test MainAPI/PostOfficeBackendProject/PostOfficeProject.Tests

## CI/CD
GitHub Actions workflow (`ci.yml`) automates builds and tests on push/pull requests.

## Future Improvements
- Docker Support
- Aim for cover 80% test

## License
MIT License (see LICENSE file).
