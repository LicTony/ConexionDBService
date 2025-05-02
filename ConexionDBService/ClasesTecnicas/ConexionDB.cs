using Microsoft.Data.SqlClient;
using System.Data;

namespace ConexionDBService.ClasesTecnicas
{
    public static class ConexionDBHelper
    {
        public static String ArmarCadenaConexion(string servidor, string baseDatos, string usuario, string password, bool trustedConnection = false, bool trustServerCertificate = false)
        {
            return $"Server={servidor};Database={baseDatos};User Id={usuario};Password={password};Trusted_Connection={trustedConnection};TrustServerCertificate={trustServerCertificate}";
        }

    }

    public class RetryOptions 
    {
        /// <summary>
        /// Number of tries to connect to the database.
        /// </summary>
        public int NumberOfTries { get; set; }

        /// <summary>
        /// Time interval between each try in seconds.
        /// </summary>
        public int DeltaTime { get; set; }

        /// <summary>
        /// Maximum time interval between each try in seconds.
        /// </summary>
        public int MaxTimeInterval { get; set; }
    }


    public class ConexionDB : IAsyncDisposable
    {
        private SqlConnection? cnnAux;
        public string UltimoMensajeError { get; private set; } = string.Empty;

        public SqlConnection? Cnn => cnnAux;

        private bool disposedValue = false;

        public ConexionDB()
        {
            cnnAux = null;
        }

        public async Task<bool> ConectarAsync(string connectionString , RetryOptions retryOptions)
        {
            try
            {
                var retryOptionsAux = new SqlRetryLogicOption
                {
                    NumberOfTries = retryOptions.NumberOfTries,
                    DeltaTime = TimeSpan.FromSeconds(retryOptions.DeltaTime),
                    MaxTimeInterval = TimeSpan.FromSeconds(retryOptions.MaxTimeInterval)
                };

                var retryProvider = SqlConfigurableRetryFactory.CreateFixedRetryProvider(retryOptionsAux);

                cnnAux = new SqlConnection(connectionString)
                {
                    RetryLogicProvider = retryProvider
                };

                await cnnAux.OpenAsync();
                UltimoMensajeError = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                UltimoMensajeError = ex.Message;
                return false;
            }
        }

        public async Task<bool> CerrarAsync()
        {
            try
            {
                if (cnnAux != null && cnnAux.State != ConnectionState.Closed)
                {
                    await cnnAux.CloseAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                UltimoMensajeError = ex.Message;
                return false;
            }
        }

        public async Task<int> ExecuteNonQueryAsync(
            string sql,
            Dictionary<string, object?>? parametros = null,
            SqlTransaction? transaccion = null,
            int commandTimeoutSegundos = 30)
        {
            ValidarConexion();

            try
            {
                await using var cmd = CrearComando(sql, parametros, transaccion, commandTimeoutSegundos);
                int filasAfectadas = await cmd.ExecuteNonQueryAsync();
                UltimoMensajeError = string.Empty;
                return filasAfectadas;
            }
            catch (Exception ex)
            {
                UltimoMensajeError = ex.Message;
                throw;
            }
        }

        public async Task<T?> ExecuteScalarAsync<T>(
            string sql,
            Dictionary<string, object?>? parametros = null,
            SqlTransaction? transaccion = null,
            int commandTimeoutSegundos = 30)
        {
            ValidarConexion();

            try
            {
                await using var cmd = CrearComando(sql, parametros, transaccion, commandTimeoutSegundos);
                object? resultado = await cmd.ExecuteScalarAsync();
                UltimoMensajeError = string.Empty;

                if (resultado == null || resultado == DBNull.Value)
                    return default;

                return (T)Convert.ChangeType(resultado, typeof(T));
            }
            catch (Exception ex)
            {
                UltimoMensajeError = ex.Message;
                throw;
            }
        }

        public async Task<DataTable> ExecuteQueryAsync(
            string sql,
            Dictionary<string, object?>? parametros = null,
            SqlTransaction? transaccion = null,
            int commandTimeoutSegundos = 30)
        {
            ValidarConexion();

            try
            {
                await using var cmd = CrearComando(sql, parametros, transaccion, commandTimeoutSegundos);
                await using var reader = await cmd.ExecuteReaderAsync();

                var table = new DataTable();
                table.Load(reader);

                UltimoMensajeError = string.Empty;
                return table;
            }
            catch (Exception ex)
            {
                UltimoMensajeError = ex.Message;
                throw;
            }
        }


        public async Task<SqlDataReader> ExecuteReaderAsync(
          string sql,
          Dictionary<string, object?>? parametros = null,
          SqlTransaction? transaccion = null,
          int commandTimeoutSegundos = 30)
        {
            ValidarConexion();

            try
            {
                await using var cmd = CrearComando(sql, parametros, transaccion, commandTimeoutSegundos);
                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                UltimoMensajeError = string.Empty;
                return reader;
            }
            catch (Exception ex)
            {
                UltimoMensajeError = ex.Message;
                throw;
            }
        }



        public async Task<int> ExecuteStoredProcedureAsync(
            string nombreSP,
            Dictionary<string, object?>? parametros = null,
            SqlTransaction? transaccion = null,
            int commandTimeoutSegundos = 30)
        {
            ValidarConexion();

            try
            {
                await using var cmd = CrearComando(nombreSP, parametros, transaccion, commandTimeoutSegundos, true);
                int filasAfectadas = await cmd.ExecuteNonQueryAsync();
                UltimoMensajeError = string.Empty;
                return filasAfectadas;
            }
            catch (Exception ex)
            {
                UltimoMensajeError = ex.Message;
                throw;
            }
        }

        private SqlCommand CrearComando(
            string sql,
            Dictionary<string, object?>? parametros,
            SqlTransaction? transaccion,
            int commandTimeoutSegundos,
            bool esStoredProcedure = false)
        {
            var cmd = cnnAux!.CreateCommand();
            cmd.CommandText = sql;
            cmd.CommandTimeout = commandTimeoutSegundos;
            cmd.Transaction = transaccion;
            cmd.CommandType = esStoredProcedure ? CommandType.StoredProcedure : CommandType.Text;

            if (parametros != null)
            {
                foreach (var param in parametros)
                {
                    cmd.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                }
            }

            return cmd;
        }

        private void ValidarConexion()
        {
            if (cnnAux == null || cnnAux.State != ConnectionState.Open)
                throw new InvalidOperationException("La conexión a la base de datos no está abierta.");
        }

        protected virtual async ValueTask DisposeAsyncCore()
        {
            if (cnnAux != null)
            {
                await CerrarAsync();
                await cnnAux.DisposeAsync();
                cnnAux = null;
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (!disposedValue)
            {
                await DisposeAsyncCore();

                disposedValue = true;
                GC.SuppressFinalize(this);
            }
        }
    }
}
