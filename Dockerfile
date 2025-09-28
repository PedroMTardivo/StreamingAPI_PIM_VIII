# Use the official .NET 8 runtime as base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Use the official .NET 8 SDK for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["StreamingApi.Api.csproj", "."]
RUN dotnet restore "StreamingApi.Api.csproj"
COPY . .
RUN dotnet build "StreamingApi.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StreamingApi.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Create uploads directory
RUN mkdir -p /app/uploads

ENTRYPOINT ["dotnet", "StreamingApi.Api.dll"]
