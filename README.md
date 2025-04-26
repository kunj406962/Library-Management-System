# Library Management System

## Overview
The Library Management System is a web-based application built using Blazor to manage books and customers in a library. It allows users to add, edit, delete, borrow, and return books, as well as manage customer information. The application uses HeidiSQL as the database management system to store and retrieve data.

## Features
- **Book Management**:
  - Add new books (`AddBookForm.razor`)
  - Edit existing books (`EditBook.razor`)
  - Delete books (`Books.razor`)
  - View all books in a table (`Books.razor`)

- **Customer Management**:
  - Add new customers (`AddCustomerForm.razor`)
  - Edit customer details (`EditCustomer.razor`)
  - Delete customers (`Customers.razor`)
  - View all customers in a table (`Customers.razor`)

- **Book Borrowing and Returning**:
  - Borrow books for customers (`Bookborrowing.razor`)
  - Return borrowed books (`ReturnBook.razor`)

- **Home Page**:
  - A welcoming interface with navigation to books and customers (`Home.razor`)

## Technologies Used
- **Frontend**: Blagor (Server-side)
- **Backend**: C# with .NET
- **Database**: HeidiSQL (MySQL-based)
- **Styling**: Bootstrap 5 with Bootstrap Icons
- **Navigation**: Blazor NavigationManager for routing

## Prerequisites
To run this application, ensure you have the following installed:
- [.NET SDK](https://dotnet.microsoft.com/download) (e.g., .NET 6 or later)
- [HeidiSQL](https://www.heidisql.com/download.php)
- A MySQL server (local or remote)
- A modern web browser (e.g., Chrome, Firefox, Edge)

## Setup Instructions
1. **Clone the Repository**:
   ```bash
   git clone <repository-url>
   cd library-management-system
   ```

2. **Database Setup**:
   - Install and configure HeidiSQL.
   - Create a MySQL database (e.g., `library_db`).
   - Create tables for:
     - `Books` (BookId, Title, Author, Genre, Quantity)
     - `Customers` (CustomerID, FirstName, LastName, Email, Phone)
     - `BorrowBooks` (BorrowId, CustomerId, BookId, Quantity, Returned)
   - Update the database connection string in the application configuration (e.g., `appsettings.json`).

3. **Configure the Application**:
   - Open the project in Visual Studio or another IDE.
   - Configure the `DatabaseManager` class to connect to your HeidiSQL database.
   - Verify dependencies in `Program.cs` for Blazor setup.

4. **Run the Application**:
   ```bash
   dotnet restore
   dotnet build
   dotnet run
   ```
   - Access the application at `https://localhost:5001` (or the specified port).

5. **Access the Application**:
   - Navigate to the home page (`/`) to start.
   - Use links to manage books (`/books`) or customers (`/customers`).

## File Structure
- **Pages**:
  - `AddBookForm.razor`: Form to add a new book.
  - `AddCustomerForm.razor`: Form to add a new customer.
  - `Bookborrowing.razor`: Interface to borrow books.
  - `Books.razor`: Manages the list of books.
  - `Customers.razor`: Manages the list of customers.
  - `EditBook.razor`: Form to edit book details.
  - `EditCustomer.razor`: Form to edit customer details.
  - `Home.razor`: Landing page with navigation.
  - `ReturnBook.razor`: Interface to return borrowed books.

- **Classes** (in `CPRG211FinalProject.Classes`):
  - `Book`: Model for book data.
  - `Customer`: Model for customer data.
  - `BorrowBooks`: Model for borrowing records.
  - `BookManager`: Handles book operations.
  - `CustomerManager`: Handles customer operations.
  - `BorrowManager`: Handles borrowing and returning logic.
  - `DatabaseManager`: Manages database interactions.

## Usage
- **Adding a Book**:
  - Go to `/addbook`, enter details (title, author, genre, quantity), and submit.
  - Quantity must be a positive whole number.

- **Adding a Customer**:
  - Go to `/addcustomer`, enter details (first name, last name, email, phone), and submit.
  - Email and phone are validated for correct formats.

- **Borrowing a Book**:
  - Go to `/bookborrowing`, enter a customer ID, select a book, specify quantity, and submit.
  - Borrow quantity cannot exceed available stock.

- **Returning a Book**:
  - Go to `/returnbook`, enter a customer ID, select a borrowed book, and submit.
  - Updates book quantity and borrow status.

- **Editing/Deleting**:
  - Use `/books` or `/customers` to edit or delete entries via action buttons.

## Notes
- The application relies on a `DatabaseManager` class for HeidiSQL interactions.
- Input validation ensures positive quantities and valid email/phone formats.
- Alerts use `App.Current.MainPage.DisplayAlert` for user feedback.
- The UI uses Bootstrap for responsive design and Bootstrap Icons for visuals.

## Contribution
1. Hans:
- Bootstrap
- BorrowBooks.razor, BorrowManager.cs
- EditBook.razor
- Output document
2. Kunj:
- Books.razor, BookManager.cs
- AddBookForm.razor
- Initially Detailed Documentaion of the Project: Database entities, Features, Methods needed for the application
- Database Operation: created dababase and SQL script for creating tables, sample data for Customer, Book and BorrowBook
- Help debugging the lists, created the interface, worked in Exception Handling
3. Minh Tam:
- Customer.razor, CustomerManager.cs
- AddCustomerForm.razor
- EditCustomer.razor
- Created Class and ERD diagrams
4. Enzo:
- ReturnBook.razor
- Added some code in customers page, return book page
- Fixed bug in dbaccessor, added comments
- Created Presentation slides
