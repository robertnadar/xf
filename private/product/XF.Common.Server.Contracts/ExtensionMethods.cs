// <copyright company="eXtensoft LLC" file="ExtensionMethods.cs">
// Copyright © 2014 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class ExtensionMethods
    {
        public static bool FieldExists(this IDataReader reader, string fieldName)
        {
            reader.GetSchemaTable().DefaultView.RowFilter = String.Format("ColumnName= '{0}'", fieldName);
            return (reader.GetSchemaTable().DefaultView.Count > 0);
        }

        public static string[] GetFields(this IDataReader reader)
        {
            return (from datarow in reader.GetSchemaTable().AsEnumerable() select datarow["ColumnName"].ToString()).ToArray<string>();
        }
    }

}
