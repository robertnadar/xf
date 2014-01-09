// <copyright company="eXtensoft LLC" file="EventWriter.cs">
// Copyright © 2014 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class EventWriter
    {
        private static object sync = new object();
        private static volatile IEventWriter writer;

        public static IEventWriter Writer
        {
            get
            {
                if (writer == null)
                {
                    lock(sync)
                    {
                        if (writer == null)
                        {
                            try
                            {
                                writer = EventWriterLoader.Load();
                            }
                            catch (Exception)
                            {
                                
                                throw;
                            }
                        }
                    }
                }
                return writer;
            }
        }

        public static void WriteError(object errorMessage, SeverityType severity)
        {
            var worker = new BackgroundWorker();
            worker.DoWork += delegate(object sender, DoWorkEventArgs e)
            {
                Writer.WriteError(errorMessage, severity);
            };
            worker.RunWorkerAsync();
            
        }

        public static void WriteError(object errorMessage, SeverityType severity, string errorCategory)
        {
            var worker = new BackgroundWorker();
            worker.DoWork += delegate(object sender, DoWorkEventArgs e)
            {
                Writer.WriteError(errorMessage, severity, errorCategory);
            };
            worker.RunWorkerAsync();
            
        }

        public static void WriteError(object errorMessage, SeverityType severity, string errorCategory, IDictionary<string, object> properties)
        {
            var worker = new BackgroundWorker();
            worker.DoWork += delegate(object sender, DoWorkEventArgs e)
            {
                Writer.WriteError(errorCategory, severity, errorCategory, properties);
            };
            worker.RunWorkerAsync();
           
        }
        

        public static void WriteEvent(string eventMessage, IDictionary<string, object> properties)
        {
            var worker = new BackgroundWorker();
            worker.DoWork += delegate(object sender, DoWorkEventArgs e)
            {
                Writer.WriteEvent(eventMessage, properties);
            };
            worker.RunWorkerAsync();
            
        }

        public static void WriteEvent(string eventMessage, string eventCategory, IDictionary<string, object> properties)
        {
            var worker = new BackgroundWorker();
            worker.DoWork += delegate(object sender, DoWorkEventArgs e)
            {
                Writer.WriteEvent(eventMessage, eventCategory, properties);
            };
            worker.RunWorkerAsync();
            
        }

        public static void WriteEvent<T>(ModelActionOption modelAction, IDictionary<string, object> properties) where T : class, new()
        {
            var worker = new BackgroundWorker();
            worker.DoWork += delegate(object sender, DoWorkEventArgs e)
            {
                Writer.WriteEvent<T>(modelAction, properties);
            };
            worker.RunWorkerAsync();
        }

        public static void WriteEvent<T>(ModelActionOption modelAction, object modelId, IDictionary<string, object> properties) where T : class, new()
        {
            var worker = new BackgroundWorker();
            worker.DoWork += delegate(object sender, DoWorkEventArgs e)
            {
                Writer.WriteEvent<T>(modelAction, modelId, properties);
            };
            worker.RunWorkerAsync();
        }
        public static void WriteEvent<T>(ModelActionOption modelAction, T t, IDictionary<string, object> properties) where T : class, new()
        {
            var worker = new BackgroundWorker();
            worker.DoWork += delegate(object sender, DoWorkEventArgs e)
            {
                Writer.WriteEvent<T>(modelAction, t, properties);
            };
            worker.RunWorkerAsync();
        }
        public static void WriteStatus(string modelType, object modelId, string modelStatus)
        {
            var worker = new BackgroundWorker();
            worker.DoWork += delegate(object sender, DoWorkEventArgs e)
            {
                Writer.WriteStatus(modelType, modelId, modelStatus);
            };
            worker.RunWorkerAsync();
        }

        public static void WriteStatus(string modelType, object modelId, string modelStatus, IDictionary<string, object> properties)
        {
            var worker = new BackgroundWorker();
            worker.DoWork += delegate(object sender, DoWorkEventArgs e)
            {
                Writer.WriteStatus(modelType, modelId, modelStatus, properties);
            };
            worker.RunWorkerAsync();
        }

        public static void WriteStatus(string modelType, object modelId, string modelStatus, DateTimeOffset statusEffective)
        {
            var worker = new BackgroundWorker();
            worker.DoWork += delegate(object sender, DoWorkEventArgs e)
            {
                Writer.WriteStatus(modelType, modelId, modelStatus, statusEffective);
            };
            worker.RunWorkerAsync();
        }

        public static void WriteStatus(string modelType, object modelId, string modelStatus, DateTimeOffset statusEffective, IDictionary<string, object> properties)
        {
            var worker = new BackgroundWorker();
            worker.DoWork += delegate(object sender, DoWorkEventArgs e)
            {
                Writer.WriteStatus(modelType, modelId, modelStatus, statusEffective, properties);
            };
            worker.RunWorkerAsync();
        }




    }

}
