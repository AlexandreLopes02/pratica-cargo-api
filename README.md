# 🚛 CargoPRO API

API REST do sistema **CargoPRO**, desenvolvida para gerenciamento de motoristas, empresas, usuários e ordens de serviço.

Este projeto foi desenvolvido utilizando **C# e .NET Web API**, com foco em criação de APIs REST, organização de código, autenticação e integração com frontend Angular.

---

## 🎯 Objetivo do projeto

A API foi criada para simular um cenário real de aplicação, permitindo:

* Gerenciamento de motoristas, empresas e serviços
* Autenticação de usuários
* Relacionamento entre entidades
* Consumo por aplicação frontend

---

## ⚙️ Funcionalidades

### 🔐 Autenticação

* Cadastro de usuários
* Login com geração de token JWT
* Proteção de rotas autenticadas

### 🚚 Motoristas

* Criar motoristas
* Listar motoristas
* Atualizar dados
* Excluir registros

### 🏢 Empresas

* Criar empresas
* Listar empresas
* Atualizar dados
* Excluir registros

### 📦 Ordens de Serviço

* Criar serviços
* Relacionar empresa e motorista
* Consultar serviços
* Validar dados antes do cadastro

### 🔎 Consultas

* Consulta por CPF
* Consulta por CNPJ
* Retorno de dados relacionados

---

## 🛠️ Tecnologias utilizadas

* C#
* .NET Web API
* Entity Framework Core
* SQL Server
* JWT Authentication
* Swagger (OpenAPI)

---

## 📂 Estrutura do projeto

```bash
├─ Controllers/     # endpoints da API
├─ Models/          # entidades do sistema
├─ Data/            # contexto do banco
├─ Services/        # regras de negócio
├─ DTOs/            # objetos de transferência de dados
├─ Migrations/      # migrations do EF
├─ appsettings.json
└─ Program.cs
```

---

## ▶️ Como executar o projeto

### 🔧 Pré-requisitos

Certifique-se de ter instalado:

* .NET SDK
* SQL Server

---

### 📥 Clonar o repositório

```bash
git clone https://github.com/AlexandreLopes02/pratica-cargo-api.git
```

---

### 📦 Restaurar dependências

```bash
dotnet restore
```

---

### ⚙️ Configurar banco de dados

No arquivo `appsettings.json`, configure a connection string:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=SEU_SERVIDOR;Database=CargoPRO;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

---

### 🗄️ Aplicar migrations

```bash
dotnet ef database update
```

---

### 🚀 Executar a aplicação

```bash
dotnet run
```

---

## 📄 Documentação da API

A API utiliza Swagger para documentação.

Após rodar o projeto, acesse:

👉 https://localhost:5001/swagger

---

## 🔗 Integração com frontend

Este projeto é consumido pelo frontend Angular:

👉 https://github.com/AlexandreLopes02/pratica-cargo-web

---

## ✅ Boas práticas aplicadas

* Estruturação em camadas
* Separação de responsabilidades
* Uso de DTOs
* Autenticação com JWT
* Validação de dados
* Integração com banco relacional

---

## 🔮 Melhorias futuras

* Tratamento global de erros
* Logs de aplicação
* Testes unitários
* Paginação e filtros
* Controle de permissões
* Deploy em nuvem

---

## 👨‍💻 Autor

**Alexandre Lopes de Lima**

* 📎 LinkedIn: https://www.linkedin.com/in/lopesalexandre02
* 💻 GitHub: https://github.com/AlexandreLopes02
