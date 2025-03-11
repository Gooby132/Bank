# Bank Application

A full-stack banking application built with React, PostgreSQL, and .NET 8. The application allows users to register, log in, manage their personal account, track previous transactions, and maintain an account balance. It includes form validation in Hebrew and supports internationalization (i18n) for translations.

## Technologies Used

### Frontend:
- **React** (UI framework)
- **Mantine** (Component library with form handling and validation)
- **Mantine Form** (For form validation)

### Backend:
- **.NET 8 (ASP.NET API)** (Web API layer)
- **Entity Framework Core** (ORM for database interactions)
- **PostgreSQL** (Database for storing user and transaction data)

### Architecture:
It was designed with DDD in mind for validation and the start of user flows

The backend follows a layered architecture with:
1. **Domain Layer** (Business logic and core models)
2. **Persistence Layer** (Database interactions using EF Core)
3. **API Layer** (Controllers and endpoints using ASP.NET API)
4. **Infrastructure** (Additional services like HTTP clients)

### Features
- **User Authentication** (Registration & Login) !not using tokens or any hashing on passwords!
- **Personal Dashboard** (Displays account balance and transactions)
- **Transaction History** (Users can view previous transactions)
- **Mocked Remote Service** (For demostration runs)

## Setup Instructions

### Prerequisites:
- Node.js (for running React frontend)
- .NET 8 SDK (for backend)
- PostgreSQL (database)

### Backend Setup:
1. Clone the repository:
   ```sh
   git clone https://github.com/Gooby132/Bank.git
   ```
2. Configure database connection in `appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Database=Bank;Username=youruser;Password=yourpassword"
     }
   }
   ```
3. Apply database migrations:
   ```sh
   dotnet ef database update
   ```
4. Run the backend:
   ```sh
   dotnet run
   ```

### Frontend Setup:
1. Navigate to the frontend directory:
   ```sh
   cd ../bank.client
   ```
2. Install dependencies:
   ```sh
   npm install
   ```
3. Run the frontend:
   ```sh
   npm run dev
   ```

### API Endpoints (Example)
- **POST /api/Users/register** - Register a new user
- **POST /api/Users/login** - User login
- **GET /api/Users/make-transaction** - Retrieve account balance