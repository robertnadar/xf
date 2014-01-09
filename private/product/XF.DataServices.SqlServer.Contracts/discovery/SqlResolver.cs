// <copyright file="SqlResolver.cs" company="eXtensoft LLC">
// Copyright © 2014 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.SqlClient;

    public static class SqlResolver
    {
        public static SqlConnection GetConnection(string key = "")
        {
            string s = GetConnectionString(key);
            return new SqlConnection(s);
        }

        public static string GetConnectionString(string key)
        {
            string s = (!String.IsNullOrEmpty(key)) ? key : "default";
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[s];
            return (settings != null) ? settings.ConnectionString : String.Empty;
        }

        public static SqlConnection ResolveConnection<T>(T t)
        {
            return GetConnection("demo");
        }

        public static SqlConnection ResolveConnection<T>(ICriterion criterion)
        {
            return GetConnection("demo");
        }

        public static SqlConnection ResolveConnection<T>()
        {
            return GetConnection("demo");
        }

        public static SqlCommand ResolveCreateCommand<T>(SqlConnection cn, string schema, T t) where T : class, new()
        {
            return ResolveCreateCommand<T>(cn, schema, t, null);
        }

        public static SqlCommand ResolveCreateCommand<T>(SqlConnection cn, string schema, T t, List<DataMap> dataMaps) where T : class, new()
        {
            return SqlServerConfigManager.Instance.ResolveCommand<T>(null, ModelActionOption.Post, cn, schema, t, dataMaps);
        }

        public static SqlCommand ResolveReadCommand<T>(SqlConnection cn, string schema, ICriterion criterion) where T : class, new()
        {
            return SqlServerConfigManager.Instance.ResolveCommand<T>(null, ModelActionOption.Get, cn, schema, criterion);
            //string model = Activator.CreateInstance<T>().GetType().Name;
            //string sprocname = String.Format("[{0}].[{1}_{2}]", schema, model, ModelActionOption.Read.ToString());
            //return CreateCommand(cn, sprocname);
        }

        public static SqlCommand ResolveUpdateCommand<T>(SqlConnection cn, string schema, T t) where T : class, new()
        {
            return SqlServerConfigManager.Instance.ResolveCommand<T>(null, ModelActionOption.Put, cn, schema,t,null);
        }

        public static SqlCommand ResolveDeleteCommand<T>(SqlConnection cn, string schema, ICriterion criterion) where T : class, new()
        {
            return SqlServerConfigManager.Instance.ResolveCommand<T>(null, ModelActionOption.Delete, cn, schema, criterion);
        }

        public static SqlCommand ResolveReadListCommand<T>(SqlConnection cn, string schema, ICriterion criterion) where T : class, new()
        {
            return SqlServerConfigManager.Instance.ResolveCommand<T>(null, ModelActionOption.GetAll, cn, schema, criterion);
        }

        public static SqlCommand ResolveReadListDisplayCommand<T>(SqlConnection cn, string schema, ICriterion criterion) where T : class, new()
        {
            return SqlServerConfigManager.Instance.ResolveCommand<T>(null, ModelActionOption.GetAllDisplay, cn, schema, criterion);
            //string model = Activator.CreateInstance<T>().GetType().Name;
            //string sprocname = String.Format("[{0}].[{1}s_{2}]", schema, model, ModelActionOption.ReadListDisplay.ToString());
            //return CreateCommand(cn, sprocname);
        }

        private static SqlCommand CreateCommand(SqlConnection cn, string storedProcedureName)
        {
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = storedProcedureName;
            return cmd;
        }
    }
}
