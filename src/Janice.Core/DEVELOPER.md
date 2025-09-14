# Janice.Core Developer Documentation

## Overview
Janice.Core is the heart of the Janice project, providing core logic, models, and services for both CLI and GUI interfaces. It is built with C# and targets .NET 8.0.

## Project Structure
- **Models/**: Data models for chat, users, requests, responses, etc.
- **Enums/**: Enumerations for roles, response types, etc.
- **Services/**: Business logic and service classes
  - **Interfaces/**: Service interfaces for dependency injection
- **Data/**: Configuration and user data (YAML)

## Key Components
### Models
- `ChatInteraction`, `ChatMessage`, `ChatRequest`, `ChatResponse`, `Conversation`, `Intention`, `User`, etc.
- Used for representing and processing chat and user data.

### Enums
- `Roles`, `ResponseType`: Used for type-safe role and response management.

### Services
- `ConfigService`: Loads and manages configuration from YAML files.
- `IntentService`: Handles user intentions and commands.
- `LoggingService`: Centralized logging.
- `LoginService`: User authentication and session management.
- `OllamaApiService`: Integrates with Ollama API for AI model interactions.
- `UserService`: Manages user data and operations.

### Interfaces
- Service interfaces for abstraction and testability.

## Configuration
- Located in `Data/config.yaml` and `Data/users.yaml`.
- Use `ConfigService` to access configuration data.

## Dependency Injection
- Services are designed for DI. Register interfaces and implementations in your host project.

## Extending Janice.Core
- Add new models in `Models/`
- Add new services in `Services/` and define interfaces in `Services/Interfaces/`
- Update configuration schemas as needed

## Testing
- Unit tests should be placed in a dedicated test project (not present by default).
- Use mock implementations for service interfaces.

## Build & Usage
- Build with `dotnet build Janice.Core/Janice.Core.csproj`
- Reference `Janice.Core` in CLI/GUI projects

## Contact
For questions or contributions, see the main `CONTRIBUTING.md`.
