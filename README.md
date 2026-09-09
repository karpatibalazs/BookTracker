# 📚 BookTracker API

REST API for tracking books built with ASP.NET Core and Entity Framework Core.

## Tech Stack
- ASP.NET Core (.NET 8)
- Entity Framework Core + SQLite
- C#

## Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/books` | Get all books |
| GET | `/api/books?name={name}` | Search by name |
| GET | `/api/books/{id}` | Get by ID |
| POST | `/api/books` | Add a book |
| PUT | `/api/books/{id}` | Update a book |
| DELETE | `/api/books/{id}` | Delete a book |

## Run locally

```bash
git clone https://github.com/karpatibalazs/BookTrackerAPI.git
cd BookTrackerAPI
dotnet ef database update
dotnet run
```

Open `https://localhost:{port}/swagger` to explore the API.
