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

# Expose port
EXPOSE 8080
EXPOSE 8081

# Set environment variables
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

# Run the application
ENTRYPOINT ["dotnet", "EmployeeDashboard.dll"]
