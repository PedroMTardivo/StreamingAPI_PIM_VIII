# Streaming API

API REST para gerenciamento de conteúdo de streaming, desenvolvida em C# com .NET 8.0 e Entity Framework Core.

Sistema construído para o PIM VIII

## 🚀 Como Executar

### Pré-requisitos
- .NET 8.0 SDK instalado

### Passos para executar:

#### Opção 1: Comando direto
```bash
# 1. Clone o repositório
git clone https://github.com/PedroMTardivo/StreamingAPI_PIM_VIII.git
cd StreamingAPI_PIM_VIII

# 2. Execute a API
dotnet run --project StreamingApi.Api.csproj --urls http://localhost:5011
```

#### Opção 2: Scripts (mais fácil!)
```bash
# Linux/Mac
./run.sh

# Windows
run.bat
```

### ✅ Pronto!
A API estará rodando em: **http://localhost:5011**

### Teste no navegador:
- Health check: http://localhost:5011/
- API endpoints: http://localhost:5011/api/criadores
- Swagger UI: http://localhost:5011/swagger

## 📋 Endpoints Disponíveis

### Criadores
- `GET /api/criadores` - Listar todos os criadores
- `POST /api/criadores` - Criar novo criador
- `GET /api/criadores/{id}` - Buscar criador por ID
- `PUT /api/criadores/{id}` - Atualizar criador
- `DELETE /api/criadores/{id}` - Excluir criador
- `GET /api/criadores/{id}/conteudos` - Listar conteúdos do criador

### Conteúdos
- `GET /api/conteudos` - Listar todos os conteúdos
- `POST /api/conteudos` - Criar novo conteúdo
- `GET /api/conteudos/{id}` - Buscar conteúdo por ID
- `PUT /api/conteudos/{id}` - Atualizar conteúdo
- `DELETE /api/conteudos/{id}` - Excluir conteúdo

### Arquivos
- `POST /api/arquivos/upload/{conteudoId}` - Upload de arquivo
- `GET /api/arquivos/download/{fileName}` - Download de arquivo
- `DELETE /api/arquivos/remove/{conteudoId}` - Remover arquivo

### Usuários
- `GET /api/usuarios` - Listar usuários
- `POST /api/usuarios` - Criar usuário

### Playlists
- `GET /api/playlists` - Listar playlists
- `POST /api/playlists` - Criar playlist
- `GET /api/playlists/{id}` - Buscar playlist por ID
- `PUT /api/playlists/{id}` - Atualizar playlist
- `DELETE /api/playlists/{id}` - Excluir playlist
- `POST /api/playlists/{id}/itens` - Adicionar item à playlist
- `DELETE /api/playlists/{id}/itens/{itemId}` - Remover item da playlist

## 🛠️ Tecnologias

- **.NET 8.0** - Framework principal
- **Entity Framework Core** - ORM para banco de dados
- **SQLite** - Banco de dados
- **Swagger/OpenAPI** - Documentação da API
- **Vercel** - Hospedagem

## 🔧 Comandos Úteis

```bash
# Build do projeto
dotnet build StreamingApi.Api.csproj

# Executar em modo desenvolvimento
dotnet run --project StreamingApi.Api.csproj

# Executar em porta específica
dotnet run --project StreamingApi.Api.csproj --urls http://localhost:5011

# Limpar e rebuildar
dotnet clean && dotnet build StreamingApi.Api.csproj
```

## 🗄️ Banco de Dados

A API usa SQLite como banco de dados:
- **Desenvolvimento**: `streaming.db` (local)
- **Produção**: `/tmp/streaming.db` (Vercel)

O banco é criado automaticamente na primeira execução.

## 🔧 Configuração

### Variáveis de Ambiente
- `ASPNETCORE_ENVIRONMENT` - Ambiente (Development/Production)

### CORS
A API está configurada para aceitar requisições de qualquer origem (para facilitar testes).

## 📁 Estrutura do Projeto

```
StreamingApi/
├── Controllers/          # Controladores da API
├── Data/                # Contexto do banco de dados
├── Models/              # Modelos de dados
├── Properties/          # Configurações
├── appsettings.json     # Configurações
├── Program.cs           # Ponto de entrada
└── vercel.json          # Configuração da Vercel
```

## 🧪 Testando a API

### Exemplo: Criar um criador
```bash
curl -X POST "https://sua-api.vercel.app/api/criadores" \
     -H "Content-Type: application/json" \
     -d '{"nome": "Criador Teste"}'
```

### Exemplo: Listar criadores
```bash
curl "https://sua-api.vercel.app/api/criadores"
```

## 🐛 Solução de Problemas

### API não responde
- Verifique se o deploy foi bem-sucedido na Vercel
- Consulte os logs na dashboard da Vercel

### Erro de CORS
- A API está configurada para aceitar qualquer origem
- Verifique se o cliente está usando a URL correta

### Banco de dados
- Na Vercel, o banco é recriado a cada deploy
- Para dados persistentes, considere usar um banco externo

## 📝 Licença

Este projeto está sob a licença MIT. Veja o arquivo [LICENSE](LICENSE) para detalhes.

## 🔄 Changelog

### v1.0.0
- ✅ API REST completa para streaming
- ✅ CRUD para criadores, conteúdos, usuários e playlists
- ✅ Upload e download de arquivos
- ✅ Deploy automatizado na Vercel
- ✅ CORS configurado para clientes

---

