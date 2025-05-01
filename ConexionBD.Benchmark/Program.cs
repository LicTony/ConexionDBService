// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using Microsoft.Extensions.Configuration;


namespace ConexionBD.Benchmark
{
    internal static class Program    {
      
        public static IConfiguration Config { get; private set; } = default!;
        public static ConexionBDSetting Conexion { get; private set; } = default!;

        private static void Main()
        {
            Console.WriteLine("Hello,  BenchmarkRunner Test!");
           
            // Cargar .env (si existe)
            DotNetEnv.Env.Load(); // opcional

            // Cargar configuración al inicio
            Config = new ConfigurationBuilder()
             .SetBasePath(AppContext.BaseDirectory)
             .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
             .AddEnvironmentVariables()
             .Build();


            Conexion = Config.GetSection("ConexionBD").Get<ConexionBDSetting>() ??
              throw new InvalidOperationException("No se ha encontrado la sección de configuración de la base de datos en appsettings.json.");

            BenchmarkRunner.Run<ComparacionFunciones>();
        }
    }

}


