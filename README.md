# MT5 Account Overview Dashboard

A simple ASP.NET Core Web API and MVC web application that displays a dashboard of MT5 client account data with mocked data API endpoints.

## Features

### Dashboard View
- **Account Number** display
- **Balance, Equity, Margin Level** with visual indicators
- **Open Trades Count** with detailed trade listing
- **Last Login Timestamp** with formatted display
- **Account Status** (Active/Disabled) with status indicators
- **Real-time P&L calculation** from open trades
- **Responsive design** with Bootstrap 5

### Mock API Endpoints
- `GET /api/mt5/accounts/{accountId}` - Returns account details
- `GET /api/mt5/accounts/{accountId}/trades` - Returns open trades list

### Architecture & Best Practices
- **Clean Architecture** with separation of concerns
- **Dependency Injection** for services and HTTP clients
- **Swagger API Documentation** for services and HTTP clients
- **JWT Authentication** for services and HTTP clients
- **Dependency Injection** for services and HTTP clients
- **API Versioning** for services and HTTP clients
- **Error Handling** for API failures and network issues
- **Unit Tests** for service layer logic
- **Logging** throughout the application
- **Async/await** patterns for better performance

## Setup Instructions

### Prerequisites
- .NET 8.0 SDK or later
- Visual Studio 2022 or VS Code with C# extension

### Installation Steps

1. **Clone or create the project**
   ```bash
   mkdir MTOV_DASHBOARD
   cd MTOV_DASHBOARD
   ```

2. **Create the project files**
   - Copy all the provided files into their respective directories
   - Ensure the folder structure matches the layout above

3. **Restore dependencies**
   ```bash
   dotnet restore
   ```

4. **Build the project**
   ```bash
   dotnet build
   ```

5. **Run the application**
   ```bash
   dotnet run
   ```

6. **Access the application**
   - Open your browser and navigate to `https://localhost:44389`
   - The dashboard should load with the default account ID "12345"

### Running Tests

```bash
dotnet test
```

### Development Configuration

The application is configured to run the mock API on the same instance. In `Program.cs`, the HttpClient base address is set to the same application URL, allowing the mock API endpoints to serve data.

## Usage

### Basic Usage
1. **Load Dashboard**: Navigate to the main page to see account "12345" data
2. **Change Account**: Enter a different account ID in the input field and click "Load Account"
3. **View Trades**: Scroll down to see the detailed trades table with P&L calculations

### Special Test Cases
You can test different scenarios by using these account IDs:
- `12345` - Returns normal mock data
- `error` - Simulates API server error (500)
- `notfound` - Simulates account not found (404)
- `notrades` - Returns account data but no trades

### API Endpoints
The mock API provides these endpoints:
- **Account Details**: `GET /api/mt5/accounts/{accountId}`
- **Open Trades**: `GET /api/mt5/accounts/{accountId}/trades`

## Architecture Details

### Clean Architecture Principles
- **Controllers** handle HTTP requests and coordinate with services
- **Services** contain business logic and orchestrate data operations
- **Interfaces** define contracts for dependency injection
- **Models** represent data structures and view models

### Error Handling
- Network timeouts and connection errors
- Invalid JSON responses
- HTTP error status codes
- Service layer exceptions
- User-friendly error messages in the UI

### Logging
- Seril Log -> Structured logging with different levels (Information, Warning, Error)
- Request/response logging in API service
- Error logging with context information
- Console and debug output providers

## Configuration

### appsettings.json
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "APIConfig": {
    "BaseUrl": "https://localhost:44389",
    "TimeOut": 30,
    "Auth": {
      "Login": "/api/Auth",

      // this is just for assessment purpose, in the realtime project, this value will be captured directly from user input.
      "AuthUser": "MTOVUser",
      "AuthPass": "M123456!"
    },
    "Account": {
      "AccountDetail": "/api/mt5/Accounts/",
      "OpenTrades": "/api/mt5/Accounts/{accountId}/trades"
    }
  },
  "AllowedHosts": "*"
}
```

## Assumptions

1. **Mock Data**: The application uses static mock data instead of connecting to real MT5 APIs
2. **Single Account View**: Dashboard shows one account at a time
3. **Real-time Updates**: No automatic refresh implemented (can be added via SignalR)
4. **Data Persistence**: No database - all data is retrieved from mock APIs
5. **Timezone**: All timestamps are displayed in local timezone
6. **Currency**: All monetary values are displayed in USD format

## Testing

### Unit Tests Coverage
- **DashboardService**: Business logic and error handling
- **DashboardApiService**: HTTP communication and JSON parsing
- **Model Calculations**: P&L calculations and property validations

### Test Scenarios
- Successful API responses
- Network failures and timeouts
- Invalid JSON responses
- Empty/null data handling
- Error propagation and logging

## Future Enhancements

1. **Real MT5 API Integration**: Replace mock endpoints with actual MT5 API calls
2. **Authentication**: Add user login and account authorization using real-time DB-connectivity
3. **Real-time Updates**: Implement WebSocket or SignalR for live data
4. **Multiple Accounts**: Support viewing multiple accounts simultaneously
5. **Historical Data**: Add charts and historical performance metrics
6. **Alerts**: Email/SMS notifications for account events
7. **Mobile App**: React Native or Xamarin mobile application
8. **Caching**: Redis caching for improved performance
9. **ReactJs or AngularJS for Web**: Building fast and interactive user interfaces, especially single-page applications, by efficiently updating and rendering components based on data changes.
10. **K8s Deployment**: Deploying web and api  


## Troubleshooting

### Common Issues

1. **Port Conflicts**: If port 7000 is in use, update the port in `Program.cs` and `appsettings.json`
2. **Build Errors**: Ensure all NuGet packages are restored with `dotnet restore`
3. **Mock API Not Working**: Verify the HttpClient base address matches your application URL
4. **Tests Failing**: Run `dotnet clean` and `dotnet build` before running tests

### Logs
Check the console output for detailed logging information about API calls, errors, and application flow.

## Contributing

1. Fork the repository
2. Create a feature branch
3. Add unit tests for new functionality
4. Ensure all tests pass
5. Submit a pull request with detailed description

## License

This project is created for demonstration purposes and is not intended for production use without proper security, authentication, and real API integration.
