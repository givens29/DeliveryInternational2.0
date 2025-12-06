# Base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy ONLY the csproj first (correct path!)
COPY ["Backend/DeliveryInternation2.0/DeliveryInternation2.0.csproj", "Backend/DeliveryInternation2.0/"]

# Restore
RUN dotnet restore "Backend/DeliveryInternation2.0/DeliveryInternation2.0.csproj"

# Copy the rest of the code
COPY . .

# Build
WORKDIR "/src/Backend/DeliveryInternation2.0"
RUN dotnet build -c Release -o /app/build

# Publish
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

# Final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DeliveryInternation2.0.dll"]
