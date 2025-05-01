using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexionDBService.ClasesTecnicas
{
    public static class Sql
    {
        public static int ReadNotNullInt(DataRow fila, string nombreCampo)
        {
            return Convert.ToInt32(fila[nombreCampo]);
        }


        public static int ReadNotNullInt(DbDataReader fila, string nombreCampo)
        {
            return Convert.ToInt32(fila[nombreCampo]);
        }

        public static int? ReadInt(DataRow fila, string nombreCampo)
        {
            if (fila.IsNull(nombreCampo))
                return null;
            else
                return Convert.ToInt32(fila[nombreCampo]);
        }


        public static int? ReadInt(DbDataReader fila, string nombreCampo)
        {
            if (fila.IsDBNull(nombreCampo))
                return null;
            else
                return Convert.ToInt32(fila[nombreCampo]);
        }


        public static string? ReadNotNullString(DataRow fila, string nombreCampo)
        {
            return fila[nombreCampo] as string;
        }


        public static string? ReadNotNullString(DbDataReader fila, string nombreCampo)
        {
            return fila[nombreCampo] as string;
        }





















        public static string? ReadString(DbDataReader fila, string nombreCampo)
        {
            if (fila.IsDBNull(nombreCampo))
                return null;
            else
                return fila[nombreCampo] as string;

        }

        public static string? ReadString(DataRow fila, string nombreCampo)
        {
            if (fila.IsNull(nombreCampo))
                return null;
            else
                return fila[nombreCampo] as string;

        }


        public static DateTime? ReadDateTime(DbDataReader fila, string nombreCampo)
        {
            if (fila.IsDBNull(nombreCampo))
                return null;
            else
                return Convert.ToDateTime(fila[nombreCampo]);
        }

        public static DateTime? ReadDateTime(DataRow fila, string nombreCampo)
        {
            if (fila.IsNull(nombreCampo))
                return null;
            else
                return Convert.ToDateTime(fila[nombreCampo]);
        }


        public static DateTime ReadNotNullDateTime(DbDataReader fila, string nombreCampo)
        {
            return Convert.ToDateTime(fila[nombreCampo]);
        }

        public static DateTime ReadNotNullDateTime(DataRow fila, string nombreCampo)
        {
            return Convert.ToDateTime(fila[nombreCampo]);
        }
    }
}
