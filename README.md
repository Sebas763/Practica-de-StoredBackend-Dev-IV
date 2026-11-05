# StoreBackend — Migraciones con Entity Framework Core

Backend .NET 10 en capas (`src/`) con la base de datos en **Azure SQL** (`sqlsebas2026.database.windows.net`, base `Store`).

- Proyecto de infraestructura (contiene `AppDbContext` y la carpeta `Migrations`): `src/StoreBackend.Infrastructure`
- Proyecto de inicio (lee la cadena de conexión de `appsettings`): `src/StoredBackend.Api`

## Requisitos

```bash
dotnet tool install --global dotnet-ef   # solo la primera vez
dotnet ef --version                      # verificar la instalación
```

Si `dotnet ef` no se reconoce en macOS/Linux: `export PATH="$PATH:$HOME/.dotnet/tools"`

Todos los comandos se ejecutan desde la **raíz de la solución** (donde está la carpeta `src`).

## Primera vez (crear la base desde cero)

```bash
dotnet ef migrations add InitialCreate --project src/StoreBackend.Infrastructure --startup-project src/StoredBackend.Api
dotnet ef database update --project src/StoreBackend.Infrastructure --startup-project src/StoredBackend.Api
```

## Cuando cambia el modelo de datos

Cada cambio se registra como una **nueva migración** con un nombre que describa el cambio; las migraciones anteriores no se editan ni se borran.

### Agregar una nueva entidad (tabla)

1. Crear la clase de la entidad (por ejemplo `Category`) en el proyecto de dominio/modelos.
2. Registrarla en `AppDbContext`:
   ```csharp
   public DbSet<Category> Categories { get; set; }
   ```
   y, si aplica, configurar su clave y relaciones en `OnModelCreating`.
3. Generar la migración y aplicarla:
   ```bash
   dotnet ef migrations add AddCategoryEntity --project src/StoreBackend.Infrastructure --startup-project src/StoredBackend.Api
   dotnet ef database update --project src/StoreBackend.Infrastructure --startup-project src/StoredBackend.Api
   ```

### Agregar una columna a una tabla existente

1. Agregar la propiedad a la entidad, por ejemplo en `Product`:
   ```csharp
   public decimal Price { get; set; }
   ```
2. Generar la migración y aplicarla:
   ```bash
   dotnet ef migrations add AddPriceToProduct --project src/StoreBackend.Infrastructure --startup-project src/StoredBackend.Api
   dotnet ef database update --project src/StoreBackend.Infrastructure --startup-project src/StoredBackend.Api
   ```

> Si la tabla ya tiene filas y la columna nueva es obligatoria (`NOT NULL`), conviene darle un valor por defecto o declararla como opcional (`decimal?`) para que la migración no falle.

## Comandos útiles

| Acción | Comando |
|---|---|
| Ver las migraciones y cuáles están aplicadas | `dotnet ef migrations list --project src/StoreBackend.Infrastructure --startup-project src/StoredBackend.Api` |
| Quitar la última migración **si aún no se aplicó** | `dotnet ef migrations remove --project src/StoreBackend.Infrastructure --startup-project src/StoredBackend.Api` |
| Volver la base a una migración anterior | `dotnet ef database update NombreMigracionAnterior --project src/StoreBackend.Infrastructure --startup-project src/StoredBackend.Api` |
| Generar el script SQL de todas las migraciones | `dotnet ef migrations script --project src/StoreBackend.Infrastructure --startup-project src/StoredBackend.Api -o migraciones.sql` |

## Notas de Azure SQL

- La base usa el nivel **serverless** con pausa automática. Si un comando falla con el error **40613** (*Database 'Store' ... is not currently available*), la base se está reanudando: esperar ~1 minuto y repetir el comando.
- El firewall del servidor solo admite las IP registradas. Si se trabaja desde otra red, agregar la nueva IP en **sqlsebas2026 → Seguridad → Redes**.
- La contraseña de la cadena de conexión **no se sube al repositorio**. En Azure App Service la cadena se configura como variable de entorno `ConnectionStrings__DefaultConnection`.
