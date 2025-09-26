@echo off
echo 🚀 Iniciando Streaming API...
echo 📁 Diretório atual: %cd%
echo 🔧 Executando dotnet run...
echo.

dotnet run --project StreamingApi.Api.csproj --urls http://localhost:5011
pause
