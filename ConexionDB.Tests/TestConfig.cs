using ConexionDBWinFormTest.Configuracion;
using Microsoft.Extensions.Configuration;

namespace ConexionDB.Tests
{

    public static class TestConfig
    {
        public static IConfiguration Config { get; }
        public static ConexionBDSetting Conexion { get; private set; } = default!;

        static TestConfig()
        {


            // Cargar .env (si existe)
            DotNetEnv.Env.Load(); // opcional

            Config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                .AddEnvironmentVariables()
                .Build();

            Conexion = Config.GetSection("ConexionBD").Get<ConexionBDSetting>() ??
                 throw new InvalidOperationException("No se ha encontrado la sección de configuración de la base de datos en appsettings.json.");

        }
    }
}
