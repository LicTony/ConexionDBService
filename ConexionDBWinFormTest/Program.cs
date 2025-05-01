using ConexionDBTest;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.IO;
using DotNetEnv;

namespace ConexionDBWinFormTest
{
    

    internal static class Program
    {


        public static IConfiguration Config { get; private set; } = default!;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {


            // Cargar .env (si existe)
            DotNetEnv.Env.Load(); // opcional

            // Cargar configuración al inicio
            Config = new ConfigurationBuilder()
             .SetBasePath(AppContext.BaseDirectory)
             .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
             .AddEnvironmentVariables()
             .Build();

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Main());
        }
    }
}