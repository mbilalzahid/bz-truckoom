# 🚗 Truckoom Service Management - Frontend

This is the frontend application for managing vehicle maintenance services. Built with **Angular**, it allows users to create, view, and manage services and their associated tasks.

---

## 🛠️ Tech Stack

- **Angular 19**
- **Angular Material**
- **Reactive Forms**
- **SCSS**
- **TypeScript**
- **Karma + Jasmine** (for unit testing)

---

## 📁 Project Structure

```
src/
│
├── app/
│   ├── services/
│   │   ├── service-list/         # List component with CRUD UI
│   │   ├── service-form/         # Create/edit service form
│   │   ├── services.service.ts   # HTTP calls
│   │   ├── services-routing.module.ts
│   │   └── services.module.ts
│
├── shared/                       # Shared components, modules, pipes
│   └── shared.module.ts
│
└── assets/                       # Images, mock data, etc.
```

---

## 🚀 Features

- View all services with associated tasks
- Add a new service with multiple tasks
- Edit existing services
- Delete services with confirmation
- User-friendly UI using Angular Material
- Form validation and feedback
- Toast/snackbar notifications
- Dialog-based delete confirmation
- Unit testing with Jasmine and Karma

---

## 🔍 Testing

Run all unit tests:

```bash
ng test
```

With code coverage:

```bash
ng test --code-coverage
```

Open the report:

```bash
open coverage/index.html
```

---

## 🔧 API Integration

This frontend connects to a backend API with the following endpoints:

- `GET /api/services` — list all services
- `GET /api/services/:id` — get single service
- `POST /api/services` — create a new service
- `PUT /api/services/:id` — update a service
- `DELETE /api/services/:id` — delete a service

---

## ✨ Components

- `ServiceListComponent` — displays services in card layout with tables for tasks
- `ServiceFormComponent` — add/edit a service and related tasks
- `SharedModule` — Angular Material imports and common declarations

---

## 📸 UI Preview

> All screenshots of the app interface are in folder "\bz-truckoom\screenshots"

---

## 👨‍💻 Author

Developed by Bilal Zahid

---
