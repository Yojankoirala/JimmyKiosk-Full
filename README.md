# 📖 README

## Overview
Jimmy Kiosk is a university-level final project designed as a self-service food ordering system. It consists of two main parts:

### Jimmy Kiosk App (Frontend)
Built with Xamarin.Forms, it provides an interactive kiosk interface where customers can:

- View menu items
- Add items to the cart
- Place orders
- Make secure card payments
- Leave feedback

### JimmysKioskWeb (Backend)
A .NET Web API that:

- Handles requests for orders, payments, and feedback
- Uses Entity Framework to connect to SQL Server
- Stores all transaction data for reporting and analysis

---

## Features
- **Order Management** – Submit, view, and manage customer orders.
- **Payment Processing** – Accept and record card payments securely.
- **Feedback Collection** – Allow customers to submit comments and suggestions.
- **Database Integration** – All data stored in SQL Server via EF Core.
- **REST API Endpoints** – `/api/Orders`, `/api/Payments`, `/api/Feedback`.

---

## Technology Stack

### Frontend (Jimmy Kiosk App)
- Xamarin.Forms (C#)
- MVVM Architecture
- HTTP Client for API requests

### Backend (JimmysKioskWeb)
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server Database

---

## Installation & Setup

### Prerequisites
- Visual Studio 2022 (with Xamarin and ASP.NET workloads)
- SQL Server & SSMS
- .NET 6 SDK or later

### Steps

#### Clone the Repository
```bash
git clone https://github.com/Yojankoirala/JimmyKiosk-Full.git
```

#### Setup Backend (JimmysKioskWeb)
1. Open the JimmysKioskWeb project in Visual Studio  
2. Update `appsettings.json` with your SQL Server connection string  
3. Run EF migrations:  
```bash
dotnet ef database update
```
4. Press **F5** to start the API

#### Setup Frontend (Jimmy Kiosk App)
1. Open the JimmyKiosk project in Visual Studio  
2. Update API base URL in `OrderService.cs` and `PaymentService.cs` (use `https://10.0.2.2:PORT` for Android Emulator)  
3. Deploy to Android Emulator or physical device  

---

## 📡 API Endpoints

### Orders
- `GET /api/Orders` – Retrieve all orders
- `POST /api/Orders` – Create a new order

### Payments
- `GET /api/Payments` – Retrieve all payments
- `POST /api/Payments` – Process a new payment

### Feedback
- `GET /api/Feedback` – Retrieve all feedback entries
- `POST /api/Feedback` – Submit new feedback

---

## 📄 License
This project is for educational purposes only.
