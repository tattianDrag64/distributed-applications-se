# 📚 Library Management System

A full-featured Library Management System designed as a course project to support both administrative and user interactions. The system provides RESTful API services with ASP.NET Core and a Blazor-based front-end for a seamless user experience. 
A member can take products (books, films, audiobooks), return them. The admin registers new employees, confirms the return of books, creates seminars, products, edits and deletes users, makes their accounts inactive and etc. A employee can regulate books and seminars.

---
## 🔧 Tech Stack

| Layer        | Technology                        |
|--------------|------------------------------------|
| Back-end     | ASP.NET Core Web API               |
| Front-end    | Blazor                             |
| Database     | SQL Server + Entity Framework Core (Code-First) |
| Auth         | JWT Tokens                         |
| Validation   | Data Annotations + FluentValidation |

---

## 🏗️ System Architecture

LibraryManagementSystem
- ├── Client/ # Blazor Frontend
- ├── Server/ # ASP.NET Core Backend
- ├── Shared/ # Shared Entities, DTOs, Responses

---

## 🚀 Features

### ✅ For All Users

- 🔍 Browse products by title, author, or genre
- 📅 View upcoming seminars by topic and date
- 🎯 Advanced search   
- ↕️ Sort and paginate book listings  
- 📊 View available copies and ratings of loans and users
- 📅 Reserve product online  

### 🔒 For Admins Only

- ⚙️ Full CRUD for:
  - Users
  - Products
  - Creators
  - Genres
  - Loans
  - Seminars
- 📦 Manage available copies
- 🧑‍⚖️ Assign roles

---

## 🗃️ Database Requirements
- Code-First approach with the following entities:
  - `User`, `Product`, `Creator`, `Category`, `Loan`, `Seminar`
- Each entity includes:
  - ➕ 6+ columns
  - 🔢 4+ data types (`int`, `string`, `DateTime`, `bool`, `double`, (`enums` and `IEnumerables`))
  - ✅ Requirement fields (e.g., `Title`, `LoanDate`, `Email`)
  - 🔡 Constraints like `MaxLength(100)`
---

## 🔐 Security & Validation
- 🔑 JWT-based authentication and role-based authorization
- ✅ Server-side validation:
  - Data Annotations
- ⚠️ Client-side validation with live feedback

---

## 📌 Project Goals
This project demonstrates:
- Building scalable full-stack applications
- Secure and validated REST API development
- Clean separation between front-end and back-end logic








----------------------------------------------------------------------

## 🧠 Coming Soon

🚀 Future updates will include the following features:

### 📊 Smart Recommendations
- 🤖 Personalized recommendations powered by machine learning:
  - Based on user interests, reservation history, genres, and reviews

### 📄 Materials & Documents
- 📥 Upload and download PDF files:
  - Presentations and resources for seminars
  - Reservation information

### ✉️ Email Notifications
- 📬 Email alerts for:
  - Reservation confirmations
  - Upcoming seminars
  - Overdue returns
- 🔐 Email verification for registration and password recovery

### 🛠 Enhanced Features
- 📷 Improved book image uploads and preview
- ✍️ Enhanced review and rating system
- 🔎 Scan QR or barcodes to instantly retrieve product information

### ⚖️ Advanced Penalty System
- 💰 Automatic registration of penalties due to:
  - 📕 Lost products
  - 📉 Damaged products
  - ⏰ Late returns

