
using ConexionDBService.ClasesTecnicas;
using ConexionDBService.Services;
using ConexionDBWinFormTest;
using ConexionDBWinFormTest.Configuracion;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace ConexionDBTest
{
    public partial class Main : Form
    {
        private readonly DbService _dbService;
        private readonly string _connectionString;


        public Main()
        {
            InitializeComponent();

            var config = Program.Config;
            var conexion = config.GetSection("ConexionBD").Get<ConexionBDSetting>() ?? 
                throw new ArgumentNullException(_connectionString, "No se ha encontrado la sección de configuración de la base de datos en appsettings.json.");

            _connectionString = ConexionDBHelper.ArmarCadenaConexion(
                     conexion!.Servidor,
                     conexion!.BaseDatos,
                     conexion!.Usuario,
                     conexion!.Password,
                     conexion!.TrustedConnection,
                     conexion!.TrustServerCertificate
                 );


            _dbService = new DbService(_connectionString, new RetryOptions()
            {
                NumberOfTries = conexion!.NumberOfTries,
                DeltaTime = conexion!.DeltaTime,
                MaxTimeInterval = conexion!.MaxTimeInterval,
            });


        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            var resultado = await _dbService.ConectarAsync();

            if (!resultado)
                MessageBox.Show(_dbService.UltimoMensaje);
        }



        private async void CmdPrueba001_Click(object sender, EventArgs e)
        {
            try
            {

                string sql = "INSERT INTO Logs (Detalle, Fecha) VALUES (@detalle, @fecha)";
                var parametros = new Dictionary<string, object?>
                {
                    { "@detalle", "Prueba de inserción" },
                    { "@fecha", DateTime.Now }
                };

                int filasAfectadas = await _dbService!.ExecuteNonQueryAsync(sql, parametros);

                MessageBox.Show($"Filas a fectadas {filasAfectadas}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private async void CmdPrueba002_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "INSERT INTO Logs (Detalle, Fecha, NuevoCampo) VALUES (@detalle, @fecha,  @nuevoCampo)";
                var parametros = new Dictionary<string, object?>
                {
                    { "@detalle", "Prueba de inserción" },
                    { "@fecha", DateTime.Now },
                    { "@nuevoCampo", "No existe" },
                };

                int filasAfectadas = await _dbService!.ExecuteNonQueryAsync(sql, parametros);

                MessageBox.Show($"Filas a fectadas {filasAfectadas}");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private async void CmdPrueba003_Click(object sender, EventArgs e)
        {

            try
            {
                string sql = "SELECT COUNT(*) FROM Logs";
                int cantidad = await _dbService.ExecuteScalarAsync<int>(sql);
                MessageBox.Show($"Cantidad de registros de la tabla dbo.Logs {cantidad}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void CmdUltimoMensajeDeError_Click(object sender, EventArgs e)
        {
            MessageBox.Show(_dbService.UltimoMensaje);


        }

        private async void CmdPrueba004_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "SELECT top 2 * FROM Logs order by Id desc";
                var tabla = await _dbService.ExecuteQueryAsync(sql);

                foreach (DataRow fila in tabla.Rows)
                {
                    int id = Convert.ToInt32(fila["Id"]);
                    string? detalle = fila["Detalle"] as string;
                    DateTime fecha = Convert.ToDateTime(fila["Fecha"]);

                    Console.WriteLine($"Id: {id}, Detalle: {detalle}, Fecha: {fecha}");
                    MessageBox.Show($"Id: {id}, Detalle: {detalle}, Fecha: {fecha}");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private async void CmdPrueba005_Click(object sender, EventArgs e)
        {
            try
            {
                Dictionary<string, object?> parametros;
                string sql = "";

                await _dbService.BeginTransactionAsync();

                sql = "SELECT COUNT(*) FROM Logs";
                int cantidad = await _dbService.ExecuteScalarAsync<int>(sql);
                MessageBox.Show($"Cantidad de registros de la tabla dbo.Logs despues de Begin tran:{cantidad}");

                sql = "INSERT INTO Logs (Detalle, Fecha) VALUES (@detalle, @fecha)";

                parametros = new Dictionary<string, object?>
                {
                    { "@detalle", "Prueba de inserción" },
                    { "@fecha", DateTime.Now }
                };
                await _dbService!.ExecuteNonQueryAsync(sql, parametros);


                parametros = new Dictionary<string, object?>
                {
                    { "@detalle", "Prueba de inserción" },
                    { "@fecha", DateTime.Now }
                };
                await _dbService!.ExecuteNonQueryAsync(sql, parametros);


                parametros = new Dictionary<string, object?>
                {
                    { "@detalle", "Prueba de inserción" },
                    { "@fecha", DateTime.Now }
                };
                await _dbService!.ExecuteNonQueryAsync(sql, parametros);

                sql = "SELECT COUNT(*) FROM Logs";
                cantidad = await _dbService.ExecuteScalarAsync<int>(sql);
                MessageBox.Show($"Cantidad de registros de la tabla dbo.Logs {cantidad} despues de 3 inserts");


                await _dbService.RollbackTransactionAsync();
                sql = "SELECT COUNT(*) FROM Logs";
                cantidad = await _dbService.ExecuteScalarAsync<int>(sql);
                MessageBox.Show($"Cantidad de registros de la tabla dbo.Logs {cantidad} despues del rollback tran");


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                if (_dbService.HayUnaTransacionAbierta())
                    await _dbService.RollbackTransactionAsync();

            }
        }

        private async void CmdPrueba006_Click(object sender, EventArgs e)
        {
            try
            {

                var parametros = new Dictionary<string, object?>
                {
                    { "@detalle", "Prueba SP" },
                    { "@fecha", DateTime.Now }
                };

                int filasAfectadas = await _dbService.ExecuteStoredProcedureAsync("sp_InsertarLog", parametros);

                MessageBox.Show($"Filas a fectadas {filasAfectadas}");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void CmdPrueba007_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "SELECT top 2 * FROM Logs order by Id desc";
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
                    MessageBox.Show($"Id: {id}, Detalle: {detalle}, Fecha: {fecha}");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private async void CmdPrueba008_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "SELECT top 2 * FROM Logs order by Id desc";
                var tabla = await _dbService.ExecuteQueryAsync(sql);

              
                foreach (DataRow fila in tabla.Rows)
                {
                    int id = Sql.ReadNotNullInt(fila, "Id");
                    string? detalle = Sql.ReadString(fila, "Detalle");
                    DateTime fecha = Sql.ReadNotNullDateTime(fila, "Fecha");

                    Console.WriteLine($"Id: {id}, Detalle: {detalle}, Fecha: {fecha}");
                    MessageBox.Show($"Id: {id}, Detalle: {detalle}, Fecha: {fecha}");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
