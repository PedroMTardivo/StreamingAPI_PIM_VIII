#!/bin/bash

echo "🚀 Iniciando Streaming API..."
echo "📁 Diretório atual: $(pwd)"
echo "🔧 Executando dotnet run..."
echo ""

dotnet run --project StreamingApi.Api.csproj --urls http://localhost:5011
