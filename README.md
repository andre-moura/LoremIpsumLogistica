# README - Como Rodar o Projeto ASP.NET Core e Angular Localmente

## Requisitos

Antes de começar, verifique se você possui os seguintes itens instalados em sua máquina:

- [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0) 
- [Node.js](https://nodejs.org/) (versão recomendada: 14.x ou superior)
- [Angular CLI](https://angular.io/cli) (instalável via npm)
- [SQL Server Express](https://www.microsoft.com/sql-server/sql-server-downloads)
- [Visual Studio](https://visualstudio.microsoft.com/)

## Configuração do Banco de Dados

1. Abra o arquivo `appsettings.json` na raiz do projeto ASP.NET Core.
2. Localize a seção `ConnectionStrings` e ajuste a string de conexão `DefaultConnection` conforme necessário:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=DESKTOP\\SQLEXPRESS;Database=LoremIpsumLogisticaDb;Integrated Security=True;TrustServerCertificate=True;"
  }
}
```

3. Certifique-se de que o servidor SQL Server está rodando e acessível com as credenciais fornecidas.

## Configuração do Backend (ASP.NET Core)

1. Abra um terminal na pasta do projeto ASP.NET Core.
2. Execute as migrações do Entity Framework para criar o banco de dados:

```
dotnet ef database update
```

3. Inicie o servidor de desenvolvimento ASP.NET Core:

```
dotnet run
```

O backend estará disponível em `https://localhost:5001` (ou a porta especificada nas configurações).
