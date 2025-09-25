# Streaming API

API REST para gerenciamento de conteúdo de streaming, desenvolvida em C# com .NET 8.0 e Entity Framework Core.

Sistema construído para o PIM VIII

## 🚀 Deploy na Vercel

Esta API está configurada para ser hospedada na Vercel:

[![Deploy with Vercel](https://vercel.com/button)](https://vercel.com/new/clone?repository-url=https://github.com/SEU_USUARIO/streaming-api)

### URL da API em produção:
```
https://streaming-api-xxx.vercel.app
```

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

## 📦 Instalação Local

### Pré-requisitos
- .NET 8.0 SDK

### Executar localmente
```bash
# Clone o repositório
git clone <url-do-repositorio>
cd streaming-api

# Restaure as dependências
dotnet restore

# Execute a API
dotnet run
```

A API estará disponível em `http://localhost:5011`

### Documentação da API
- Swagger UI: `http://localhost:5011/swagger`

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

