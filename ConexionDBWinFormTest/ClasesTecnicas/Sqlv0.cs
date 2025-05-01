namespace ConexionDBWinFormTest.ClasesTecnicas
{
    //public static class Sqlv0
    //{
    //    public static T? Read<T>(DbDataReader DataReader, string FieldName)
    //    {
    //        int FieldIndex;
    //        try { FieldIndex = DataReader.GetOrdinal(FieldName); }
    //        catch { return default; }

    //        if (DataReader.IsDBNull(FieldIndex))
    //        {
    //            return default;
    //        }
    //        else
    //        {
    //            object readData = DataReader.GetValue(FieldIndex);
    //            if (readData is T)
    //            {
    //                return (T)readData;
    //            }
    //            else
    //            {
    //                try
    //                {
    //                    return (T)Convert.ChangeType(readData, typeof(T));
    //                }
    //                catch (InvalidCastException)
    //                {
    //                    return default;
    //                }
    //            }
    //        }
    //    }


    //    public static T ReadNotNull<T>(DbDataReader DataReader, string FieldName)
    //    {
    //        int FieldIndex;
    //        try
    //        {
    //            FieldIndex = DataReader.GetOrdinal(FieldName);
    //        }
    //        catch
    //        {
    //            throw new  KeyNotFoundException(FieldName + " no existe como columna en la base de datos");
    //        }

    //        if (DataReader.IsDBNull(FieldIndex))
    //        {
    //            throw new KeyNotFoundException(FieldName + " es nulo en la base de datos");
    //        }
    //        else
    //        {
    //            object readData = DataReader.GetValue(FieldIndex);
    //            if (readData is T)
    //            {
    //                return (T)readData;
    //            }
    //            else
    //            {
    //                try
    //                {
    //                    return (T)Convert.ChangeType(readData, typeof(T));
    //                }
    //                catch (InvalidCastException)
    //                {
    //                    throw new InvalidCastException(FieldName + " InvalidCastException");
    //                }
    //            }
    //        }
    //    }



    //}
}
