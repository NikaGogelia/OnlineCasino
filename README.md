# 🎰 Online Casino Platform  

## 📌 Description  
This is an **ASP.NET Core**-based **Online Casino** project, built using a **layered architecture** and divided into three independent projects:  

- **Online Casino MVC** – Manages authentication, roles, and financial transactions.  
- **Banking API** – Processes deposit and withdrawal requests via a callback mechanism.  
- **Online Casino API** – Handles betting, winnings, and user token-based transactions.  

## 🚀 Features  
- ✅ **Authentication & Authorization** – ASP.NET Core Identity with **Admin** & **Player** roles.  
- ✅ **Admin Panel** – Manage player **withdrawal requests** (approve/reject).  
- ✅ **Banking API Integration** – MVC project acts as a merchant, processing transactions.  
- ✅ **Casino API** – Manages bets, winnings, and transaction tokens.  
- ✅ **Database Access** – Uses **Dapper** for efficient database operations.  
- ✅ **Logging** – Implemented with **Serilog** for activity tracking.  
- ✅ **Database Backup** – Includes a `.bak` file for easy setup.  

## 🛠 Tech Stack  
- **Backend:** ASP.NET Core (MVC & API)  
- **Database:** SQL Server (via Dapper)  
- **Authentication:** ASP.NET Core Identity  
- **Logging:** Serilog  

## 📖 Setup Instructions  
1. Restore the database from the provided `.bak` file.  
2. Configure **connection strings** and **Banking API** settings.  
3. Run the **MVC** and **API** projects.  
