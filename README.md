# teste-cadastro-alunos


passo a passo para rodar o projeto: 
1 - docker instalado e criar bando de dados:
    rodar: docker run -v ~/docker --name sqlserver -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=1q2w3e4r@#$" -p 1433:1433 -d mcr.microsoft.com/mssql/server
    isso irá criar o banco de dados sql server no windows, com a senha que está configurada no appsettings.json
    
2 - criar o banco do cadastro de alunos, entrar no projeto CadastroEscolar.Infra.Data e executar:
    dotnet ef database update --startup-project ../CadastroEscolar.Presentation.MVC
    
    possível erro: não ter o ef global instalado.
    Solução:
    dotnet tool install --global dotnet-ef

3 -  rodar a aplicação pelo vs, rider ou cli.

