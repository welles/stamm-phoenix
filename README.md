# Persistence

## Add database migration

```
dotnet-ef migrations add InitialMigration -p .\src\StammPhoenix.Infrastructure\ -o Persistence\Migrations\
```

## Update database
```
dotnet-ef database update -p .\src\StammPhoenix.Infrastructure\ --connection "Server=localhost;Database=stamm-phoenix;Port=5055;User Id=postgres;Password=postgres"
```
