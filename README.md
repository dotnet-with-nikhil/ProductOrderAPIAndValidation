# 🛒 Product & Order API (.NET 8 + Clean Architecture)

A production-ready **.NET 8 Web API** implementing:

* ✅ Clean Architecture (Controller → Service → Repository)
* ✅ Entity Framework Core (Code First + Migrations)
* ✅ SQL Server Integration
* ✅ FluentValidation
* ✅ Unit Testing (xUnit + Moq + FluentAssertions)
* ✅ Generic API Response Wrapper
* ✅ Logging Middleware

---

# 📁 Project Structure

```
ProductOrderAPIAndValidation/
│
├── ── ProductOrderAPI/
│
├── ── ProductOrderAPI.Tests/
```

---

# 🚀 Getting Started

## 🔧 Prerequisites

Make sure you have installed:

* .NET 8 SDK
* SQL Server (or LocalDB)
* Visual Studio 2022

---

## ▶️ Run the API

### Step 1: Clone the repository

```
git clone https://github.com/your-username/your-repo-name.git
cd ProductOrderAPIAndValidation
```

---

### Step 2: Update Connection String

Open:

```
src/ProductOrderAPI/appsettings.json
```

Update:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=ProductOrderDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

---

### Step 3: Apply Migrations

Run:

```
cd src/ProductOrderAPI
dotnet ef database update
```

---

### Step 4: Run the API

```
dotnet run
```

👉 API will start at:

```
https://localhost:xxxx
```

---

# 🧪 Run Unit Tests

From root folder:

```
dotnet test
```

---

# 📦 API Endpoints

## Product

| Method  | Endpoint          | Description         |
| ------- | ----------------- | ------------------- |
| GET     | /api/product      | Get all products    |
| GET     | /api/product/{id} | Get product by id   |
| POST    | /api/product      | Create product      |
| PUT     | /api/product/{id} | Update product      |
| PATCH   | /api/product/{id} | Partial update      |
| DELETE  | /api/product/{id} | Delete product      |
| HEAD    | /api/product/{id} | Check existence     |
| OPTIONS | /api/product      | Get allowed methods |

---

# 🧠 Key Concepts Covered

* EF Core Migrations
* Clean Architecture
* Dependency Injection
* Middleware Logging
* Generic API Response
* Unit Testing with Mocking
* RESTful API Design

---

# 🛠️ Tech Stack

* .NET 8
* ASP.NET Core Web API
* Entity Framework Core
* SQL Server
* xUnit + Moq + FluentAssertions
* FluentValidation

---

# 🎯 Use Cases

* Learning Clean Architecture
* Interview Preparation (3–8 years experience)
* Real-world API project reference

---

# 👨‍💻 Author

Nikhil G Parate

---

# ⭐ Support

If you found this helpful, give it a ⭐ on GitHub!
