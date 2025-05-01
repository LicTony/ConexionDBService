# ConexionDBService

Servicio de acceso a base de datos SQL Server para .NET 8.

**ConexionDBService** es una implementación liviana y asíncrona de `IDbService`, diseñada para facilitar la interacción con bases de datos Microsoft SQL Server en aplicaciones .NET. Incluye operaciones comunes como ejecución de consultas, procedimientos almacenados y manejo de transacciones.

---

## Características

- Conexión y cierre de base de datos asíncronos.
- Métodos disponibles:
  - `ExecuteNonQueryAsync`
  - `ExecuteScalarAsync<T>`
  - `ExecuteQueryAsync` (retorna `DataTable`)
  - `ExecuteReaderAsync` (retorna `SqlDataReader`)
  - `ExecuteStoredProcedureAsync`
- Soporte completo para transacciones:
  - `BeginTransactionAsync`
  - `CommitTransactionAsync`
  - `RollbackTransactionAsync`
- Manejo de parámetros mediante `Dictionary<string, object?>`
- Reutilizable, testeable e ideal para entornos desacoplados.

---

## Requisitos

- .NET 8.0 o superior  
- Paquete NuGet: `Microsoft.Data.SqlClient`

---

## Instalación

Clonar el repositorio:

```bash
git clone https://github.com/LicTony/ConexionDBService.git
