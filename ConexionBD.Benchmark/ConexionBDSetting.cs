using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexionBD.Benchmark
{
    public class ConexionBDSetting
    {
        public string Servidor { get; set; } = string.Empty;
        public string BaseDatos { get; set; } = string.Empty;
        public string Usuario { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool TrustedConnection { get; set; }
        public bool TrustServerCertificate { get; set; }
    }
}
