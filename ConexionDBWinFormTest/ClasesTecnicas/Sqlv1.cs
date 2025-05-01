namespace ConexionDBWinFormTest.ClasesTecnicas
{
    //public static class Sqlv1
    //{
    //    // Almacenamos los índices de las columnas al principio
    //    public static T? Read<T>(DbDataReader dataReader, int columnIndex)
    //    {
    //        if (dataReader.IsDBNull(columnIndex))
    //            return default;

    //        object value = dataReader.GetValue(columnIndex);

    //        if (value is T typedValue)
    //            return typedValue;

    //        // Solo convertir cuando sea necesario
    //        return (T?)Convert.ChangeType(value, typeof(T));
    //    }





    //    // Para leer de un DataRow
    //    public static T? Read<T>(DataRow row, int columnIndex)
    //    {
    //        if (row.IsNull(columnIndex))
    //            return default;

    //        object value = row[columnIndex];

    //        if (value is T typedValue)
    //            return typedValue;

    //        return (T?)Convert.ChangeType(value, typeof(T));
    //    }

    //    public static T ReadNotNull<T>(DataRow row, int columnIndex)
    //    {
    //        if (row.IsNull(columnIndex))
    //            throw new InvalidOperationException($"El valor de la columna en el índice {columnIndex} es nulo.");

    //        object value = row[columnIndex];

    //        if (value is T typedValue)
    //            return typedValue;

    //        return (T)Convert.ChangeType(value, typeof(T));
    //    }


    //    public static T ReadNotNull<T>(DbDataReader dataReader, int columnIndex)
    //    {
    //        if (dataReader.IsDBNull(columnIndex))
    //            throw new InvalidOperationException($"El valor de la columna en el índice {columnIndex} es nulo.");

    //        object value = dataReader.GetValue(columnIndex);

    //        if (value is T typedValue)
    //            return typedValue;

    //        // Solo convertir cuando sea necesario
    //        return (T)Convert.ChangeType(value, typeof(T));
    //    }



    //}
}
