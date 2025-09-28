# 🚀 Deploy no Render - Streaming API

## 📋 **Configuração Completa**

### **1. Arquivos de Configuração Criados:**
- ✅ `render.yaml` - Configuração do Render
- ✅ `Dockerfile` - Container Docker
- ✅ `.dockerignore` - Arquivos ignorados no Docker
- ✅ `DEPLOY.md` - Este guia

### **2. Configurações Ajustadas:**
- ✅ Porta dinâmica para Render (`PORT` environment variable)
- ✅ Pasta de uploads configurada para `/tmp` em produção
- ✅ Health check endpoint configurado
- ✅ Swagger habilitado em produção

## 🔧 **Passos para Deploy:**

### **1. Commit e Push no GitHub:**
```bash
git add .
git commit -m "Configure for Render deployment"
git push origin main
```

### **2. Criar Serviço no Render:**

1. **Acesse**: https://render.com
2. **Conecte seu GitHub**
3. **Crie novo "Web Service"**
4. **Selecione seu repositório**
5. **Configure**:
   - **Name**: `streaming-api-pim-viii`
   - **Environment**: `Docker`
   - **Build Command**: `dotnet publish StreamingApi.Api.csproj -c Release -o ./publish`
   - **Start Command**: `dotnet ./publish/StreamingApi.Api.dll`
   - **Instance Type**: `Free` (para começar)

### **3. Variáveis de Ambiente:**
```
ASPNETCORE_ENVIRONMENT=Production
ASPNETCORE_URLS=http://0.0.0.0:$PORT
```

### **4. Health Check:**
- **Path**: `/health`
- **Timeout**: `300s`

## 🌐 **URLs Após Deploy:**

- **API**: `https://streaming-api-pim-viii.onrender.com`
- **Swagger**: `https://streaming-api-pim-viii.onrender.com/swagger`
- **Health Check**: `https://streaming-api-pim-viii.onrender.com/health`

## 📁 **Estrutura de Arquivos:**

```
StreamingApi_Repo/
├── Controllers/          # API Controllers
├── Models/              # Data Models
├── Data/                # Database Context
├── render.yaml          # Render Configuration
├── Dockerfile           # Docker Configuration
├── .dockerignore        # Docker Ignore
├── Program.cs           # Main Application
├── appsettings.json     # Configuration
└── DEPLOY.md           # This Guide
```

## 🔍 **Verificação Pós-Deploy:**

### **1. Teste Health Check:**
```bash
curl https://sua-api.onrender.com/health
```

### **2. Teste Swagger:**
```
https://sua-api.onrender.com/swagger
```

### **3. Teste Endpoints:**
```bash
# Listar criadores
curl https://sua-api.onrender.com/api/criadores

# Criar criador
curl -X POST https://sua-api.onrender.com/api/criadores \
  -H "Content-Type: application/json" \
  -d '{"nome": "Teste Render"}'
```

## ⚠️ **Limitações do Plano Gratuito:**

- **Sleep Mode**: App "dorme" após 15min de inatividade
- **Cold Start**: Primeira requisição pode demorar 30s
- **Disk**: Arquivos são temporários (perdidos no restart)
- **Memory**: 512MB RAM

## 🚀 **Próximos Passos:**

1. **Deploy no Render**
2. **Teste todos os endpoints**
3. **Configure domínio customizado** (opcional)
4. **Upgrade para plano pago** (se necessário)

## 📞 **Suporte:**

- **Render Docs**: https://render.com/docs
- **.NET on Render**: https://render.com/docs/deploy-dotnet
- **Health Checks**: https://render.com/docs/health-checks

---

**🎉 Sua API estará online em minutos!**
