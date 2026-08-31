# Personal Trainer

An AI-powered personal training app that generates personalised workout plans based on your fitness profile. Powered by Google Gemini with function calling, backed by a real exercise database.

## Tech Stack

**Backend** — .NET 9 Web API
- ASP.NET Core with EF Core 9 + SQL Server
- ASP.NET Identity for user management
- JWT authentication (access tokens + refresh tokens)
- Google Gemini 2.5 Flash via function calling (tool use)
- Scalar API reference (OpenAPI)

**Frontend** — React 19 + TypeScript
- Vite build tool
- React Bootstrap 5
- React Router v7
- Axios with request/response interceptors

## Features

- **User registration & login** with JWT-based auth
- **Workout profile builder** — focus area, equipment, duration, fitness level, goal, health limitations
- **AI workout generation** — Gemini queries the exercise database via a tool call, then builds a personalised plan
- **Live workout session** — exercise-by-exercise timer with voice cues, rest timers, and progress tracking
- **Saved workouts** — view previously generated plans

## Project Structure

```
PersonalTrainer/
├── PersonalTrainer.API/          # .NET 9 Web API
│   ├── Controllers/              # Auth, Exercise, WorkoutProfile, WorkoutOptions
│   ├── Data/                     # AppDbContext + seed data (54 exercises)
│   ├── Enums/                    # FocusArea, Equipment, MuscleGroup
│   ├── Mappers/                  # FocusAreaMapper (FocusArea → MuscleGroup[])
│   ├── Models/                   # AppUser, ExerciseTemplate, WorkoutPlan, DTOs
│   ├── Services/
│   │   ├── AI/                   # WorkoutGenerationAgent (Gemini agent loop)
│   │   ├── AuthService.cs
│   │   ├── ExerciseQueryService.cs
│   │   └── WorkoutPlanService.cs
│   └── docker-compose.yml
└── PersonalTrainer.Client/       # React + TypeScript frontend
    └── src/
        ├── pages/                # Login, Register, Home, CreateWorkout, Session, MyWorkouts
        ├── services/             # API clients (auth, workout, exercise)
        └── context/              # AuthContext (JWT + current user)
```

## How the AI Works

1. User fills out a workout profile (focus area, equipment, duration, etc.)
2. The API sends the profile to Gemini along with a `get_exercises` tool declaration
3. Gemini calls `get_exercises` — the API queries the database, filtering by focus area (mapped to muscle groups) and available equipment
4. Gemini receives the real exercises and builds a personalised plan within the user's time budget
5. The plan is returned as structured JSON and stored for the session

## Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- [Node.js 20+](https://nodejs.org)
- [Docker Desktop](https://www.docker.com/products/docker-desktop) (for SQL Server)
- A [Google Gemini API key](https://aistudio.google.com/app/apikey)

### 1. Start SQL Server

```bash
docker-compose up -d
```

### 2. Configure the API

Create `PersonalTrainer.API/appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;Database=PersonalTrainer;User Id=sa;Password=YOUR_SA_PASSWORD;TrustServerCertificate=True;"
  },
  "Jwt": {
    "Key": "your-secret-key-at-least-32-chars-long",
    "Issuer": "PersonalTrainer.API",
    "Audience": "PersonalTrainer.Client",
    "DurationInMinutes": 15
  },
  "Gemini": {
    "ApiKey": "your-gemini-api-key"
  }
}
```

Also create a `.env` file in the project root for Docker:

```
MSSQL_SA_PASSWORD=YOUR_SA_PASSWORD
```

### 3. Run the API

```bash
cd PersonalTrainer.API
dotnet ef database update   # applies migrations + seeds exercise data
dotnet run
```

API runs on `http://localhost:5202`. OpenAPI reference at `http://localhost:5202/scalar`.

### 4. Run the Client

```bash
cd PersonalTrainer.Client
npm install
npm run dev
```

Client runs on `http://localhost:5173`.

## Database Migrations

```bash
# Create a new migration
dotnet ef migrations add MigrationName --project PersonalTrainer.API

# Apply migrations
dotnet ef database update --project PersonalTrainer.API
```

## Environment Variables Summary

| Key | Where | Description |
|-----|-------|-------------|
| `ConnectionStrings:DefaultConnection` | appsettings | SQL Server connection string |
| `Jwt:Key` | appsettings | Signing key (min 32 chars) |
| `Gemini:ApiKey` | appsettings | Google Gemini API key |
| `MSSQL_SA_PASSWORD` | .env | SQL Server SA password for Docker |
