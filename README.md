# README - Como Rodar o Projeto ASP.NET Core e Angular Localmente

## Requisitos

Antes de começar, verifique se você possui os seguintes itens instalados em sua máquina:

- [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0) 
- [Node.js](https://nodejs.org/) (versão recomendada: 14.x ou superior)
- [Angular CLI](https://angular.io/cli) (instalável via npm)
- [SQL Server Express](https://www.microsoft.com/sql-server/sql-server-downloads)
- [Visual Studio](https://visualstudio.microsoft.com/) ou [Visual Studio Code](https://code.visualstudio.com/) (com extensões para C# e Angular)

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

## Configuração do Frontend (Angular)

1. Abra um novo terminal na pasta do projeto Angular.
2. Instale as dependências do projeto:

```
npm install
```

3. Inicie o servidor de desenvolvimento Angular:

```
ng serve
```

O frontend estará disponível em `http://localhost:4200`.

## Executando o Projeto

1. Certifique-se de que tanto o backend (ASP.NET Core) quanto o frontend (Angular) estejam rodando simultaneamente.
2. Abra um navegador e acesse `http://localhost:4200` para interagir com a aplicação.

## Solução de Problemas

- Se encontrar problemas com as migrações do Entity Framework, tente:
  ```
  dotnet ef migrations add InitialCreate
  dotnet ef database update
  ```
- Verifique se todas as dependências estão instaladas corretamente tanto para o backend quanto para o frontend.
- Certifique-se de que as portas 5001 (backend) e 4200 (frontend) estão disponíveis e não bloqueadas por firewall.

Para mais informações ou suporte, consulte a documentação oficial do [ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core) e [Angular](https://angular.io/docs).