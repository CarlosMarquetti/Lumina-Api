@echo off
IF "%1"=="add" (
    dotnet ef migrations add %2 --project .\src\Lumina.Infrastructure --startup-project .\src\Lumina.API --output-dir Migrations
) ELSE IF "%1"=="update" (
    dotnet ef database update --project .\src\Lumina.Infrastructure --startup-project .\src\Lumina.API
) ELSE (
    echo Uso: manage-db add [NomeDaMigration] ou manage-db update
)