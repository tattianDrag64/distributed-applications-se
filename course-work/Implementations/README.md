Library Management System
🔹 Introduction
The goal of this course project is to design and implement a full-featured library management system that facilitates both administrative and user interactions. The system is composed of two major components:
1.	Back-end – implemented as a RESTful Web API using ASP.NET Core
2.	Front-end – implemented with Blazor
The application aims to support essential library functionalities such as book browsing, reservations, track penalties.
🔹 System Architecture and Technologies
•	Back-end: ASP.NET Core Web API
•	Front-end: Blazor
•	Database: SQL Server using Entity Framework Core (Code-First approach)
•	Authentication: JWT Tokens
•	Object Mapping: AutoMapper
•	Validation: Data Annotations + FluentValidation
🔹 Core Functionalities
✅ For All Users:
•	Browse books by title, author, or genre
•	Search using multiple filters (e.g., author + genre)
•	Sort and paginate book listings
•	View available copies and book ratings
•	Submit and view reviews
•	Reserve books online
•	Scan entry codes to instantly display book info
🔒 For Admins Only:
•	Full CRUD access to:
o	Users
o	Books
o	Authors
o	Genres
o	Reservations
o	Reviews
o	Penalties
•	Manage available copies
•	Assign roles and handle penalty payments
🔹 Database Design Requirements
The project follows a Code-First approach with the following requirements fulfilled:
User, Book, Author, Genre, Reservation, Penalty
•	Each table has:
o	6 or more columns
o	At least 4 different data types (int, string, DateTime, bool, double)
o	Mandatory fields (e.g., Title, ReservationDate)
o	Text constraints (e.g., MaxLength(100))
🔹 Security and Validation
•	Full JWT-based authentication and authorization
•	Role-based access (e.g., Admin vs. Regular User)
•	Form and API validation using both:
o	DataAnnotations (server-side)
o	FluentValidation (complex rules)
•	Client-side input validation with feedback
•	📷 Book image uploads and previews
🔹 Folder Structure
LibraryManagementSystem/
├── Client/          # Blazor Frontend
├── Server/          # ASP.NET Core Backend
├── BaseLibrary/     # Shared Entities, DTOs, Mapping Profiles, Responses


