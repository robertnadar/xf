// <copyright file="Exceptions.cs" company="eXtensoft LLC">
// Copyright © 2014 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class Exceptions
    {
        public static string ComposeDbConnectionCreationError<T>(ModelActionOption modelActionOption, T t, IContext context, string connectionStringKey, bool includeDetail = false) where T : class, new()
        {
            string message = String.Empty;
            if (eXtensibleConfig.IsSeverityAtLeast(TraceEventTypeOption.Verbose) || includeDetail)
            {
                StringBuilder sb = new StringBuilder();
                if (t != null)
                {
                    sb.AppendLine(String.Format(ErrorMessages.ModelFormat, t.ToString()));
                }
                sb.AppendLine(String.Format(ErrorMessages.IContextFormat, context.ToString()));
                message = String.Format(ErrorMessages.DbConnectionCreationFormatVerbose, GetModelType<T>().FullName, modelActionOption,connectionStringKey, sb.ToString());
            }
            else
            {
                message = String.Format(ErrorMessages.DbConnectionCreationFormatVerbose, GetModelType<T>().FullName, modelActionOption,connectionStringKey);
            }
            return message;
        }

        public static string ComposeDbConnectionStringKeyResolutionError<T>(ModelActionOption modelActionOption, T t, IContext context, bool includeDetail = false) where T : class, new()
        {
            string message = String.Empty;
            if (eXtensibleConfig.IsSeverityAtLeast(TraceEventTypeOption.Verbose) || includeDetail)
            {
                StringBuilder sb = new StringBuilder();
                if (t != null)
                {
                    sb.AppendLine(String.Format(ErrorMessages.ModelFormat, t.ToString()));
                }
                sb.AppendLine(String.Format(ErrorMessages.IContextFormat, context.ToString()));
                message = String.Format(ErrorMessages.DbConnectionStringResolutionVerbose, GetModelType<T>().FullName, modelActionOption,sb.ToString());
            }
            else
            {
                message = String.Format(ErrorMessages.DbConnectionStringResolution, GetModelType<T>().FullName, modelActionOption);
            }
            return message;
        }

        public static string ComposeImplementorInstantiationError<T>(ModelActionOption modelActionOption, T t, IContext context, string implementor, bool includeDetail = false) where T : class, new()
        {
            string message = String.Empty;
            if (eXtensibleConfig.IsSeverityAtLeast(TraceEventTypeOption.Verbose) || includeDetail)
            {
                StringBuilder sb = new StringBuilder();
                if (t != null)
                {
                    sb.AppendLine(String.Format(ErrorMessages.ModelFormat, t.ToString()));
                }
                sb.AppendLine(String.Format(ErrorMessages.IContextFormat, context.ToString()));
                message = String.Format(ErrorMessages.ModelDataGatewayImplementationInstantiationVerbose, GetModelType<T>().FullName, modelActionOption,implementor, sb.ToString());
            }
            else
            {
                message = String.Format(ErrorMessages.ModelDataGatewayImplementationInstantiation, GetModelType<T>().FullName, modelActionOption,implementor);
            }           
            return message;
        }

        public static string ComposeImplementorResolutionError<T>(ModelActionOption modelActionOption, T t, IContext context, bool includeDetail = false) where T : class, new()
        {
            string message = String.Empty;
            StringBuilder sb = new StringBuilder();
            if (t != null)
            {
                sb.AppendLine(String.Format(ErrorMessages.ModelFormat, t.ToString()));
            }
            sb.AppendLine(String.Format(ErrorMessages.IContextFormat, context.ToString()));
            message = String.Format(ErrorMessages.NullModelDataGatewayImplementation, GetModelType<T>().FullName, modelActionOption);
            return message;
        }

        public static string ComposeNullSqlCommand<T>(ModelActionOption modelActionOption, T t, ICriterion criterion, IContext context, string implementor, bool includeDetail = false) where T : class, new()
        {
            string message = String.Empty;
            if (eXtensibleConfig.IsSeverityAtLeast(TraceEventTypeOption.Verbose) || includeDetail)
            {
                StringBuilder sb = new StringBuilder();
                if (t != null)
                {
                    sb.AppendLine(String.Format(ErrorMessages.ModelFormat, t.ToString()));
                }
                if (criterion != null)
                {
                    sb.AppendLine(String.Format(ErrorMessages.ICriterionFormat, criterion.ToString()));
                }
                sb.AppendLine(String.Format(ErrorMessages.IContextFormat, context.ToString()));
                message = String.Format(ErrorMessages.NullSqlCommandFormatVerbose, GetModelType<T>().FullName, modelActionOption, implementor, sb.ToString());
            }
            else
            {
                message = String.Format(ErrorMessages.NullSqlCommandFormat, GetModelType<T>().FullName, modelActionOption, implementor);
            }
            return message;
        }

        public static string ComposeSqlException<T>(ModelActionOption modelActionOption, Exception ex, T t, ICriterion criterion, IContext context, string implementor, string dataSource = "", bool includeDetail = false) where T : class, new()
        {

            string message = String.Empty;
            if (eXtensibleConfig.IsSeverityAtLeast(TraceEventTypeOption.Verbose) || includeDetail)
            {
                StringBuilder sb = new StringBuilder();
                string s = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
                sb.AppendLine(String.Format(ErrorMessages.ExceptionFormat, s));
                if (t != null)
                {
                    sb.AppendLine(String.Format(ErrorMessages.ModelFormat, t.ToString()));
                }
                if (!String.IsNullOrWhiteSpace(dataSource))
                {
                    sb.AppendLine(dataSource);
                }
                if (criterion != null)
                {
                    sb.AppendLine(String.Format(ErrorMessages.ICriterionFormat, criterion.ToString()));
                }
                sb.AppendLine(String.Format(ErrorMessages.IContextFormat, context.ToString()));
                message = String.Format(ErrorMessages.SqlExceptionFormatVerbose, GetModelType<T>().FullName, modelActionOption, implementor, sb.ToString());
            }
            else
            {
                message = String.Format(ErrorMessages.SqlExceptionFormat, GetModelType<T>().FullName, modelActionOption, implementor);
            }
            return message;
        }

        public static string ComposeDatastoreException<T>(ModelActionOption modelActionOption, string ex, T t, ICriterion criterion, IContext context, string implementor, string datastoreType = "Datastore", string dataSource = "", bool includeDetail = false) where T : class, new()
        {

            string message = String.Empty;
            if (eXtensibleConfig.IsSeverityAtLeast(TraceEventTypeOption.Verbose) || includeDetail)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(String.Format(ErrorMessages.ExceptionFormat, ex));
                if (t != null)
                {
                    sb.AppendLine(String.Format(ErrorMessages.ModelFormat, t.ToString()));
                }
                if (!String.IsNullOrWhiteSpace(dataSource))
                {
                    sb.AppendLine(dataSource);
                }
                if (criterion != null)
                {
                    sb.AppendLine(String.Format(ErrorMessages.ICriterionFormat, criterion.ToString()));
                }
                sb.AppendLine(String.Format(ErrorMessages.IContextFormat, context.ToString()));
                message = String.Format(ErrorMessages.DatastoreExceptionFormatVerbose, GetModelType<T>().FullName, modelActionOption, implementor, datastoreType, sb.ToString());
            }
            else
            {
                message = String.Format(ErrorMessages.SqlExceptionFormat, GetModelType<T>().FullName, modelActionOption, implementor, datastoreType);
            }
            return message;
        }


        public static string ComposeBorrowReaderError<T>(ModelActionOption modelActionOption, Exception ex, T t, ICriterion criterion, IContext context, string implementor, bool includeDetail = false) where T : class, new()
        {
            string message = String.Empty;
            if (eXtensibleConfig.IsSeverityAtLeast(TraceEventTypeOption.Verbose) || includeDetail)
            {
                StringBuilder sb = new StringBuilder();
                string s = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
                sb.AppendLine(String.Format(ErrorMessages.ExceptionFormat, s));
                if (t != null)
                {
                    sb.AppendLine(String.Format(ErrorMessages.ModelFormat, t.ToString()));
                }
                if (criterion != null)
                {
                    sb.AppendLine(String.Format(ErrorMessages.ICriterionFormat, criterion.ToString()));
                }
                sb.AppendLine(String.Format(ErrorMessages.IContextFormat, context.ToString()));
                message = String.Format(ErrorMessages.BorrowReaderExceptionFormatVerbose, GetModelType<T>().FullName, modelActionOption, implementor, sb.ToString());
            }
            else
            {
                message = String.Format(ErrorMessages.BorrowReaderExceptionFormat, GetModelType<T>().FullName, modelActionOption, implementor);
            }
            return message;
        }

        public static string ComposeGeneralExceptionError<T>(ModelActionOption modelActionOption, Exception ex, T t, ICriterion criterion, IContext context, string implementor, bool includeDetail = false) where T : class, new()
        {
            string message = String.Empty;
            if (eXtensibleConfig.IsSeverityAtLeast(TraceEventTypeOption.Verbose) || includeDetail)
            {
                StringBuilder sb = new StringBuilder();
                string s = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
                sb.AppendLine(String.Format(ErrorMessages.ExceptionFormat, s));
                if (t != null)
                {
                    sb.AppendLine(String.Format(ErrorMessages.ModelFormat, t.ToString()));
                }
                if (criterion != null)
                {
                    sb.AppendLine(String.Format(ErrorMessages.ICriterionFormat, criterion.ToString()));
                }
                sb.AppendLine(String.Format(ErrorMessages.IContextFormat, context.ToString()));
                message = String.Format(ErrorMessages.GeneralExceptionInFormatVerbose, GetModelType<T>().FullName, modelActionOption, implementor, sb.ToString());
            }
            else
            {
                message = String.Format(ErrorMessages.GeneralExceptionInFormat, GetModelType<T>().FullName, modelActionOption, implementor);
            }
            return message;
        }

        public static string ComposeGeneralExceptionError<T>(ModelActionOption modelActionOption, Exception ex, T t, ICriterion criterion, IContext context, bool includeDetail = false) where T : class, new()
        {
            string message = String.Empty;
            if (eXtensibleConfig.IsSeverityAtLeast(TraceEventTypeOption.Verbose) || includeDetail)
            {
                StringBuilder sb = new StringBuilder();
                string s = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
                sb.AppendLine(String.Format(ErrorMessages.ExceptionFormat, s));
                if (t != null)
                {
                    sb.AppendLine(String.Format(ErrorMessages.ModelFormat, t.ToString()));
                }
                if (criterion != null)
                {
                    sb.AppendLine(String.Format(ErrorMessages.ICriterionFormat, criterion.ToString()));
                }
                sb.AppendLine(String.Format(ErrorMessages.IContextFormat, context.ToString()));
                message = String.Format(ErrorMessages.GeneralExceptionFormatVerbose, GetModelType<T>().FullName, modelActionOption,  sb.ToString());
            }
            else
            {
                message = String.Format(ErrorMessages.GeneralExceptionFormat, GetModelType<T>().FullName, modelActionOption);
            }
            return message;
        }

        public static string ComposeResourceNotFoundError<T>(ModelActionOption modelActionOption, T t, ICriterion criterion, IContext context, bool includeDetail = false) where T : class, new()
        {
            string message = String.Empty;
            if (eXtensibleConfig.IsSeverityAtLeast(TraceEventTypeOption.Verbose) || includeDetail)
            {
                StringBuilder sb = new StringBuilder();
                if (t != null)
                {
                    sb.AppendLine(String.Format(ErrorMessages.ModelFormat, t.ToString()));
                }
                if (criterion != null)
                {
                    sb.AppendLine(String.Format(ErrorMessages.ICriterionFormat, criterion.ToString()));
                }
                sb.AppendLine(String.Format(ErrorMessages.IContextFormat, context.ToString()));
                message = String.Format(ErrorMessages.ResourceNotFoundFormatVerbose, GetModelType<T>().FullName, modelActionOption, sb.ToString());
            }
            else
            {
                message = String.Format(ErrorMessages.ResourceNotFoundFormat, GetModelType<T>().FullName, modelActionOption);
            }
            return message;
        }

        private static Type GetModelType<T>() where T : class, new()
        {
            return Activator.CreateInstance<T>().GetType();
        }

    }
}
