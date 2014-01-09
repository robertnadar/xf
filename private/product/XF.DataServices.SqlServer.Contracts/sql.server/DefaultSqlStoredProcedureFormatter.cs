// <copyright file="DefaultSqlStoredProcedureFormatter.cs" company="eXtensoft LLC">
// Copyright © 2014 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;
    using System.Threading.Tasks;

    public class DefaultSqlStoredProcedureFormatter : ISqlStoredProcedureFormatter
    {

        string ISqlStoredProcedureFormatter.ApplicationContext
        {
            get { return "demo"; }
        }

        IEnumerable<string> ISqlStoredProcedureFormatter.Schemas
        {
            get { return new string[]{"dbo"}; }
        }

        string ISqlStoredProcedureFormatter.ComposeFormat<T>(ModelActionOption modelActionOption)
        {
            T t = new T();
            string model = t.GetType().Name;
            string format = "{0}:{1}:";
            return String.Format(format, model, modelActionOption.ToString());
        }
    }
}
