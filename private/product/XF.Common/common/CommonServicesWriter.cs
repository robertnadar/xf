// <copyright company="eXtensoft LLC" file="CommonServicesWriter.cs">
// Copyright © 2014 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class CommonServicesWriter : IEventWriter
    {
        #region interface implementations

        void IEventWriter.WriteError(object errorMessage, SeverityType severity)
        {
            WriteError(errorMessage, severity);
        }

        void IEventWriter.WriteError(object errorMessage, SeverityType severity, string errorCategory)
        {
            WriteError(errorMessage, severity, errorCategory);
        }

        void IEventWriter.WriteError(object errorMessage, SeverityType severity, string errorCategory, IDictionary<string, object> properties)
        {
            WriteError(errorMessage, severity, errorCategory, properties);
        }

        void IEventWriter.WriteEvent(string eventMessage, IDictionary<string, object> properties)
        {
            WriteEvent(eventMessage, properties);
        }

        void IEventWriter.WriteEvent(string eventMessage, string eventCategory, IDictionary<string, object> properties)
        {
            WriteEvent(eventMessage, eventCategory, properties);
        }

        void IEventWriter.WriteEvent<T>(ModelActionOption modelAction, IDictionary<string, object> properties)
        {
            WriteEvent<T>(modelAction, properties);
        }

        void IEventWriter.WriteEvent<T>(ModelActionOption modelAction, object modelId, IDictionary<string, object> properties)
        {
            WriteEvent<T>(modelAction, modelId, properties);
        }

        void IEventWriter.WriteEvent<T>(ModelActionOption modelAction, T t, IDictionary<string, object> properties)
        {
            WriteEvent<T>(modelAction, t, properties);
        }

        void IEventWriter.WriteStatus(string modelType, object modelId, string modelStatus)
        {
            WriteStatus(modelType, modelId, modelStatus);
        }

        void IEventWriter.WriteStatus(string modelType, object modelId, string modelStatus, IDictionary<string, object> properties)
        {
            WriteStatus(modelType, modelId, modelStatus, properties);
        }

        void IEventWriter.WriteStatus(string modelType, object modelId, string modelStatus, DateTimeOffset statusEffective)
        {
            WriteStatus(modelType, modelId, modelStatus, statusEffective);
        }

        void IEventWriter.WriteStatus(string modelType, object modelId, string modelStatus, DateTimeOffset statusEffective, IDictionary<string, object> properties)
        {
            WriteStatus(modelType, modelId, modelStatus,statusEffective,properties);
        }
        #endregion

        #region local implementations

        private void WriteError(object errorMessage, SeverityType severity)
        {
            WriteError(errorMessage, severity, eXtensibleConfig.DefaultLoggingCategory);
        }

        private void WriteError(object errorMessage, SeverityType severity, string errorCategory)
        {
            var properties = eXtensibleConfig.GetProperties();
            WriteError(errorMessage, severity, errorCategory, properties);
        }

        private void WriteError(object errorMessage, SeverityType severity, string errorCategory, IDictionary<string, object> properties)
        {
            ErrorEntry entry = new ErrorEntry();
            entry.Severity = severity;
            entry.Category = errorCategory;
            entry.Properties = properties;

            Write(entry);
        }

        private void WriteEvent(string eventMessage, IDictionary<string, object> properties)
        {
            WriteEvent(eventMessage, eXtensibleConfig.DefaultLoggingCategory, properties);
        }

        private void WriteEvent(string eventMessage, string eventCategory, IDictionary<string, object> properties)
        {
            EventEntry entry = new EventEntry();
            entry.Category = eventCategory;
            entry.Properties = properties;
            Write(entry);
        }

        private void WriteEvent<T>(ModelActionOption modelAction, IDictionary<string, object> properties)
        {
            if (!properties.ContainsKey("model"))
            {
                string modelname = GetModelType<T>().FullName;
                properties.Add("model",modelname);
            }
            if (!properties.ContainsKey("modelAction"))
            {
                properties.Add("modelAction", modelAction);
            }
            EventEntry entry = new EventEntry();
            entry.Category = eXtensibleConfig.DefaultLoggingCategory;
            entry.Properties = properties;
            Write(entry);
        }

        private void WriteEvent<T>(ModelActionOption modelAction, object modelId, IDictionary<string, object> properties)
        {
            if (!properties.ContainsKey("model"))
            {
                string modelname = GetModelType<T>().FullName;
                properties.Add("model", modelname);
            }
            if (!properties.ContainsKey("modelAction"))
            {
                properties.Add("modelAction", modelAction);
            }
            if (!properties.ContainsKey("modelId"))
            {
                properties.Add("modelId", modelId.ToString());
            }
            EventEntry entry = new EventEntry();
            entry.Category = eXtensibleConfig.DefaultLoggingCategory;
            entry.Properties = properties;
            Write(entry);
        }

        private void WriteEvent<T>(ModelActionOption modelAction, T t, IDictionary<string, object> properties)
        {
            if (!properties.ContainsKey("model"))
            {
                string modelname = GetModelType<T>().FullName;
                properties.Add("model", modelname);
            }
            if (!properties.ContainsKey("modelAction"))
            {
                properties.Add("modelAction", modelAction);
            }
            if (!properties.ContainsKey("modelInstance"))
            {
                properties.Add("modelInstance", t.ToString());
            }
            EventEntry entry = new EventEntry();
            entry.Category = eXtensibleConfig.DefaultLoggingCategory;
            entry.Properties = properties;
            Write(entry);
        }

        private void WriteStatus(string modelType, object modelId, string modelStatus)
        {
            var properties = eXtensibleConfig.GetProperties();
            WriteStatus(modelType, modelId, modelStatus, properties);
        }

        private void WriteStatus(string modelType, object modelId, string modelStatus, IDictionary<string, object> properties)
        {
            WriteStatus(modelType, modelId, modelStatus, DateTimeOffset.Now, properties);
        }

        private void WriteStatus(string modelType, object modelId, string modelStatus, DateTimeOffset statusEffective)
        {
            var properties = eXtensibleConfig.GetProperties();
            WriteStatus(modelType, modelId, modelStatus, statusEffective, properties);
        }

        private void WriteStatus(string modelType, object modelId, string modelStatus, DateTimeOffset statusEffective, IDictionary<string, object> properties)
        {
            if (!properties.ContainsKey("modelType"))
            {
                properties.Add("modelType", modelType);
            }
            if (!properties.ContainsKey("modelId"))
            {
                properties.Add("modelId", modelId.ToString());
            }
            if (!properties.ContainsKey("modelStatus"))
            {
                properties.Add("modelStatus", modelStatus);
            }
            StatusEntry entry = new StatusEntry();
            entry.Category = eXtensibleConfig.DefaultLoggingCategory;
            entry.Properties = properties;
            Write(entry);
        }

        #endregion


        private void Write(Entry entry)
        {

        }

        private static Type GetModelType<T>()
        {
            return Activator.CreateInstance<T>().GetType();
        }

    }

}
