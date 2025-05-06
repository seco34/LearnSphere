# LearnSphere – Project Architecture

## Overview

LearnSphere is designed as a modular, scalable and testable e-learning web application using the .NET ecosystem.

---

## 🔧 Tech Stack

- **Frontend**: Razor Pages (.NET Core 7), Bootstrap 5  
- **Backend**: ASP.NET Core Web API  
- **Database**: SQL Server, Entity Framework Core  
- **Authentication**: ASP.NET Core Identity, JWT Token  
- **Real-time Features**: SignalR  
- **Containerization**: Docker (Planned)  
- **CI/CD**: GitHub Actions + Azure DevOps (Planned)

---

## 🧱 Project Layers

```plaintext
┌──────────────────────────────┐
│         Presentation         │  --> Razor Pages + Admin Dashboard
└──────────────────────────────┘
             │
┌──────────────────────────────┐
│         Application          │  --> Services, ViewModels, Business Rules
└──────────────────────────────┘
             │
┌──────────────────────────────┐
│         Domain/Core          │  --> Interfaces, Entities, Business Contracts
└──────────────────────────────┘
             │
┌──────────────────────────────┐
│         Infrastructure       │  --> EF Core, Repositories, Data Context
└──────────────────────────────┘

🧩 Key Modules
User Authentication Module

Course Management

Video Lesson Module

Quiz & Certificate System

Admin Panel

Notifications & Real-time Messaging
