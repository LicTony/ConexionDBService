using BenchmarkDotNet.Attributes;
using System.Data;
using ConexionDBService.Services;
using ConexionDBService.ClasesTecnicas;
using Microsoft.Extensions.Configuration;
using BenchmarkDotNet.Configs;


namespace ConexionBD.Benchmark
{
    [MemoryDiagnoser] // Opcional: mide uso de memoria
    public class ComparacionFunciones
    {
        
        string sql;
        private DbService _dbService = null!;

        public static IConfiguration Config { get; private set; } = default!;
     

        public ComparacionFunciones()
        {
            sql = "";
        }


        [GlobalSetup]
        public void Setup()
        {
            // Cargar .env (si existe)
            DotNetEnv.Env.Load(); // opcional

            // Cargar configuración al inicio
            Config = new ConfigurationBuilder()
             .SetBasePath(AppContext.BaseDirectory)
             .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
             .AddEnvironmentVariables()
            .Build();


            var Conexion = Config.GetSection("ConexionBD").Get<ConexionBDSetting>() ??
              throw new InvalidOperationException("No se ha encontrado la sección de configuración de la base de datos en appsettings.json.");

            string _connectionStringValida = ConexionDBHelper.ArmarCadenaConexion(
                           Conexion!.Servidor,
                           Conexion!.BaseDatos,
                           Conexion!.Usuario,
                           Conexion!.Password,
                           Conexion!.TrustedConnection,
                           Conexion!.TrustServerCertificate
            );

            _dbService = new DbService(_connectionStringValida , new RetryOptions() { 
                NumberOfTries = Conexion!.NumberOfTries,
                DeltaTime = Conexion!.DeltaTime,
                MaxTimeInterval = Conexion!.MaxTimeInterval,
            });
            

            sql = "SELECT top 1000  Id, Detalle, Fecha FROM dbo.Logs (nolock)  order by Id desc";
        }

        

        [Benchmark]
        public async Task ExecuteReaderAsync_DataTable_TopLog_SinHelper()
        {
            await _dbService.ConectarAsync();
            var tabla = await _dbService.ExecuteQueryAsync(sql);

            List<Prueba> lista = [];
            foreach (DataRow fila in tabla.Rows)
            {
                int id = Convert.ToInt32(fila["Id"]);
                string? detalle = fila["Detalle"] as string;
                DateTime fecha = Convert.ToDateTime(fila["Fecha"]);
                lista.Add(new Prueba { Id = id, Detalle = detalle, Fecha = fecha });
                
            }
            await _dbService.CerrarAsync();
        }




        [Benchmark]
        public async Task ExecuteReaderAsync_DataTable_TopLog_con_SQL_Helper_V1()
        {
            await _dbService.ConectarAsync();
            var tabla = await _dbService.ExecuteQueryAsync(sql);


            // Obtener los índices de las columnas antes del ciclo
            int idIndex = tabla.Columns.IndexOf("Id");
            int detalleIndex = tabla.Columns.IndexOf("Detalle");
            int fechaIndex = tabla.Columns.IndexOf("Fecha");

            List<Prueba> lista = [];
            foreach (DataRow fila in tabla.Rows)
            {
                int id = Sqlv1.ReadNotNull<int>(fila, idIndex);
                string? detalle = Sqlv1.Read<string>(fila, detalleIndex);
                DateTime fecha = Sqlv1.ReadNotNull<DateTime>(fila, fechaIndex);
                lista.Add(new Prueba { Id = id, Detalle = detalle, Fecha = fecha });

                
            }
            await _dbService.CerrarAsync();

        }


        [Benchmark]
        public async Task ExecuteReaderAsync_DataTable_TopLog_con_SQL_Helper()
        {
            await _dbService.ConectarAsync();
            var tabla = await _dbService.ExecuteQueryAsync(sql);

            List<Prueba> lista = [];
            foreach (DataRow fila in tabla.Rows)
            {
                int id = Sql.ReadNotNullInt(fila, "Id");
                string? detalle = Sql.ReadString(fila, "Detalle");
                DateTime fecha = Sql.ReadNotNullDateTime(fila, "Fecha");
                lista.Add(new Prueba { Id = id, Detalle = detalle, Fecha = fecha });

            }
            await _dbService.CerrarAsync();
        }




        [Benchmark]
        public async Task ExecuteReaderAsync_SQLDataReader_TopLog_SinHelper()
        {
            await _dbService.ConectarAsync();
            var reader = await _dbService.ExecuteReaderAsync(sql);

            List<Prueba> lista = [];
            while (reader.Read())
            {
                int id = Convert.ToInt32(reader["Id"].ToString());
                string? detalle = reader.IsDBNull(reader.GetOrdinal("Detalle")) ? null : reader["Detalle"].ToString();

                int ordinal = reader.GetOrdinal("Fecha");
                DateTime fecha = reader.GetDateTime(ordinal);
                lista.Add(new Prueba { Id = id, Detalle = detalle, Fecha = fecha });

                
            }
            await _dbService.CerrarAsync();
        }


        [Benchmark]
        public async Task ExecuteReaderAsync_SQLDataReader_TopLog_con_SQL_Helper_V0()
        {
            await _dbService.ConectarAsync();
            var reader = await _dbService.ExecuteReaderAsync(sql);

            List<Prueba> lista = [];
            while (reader.Read())
            {

                int id = Sqlv0.ReadNotNull<int>(reader, "Id");
                string? detalle = Sqlv0.Read<String>(reader, "Detalle");
                DateTime fecha = Sqlv0.ReadNotNull<DateTime>(reader, "Fecha");
                lista.Add(new Prueba { Id = id, Detalle = detalle, Fecha = fecha });

            }
            await _dbService.CerrarAsync();
        }


        
        [Benchmark]
        public async Task ExecuteReaderAsync_SQLDataReader_TopLog_con_SQL_Helper_V1()
        {
            await _dbService.ConectarAsync();
            var reader = await _dbService.ExecuteReaderAsync(sql);

            // Obtener los índices de las columnas antes del ciclo
            int idIndex = reader.GetOrdinal("Id");
            int detalleIndex = reader.GetOrdinal("Detalle");
            int fechaIndex = reader.GetOrdinal("Fecha");

            List<Prueba> lista = [];
            while (reader.Read())
            {
                int id = Sqlv1.ReadNotNull<int>(reader, idIndex);
                string? detalle = Sqlv1.Read<string>(reader, detalleIndex);
                DateTime fecha = Sqlv1.ReadNotNull<DateTime>(reader, fechaIndex);
                lista.Add(new Prueba { Id = id, Detalle = detalle, Fecha = fecha });

            }
            await _dbService.CerrarAsync();
        }


        [Benchmark]
        public async Task ExecuteReaderAsync_SQLDataReader_TopLog_con_SQL_Helper()
        {
            await _dbService.ConectarAsync();
            var reader = await _dbService.ExecuteReaderAsync(sql);

            List<Prueba> lista = [];
            while (reader.Read())
            {
                int id = Sql.ReadNotNullInt(reader, "Id");
                string? detalle = Sql.ReadString(reader, "Detalle");
                DateTime fecha = Sql.ReadNotNullDateTime(reader, "Fecha");
                lista.Add(new Prueba { Id = id, Detalle = detalle, Fecha = fecha });

            }
            await _dbService.CerrarAsync();
        }
    }
}
