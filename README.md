# README - Como Rodar o Projeto ASP.NET Core e Angular Localmente

## Requisitos

Antes de come�ar, verifique se voc� possui os seguintes itens instalados em sua m�quina:

- [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0) 
- [Node.js](https://nodejs.org/) (vers�o recomendada: 14.x ou superior)
- [Angular CLI](https://angular.io/cli) (instal�vel via npm)
- [SQL Server Express](https://www.microsoft.com/sql-server/sql-server-downloads)
- [Visual Studio](https://visualstudio.microsoft.com/) ou [Visual Studio Code](https://code.visualstudio.com/) (com extens�es para C# e Angular)

## Configura��o do Banco de Dados

1. Abra o arquivo `appsettings.json` na raiz do projeto ASP.NET Core.
2. Localize a se��o `ConnectionStrings` e ajuste a string de conex�o `DefaultConnection` conforme necess�rio:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=DESKTOP\\SQLEXPRESS;Database=LoremIpsumLogisticaDb;Integrated Security=True;TrustServerCertificate=True;"
  }
}
```

3. Certifique-se de que o servidor SQL Server est� rodando e acess�vel com as credenciais fornecidas.

## Configura��o do Backend (ASP.NET Core)

1. Abra um terminal na pasta do projeto ASP.NET Core.
2. Execute as migra��es do Entity Framework para criar o banco de dados:

```
dotnet ef database update
```

3. Inicie o servidor de desenvolvimento ASP.NET Core:

```
dotnet run
```

O backend estar� dispon�vel em `https://localhost:5001` (ou a porta especificada nas configura��es).

## Configura��o do Frontend (Angular)

1. Abra um novo terminal na pasta do projeto Angular.
2. Instale as depend�ncias do projeto:

```
npm install
```

3. Inicie o servidor de desenvolvimento Angular:

```
ng serve
```

O frontend estar� dispon�vel em `http://localhost:4200`.

## Executando o Projeto

1. Certifique-se de que tanto o backend (ASP.NET Core) quanto o frontend (Angular) estejam rodando simultaneamente.
2. Abra um navegador e acesse `http://localhost:4200` para interagir com a aplica��o.

## Solu��o de Problemas

- Se encontrar problemas com as migra��es do Entity Framework, tente:
  ```
  dotnet ef migrations add InitialCreate
  dotnet ef database update
  ```
- Verifique se todas as depend�ncias est�o instaladas corretamente tanto para o backend quanto para o frontend.
- Certifique-se de que as portas 5001 (backend) e 4200 (frontend) est�o dispon�veis e n�o bloqueadas por firewall.

Para mais informa��es ou suporte, consulte a documenta��o oficial do [ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core) e [Angular](https://angular.io/docs).