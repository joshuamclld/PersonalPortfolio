# My Dynamic ASP.NET Core Portfolio

![Portfolio Screenshot](https://via.placeholder.com/800x400.png?text=Your+Portfolio+Screenshot+Here)

This is my personal portfolio website, built from the ground up using ASP.NET Core. It's a fully dynamic web application with a custom admin dashboard that allows me to update every part of the site—from my personal bio to my latest projects—without ever touching the code.

**Live Demo:** [Link to your live site on Azure will go here!]

---

## About The Project

I wanted to create a portfolio that was more than just a static page. My goal was to build a real, data-driven application that I could easily manage and update as my career progresses. This project demonstrates my skills in full-stack .NET development, from database design to front-end styling and interactivity.

### Built With

This project was built with a modern .NET technology stack:

*   **ASP.NET Core 8**: The core web framework for building the application.
*   **Entity Framework Core**: For object-relational mapping (ORM) to manage the database.
*   **SQLite**: A lightweight, file-based database perfect for this type of application.
*   **ASP.NET Core Identity**: For secure user authentication and managing the admin login.
*   **Bootstrap 5**: For a responsive, mobile-first front-end design.
*   **jQuery & Vanilla JavaScript**: For client-side interactivity, including the theme toggle and contact form submission.
*   **AOS (Animate On Scroll)**: For the subtle and beautiful scroll animations.

---

## Features

*   **Dynamic Content Management**: Every piece of data is pulled from a database.
*   **Secure Admin Dashboard**: A password-protected `/admin` route where I can manage all site content.
*   **Full CRUD Functionality**: I can Create, Read, Update, and Delete entries for:
    *   My Profile (Name, Title, Bio, Picture, CV)
    *   About Me Section
    *   Services I Offer
    *   Skills & Expertise
    *   Education History
    *   Work Experience
    *   Portfolio Projects
    *   Social Media Links
*   **Image Uploads**: The system handles file uploads for my profile picture, project images, and service images, storing them securely on the web server.
*   **Light/Dark Mode Toggle**: A theme switcher that saves the user's preference in their browser.
*   **Interactive UI**: Smooth animations and hover effects using CSS and JavaScript.
*   **Automatic Project Scroller**: An infinitely scrolling marquee to showcase my projects in a clean, automated way.

---

## Getting Started

To get a local copy up and running, follow these simple steps.

### Prerequisites

You will need to have the .NET 8 SDK installed on your machine.
*   [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

### Installation

1.  **Clone the repo**
    ```sh
    git clone https://github.com/joshuamclld/PersonalPortfolio.git
    ```
2.  **Navigate to the project directory**
    ```sh
    cd PersonalPortfolio/FinalProjectPortfolio
    ```
3.  **Restore NuGet packages**
    ```sh
    dotnet restore
    ```
4.  **Set up the database**
    
    This project uses Entity Framework Core migrations to set up the database. If you don't have the EF tools installed, run this first:
    ```sh
    dotnet tool install --global dotnet-ef
    ```
    Then, run the migration to create your local SQLite database file:
    ```sh
    dotnet ef migrations add InitialCreate
    dotnet ef database update
    ```
5.  **Run the application**
    ```sh
    dotnet run
    ```
    The application will now be running on `https://localhost:7123` or a similar port.

---

## Usage

Once the application is running, you can view the public-facing portfolio. To manage the content:

1.  Navigate to `/Identity/Account/Login` to log in.
2.  After logging in, you will see a link to the **Admin Dashboard**.
3.  From the dashboard, you can click on any section to add, edit, or delete content. All changes will be reflected on the live site immediately.

---

## Contact

Joshua Macalalad - [joshuamacalalad550@gmail.com](mailto:joshuamacalalad550@gmail.com)

Project Link: [https://github.com/joshuamclld/PersonalPortfolio](https://github.com/joshuamclld/PersonalPortfolio)
