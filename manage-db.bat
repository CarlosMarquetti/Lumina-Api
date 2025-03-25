@echo off
IF "%1"=="add" (
    dotnet ef migrations add %2 --project .\src\WL.Infrastructure --startup-project .\src\WL.API --output-dir Migrations
) ELSE IF "%1"=="update" (
    dotnet ef database update --project .\src\WL.Infrastructure --startup-project .\src\WL.API
) ELSE (
    echo Uso: manage-db add [NomeDaMigration] ou manage-db update
)