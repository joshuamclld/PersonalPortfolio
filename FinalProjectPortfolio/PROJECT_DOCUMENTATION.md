# Dynamic Portfolio Management System

## System Architecture
The system is built using **ASP.NET Core MVC (Model-View-Controller)** architecture, leveraging **Entity Framework Core** for data access and **ASP.NET Core Identity** for authentication.

### Architectural Components:
1.  **Models (Data Layer)**: 
    *   Defines the data structure (Profile, Project, Experience, etc.).
    *   Uses Data Annotations for validation (Required, StringLength).
2.  **Views (Presentation Layer)**:
    *   **Public View (`Home/Index`)**: A single-page responsive layout displaying all portfolio sections.
    *   **Admin Views (`Admin/*`)**: Secure pages for managing content.
    *   **Layout**: Uses Bootstrap 5 for responsiveness and includes a dark mode toggle.
3.  **Controllers (Logic Layer)**:
    *   `HomeController`: Fetches data from the database and passes it to the public view.
    *   `AdminController`: Handles CRUD operations and file uploads. Protected by the `[Authorize]` attribute.

## Database Design
The database is managed via **SQL Server** using **Entity Framework Core Code-First Migrations**.

### Entities (Tables):
1.  **Profile**: Stores personal details (Name, Title, Bio, Profile Picture URL).
2.  **About**: Stores the "About Me" content (supports HTML).
3.  **Experience**: Stores work history (Job Title, Company, Start/End Dates, Description).
4.  **Project**: Stores portfolio projects (Title, Description, Image URL, Link).
5.  **Contact**: Stores contact info (Email, Phone, Social Links).
6.  **AspNetUsers**: Managed by Identity for admin authentication.

### Relationships:
*   The system is primarily composed of independent entities.
*   Images are stored in the file system (`wwwroot/uploads`), and the database stores the relative file paths.

## Features
*   **Authentication**: Secure login for the admin.
*   **CRUD Operations**: Full control over all content.
*   **Image Upload**: Automatic file renaming and storage management (deletes old images on update/delete).
*   **Responsive Design**: Adapts to Mobile, Tablet, and Desktop.
*   **Dark Mode**: Persists user preference using LocalStorage.

## Setup Instructions
1.  Update `appsettings.json` with your SQL Server connection string.
2.  Run Entity Framework migrations:
    ```bash
    dotnet ef migrations add InitialCreate
    dotnet ef database update
    ```
3.  Run the application.
4.  Register a user via the `/Identity/Account/Register` page (or Login if seeded).
