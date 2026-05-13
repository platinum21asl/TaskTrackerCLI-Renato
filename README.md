# 🚀 Task Tracker CLI - Enterprise Edition

**Task Tracker CLI** is a terminal-based task management application built with modern software development standards. This project is not just a basic CRUD tool, but a demonstration of implementing **Clean Architecture**, **Dependency Injection**, and **Automated Testing** within the .NET ecosystem.

---

## ✨ Key Features

- ✅ **Comprehensive Task Management**: Easily Add, Edit, Delete, and Filter tasks.
- 🎨 **Rich Terminal UI**: Utilizes `Spectre.Console` for elegant and colorful table displays.
- 🛠️ **Robust Argument Parsing**: Powered by `System.CommandLine` for input validation and automatic help menus.
- 💾 **Data Persistence**: Automatic storage using a structured JSON format.
- 🧪 **Test-Driven Design**: Business logic is fully tested using Unit Testing to ensure stability.

---

## 🏗️ Architecture & Design Patterns

This application is designed using the **Separation of Concerns** principle to ensure the code is maintainable and scalable:

1.  **Domain Model**: Pure data representation (`TaskItem`).
2.  **Repository Pattern**: Data access abstraction using `ITaskRepository`. This allows switching storage media (e.g., from JSON to SQL) without modifying the core business logic.
3.  **Service Layer**: The core of the business logic (`TaskService`), isolated from the UI and Database.
4.  **Dependency Injection (DI)**: Uses `Microsoft.Extensions.DependencyInjection` to manage object lifecycles and increase code flexibility.

---

## 🛠️ Tech Stack

- **Language**: C#
- **Framework**: .NET 8.0
- **CLI Framework**: System.CommandLine (Beta 4)
- **UI Library**: Spectre.Console
- **Unit Testing**: xUnit
- **Mocking**: Moq

---

## 🚀 Getting Started

### Prerequisites
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

### Installation & Running
1. Clone the repository:
   ```bash
   git clone [https://github.com/platinum21asl/TaskTrackerCLI-Renato.git](https://github.com/platinum21asl/TaskTrackerCLI-Renato.git)
   cd TaskTrackerCLI-Renato
2. Restore the packages:
	```bash
	dotnet restore
3. Run the application:
	```bash
	dotnet run -- --help


## 💻 Usage

| Command        | Description                     | Example                                               |
|----------------|---------------------------------|-------------------------------------------------------|
| `add`          | Add a new task                  | `dotnet run add "Learn Clean Architecture"`           |
| `list`         | Display all tasks               | `dotnet run list`                                     |
| `list --status`| Filter tasks by status          | `dotnet run list --status todo`                       |
| `update`       | Update task description         | `dotnet run update 1 "New Description"`               |
| `delete`       | Delete a task                   | `dotnet run delete 1`                                 |
| `mark-done`    | Mark a task as done             | `dotnet run mark-done 1`                              |

## Testing
This project includes an automated testing suite to ensure functionality works correctly. The tests utilize Mocking techniques to avoid mutating the actual data files.
	```bash
	cd TaskTracker.Tests
	dotnet test


*Last Updated: May 2026*