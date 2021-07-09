RISCO PROTOTIPO
RODAR AS MIGRATIONS 
    - dotnet ef migrations add addInfoMercadoArquivo --startup-project
    - dotnet ef database update --startup-project ..\risco-prototipo\Risco.DadoExterno.CCEE.InfoMercado\Risco.DadoExterno.CCEE.InfoMercado.csproj
    - dotnet ef database update --startup-project ..\Risco.DadoExterno.CCEE.InfoMercado\Risco.DadoExterno.CCEE.InfoMercado.csproj --context Risco.Prototipo.Domain.Models.RiscoPrototipoContextBas
e

PUBLISH COM MSBUILD 
    - msbuild.exe C:\Users\Pablo\Documents\Projetos\risco-prototipo\Risco.Prototipo.sln /p:DeployOnBuild=true /p:Configuration=Release;OutDir=C:\Tmp\myApp\
    
ACESSAR BANCO DE DADOS VIA CONSOLE
    - docker exec -it SQL-SERVER /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P (!)Password(@)
    
Atualizcoes
    DBContext connection string
    add migrations (remover idInfomercado)
    add migrations (remover codigoperfilagente)
    add migrations (adicionar relacionamentos)
    
    