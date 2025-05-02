using Microsoft.Extensions.Logging;
using Moq;
using System.Data;
using ConexionDBService.ClasesTecnicas;
using ConexionDBService.Services;


namespace ConexionDB.Tests
{
    public class DbServiceTests
    {
        readonly string _connectionStringValida;
        readonly string _connectionStringInvalida;
        readonly RetryOptions _retryOptions;


        public DbServiceTests()
        {

            var configuracion = TestConfig.Conexion;

            _connectionStringValida = ConexionDBHelper.ArmarCadenaConexion(
                         configuracion.Servidor,
                         configuracion.BaseDatos,
                         configuracion.Usuario,
                         configuracion.Password,
                         false,
                         true
                     );



                _connectionStringInvalida = ConexionDBHelper.ArmarCadenaConexion(
                     configuracion.Servidor,
                     configuracion.BaseDatos,
                     configuracion.Usuario,
                     "NoLaSe",
                     false,
                     true
                 );


            _retryOptions = new RetryOptions()
            {
                NumberOfTries = configuracion!.NumberOfTries,
                DeltaTime = configuracion!.DeltaTime,
                MaxTimeInterval = configuracion!.MaxTimeInterval,
            };


        }


        [Fact]
        public async Task ConectarAsync_DeberiaRetornarTrue_CuandoConexionExitosa()
        {

            // Arrange
            var dbService = new DbService(_connectionStringValida, _retryOptions);

            // Act
            var resultado = await dbService.ConectarAsync();

            // Assert
            Assert.True(resultado);
        }

        [Fact]
        public async Task ConectarAsync_DeberiaRetornarFalse_CuandoConexionInvalida()
        {

            // Arrange
            var dbService = new DbService(_connectionStringInvalida, _retryOptions);

            // Act
            var resultado = await dbService.ConectarAsync();

            // Assert
            Assert.False(resultado);
        }


        [Fact]
        public async Task CerrarAsync_DeberiaCerrarLaConexion_CuandoEstaAbierta()
        {
            // Arrange
            var dbService = new DbService(_connectionStringValida, _retryOptions);
            await dbService.ConectarAsync();

            // Act
            var resultado = await dbService.CerrarAsync();

            // Assert
            Assert.True(resultado);
        }

        [Fact]
        public async Task ExecuteNonQueryAsync_DeberiaInsertarLogCorrectamente()
        {
            // Arrange
            var dbService = new DbService(_connectionStringValida, _retryOptions);
            await dbService.ConectarAsync();

            string sql = "INSERT INTO Logs (Detalle, Fecha) VALUES (@detalle, @fecha)";
            var parametros = new Dictionary<string, object?>
            {
                { "@detalle", "Test de Insert" },
                { "@fecha", DateTime.Now }
            };

            // Act
            var filasAfectadas = await dbService.ExecuteNonQueryAsync(sql, parametros);

            // Assert
            Assert.True(filasAfectadas > 0);
        }



        [Fact]
        public async Task ExecuteNonQueryAsyncMasivo_DeberiaInsertarLogCorrectamente()
        {
            // Arrange
            var dbService = new DbService(_connectionStringValida, _retryOptions);
            await dbService.ConectarAsync();

            string sql = "INSERT INTO Logs (Detalle, Fecha) VALUES (@detalle, @fecha)";
            var parametros = new Dictionary<string, object?>
            {
                { "@detalle", "Test de Insert" },
                { "@fecha", DateTime.Now }
            };


            for (int i = 0; i < 2; i++)
            {
                // Act
                var filasAfectadas = await dbService.ExecuteNonQueryAsync(sql, parametros);

                // Assert
                Assert.True(filasAfectadas > 0);
            }
            
        }


        [Fact]
        public async Task ExecuteScalarAsync_DeberiaDevolverCantidadDeLogs()
        {
            // Arrange
            var dbService = new DbService(_connectionStringValida, _retryOptions);
            await dbService.ConectarAsync();

            string sql = "SELECT COUNT(*) FROM Logs";

            // Act
            var cantidad = await dbService.ExecuteScalarAsync<int>(sql);

            // Assert
            Assert.True(cantidad >= 0); // Puede ser 0 o más
        }

        [Fact]
        public async Task ExecuteReaderAsync_DeberiaDevolverLogs()
        {
            // Arrange
            var dbService = new DbService(_connectionStringValida,_retryOptions);
            await dbService.ConectarAsync();

            string sql = "SELECT TOP 5 * FROM Logs";

            // Act
            var tabla = await dbService.ExecuteQueryAsync(sql);

            // Assert
            Assert.NotNull(tabla);
            Assert.True(tabla.Rows.Count >= 0); // Puede ser 0 o más
        }

        [Fact]
        public async Task BeginCommitTransaction_DeberiaFuncionarCorrectamente()
        {
            // Arrange
            var dbService = new DbService(_connectionStringValida, _retryOptions);
            await dbService.ConectarAsync();

            // Act
            await dbService.BeginTransactionAsync();
            await dbService.CommitTransactionAsync();

            // Assert
            Assert.True(true); // Si no explota, la transacción funcionó.
        }


        [Fact]
        public async Task ConectarAsync_DeberiaRetornarTrue_ConLoggerMockeado()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<DbService>>();
            var dbService = new DbService(_connectionStringValida, _retryOptions, mockLogger.Object);

            // Act
            var resultado = await dbService.ConectarAsync();

            // Assert
            Assert.True(resultado);

            // Verificación opcional: que el logger no haya logueado errores
            mockLogger.Verify(
                x => x.Log(
                    It.IsAny<LogLevel>(),
                    It.IsAny<EventId>(),
                    It.IsAny<It.IsAnyType>(),
                    It.IsAny<Exception?>(),
                    (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>()
                ),
                Times.Never
            );
        }



        [Fact]
        public async Task ExecuteReaderAsync_DataTable_TopLog_SinHelper()
        {

            var _dbService = new DbService(_connectionStringValida, _retryOptions);
            await _dbService.ConectarAsync();

            string sql = "SELECT  * FROM dbo.Logs (nolock)  order by Id desc";
            var tabla = await _dbService.ExecuteQueryAsync(sql);

            foreach (DataRow fila in tabla.Rows)
            {
                int id = Convert.ToInt32(fila["Id"]);
                string? detalle = fila["Detalle"] as string;
                DateTime fecha = Convert.ToDateTime(fila["Fecha"]);

                Console.WriteLine($"Id: {id}, Detalle: {detalle}, Fecha: {fecha}");
            }
            Assert.True(tabla.Rows.Count >= 0); // Puede ser 0 o mas
        }




        [Fact]
        public async Task ExecuteReaderAsync_DataTable_TopLog_con_SQL_Helper_V1()
        {
            var _dbService = new DbService(_connectionStringValida, _retryOptions);
            await _dbService.ConectarAsync();

            string sql = "SELECT  * FROM dbo.Logs (nolock)  order by Id desc";
            var tabla = await _dbService.ExecuteQueryAsync(sql);


            // Obtener los índices de las columnas antes del ciclo
            int idIndex = tabla.Columns.IndexOf("Id");
            int detalleIndex = tabla.Columns.IndexOf("Detalle");
            int fechaIndex = tabla.Columns.IndexOf("Fecha");

            foreach (DataRow fila in tabla.Rows)
            {
                int id = Sqlv1.ReadNotNull<int>(fila, idIndex);
                string? detalle = Sqlv1.Read<string>(fila, detalleIndex);
                DateTime fecha = Sqlv1.ReadNotNull<DateTime>(fila, fechaIndex);

                Console.WriteLine($"Id: {id}, Detalle: {detalle}, Fecha: {fecha}");
            }
            Assert.True(tabla.Rows.Count >= 0); // Puede ser 0 o mas
        }


        [Fact]
        public async Task ExecuteReaderAsync_DataTable_TopLog_con_SQL_Helper()
        {
            var _dbService = new DbService(_connectionStringValida, _retryOptions);
            await _dbService.ConectarAsync();

            string sql = "SELECT  * FROM dbo.Logs (nolock)  order by Id desc";
            var tabla = await _dbService.ExecuteQueryAsync(sql);


            foreach (DataRow fila in tabla.Rows)
            {
                int id = Sql.ReadNotNullInt(fila, "Id");
                string? detalle = Sql.ReadString(fila, "Detalle");
                DateTime fecha = Sql.ReadNotNullDateTime(fila, "Fecha");

                Console.WriteLine($"Id: {id}, Detalle: {detalle}, Fecha: {fecha}");
            }
            Assert.True(tabla.Rows.Count >= 0); // Puede ser 0 o mas
        }




        [Fact]
        public async Task ExecuteReaderAsync_SQLDataReader_TopLog_SinHelper()
        {

            var _dbService = new DbService(_connectionStringValida, _retryOptions);
            await _dbService.ConectarAsync();

            string sql = "SELECT  * FROM dbo.Logs (nolock)  order by Id desc";
            var reader = await _dbService.ExecuteReaderAsync(sql);

            while (reader.Read())
            {
                int id = Convert.ToInt32(reader["Id"].ToString());
                string? detalle = reader.IsDBNull(reader.GetOrdinal("Detalle")) ? null : reader["Detalle"].ToString();

                int ordinal = reader.GetOrdinal("Fecha");
                DateTime fecha = reader.GetDateTime(ordinal);

                Console.WriteLine($"Id: {id}, Detalle: {detalle}, Fecha: {fecha}");
            }
            Assert.True(reader.HasRows); // Puede ser 0 o mas
        }


        [Fact]
        public async Task ExecuteReaderAsync_SQLDataReader_TopLog_con_SQL_Helper_V0()
        {
            var _dbService = new DbService(_connectionStringValida, _retryOptions);
            await _dbService.ConectarAsync();

            string sql = "SELECT  * FROM dbo.Logs (nolock) order by Id desc";
            var reader = await _dbService.ExecuteReaderAsync(sql);


            while (reader.Read())
            {

                int id = Sqlv0.ReadNotNull<int>(reader, "Id");
                string? detalle = Sqlv0.Read<String>(reader, "Detalle");
                DateTime fecha = Sqlv0.ReadNotNull<DateTime>(reader, "Fecha");

                Console.WriteLine($"Id: {id}, Detalle: {detalle}, Fecha: {fecha}");
            }
            Assert.True(reader.HasRows); // Puede ser 0 o mas
        }


        //
        [Fact]
        public async Task ExecuteReaderAsync_SQLDataReader_TopLog_con_SQL_Helper_V1()
        {
            var _dbService = new DbService(_connectionStringValida, _retryOptions);
            await _dbService.ConectarAsync();

            string sql = "SELECT  * FROM dbo.Logs (nolock)  order by Id desc";
            var reader = await _dbService.ExecuteReaderAsync(sql);


            // Obtener los índices de las columnas antes del ciclo
            int idIndex = reader.GetOrdinal("Id");
            int detalleIndex = reader.GetOrdinal("Detalle");
            int fechaIndex = reader.GetOrdinal("Fecha");

            while (reader.Read())
            {
                int id = Sqlv1.ReadNotNull<int>(reader, idIndex);
                string? detalle = Sqlv1.Read<string>(reader, detalleIndex);
                DateTime fecha = Sqlv1.ReadNotNull<DateTime>(reader, fechaIndex);

                Console.WriteLine($"Id: {id}, Detalle: {detalle}, Fecha: {fecha}");
            }
            Assert.True(reader.HasRows); // Puede ser 0 o mas
        }


        [Fact]
        public async Task ExecuteReaderAsync_SQLDataReader_TopLog_con_SQL_Helper()
        {
            var _dbService = new DbService(_connectionStringValida, _retryOptions);
            await _dbService.ConectarAsync();

            string sql = "SELECT  * FROM dbo.Logs (nolock)  order by Id desc";
            var reader = await _dbService.ExecuteReaderAsync(sql);


            while (reader.Read())
            {
                int id = Sql.ReadNotNullInt(reader, "Id");
                string? detalle = Sql.ReadString(reader, "Detalle");
                DateTime fecha = Sql.ReadNotNullDateTime(reader, "Fecha");

                Console.WriteLine($"Id: {id}, Detalle: {detalle}, Fecha: {fecha}");
            }
            Assert.True(reader.HasRows); // Puede ser 0 o mas
        }


    }
}
