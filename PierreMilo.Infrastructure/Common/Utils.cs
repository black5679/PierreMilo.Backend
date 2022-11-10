using System.Data;
using System.Reflection;

namespace PierreMilo.Infrastructure.Common
{
    public static class Utils
    {
        public static DataTable ToDataTable<T>(this List<T> items)
        {
            DataTable dataTable = new(typeof(T).Name);
            PropertyInfo[] array = (from x in typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                    where ValidateType(Nullable.GetUnderlyingType(x.PropertyType) ?? x.PropertyType)
                                    select x).ToArray();
            PropertyInfo[] array2 = array;
            foreach (PropertyInfo propertyInfo in array2)
            {
                dataTable.Columns.Add(propertyInfo.Name);
                dataTable.Columns[propertyInfo.Name]!.DataType = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
            }

            foreach (T item in items)
            {
                object[] array3 = new object[array.Length];
                for (int j = 0; j < array.Length; j++)
                {
                    PropertyInfo propertyInfo2 = array[j];
                    array3[j] = propertyInfo2.GetValue(item, null);
                }

                dataTable.Rows.Add(array3);
            }

            return dataTable;
        }

        private static bool ValidateType(Type type)
        {
            if (!type.IsEnum)
            {
                switch (Type.GetTypeCode(type))
                {
                    case TypeCode.DBNull:
                    case TypeCode.Boolean:
                    case TypeCode.Char:
                    case TypeCode.Byte:
                    case TypeCode.Int16:
                    case TypeCode.Int32:
                    case TypeCode.Int64:
                    case TypeCode.Double:
                    case TypeCode.Decimal:
                    case TypeCode.DateTime:
                    case TypeCode.String:
                        return true;
                }
            }

            return false;
        }
    }
}
