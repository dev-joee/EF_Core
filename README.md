Got the structure. Now let me fetch the key files for a complete picture.

Got everything I need. Here's a full description based on the actual code:

---

## EF_Core — Blog Manager (Console App)

A C# console application built on **.NET 10** that demonstrates the **Code First** approach with **Entity Framework Core**, using **SQLite** as the database backend.

### What it does

It's an interactive terminal app called **Blog Manager** that lets you manage blogs and their posts through a numbered menu.

### Architecture

The project follows a clean layered architecture with a clear comment in the code describing it:

```
UI ↔ DataManager ↔ Models ↔ BloggingContext ↔ Database
```

- **`Program.cs`** — entry point; drives the menu loop with input validation and a continue/quit prompt
- **`DataManager.cs`** — the service/logic layer; handles all console I/O and delegates to models
- **`models/`** — contains the `Blog` and `Post` entity classes
- **`context/`** — contains `BloggingContext` (the `DbContext` connecting EF Core to SQLite)
- **`db/`** — likely holds the SQLite `.db` file
- **`Migrations/`** — EF Core Code First migration history

### Features

**Blog management:**
- Add a new blog (name + URL)
- List all blogs (id, name, URL)

**Post management:**
- Add a post (title + content, linked to a chosen blog by ID)
- List all posts (id, title, content, blog ID)
- Update a post (by ID — change title and content)
- Delete a post (by ID, with existence check before deletion)

### Technical highlights

- **EF Core 10** with **SQLite** provider — lightweight, no separate DB server needed
- **Code First** with migrations for schema management
- Input validation throughout — numeric parsing guards, empty string fallbacks with `?? string.Empty`, and loop-until-valid patterns
- Blog ID validation when adding a post (checks against real IDs from the DB, not just any number)
- Graceful empty-state handling (e.g. "no blogs yet", "post not found")
- Nullable reference types enabled (`<Nullable>enable</Nullable>`)

### Stack

| Layer | Technology |
|---|---|
| Language | C# |
| Framework | .NET 10 |
| ORM | EF Core 10 |
| Database | SQLite |
| App type | Console (CLI) |
