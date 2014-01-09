// <copyright file="ISqlStoredProcedureFormatter.cs" company="eXtensoft LLC">
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

    public interface ISqlStoredProcedureFormatter 
    {
        string ApplicationContext { get; }
        IEnumerable<string> Schemas { get; }
        string ComposeFormat<T>(ModelActionOption modelActionOption) where T : class, new();
    }
}
