# ConexionDBService
Servicio de acceso a base de datos SQL Server para .NET 8.

ConexionDBService es una implementación liviana y asíncrona de IDbService, diseñada para facilitar la interacción con bases de datos Microsoft SQL Server en aplicaciones .NET. Incluye operaciones comunes como ejecución de consultas, procedimientos almacenados y manejo de transacciones.

## Características

- Conexión y cierre de base de datos asíncronos.
- Métodos disponibles:
  - ExecuteNonQueryAsync
  - ExecuteScalarAsync<T>
  - ExecuteQueryAsync (retorna DataTable)
  - ExecuteReaderAsync (retorna SqlDataReader)
  - ExecuteStoredProcedureAsync
- Soporte completo para transacciones:
  - BeginTransactionAsync
  - CommitTransactionAsync
  - RollbackTransactionAsync
- Manejo de parámetros mediante `Dictionary<string, object?>`
- Reutilizable, testeable e ideal para entornos desacoplados.

## Requisitos

- .NET 8.0 o superior
- Paquete NuGet: Microsoft.Data.SqlClient

## Instalación

Clonar el repositorio:

```bash
git clone https://github.com/LicTony/ConexionDBService.git
```

Agregar la clase a tu proyecto .NET:

```csharp
services.AddScoped<IDbService, DbService>();
```

## Ejemplo de uso

```csharp
public class MiServicio
{
    private readonly IDbService _db;

    public MiServicio(IDbService db)
    {
        _db = db;
    }

    public async Task<DataTable> ObtenerDatosAsync()
    {
        await _db.ConectarAsync();

        var datos = await _db.ExecuteQueryAsync(
            "SELECT * FROM Productos WHERE Activo = @Activo",
            new Dictionary<string, object?> { ["@Activo"] = true });
        
        await _db.CerrarAsync();
        return datos;
    }
}
```


### Mecanismo de reintentos

La clase `DbService` implementa lógica de reintentos basada en `SqlRetryLogicOption` para operaciones con SQL Server. Actualmente, utiliza la siguiente configuración predeterminada:

- **NumberOfTries:** 5 intentos
- **DeltaTime:** 1 segundo
- **MaxTimeInterval:** 5 segundos

Este mecanismo introduce un retraso incremental y aleatorio entre reintentos, lo que permite una mayor resiliencia frente a errores transitorios en la conexión o ejecución de comandos SQL.

🔜 En versiones futuras, estos parámetros serán completamente configurables a través de opciones externas para una mayor flexibilidad.


## Licencia

MIT

## Contribuciones

¡Son bienvenidas! Abrí un issue o hacé un pull request si querés mejorar el proyecto.

## Autor

[LicTony en GitHub](https://github.com/LicTony)
