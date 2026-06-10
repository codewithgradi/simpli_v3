# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy the .csproj files using your exact folder names
COPY ["simpli.Api/simpli.Api.csproj", "simpli.Api/"]
COPY ["simpli.Application/simpli.Application.csproj", "simpli.Application/"]
COPY ["simpli.Infrastructure/simpli.Infrastructure.csproj", "simpli.Infrastructure/"]
COPY ["simpli.Domain/simpli.Domain.csproj", "simpli.Domain/"]

# Restore dependencies using the correct API project path
RUN dotnet restore "simpli.Api/simpli.Api.csproj"

# Copy the rest of the source code
COPY . .

# Move directly into the API project directory to build
WORKDIR "/src/simpli.Api"
RUN dotnet publish "simpli.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

EXPOSE 8080

ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

# Copy compiled files from the build stage
COPY --from=build /app/publish .

# Execute the compiled assembly 
ENTRYPOINT ["dotnet", "simpli.Api.dll"]