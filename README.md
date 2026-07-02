# Dental Clinic Management System

A full-featured web application for managing a dental clinic's daily operations — patients, appointments, and payments — built with ASP.NET Core Razor Pages and a fully Arabic user interface.

## 🦷 Overview

This system helps dental clinic staff manage patient records, schedule and track appointments, and handle payment records, all through a clean and simple Arabic UI designed for ease of use by non-technical staff.

## ✨ Features

- **Patient Management** — Add, edit, and view patient records and history
- **Appointment Scheduling** — Book appointments with automatic double-booking conflict prevention
- **Dashboard** — At-a-glance view of appointments with time-based status indicators (منجز / قادم / ملغى)
- **Payment Tracking** — Dedicated payment records linked to patients
- **Session-Based Login** — Simple authentication system to secure clinic data
- **Confirmation Dialogs** — JavaScript-based confirmation prompts for delete and reschedule actions across all pages
- **Arabic UI** — Fully localized interface for Arabic-speaking staff

## 🛠️ Tech Stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET Core (.NET 8) — Razor Pages |
| ORM | Entity Framework Core |
| Database | SQL Server |
| Frontend | Bootstrap 5, JavaScript |
| IDE | Visual Studio 2022 |

## 📂 Project Structure

```
DentalClinic/
├── Migrations/          # EF Core database migrations
├── Models/              # Patient, Appointment, Payment, User
├── Pages/               # Razor Pages (Patients, Appointments, Payments, Dashboard, Login)
├── Properties/
├── appsettings.json      # Configuration (connection string)
└── Program.cs
```

## 🚀 Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- SQL Server (LocalDB or full instance)
- Visual Studio 2022 (recommended)

### Setup

1. **Clone the repository**
   ```bash
   git clone https://github.com/Khaledkhobe99/DentalClinic-Management-System.git
   ```

2. **Update the connection string**

   Open `appsettings.json` and adjust the `DefaultConnection` string if needed:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=localhost;Database=DentalClinicDB;Trusted_Connection=True;TrustServerCertificate=True;"
   }
   ```

3. **Apply database migrations**

   Open the **Package Manager Console** in Visual Studio and run:
   ```
   Update-Database
   ```

4. **Run the project**

   Press `F5` in Visual Studio, or run:
   ```bash
   dotnet run
   ```

## 📸 Screenshots

*(Add screenshots of the Dashboard, Appointments, and Patients pages here)*

## 👤 Author

**Khaled Obeidat**
Computer Information Systems Graduate — Yarmouk University

## 📄 License

This project is open for educational and portfolio purposes.
