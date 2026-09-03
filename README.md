ProductManager

Instrucciones rápidas para aplicar migraciones y ejecutar la API (desarrollo)

Prerequisitos:
- .NET 10 SDK
- SQL Server / LocalDB accesible

1) Compilar el proyecto:

```sh
dotnet build
```

2) Aplicar migraciones con el script PowerShell (desde la raíz del repositorio):

```powershell
./scripts/apply-migrations.ps1
```

Este script ejecuta:

```sh
dotnet ef database update --project src/ProductManager.Infrastructure --startup-project src/ProductManager.API
```

3) Alternativa: arrancar la API; Program.cs ya invoca Database.Migrate(), por lo que al iniciar la aplicación se aplicarán las migraciones pendientes automáticamente:

```sh
dotnet run --project src/ProductManager.API
```

4) Verificar en SQL Server que la tabla Products existe:

```sql
SELECT SCHEMA_NAME(t.schema_id) AS SchemaName, t.name FROM sys.tables WHERE t.name = 'Products';
```

Notas:
- En desarrollo, EF Core está configurado para loggear SQL en consola (EnableSensitiveDataLogging). No habilitar esto en producción.
- Si usa otra cadena de conexión, actualice appsettings.json o variables de entorno.
