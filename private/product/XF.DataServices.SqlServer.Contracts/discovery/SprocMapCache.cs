// <copyright file="SprocMapCache.cs" company="eXtensoft LLC">
// Copyright © 2014 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using XF.Common;
    using XF.SqlServer;

    public class SprocMapCache
    {
        public DateTime Start { get; set; }

        //TODO build extensibility into the sproc formatter functionality by appcontext.schema
        //private IDictionary<string, int> _SprocFormatterMap =  new Dictionary<string, int>
        //{
        //    {"demo.dbo",0},
        //    {"xf.arc",1},
        //    {"other.all",2},
        //};

        private List<ISqlStoredProcedureFormatter> _SprocFormatters = new List<ISqlStoredProcedureFormatter>
        {
            {new DefaultSqlStoredProcedureFormatter()},
        };

        public static SprocMapCache Instance { get; set; }

        public Dictionary<string, List<Discovery.SqlStoredProcedure>> StoredProcedures { get; private set; }

        #region constructors

        private SprocMapCache()
        {
            Start = DateTime.Now;
        }

        static SprocMapCache()
        {
            Instance = new SprocMapCache();
            //Instance.Initialize();
        }

        #endregion constructors

        public string Get<T>(IContext context, ICriterion criterion, ModelActionOption option, SqlConnection connection) where T : class, new()
        {
            string s = String.Empty;
            if (!IsCacheLoaded(connection))
            {
                LoadCache(connection);
            }
            string hash = ComposeHash<T>("demo",criterion, option);

            List<Discovery.SqlStoredProcedure> list = StoredProcedures[connection.ConnectionString];
            if (list != null && list.Count > 0)
            {
                var found = list.FirstOrDefault(x =>
                {
                    return !String.IsNullOrEmpty(x.Hash) && x.Hash.Equals(hash, StringComparison.OrdinalIgnoreCase);
                }
                    );
                if (found != null)
                {
                    s = String.Format("[{0}].[{1}]", found.Schema, found.Name);
                }
            }
            return s;
        }

        public bool TryGetStoredProcedure<T>(IContext context, ICriterion criterion, ModelActionOption option, SqlConnection connection, out Discovery.SqlStoredProcedure storedProcedure) where T : class, new()
        {
            bool b = false;
            storedProcedure = null;
            string s = String.Empty;
            if (!IsCacheLoaded(connection))
            {
                LoadCache(connection);
            }
            string hash = ComposeHash<T>("demo",criterion, option);

            List<Discovery.SqlStoredProcedure> list = StoredProcedures[connection.ConnectionString];
            if (list != null && list.Count > 0)
            {
                Discovery.SqlStoredProcedure found = null;
                if (option == ModelActionOption.Post | option == ModelActionOption.Put)
                {
                    found = list.FirstOrDefault(x =>
                    {
                        bool isMatch = false;
                        if (!String.IsNullOrEmpty(x.Hash) && x.Hash.Length >= hash.Length)
                        {
                            isMatch = hash.Equals(x.Hash.Substring(0, hash.Length), StringComparison.OrdinalIgnoreCase);
                        }
                        return isMatch;
                    });
                }
                else
                {
                    found = list.FirstOrDefault(x =>
                    {
                        return !String.IsNullOrEmpty(x.Hash) && x.Hash.Equals(hash, StringComparison.OrdinalIgnoreCase);
                    });
                }

                if (found != null)
                {
                    storedProcedure = found;
                    b = true;
                }
            }
            return b;
        }

        private string ComposeHash<T>(string applicationContext, ICriterion criteria, ModelActionOption option) where T : class, new()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(_SprocFormatters[0].ComposeFormat<T>(option));
            if (criteria != null && criteria.Items != null)
            {
                List<TypedItem> list = (criteria.Items.Where(x => !x.Key.Equals("UserIdentity", StringComparison.OrdinalIgnoreCase)
                    & !x.Key.Equals("action.modifier", StringComparison.OrdinalIgnoreCase)).OrderBy(x => x.Key)).ToList();

                if (list != null && list.Count > 0)
                {
                    foreach (var item in list)
                    {
                        sb.Append(String.Format("@{0}", item.Key));
                    }
                }
            }
            return sb.ToString();
        }

        private bool IsCacheLoaded(SqlConnection connection)
        {
            if (StoredProcedures == null || !StoredProcedures.ContainsKey(connection.ConnectionString))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void LoadCache(SqlConnection connection)
        {
            if (StoredProcedures == null)
            {
                StoredProcedures = new Dictionary<string, List<Discovery.SqlStoredProcedure>>();
            }

            string sql = Resources.Discovery_StoredProcedures;
            List<Discovery.SqlParameter> allstoredprocedures = new List<Discovery.SqlParameter>();

            using (DbCommand cmd = connection.CreateCommand())
            {
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;
                using (IDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        allstoredprocedures.Add(new Discovery.SqlParameter(reader));
                    }
                }
            }

            Dictionary<string, List<Discovery.SqlParameter>> parameters = new Dictionary<string, List<Discovery.SqlParameter>>();
            foreach (var item in allstoredprocedures)
            {
                if (!parameters.ContainsKey(item.StoredProcedureName))
                {
                    parameters.Add(item.StoredProcedureName, new List<Discovery.SqlParameter>());
                }
                parameters[item.StoredProcedureName].Add(item);
            }

            List<Discovery.SqlStoredProcedure> standardcontractstoredprocedures = new List<Discovery.SqlStoredProcedure>();
            foreach (var item in parameters)
            {
                Discovery.SqlStoredProcedure sproc = new Discovery.SqlStoredProcedure(item.Value);
                if (sproc.IsModelAction)
                {
                    standardcontractstoredprocedures.Add(sproc);
                }
            }
            StoredProcedures.Add(connection.ConnectionString, standardcontractstoredprocedures);
        }
    }
}