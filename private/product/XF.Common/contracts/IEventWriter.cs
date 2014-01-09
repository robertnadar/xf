// <copyright company="eXtensoft LLC" file="IEventWriter.cs">
// Copyright © 2014 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IEventWriter
    {
        void WriteError(object errorMessage, SeverityType severity);

        void WriteError(object errorMessage, SeverityType severity, string errorCategory);

        void WriteError(object errorMessage, SeverityType severity, string errorCategory, IDictionary<string, object> properties);

        void WriteEvent(string eventMessage, IDictionary<string, object> properties);

        void WriteEvent(string eventMessage, string eventCategory, IDictionary<string, object> properties);

        void WriteEvent<T>(ModelActionOption modelAction, IDictionary<string, object> properties) where T : class, new();

        void WriteEvent<T>(ModelActionOption modelAction, object modelId, IDictionary<string, object> properties) where T : class, new();

        void WriteEvent<T>(ModelActionOption modelAction, T t, IDictionary<string, object> properties) where T : class, new();

        void WriteStatus(string modelType, object modelId, string modelStatus);

        void WriteStatus(string modelType, object modelId, string modelStatus, IDictionary<string,object> properties);

        void WriteStatus(string modelType, object modelId, string modelStatus, DateTimeOffset statusEffective);

        void WriteStatus(string modelType, object modelId, string modelStatus, DateTimeOffset statusEffective, IDictionary<string, object> properties);




    }

}
