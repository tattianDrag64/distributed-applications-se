Library Management System
A full-featured system that allows both end users and administrators to interact with a
digital library environment. It offers book browsing, reservations, review submission, and
penalty tracking, while enabling admins to manage the system through CRUD operations.
Technologies Used:
- ASP.NET Core Web API (Back-end)
- Blazor (Front-end)
- SQL Server with Entity Framework Core (Code-First)
- JWT Authentication, Role-based Authorization
- AutoMapper, FluentValidation
Security and Validation:
- JWT-based authentication
- Admin vs. User role separation
- DataAnnotations + FluentValidation
- Client-side validation feedback
Entities and Table Structure:
CREATE TABLE Users (
Id INT PRIMARY KEY,
FullName NVARCHAR(100) NOT NULL,
Username NVARCHAR(50) NOT NULL,
PasswordHash NVARCHAR(255) NOT NULL,
Email NVARCHAR(100),
PhoneNumber NVARCHAR(20),
Role NVARCHAR(20) NOT NULL,
ImageUrl NVARCHAR(255),
Description NVARCHAR(1000),
TotalReadBooks INT DEFAULT 0,
CreatedAt DATETIME NOT NULL,
UpdatedAt DATETIME,
IsDeleted BIT DEFAULT 0
);
CREATE TABLE Authors (
Id INT PRIMARY KEY,
Name NVARCHAR(50) NOT NULL,
Biography NVARCHAR(1000),
DateOfBirth DATETIME NOT NULL,
ImageUrl NVARCHAR(255),
CreatedAt DATETIME NOT NULL,
UpdatedAt DATETIME,
IsDeleted BIT DEFAULT 0
);
CREATE TABLE Genres (

Id INT PRIMARY KEY,
Name NVARCHAR(20),
Description NVARCHAR(1000),
CreatedAt DATETIME NOT NULL,
UpdatedAt DATETIME,
IsDeleted BIT DEFAULT 0
);
CREATE TABLE Books (
Id INT PRIMARY KEY,
Title NVARCHAR(100) NOT NULL,
Description NVARCHAR(1000),
ISBN BIGINT NOT NULL,
BookCode NVARCHAR(10) NOT NULL,
PublishedDate DATETIME,
PageCount INT,
Language NVARCHAR(50),
CoverImageUrl NVARCHAR(255),
AuthorId INT FOREIGN KEY REFERENCES Authors(Id),
GenreId INT FOREIGN KEY REFERENCES Genres(Id),
TotalCopies INT DEFAULT 1,
AvailableCopies INT DEFAULT 1,
CreatedAt DATETIME NOT NULL,
UpdatedAt DATETIME,
IsDeleted BIT DEFAULT 0
);
CREATE TABLE Reservations (
Id INT PRIMARY KEY,
UserId INT FOREIGN KEY REFERENCES Users(Id),
BookId INT FOREIGN KEY REFERENCES Books(Id),
DueDate DATETIME NOT NULL,
Status NVARCHAR(10),
CreatedAt DATETIME NOT NULL,
UpdatedAt DATETIME,
IsDeleted BIT DEFAULT 0
);
CREATE TABLE Penalties (
Id INT PRIMARY KEY,
UserId INT FOREIGN KEY REFERENCES Users(Id),
BookId INT FOREIGN KEY REFERENCES Books(Id),
DueDate DATETIME NOT NULL,
Amount DECIMAL(10,2),
IsPaid BIT DEFAULT 0,
Reason NVARCHAR(255),
CreatedAt DATETIME NOT NULL,
UpdatedAt DATETIME,

IsDeleted BIT DEFAULT 0
);
System Features:
For Users:
- Browse the library catalog by title, author, or genre
- Search books using combinational filters (e.g., genre + author)
- View book details: number of available copies and cover images
- Reserve books with automatic due dates
- Edit profile and authentication settings
Features for Admins:
Full CRUD access to:
Users, Books, Authors, Genres, Reservations, Penalties
- Manage the inventory of available copies
- Assign roles and permissions
- Handle penalty payment processing
- Extend reservations and monitor penaltie
