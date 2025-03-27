# 🌩️ CRMSystem - Basic CRM Application

This project is a **basic CRM (Customer Relationship Management)** application designed to manage customer data efficiently.  
Users can log in, register, create, list, update, and delete customer records.  
Authentication is handled via JWT tokens and session management is used on the web interface.

---

## 🚀 Technologies Used

- **.NET 9** (.NET Core)
- **Razor Pages** (CRM.Web UI Layer)
- **PostgreSQL** (Database)
- **Entity Framework Core** (ORM)
- **Swagger** (API testing interface)
- **JWT Authentication** (Token-based authentication)
- **Session Management** (For maintaining user sessions on the web)
- **Clean Architecture + Layered Architecture**
  - `CRM.API` – RESTful API layer
  - `CRM.Web` – Razor Pages frontend
  - `CRM.Infrastructure` – Database and service layer
  - `CRM.Application` – DTOs and service contracts
  - `CRM.Domain` – Entity models

---

## 🛠️ Setup Instructions

### 1. Clone the Repository

```bash
git clone https://github.com/FurkanYazbahar/CRMSystem.git
cd CRMSystem
```

### 2. Database Setup (via Docker)

Ensure you have Docker installed and running.  
You can start a PostgreSQL container with the following command:

```bash
docker run --name crm-db -e POSTGRES_PASSWORD=postgres -e POSTGRES_DB=crm-db -p 5432:5432 -d postgres
```

Update the connection string in `appsettings.json` accordingly:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Database=crm_db;Username=postgres;Password=postgres"
}
```

---

### 3. Apply Migrations

In Visual Studio's **Package Manager Console**, run:

```powershell
# Navigate to the solution root if needed
Add-Migration InitialCreate -StartupProject CRM.API -Project CRM.Infrastructure
Update-Database
```

---

### 4. Run the Application

- Set **CRM.API** as the startup project to launch the API (Swagger will open automatically).
- Set **CRM.Web** as the startup project to launch the frontend (Razor Pages).

---

## 🔐 Authentication

- Authentication is handled via JWT tokens.
- Login and Register endpoints are available via Swagger or through the Razor Pages UI.
- Upon successful login, the token is stored in the session and used for authenticated requests.

---

## 🥪 Swagger Test Interface

Once the API is running, open:

```
https://localhost:{port}/swagger
```

You can test endpoints like `/api/auth/login`, `/api/customer`, etc.  
Add the token via the `Authorize` button (Bearer token input).

---

## 📁 Folder Structure

```
CRMSystem/
├── CRM.API             # ASP.NET Core Web API
├── CRM.Web             # Razor Pages UI
├── CRM.Application     # DTOs and service interfaces
├── CRM.Infrastructure  # EF Core, repositories, services
├── CRM.Domain          # Entity definitions
```

---

## ✅ Features

- User registration and login
- Token-based authentication with JWT
- Session handling on the web frontend
- Customer CRUD operations
- Filtering by name, region, and registration date
- Role-based access support (can be extended)
- Responsive design for the Razor Pages UI

---

## 📌 Notes

- Make sure `ApiBaseUrl` is correctly set in `appsettings.json` inside **CRM.Web** for API communication.
- You can modify UI templates easily by editing Razor Pages and partial views.

---

Happy coding! 🚀

