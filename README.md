# Teste Técnico NTT - Backend .NET 8

![.NET 8](https://img.shields.io/badge/.NET-8.0-blue) ![PostgreSQL](https://img.shields.io/badge/Database-PostgreSQL-blue) ![MongoDB](https://img.shields.io/badge/Database-MongoDB-green) ![Kafka](https://img.shields.io/badge/Event%20Streaming-Kafka-orange) ![CQRS](https://img.shields.io/badge/Architecture-CQRS-purple) ![Clean Architecture](https://img.shields.io/badge/Pattern-Clean%20Architecture-success)

**Teste Técnico NTT** é um projeto desenvolvido com **.NET 8**, **CQRS**, **Kafka**, **PostgreSQL**, **MongoDB**, **API Gateway**, **BFF** e **OAuth2**.  
O sistema gerencia **Clientes** e **Pagamentos**, garantindo consistência eventual entre bancos de dados SQL e NoSQL.

---

## 📌 **🎯 Tecnologias Utilizadas**
- **.NET 8** 🚀
- **Entity Framework Core** (PostgreSQL)
- **MongoDB** (Banco de Leitura)
- **Kafka** (Mensageria e Event Streaming)
- **CQRS** (Separação de leitura e escrita)
- **FluentValidation** (Validações avançadas)
- **MediatR** (Handlers para eventos e comandos)
- **Ocelot API Gateway** (Encaminhamento de requisições)
- **Serilog** (Logging avançado)
- **Swagger** (Documentação automática da API)

---

## 📌 **⚙️ Arquitetura do Projeto**
O projeto segue **Clean Architecture** e **CQRS**, garantindo modularidade e escalabilidade.

📂 **Estrutura do Projeto**
```
/TesteTecnicoNTT
│── src/
│   ├── TesteTecnicoNTT.API/              # API Principal
│   ├── TesteTecnicoNTT.BFF/              # Backend for Frontend (BFF)
│   ├── TesteTecnicoNTT.Gateway/          # API Gateway (Ocelot)
│   ├── TesteTecnicoNTT.Application/      # Camada de Aplicação (Handlers, Commands, Queries)
│   ├── TesteTecnicoNTT.Domain/           # Camada de Domínio (Entidades, Interfaces, Eventos)
│   ├── TesteTecnicoNTT.Infrastructure/   # Persistência (PostgreSQL, MongoDB, Kafka)
│   ├── TesteTecnicoNTT.Common/           # Validações e Shared Components
│── tests/                                # Testes Automatizados
│   ├── TesteTecnicoNTT.UnitTests/        # Testes Unitários
│   ├── TesteTecnicoNTT.IntegrationTests/ # Testes de Integração
│── README.md                             # Documentação do projeto
```

✅ **Banco de Escrita:** PostgreSQL  
✅ **Banco de Leitura:** MongoDB  
✅ **Mensageria:** Kafka  
✅ **Gateway:** Ocelot  
✅ **Autenticação:** OAuth2 (Simulação) + JWT  
✅ **Logging:** Serilog  

---

## 📌 **💻 Como Rodar o Projeto**
### **1️⃣ Clonar o Repositório**
```sh
git clone https://github.com/pedrohsz/TesteTecnicoNTT.git
cd TesteTecnicoNTT
```

### **2️⃣ Configurar o PostgreSQL e MongoDB**
**📌 PostgreSQL**: Criar um banco chamado `TesteTecnicoNTT` e configurar a connection string no `appsettings.json`:
```json
"ConnectionStrings": {
  "PostgresConnection": "Host=localhost;Database=TesteTecnicoNTT;Username=postgres;Password=teste"
}
```
**📌 MongoDB**: Criar o banco `TesteTecnicoNTT` e configurar:
```json
"MongoDbSettings": {
  "ConnectionString": "mongodb://localhost:27017",
  "DatabaseName": "TesteTecnicoNTT"
}
```

### **3️⃣ Rodar as Migrações do EF Core**
```sh
cd src/TesteTecnicoNTT.API
dotnet ef database update
```

### **4️⃣ Iniciar o Kafka**
```sh
bin\windows\zookeeper-server-start.bat config\zookeeper.properties
bin\windows\kafka-server-start.bat config\server.properties
```

### **5️⃣ Executar a API, Gateway e BFF**
```sh
cd src/TesteTecnicoNTT.API && dotnet run
cd src/TesteTecnicoNTT.Gateway && dotnet run
cd src/TesteTecnicoNTT.BFF && dotnet run
```

---

## 📌 **🔗 Endpoints e URLs**
| Serviço        | URL                      |
|---------------|-------------------------|
| **API**       | `https://localhost:7230` |
| **BFF**       | `https://localhost:7062` |
| **Gateway**   | `https://localhost:8080` |
| **Swagger API** | `https://localhost:7230/swagger` |
| **Swagger BFF** | `https://localhost:7062/swagger` |

---

## 📌 **🛠️ Testando a API**
### **📌 1️⃣ Autenticação - Obter Token**
```sh
POST https://localhost:7062/bff/auth/login
Body:
{
  "username": "admin",
  "password": "123456"
}
```

### **📌 2️⃣ Criar um Cliente**
```sh
POST https://localhost:8080/clientes
Authorization: Bearer <TOKEN> (Deixei livre pra facilitar os testes)
Body:
{
  "CpfCnpj": "12345678901",
  "Nome": "João Silva",
  "NumeroContrato": "CTR-0001",
  "Cidade": "São Paulo",
  "Estado": "SP",
  "RendaBruta": 5000.00
}
```

### **📌 3️⃣ Criar um Pagamento**
```sh
POST https://localhost:8080/pagamentos
Authorization: Bearer <TOKEN> (Deixei livre pra facilitar os testes)
Body:
{
  "ClienteId": "<ID_DO_CLIENTE>",
  "NumeroContrato": "CTR-0001",
  "Parcela": 1,
  "Valor": 1500.00,
  "DataPagamento": "2024-03-12",
  "Status": 1
}
```

---

## 📌 **🎯 Conclusão**
Este projeto implementa **Clean Architecture, CQRS e Event-Driven Architecture com Kafka**, garantindo escalabilidade e performance.  
Ele permite **CRUD de Clientes e Pagamentos**, **Relatórios no MongoDB**, **Autenticação OAuth2 (Simulação)**, **API Gateway** e **BFF**.

✅ **Projeto Finalizado e Pronto para Uso!** 🚀🔥  
---

### 📌 **🔗 Autor & Contribuições**
**Desenvolvido por:** `Pedro Souza`
