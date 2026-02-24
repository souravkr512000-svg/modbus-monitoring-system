# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY EmployeeDashboard.csproj .
RUN dotnet restore

# Copy everything else and build
COPY . .
RUN dotnet build --configuration Release --no-restore

# Publish stage
FROM build AS publish
RUN dotnet publish --configuration Release --no-build --output /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copy published app
COPY --from=publish /app/publish .

# Expose port (Render uses PORT env variable)
EXPOSE 10000

# Set environment variables
# Render will set PORT automatically, but we provide a default
ENV ASPNETCORE_URLS=http://+:${PORT:-10000}
ENV ASPNETCORE_ENVIRONMENT=Production
ENV PORT=10000

# Run the application
ENTRYPOINT ["dotnet", "EmployeeDashboard.dll"]
