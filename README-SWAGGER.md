# 📚 Documentação Swagger - Streaming API

## 🎯 Como Acessar a Documentação

### Desenvolvimento Local:
```
http://localhost:5011/swagger
```

### Produção:
```
https://sua-api.com/swagger
```

## 📋 Funcionalidades da Documentação

### ✅ **Interface Interativa**
- **Teste de Endpoints**: Execute requisições diretamente na interface
- **Autenticação**: Configure headers e parâmetros facilmente
- **Exemplos**: Veja exemplos de requisições e respostas
- **Validação**: Teste validações de dados em tempo real

### ✅ **Documentação Completa**
- **Descrições Detalhadas**: Cada endpoint tem descrição clara
- **Códigos de Resposta**: Documentação de todos os códigos HTTP
- **Modelos de Dados**: Estrutura completa dos objetos
- **Validações**: Regras de validação dos campos
- **Exemplos**: Exemplos práticos de uso

### ✅ **Organização por Categorias**
- **Criadores**: Gerenciamento de criadores de conteúdo
- **Conteúdos**: Gerenciamento de mídias
- **Arquivos**: Upload/download de arquivos
- **Health**: Endpoints de monitoramento

## 🔧 Endpoints Disponíveis

### 🎭 **Criadores** (`/api/criadores`)
| Método | Endpoint | Descrição |
|--------|----------|-----------|
| `GET` | `/api/criadores` | Lista todos os criadores |
| `POST` | `/api/criadores` | Cria novo criador |
| `GET` | `/api/criadores/{id}` | Busca criador por ID |
| `GET` | `/api/criadores/{id}/conteudos` | Lista conteúdos do criador |
| `DELETE` | `/api/criadores/{id}` | Exclui criador e seus conteúdos |

### 📺 **Conteúdos** (`/api/conteudos`)
| Método | Endpoint | Descrição |
|--------|----------|-----------|
| `GET` | `/api/conteudos` | Lista todos os conteúdos |
| `POST` | `/api/conteudos` | Cria novo conteúdo |
| `GET` | `/api/conteudos/{id}` | Busca conteúdo por ID |
| `PUT` | `/api/conteudos/{id}` | Atualiza conteúdo |
| `DELETE` | `/api/conteudos/{id}` | Exclui conteúdo |

### 📁 **Arquivos** (`/api/arquivos`)
| Método | Endpoint | Descrição |
|--------|----------|-----------|
| `POST` | `/api/arquivos/upload/{conteudoId}` | Upload de arquivo |
| `GET` | `/api/arquivos/download/{fileName}` | Download de arquivo |
| `DELETE` | `/api/arquivos/remove/{conteudoId}` | Remove arquivo |

### ❤️ **Health Check**
| Método | Endpoint | Descrição |
|--------|----------|-----------|
| `GET` | `/` | Status básico da API |
| `GET` | `/health` | Status detalhado da API |

## 📝 Exemplos de Uso

### Criar um Criador:
```json
POST /api/criadores
{
  "nome": "João Silva"
}
```

### Criar um Conteúdo:
```json
POST /api/conteudos
{
  "titulo": "Podcast sobre Tecnologia",
  "varchar": "Podcast",
  "criadorId": 1
}
```

### Upload de Arquivo:
```multipart
POST /api/arquivos/upload/1
Content-Type: multipart/form-data

arquivo: [arquivo.mp3]
```

## 🎨 Recursos Visuais

### **Cores e Ícones:**
- 🎭 **Criadores**: Azul
- 📺 **Conteúdos**: Verde  
- 📁 **Arquivos**: Laranja
- ❤️ **Health**: Vermelho

### **Indicadores:**
- ✅ **200**: Sucesso
- ⚠️ **400**: Erro de validação
- ❌ **404**: Não encontrado
- 🚫 **500**: Erro interno

## 🔍 Como Usar o Swagger

### 1. **Explorar Endpoints**
- Clique em qualquer endpoint para ver detalhes
- Expanda as seções para ver parâmetros
- Veja exemplos de requisições/respostas

### 2. **Testar Requisições**
- Clique em "Try it out"
- Preencha os parâmetros necessários
- Clique em "Execute"
- Veja a resposta em tempo real

### 3. **Entender Modelos**
- Veja a estrutura dos objetos
- Entenda os tipos de dados
- Verifique validações obrigatórias

### 4. **Códigos de Resposta**
- Cada endpoint mostra todos os códigos possíveis
- Veja exemplos de respostas de erro
- Entenda quando cada código é retornado

## 🚀 Dicas de Uso

### **Para Desenvolvedores:**
- Use os exemplos para integrar com a API
- Teste validações antes de implementar
- Verifique tipos de dados esperados

### **Para Testadores:**
- Execute cenários de sucesso e erro
- Teste validações de campos obrigatórios
- Verifique códigos de resposta

### **Para Documentação:**
- A documentação é sempre atualizada
- Exemplos práticos incluídos
- Interface intuitiva e fácil de usar

## 🔧 Configuração Técnica

### **Swagger UI Configuração:**
```csharp
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Streaming API v1");
    c.RoutePrefix = "swagger";
    c.DocumentTitle = "Streaming API Documentation";
    c.DefaultModelsExpandDepth(-1);
    c.DisplayRequestDuration();
});
```

### **Geração de Documentação:**
- Comentários XML incluídos automaticamente
- Validações de modelo documentadas
- Exemplos de código gerados

## 📊 Estatísticas da API

- **Total de Endpoints**: 11
- **Controllers**: 4
- **Modelos**: 6
- **Códigos de Resposta**: 15+
- **Exemplos**: 20+

---

**🎉 A documentação Swagger está sempre atualizada e pronta para uso!**
