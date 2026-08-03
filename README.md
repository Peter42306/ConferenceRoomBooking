# Conference Room Booking API

***Conference Room Booking API*** is a REST API for managing conference rooms, additional services and room bookings.

The application allows administrators to create and manage conference rooms and services, search for available rooms by date, time and capacity, and create bookings with automatic price calculation.

## Features

- Conference room management
- Additional service management
- Search for available conference rooms
- Conference room capacity filtering
- Room-specific service selection
- Dynamic hourly pricing
- Booking price calculation
- Global exception handling
- Swagger API documentation

## Screenshots

<img width="1080" height="1920" alt="Swagger demo" src="https://github.com/user-attachments/assets/9ea59d7b-672a-4d54-a00c-4a4393b1eb38" />



## Project Structure

```text
ConferenceRoomBooking/
│
├── ConferenceRoomBooking.Api/
│   ├── Controllers/
│   │   ├── BookingsController.cs
│   │   ├── ConferenceRoomsController.cs
│   │   └── ServicesController.cs
│   │
│   ├── ExceptionHandling/
│   │   └── GlobalExceptionHandler.cs
│   │
│   ├── Properties/
│   ├── Program.cs
│   └── appsettings.json
│
├── ConferenceRoomBooking.Application/
│   ├── DTOs/
│   │   ├── Bookings/
│   │   ├── ConferenceRooms/
│   │   └── Services/
│   │
│   ├── Interfaces/
│   │   ├── Repositories/
│   │   └── Services/
│   │
│   ├── Services/
│   │   ├── BookingService.cs
│   │   ├── ConferenceRoomService.cs
│   │   └── ServiceService.cs
│   │
│   └── DependencyInjection.cs
│
├── ConferenceRoomBooking.Domain/
│   ├── Entities/
│   │   ├── Booking.cs
│   │   ├── BookingService.cs
│   │   ├── ConferenceRoom.cs
│   │   └── Service.cs
│   │
│   └── Enums/
│       └── BookingStatus.cs
│
├── ConferenceRoomBooking.Infrastructure/
│   ├── Data/
│   │   ├── Configurations/
│   │   ├── Migrations/
│   │   └── ApplicationDbContext.cs
│   │
│   ├── Repositories/
│   │   ├── BookingRepository.cs
│   │   ├── ConferenceRoomRepository.cs
│   │   └── ServiceRepository.cs
│   │
│   └── DependencyInjection.cs
│
├── docs/
│   └── DevelopmentPlan.xlsx
│
├── .gitignore
└── README.md
```

## Technology Stack

### Backend

- C#
- ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL
- Swagger / OpenAPI
- Dependency Injection
- Repository Pattern

### Architecture

The solution uses a layered architecture:

```text
API
 ↓
Application
 ↓
Domain

Infrastructure
 ↓
Application
 ↓
Domain
```

Responsibilities:

- **API** — HTTP endpoints, controllers and exception handling
- **Application** — business logic, DTOs, service interfaces and repository interfaces
- **Domain** — entities and enums
- **Infrastructure** — EF Core, PostgreSQL, repositories and migrations

## Request Flow

```text
HTTP Request
      ↓
Controller
      ↓
Application Service
      ↓
Repository
      ↓
Entity Framework Core
      ↓
PostgreSQL
```

## API Endpoints

| Method | Endpoint | Description |
|---|---|---|
| GET | /api/services | Get all additional services |
| POST | /api/services | Create an additional service |
| PUT | /api/services/{id} | Update a service |
| DELETE | /api/services/{id} | Delete a service |
| GET | /api/conference-rooms | Get all conference rooms with available services |
| POST | /api/conference-rooms | Create a conference room |
| PUT | /api/conference-rooms/{id} | Update a conference room |
| DELETE | /api/conference-rooms/{id} | Delete a conference room |
| POST | /api/conference-rooms/search | Search available conference rooms |
| GET | /api/bookings | Get all bookings with room, services, status, and price details |
| POST | /api/bookings | Create a booking and calculate its price |

## Business Rules

### Conference Rooms

- Conference room names must be unique.
- Capacity and hourly rate are stored for every room.
- A room may provide multiple additional services.
- A service may be available in multiple rooms.

### Availability

A conference room is available when:

- its capacity is equal to or greater than the requested capacity;
- it has no booking that overlaps the requested time interval.

Bookings that end exactly when another booking begins are allowed.

### Booking

- Booking start time cannot be in the past.
- Bookings must start at the beginning of an hour.
- Duration must be at least one hour.
- Selected services must be available for the selected room.
- Overlapping bookings for the same room are not allowed.
- Successful bookings are stored with the `Confirmed` status.

### Pricing

The room rental price is calculated separately for every booked hour.

| Time period | Price rule |
|---|---|
| 06:00–09:00 | 10% discount |
| 09:00–12:00 | Base hourly rate |
| 12:00–14:00 | 15% surcharge |
| 14:00–18:00 | Base hourly rate |
| 18:00–23:00 | 20% discount |

The prices of selected services are added to the room rental price.

Service prices are stored with the booking so that previous bookings keep their original prices if service prices change later.

## Error Handling

The API uses a global exception handler and returns `ProblemDetails` responses.

Examples:

- `400 Bad Request` — invalid dates, capacity or unavailable service
- `404 Not Found` — conference room was not found
- `409 Conflict` — duplicate name or overlapping booking
- `500 Internal Server Error` — unexpected server error

## Date and Time

- Incoming dates are expected in UTC.
- Date comparisons use UTC.
- Client applications should convert local time to UTC before sending requests.
- Local time conversion should be handled by the client application.
