# Mini Library Management System

A simple library management system built using ASP.NET Core and PostgreSQL. This system allows users to manage books, authors, and their relationships through a user-friendly interface.

## Table of Contents

- [Project Overview](#project-overview)
- [Technologies Used](#technologies-used)
- [Features](#features)
- [Installation](#installation)
- [Usage](#usage)
- [Code Overview](#code-overview)
- [Example Usage](#example-usage)
- [Sample Outputs](#sample-outputs)
- [Contributions](#contributions)
- [License](#license)

## Project Overview

This Library Management System allows users to manage books, authors, and their relationships. It supports creating, reading, updating, and deleting books and authors.

Key features include:
- Adding and viewing authors and books.
- Associating authors with books (one-to-many relationship).
- Validations for adding/editing books and authors.
- Responsive UI using Bootstrap.

The project is built with **ASP.NET Core**, **Entity Framework Core**, and **PostgreSQL** as the database.

## Technologies Used

- **ASP.NET Core 8** - The web framework for building the application.
- **Entity Framework Core** - The ORM for interacting with PostgreSQL.
- **PostgreSQL** - Relational database for storing data.
- **Bootstrap** - Frontend framework used to build the responsive UI.

## Features

- **Author Management**:
  - Create, read, update, and delete authors.
  - View the list of authors and associated books.
- **Book Management**:
  - Create, read, update, and delete books.
  - Select authors for each book and manage their details.
- **CRUD Operations** for both books and authors.
- **Database Migrations** using Entity Framework Core.

## Installation

Follow these steps to set up the project on your local machine:

1. **Clone the repository**:

    ```bash
    git clone https://github.com/yourusername/library-management-system.git
    ```

2. **Install the necessary dependencies**:
   - Ensure you have **.NET 8 SDK** and **PostgreSQL** installed on your machine.
   - Run the following command to install required dependencies:

    ```bash
    dotnet restore
    ```

3. **Set up the PostgreSQL database**:
   - Create a PostgreSQL database.
   - Update the connection string in `appsettings.json`:

    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Host=localhost;Database=librarydb;Username=yourusername;Password=yourpassword"
    }
    ```

4. **Apply migrations to the database**:

    ```bash
    dotnet ef database update
    ```

5. **Run the application**:

    ```bash
    dotnet run
    ```

6. Open a browser and navigate to `http://localhost:5000` to use the application.

## Usage

### Author Management

- **Create Author**: Navigate to the **Create Author** page, enter the first name, last name, and date of birth, then submit.
- **Edit Author**: You can update author information by clicking on the **Edit** button next to an author’s name in the author list.
- **Delete Author**: You can delete an author by clicking on the **Delete** button next to the author's name. This will also delete all the associated books.

### Book Management

- **Create Book**: Select an author from a dropdown, enter the book details (title, genre, ISBN, publish date, and available copies), and submit.
- **Edit Book**: Update book details (such as title, genre, or author) by clicking on the **Edit** button next to a book in the list.
- **Delete Book**: You can delete a book by clicking on the **Delete** button next to the book's title.

## Code Overview

The core files and structures of the project are as follows:

### Models
- **Author.cs** - Represents an author in the system.
- **Book.cs** - Represents a book in the system.
- **BookViewModel.cs** - Represents the data for creating and editing books.
- **AuthorViewModel.cs** - Represents the data for creating and editing authors.

### Controllers
- **AuthorController.cs** - Handles the CRUD operations for authors.
- **BookController.cs** - Handles the CRUD operations for books.

### Views
- **Books/Index.cshtml** - Displays the list of books.
- **Books/Create.cshtml** - Form for creating a new book.
- **Authors/Index.cshtml** - Displays the list of authors.
- **Authors/Create.cshtml** - Form for creating a new author.

### Database
- Uses **Entity Framework Core** to interact with the PostgreSQL database. The models are linked via a one-to-many relationship where one author can write multiple books.

## Example Usage

### Creating an Author:

1. Navigate to **Authors > Create**.
2. Enter the following details:
    - First Name: **Ahmet**
    - Last Name: **Ümit**
    - Date of Birth: **28.11.1966**
3. Click **Save**.
4. You will be redirected to the authors list, and the new author will appear there.

### Creating a Book:

1. Navigate to **Books > Create**.
2. Enter the following details:
    - Title: **Beyoglu Rapsodisi**
    - Genre: **Fiction**
    - Author: **Ahmet Ümit**
    - Publish Date: **05.10.2003**
    - ISBN: **1234567890**
    - Copies Available: **7**
3. Click **Save**.
4. You will be redirected to the books list, and the new book will appear there.

### Sample Outputs

1. **Author List**:

| Full Name           | Date of Birth | Books Written | Actions           |
|---------------------|---------------|---------------|-------------------|
| Ahmet Ümit          | 28.11.1966    | 2             | [Details] [Edit] [Delete] |
| Ahmet Hamdi Tanpınar | 22.06.1901    | 0             | [Details] [Edit] [Delete] |

2. **Book List**:

| Title                | Genre           | Author          | Publish Date | Copies Available | Actions           |
|----------------------|-----------------|-----------------|--------------|------------------|-------------------|
| Beyoglu Rapsodisi    | Fiction         | Ahmet Ümit      | 05.10.2003   | 7                | [Details] [Edit] [Delete] |
| The Puppet (Kukla)   | Fantasy Fiction | Ahmet Ümit      | 07.07.2002   | 69               | [Details] [Edit] [Delete] |

## Contributions

Feel free to fork the project and submit pull requests. If you have suggestions for improvements, issues, or features, please create an issue or a pull request.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
