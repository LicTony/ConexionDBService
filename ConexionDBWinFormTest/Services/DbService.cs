using ConexionDBWinFormTest.ClasesTecnicas;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System.Data;


namespace ConexionDBWinFormTest.Services
{
    //public interface IDbService : IAsyncDisposable
    //{
    //    Task<bool> ConectarAsync();
    //    Task<bool> CerrarAsync();
    //    Task<int> ExecuteNonQueryAsync(string sql, Dictionary<string, object?>? parametros = null);
    //    Task<T?> ExecuteScalarAsync<T>(string sql, Dictionary<string, object?>? parametros = null);
    //    Task<DataTable> ExecuteQueryAsync(string sql, Dictionary<string, object?>? parametros = null);
    //    Task<SqlDataReader> ExecuteReaderAsync(string sql, Dictionary<string, object?>? parametros = null);
    //    Task<int> ExecuteStoredProcedureAsync(string nombreSP, Dictionary<string, object?>? parametros = null);

    //    Task BeginTransactionAsync();
    //    Task CommitTransactionAsync();
    //    Task RollbackTransactionAsync();

    //}

    //public class DbService : IDbService
    //{
    //    private readonly string _connectionString;
    //    private readonly ILogger<DbService>? _logger;
    //    private ConexionDB? _conexion;
    //    private SqlTransaction? _transaction;

    //    private bool _disposed = false;

    //    public DbService(string connectionString, ILogger<DbService>? logger = null)
    //    {
    //        _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
    //        _logger = logger;
    //    }

    //    public async Task<bool> ConectarAsync()
    //    {
    //        try
    //        {
    //            _conexion = new ConexionDB();
    //            return await _conexion.ConectarAsync(_connectionString);
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger?.LogError(ex, "Error al conectar a la base de datos.");
    //            throw;
    //        }
    //    }

    //    public async Task<bool> CerrarAsync()
    //    {
    //        if (_conexion != null)
    //            return await _conexion.CerrarAsync();

    //        return true;
    //    }

    //    public async Task<int> ExecuteNonQueryAsync(string sql, Dictionary<string, object?>? parametros = null)
    //    {
    //        ValidarConexion();
    //        return await _conexion!.ExecuteNonQueryAsync(sql, parametros, _transaction);
    //    }

    //    public async Task<T?> ExecuteScalarAsync<T>(string sql, Dictionary<string, object?>? parametros = null)
    //    {
    //        ValidarConexion();
    //        return await _conexion!.ExecuteScalarAsync<T>(sql, parametros, _transaction);
    //    }

    //    public async Task<DataTable> ExecuteQueryAsync(string sql, Dictionary<string, object?>? parametros = null)
    //    {
    //        ValidarConexion();
    //        return await _conexion!.ExecuteQueryAsync(sql, parametros, _transaction);
    //    }

    //    public async Task<SqlDataReader> ExecuteReaderAsync(string sql, Dictionary<string, object?>? parametros = null)
    //    {
    //        ValidarConexion();
    //        return await _conexion!.ExecuteReaderAsync(sql, parametros, _transaction);
    //    }


    //    public async Task<int> ExecuteStoredProcedureAsync(string nombreSP, Dictionary<string, object?>? parametros = null)
    //    {
    //        ValidarConexion();
    //        return await _conexion!.ExecuteStoredProcedureAsync(nombreSP, parametros, _transaction);
    //    }

    //    public async Task BeginTransactionAsync()
    //    {
    //        ValidarConexion();
    //        if (_transaction != null)
    //            throw new InvalidOperationException("Ya hay una transacción activa.");

    //        _transaction = (SqlTransaction)await _conexion!.Cnn!.BeginTransactionAsync(); // Cambio aquí
    //    }

    //    public async Task CommitTransactionAsync()
    //    {
    //        if (_transaction == null)
    //            throw new InvalidOperationException("No hay una transacción activa para confirmar.");

    //        await _transaction.CommitAsync();
    //        await _transaction.DisposeAsync();
    //        _transaction = null;
    //    }

    //    public async Task RollbackTransactionAsync()
    //    {
    //        if (_transaction == null)
    //            throw new InvalidOperationException("No hay una transacción activa para cancelar.");

    //        await _transaction.RollbackAsync();
    //        await _transaction.DisposeAsync();
    //        _transaction = null;
    //    }

    //    private void ValidarConexion()
    //    {
    //        if (_conexion == null || _conexion.Cnn == null || _conexion.Cnn.State != ConnectionState.Open)
    //            throw new InvalidOperationException("La conexión no está abierta.");
    //    }

    //    protected virtual async ValueTask DisposeAsyncCore()
    //    {
    //        if (_transaction != null)
    //        {
    //            await _transaction.DisposeAsync();
    //            _transaction = null;
    //        }

    //        if (_conexion != null)
    //        {
    //            await _conexion.DisposeAsync();
    //            _conexion = null;
    //        }
    //    }

    //    public async ValueTask DisposeAsync()
    //    {
    //        if (!_disposed)
    //        {
    //            await DisposeAsyncCore();
    //            _disposed = true;
    //            GC.SuppressFinalize(this);
    //        }
    //    }

    //    internal bool HayUnaTransacionAbierta()
    //    {
    //        return _transaction != null;
    //    }

    //    public string UltimoMensaje
    //    {
    //        get { return _conexion!.UltimoMensajeError; }
    //    }


    //}
}
