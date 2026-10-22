<div align="center">

# 💸 SmartSpend API

**A personal budget and expense tracker backend built with ASP.NET Core, PostgreSQL and JWT.**

Track expenses, set budgets per category, get warned when you go over, and see where your money went in any date range.

![.NET](https://img.shields.io/badge/.NET-ASP.NET_Core-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-4169E1?style=for-the-badge&logo=postgresql&logoColor=white)
![JWT](https://img.shields.io/badge/Auth-JWT-000000?style=for-the-badge&logo=jsonwebtokens&logoColor=white)
![EF Core](https://img.shields.io/badge/EF_Core-Code_First-6DB33F?style=for-the-badge)

</div>

---

## ✨ Features

| | Feature | What it does |
|---|---|---|
| 🔐 | **Authentication** | Register and login with BCrypt hashed passwords and JWT bearer tokens |
| 🗂️ | **Categories** | Six seeded default categories plus your own custom ones |
| 🧾 | **Expenses** | Full CRUD, always scoped to the logged in user |
| 🎯 | **Budgets** | Set a budget per category for any custom date range |
| ⚠️ | **Budget warnings** | Every create or update of an expense returns a `budgetExceeded` flag |
| 📊 | **Reports** | One flexible endpoint: total spent plus a breakdown by category for any date range |
| 🛡️ | **Global error handling** | One middleware turns exceptions into clean JSON errors |

---

## 🧱 Tech Stack

- **ASP.NET Core Web API** (controller based)
- **Entity Framework Core** with the code first approach and migrations
- **PostgreSQL** through Npgsql
- **JWT Bearer** authentication
- **BCrypt.Net** for password hashing
- **User Secrets** to keep keys and passwords out of the repo

---

## 🏗️ Architecture

Each layer has one job.

```
HTTP Request
     │
     ▼
┌──────────────────────────┐
│ GlobalExceptionMiddleware │  catches any exception, returns clean JSON
└────────────┬─────────────┘
             ▼
┌──────────────────────────┐
│ Authentication / [Authorize] │  validates the JWT, reads userId from claims
└────────────┬─────────────┘
             ▼
┌──────────────────────────┐
│ Controllers               │  HTTP only: receive, call service, return status code
└────────────┬─────────────┘
             ▼
┌──────────────────────────┐
│ Services (via interfaces) │  business rules, ownership checks, budget logic
└────────────┬─────────────┘
             ▼
┌──────────────────────────┐
│ AppDbContext (EF Core)    │  talks to PostgreSQL
└──────────────────────────┘
```

### Project structure

```
SmartSpend.Api/
├── Controllers/     Auth, Categories, Expense, Budgets, Reports
├── Dtos/            Input and output shapes, grouped per feature
├── Interfaces/      One contract per service
├── Services/        Business logic
├── Models/          User, Category, Expense, Budget
├── Data/            AppDbContext, migrations, seeding
├── Middleware/      GlobalExceptionMiddleware
└── Program.cs       DI, JWT setup, pipeline
```

### Data model

```
User ──┬── has many ──> Expense  ──> belongs to one Category
       ├── has many ──> Budget   ──> belongs to one Category
       └── has many ──> Category (custom ones only)
```

Default categories have `UserId = null` and `IsDefault = true`, so every user can see them but nobody can edit or delete them.

---

## 🔒 Design decisions

- **userId comes from the token, never from the request.** Every query filters by it, so users cannot see or touch each other's data.
- **Models never leave the service layer.** Controllers only send and receive DTOs, which avoids leaking fields and circular JSON.
- **Ownership is checked before saving.** A category id sent by the client is validated as "mine or default" before any expense or budget is created.
- **Money uses `decimal`**, not `double`, to avoid rounding errors.
- **Dates use `DateOnly?` in input DTOs** so `[Required]` really fails when a date is missing.
- **Expenses are never blocked by budgets.** Tracking comes first. The API only warns with `budgetExceeded`.
- **A category in use cannot be deleted.** This stops the database from silently cascading and wiping expenses.
- **Secrets are not in the repo.** Keys and passwords live in user secrets.

---

## 🚀 Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download)
- [PostgreSQL](https://www.postgresql.org/download/)
- The EF Core CLI: `dotnet tool install --global dotnet-ef`

### 1. Clone

```bash
git clone https://github.com/Muhammad-Qasim-Akram/SmartSpend.Api.git
cd SmartSpend.Api
```

### 2. Set your secrets

```bash
dotnet user-secrets init
dotnet user-secrets set "JwtSettings:SecretKey" "use_a_long_random_string_of_32_or_more_characters"
dotnet user-secrets set "ConnectionStrings:SmartSpendDb" "Host=localhost;Port=5432;Database=SmartSpend;Username=postgres;Password=YOUR_PASSWORD"
```

### 3. Run

```bash
dotnet run
```

On startup the app applies migrations, creates the database and tables, and seeds the default categories. Nothing else to do.

---

## 📡 API Reference

Everything except register and login needs this header:

```
Authorization: Bearer <your token>
```

### Auth (public)

| Method | Endpoint | Description |
|---|---|---|
| POST | `/api/auth/register` | Create an account, returns user info and token |
| POST | `/api/auth/login` | Login, returns user info and token |

### Categories

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/categories` | Default categories plus your own |
| GET | `/api/categories/{id}` | One category |
| POST | `/api/categories` | Create a custom category |
| PUT | `/api/categories/{id}` | Rename your own category |
| DELETE | `/api/categories/{id}` | Delete your own category (blocked if in use) |

### Expenses

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/expense` | All your expenses |
| GET | `/api/expense/{id}` | One expense |
| POST | `/api/expense` | Add an expense, returns `budgetExceeded` |
| PUT | `/api/expense/{id}` | Update an expense, returns `budgetExceeded` |
| DELETE | `/api/expense/{id}` | Delete an expense |

### Budgets

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/budgets` | All your budgets |
| GET | `/api/budgets/{id}` | One budget |
| POST | `/api/budgets` | Create a budget for a category and date range |
| PUT | `/api/budgets/{id}` | Update a budget |
| DELETE | `/api/budgets/{id}` | Delete a budget |

### Reports

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/reports?startDate=2026-10-01&endDate=2026-10-31` | Total spent and per category breakdown |

Last week, last month, all time and custom ranges are all the same call with different dates. The frontend picks the dates.

---

## 🧪 Example

**Create an expense**

```http
POST /api/expense
Authorization: Bearer <token>
Content-Type: application/json

{
  "amount": 500,
  "description": "Lunch",
  "date": "2026-10-22",
  "categoryId": 1
}
```

**Response `201 Created`**

```json
{
  "id": 7,
  "amount": 500,
  "description": "Lunch",
  "categoryName": "Food",
  "date": "2026-10-22",
  "budgetExceeded": false
}
```

**Report response**

```json
{
  "startDate": "2026-10-01",
  "endDate": "2026-10-31",
  "totalSpent": 5200,
  "categoryBreakdown": [
    { "categoryName": "Food", "totalSpending": 5200, "totalExpenses": 3 }
  ]
}
```

**Error response**

```json
{ "error": "Email already exists." }
```

### Status codes

| Code | Meaning |
|---|---|
| 200 | Success |
| 201 | Created |
| 204 | Deleted |
| 400 | Invalid input or a business rule failed |
| 401 | Missing, expired or invalid token |
| 404 | Not found, or it belongs to someone else |
| 500 | Unexpected server error, details are logged only on the server |

---

## 🗺️ Roadmap

- [ ] Refresh tokens
- [ ] Email verification on register
- [ ] Custom exceptions for 401 and 409 responses
- [ ] Unit and integration tests
- [ ] Pagination on list endpoints
- [ ] `DeleteBehavior.Restrict` on category foreign keys as a database level safety net
- [ ] React frontend dashboard

---

## 👤 Author

**Muhammad Qasim Akram**
BS Computer Science, Islamia University of Bahawalpur (2023 to 2027)

[Portfolio](https://qasim-akram.netlify.app) · [GitHub](https://github.com/Muhammad-Qasim-Akram) · [LinkedIn](https://linkedin.com/in/qasimakram)

---

<div align="center">

Built to learn backend engineering properly, one layer at a time. ⭐ a star is always welcome.

</div>