// <copyright file="SqlServerModelDataGateway`1.cs" company="eXtensoft LLC">
// Copyright © 2014 All Right Reserved
// </copyright>

namespace XF.DataServices
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Data.SqlClient;
    using System.Reflection;
    using System.Text;
    using XF.Common;

    [InheritedExport(typeof(ITypeMap))]
    public abstract class SqlServerModelDataGateway<T> : IModelDataGateway<T>, ISqlCommandContext<T>, IModelDataGatewayInitializeable where T : class, new()
    {
        private const string category = "dataservice";

        private ICache _Cache;
        protected ICache Cache
        {
            get
            {
                if (_Cache == null)
                {
                    _Cache = CacheStrategyLoader.Load();
                }
                return _Cache;
            }
        }

        private const string Module = "XF.Common.Server";
        private const string Class = "SqlServerModelDataGatewayT";

        private IDatastoreService _DataService = null;
        public IDatastoreService DataService
        {
            get
            {
                return _DataService;
            }
            set
            {
                _DataService = value;
            }
        }

        private IContext _Context = null;

        public IContext Context
        {
            get
            {
                return _Context;
            }
            set
            {
                _Context = value; ;
            }
        }

        string ITypeMap.Domain
        {
            get { throw new NotImplementedException(); }
        }

        Type ITypeMap.KeyType
        {
            get { return GetModelType(); }
        }

        SqlConnection _DbConnection = null;
        public SqlConnection DbConnection
        {
            get
            {
                return _DbConnection;
            }
            set
            {
                _DbConnection = value;
            }
        }


        string ISqlCommandContext<T>.Schema
        {
            get { return GetDatabaseSchema(); }
        }

        Type ITypeMap.TypeResolution
        {
            get { return this.GetType(); }
        }

        T IModelDataGateway<T>.Post(T t, IContext context)
        {
            T result = null;
            try
            {
                result = Post(t, context);
            }
            catch (Exception ex)
            {
                context.SetError(500, Exceptions.ComposeGeneralExceptionError<T>(ModelActionOption.Post, ex, t, null, context, this.GetType().FullName));
                string message = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
                var props = eXtensibleConfig.GetProperties<T>(context.TypedItems, ModelActionOption.Post.ToString(), Module, Class, "104");
                EventWriter.WriteError(ex, SeverityType.Error, category, props);
            }
            return result;
        }

        T IModelDataGateway<T>.Get(ICriterion criterion, IContext context)
        {

            T result = null;
            try
            {
                result = Get(criterion, context);
            }
            catch (Exception ex)
            {
                context.SetError(500, Exceptions.ComposeGeneralExceptionError<T>(ModelActionOption.Get, ex, null, criterion, context, this.GetType().FullName));
                string message = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
                var props = eXtensibleConfig.GetProperties<T>(context.TypedItems, ModelActionOption.Get.ToString(), Module, Class, "122");
                EventWriter.WriteError(ex, SeverityType.Error, category, props);
            }
            return result;
        }

        T IModelDataGateway<T>.Put(T t, ICriterion criterion, IContext context)
        {
            T result = null;
            try
            {
                result = Put(t, criterion, context);
            }
            catch (Exception ex)
            {
                context.SetError(500, Exceptions.ComposeGeneralExceptionError<T>(ModelActionOption.Put, ex, t, null, context, this.GetType().FullName));
                string message = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
                var props = eXtensibleConfig.GetProperties<T>(context.TypedItems, ModelActionOption.Put.ToString(), Module, Class, "139");
                EventWriter.WriteError(ex, SeverityType.Error, category, props);
            }
            return result;
        }

        ICriterion IModelDataGateway<T>.Delete(ICriterion criterion, IContext context)
        {
            ICriterion result = null;
            try
            {
                result = Delete(criterion, context);
            }
            catch (Exception ex)
            {
                context.SetError(500, Exceptions.ComposeGeneralExceptionError<T>(ModelActionOption.Put, ex, null, criterion, context, this.GetType().FullName));
                string message = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
                var props = eXtensibleConfig.GetProperties<T>(context.TypedItems, ModelActionOption.Delete.ToString(), Module, Class, "156");
                EventWriter.WriteError(ex, SeverityType.Error, category, props);
            }
            return result;
        }

        IEnumerable<T> IModelDataGateway<T>.GetAll(ICriterion criterion, IContext context)
        {
            IEnumerable<T> result = null;
            try
            {
                result = GetAll(criterion, context);
            }
            catch (Exception ex)
            {
                context.SetError(500, Exceptions.ComposeGeneralExceptionError<T>(ModelActionOption.GetAll, ex, null, criterion, context, this.GetType().FullName));
                string message = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
                var props = eXtensibleConfig.GetProperties<T>(context.TypedItems, ModelActionOption.GetAll.ToString(), Module, Class, "173");
                EventWriter.WriteError(ex, SeverityType.Error, category, props);
            }
            return result;
        }

        IEnumerable<DisplayItem> IModelDataGateway<T>.GetAllDisplay(ICriterion criterion, IContext context)
        {
            IEnumerable<DisplayItem> result = null;
            try
            {
                result = GetAllDisplay(criterion, context);
            }
            catch (Exception ex)
            {
                context.SetError(500, Exceptions.ComposeGeneralExceptionError<T>(ModelActionOption.GetAllDisplay, ex, null, criterion, context, this.GetType().FullName));
                string message = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
                var props = eXtensibleConfig.GetProperties<T>(context.TypedItems, ModelActionOption.GetAllDisplay.ToString(), Module, Class, "190");
                EventWriter.WriteError(ex, SeverityType.Error, category, props);
            }
            return result;
        }

        U IModelDataGateway<T>.ExecuteAction<U>(T t, ICriterion criterion, IContext context)
        {
            U u = default(U);
            object o = null;
            if (criterion.Contains(XFConstants.Application.ACTIONEXECUTESTRATEGY))
            {
                o = DynamicExecuteAction<U>(t, criterion, context);
            }
            else
            {
                try
                {
                    o = ExecuteAction<U>(t, criterion, context);
                }
                catch (Exception ex)
                {
                    string message = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
                    var props = eXtensibleConfig.GetProperties<T>(context.TypedItems, ModelActionOption.ExecuteAction.ToString(), Module, Class, "214");
                    EventWriter.WriteError(ex, SeverityType.Error,category,props);
                }
            }
            if (o is U)
            {
                u = (U)o;
            }
            return u;
        }

        SqlCommand ISqlCommandContext<T>.InsertCommand(SqlConnection cn, T t, IContext context)
        {
            return InsertDbCommand(cn, t, context);
        }

        SqlCommand ISqlCommandContext<T>.SelectOneCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            return SelectOneDbCommand(cn, criterion, context);
        }

        SqlCommand ISqlCommandContext<T>.UpdateCommand(SqlConnection cn, T t, ICriterion criterion, IContext context)
        {
            return UpdateDbCommand(cn, t, criterion, context);
        }

        SqlCommand ISqlCommandContext<T>.DeleteCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            return DeleteDbCommand(cn, criterion, context);
        }

        SqlCommand ISqlCommandContext<T>.SelectManyCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            return SelectManyDbCommand(cn, criterion, context);
        }

        SqlCommand ISqlCommandContext<T>.SelectManyDisplayCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            return SelectManyDisplayDbCommand(cn, criterion, context);
        }



        #region overrideable methods

        public virtual T Post(T t, IContext context)
        {
            IRequestContext ctx = context as IRequestContext;
            T item = default(T);
            List<T> list = new List<T>();
            ISqlCommandContext<T> resolver = (ISqlCommandContext<T>)this;
            SqlCommand cmd = null;
            using (SqlConnection cn = DbConnection)
            {
                try
                {
                    cn.Open();
                    cmd = resolver.InsertCommand(cn, t, context);
                    if (cmd == null)
                    {
                        cmd = SqlResolver.ResolveCreateCommand<T>(cn, resolver.Schema, t, GetDataMaps());
                    }
                    if (cmd != null)
                    {
                        if (eXtensibleConfig.CaptureMetrics())
                        {
                            ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, cmd.CommandType.ToString(), cmd.CommandText);
                            ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.Scope.SqlCommand.Begin, DateTimeOffset.Now);
                        }
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            try
                            {
                                BorrowReader(reader, list);
                                if (list.Count > 0)
                                {
                                    item = list[0];
                                }
                            }
                            catch (Exception ex)
                            {
                                Context.SetError(500, Exceptions.ComposeBorrowReaderError<T>(ModelActionOption.Post, ex, t, null, context, this.GetType().FullName));
                                string message = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
                                var props = eXtensibleConfig.GetProperties<T>(context.TypedItems, ModelActionOption.Post.ToString(), Module, Class, "296");
                                EventWriter.WriteError(ex, SeverityType.Error, category, props);
                            }
                        }
                        if (eXtensibleConfig.CaptureMetrics())
                        {
                            ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.Scope.SqlCommand.End, DateTimeOffset.Now);
                        }
                    }
                    else
                    {
                        context.SetError(500, Exceptions.ComposeNullSqlCommand<T>(ModelActionOption.Post, t, null, context, this.GetType().FullName));
                    }
                }
                catch (Exception ex)
                {
                    string database = String.Format("server:{0};database:{1}", cn.DataSource, cn.Database);
                    Context.SetError(500, Exceptions.ComposeSqlException<T>(ModelActionOption.Post, ex, t, null, context, this.GetType().FullName, database));
                    string message = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
                    var props = eXtensibleConfig.GetProperties<T>(context.TypedItems, ModelActionOption.Post.ToString(), Module, Class, "314");
                    EventWriter.WriteError(ex, SeverityType.Error, category, props);
                }
                if (list.Count >= 1)
                {
                    item = list[0];
                }
            }

            return item;
        }

        public virtual T Get(ICriterion criterion, IContext context)
        {
            IRequestContext ctx = context as IRequestContext;
            T item = default(T);
            List<T> list = new List<T>();
            ISqlCommandContext<T> resolver = (ISqlCommandContext<T>)this;
            SqlCommand cmd = null;
            using (SqlConnection cn = DbConnection)
            {
                try
                {
                    cn.Open();
                    cmd = resolver.SelectOneCommand(cn, criterion, context);
                    if (cmd == null)
                    {
                        cmd = SqlResolver.ResolveReadCommand<T>(cn, resolver.Schema, criterion);
                    }
                    if (cmd != null)
                    {
                        if (eXtensibleConfig.CaptureMetrics())
                        {
                            ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, cmd.CommandType.ToString(), cmd.CommandText);
                            ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.Scope.SqlCommand.Begin, DateTimeOffset.Now);
                        }
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            try
                            {
                                BorrowReader(reader, list);
                            }
                            catch (Exception ex)
                            {
                                Context.SetError(500, Exceptions.ComposeBorrowReaderError<T>(ModelActionOption.Get, ex, null, criterion, context, this.GetType().FullName));
                                string message = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
                                var props = eXtensibleConfig.GetProperties<T>(context.TypedItems, ModelActionOption.Get.ToString(), Module, Class, "361");
                                EventWriter.WriteError(ex, SeverityType.Error, category, props);
                            }
                        }
                        if (eXtensibleConfig.CaptureMetrics())
                        {
                            ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.Scope.SqlCommand.End, DateTimeOffset.Now);
                        }
                    }
                    else
                    {
                        context.SetError(500, Exceptions.ComposeNullSqlCommand<T>(ModelActionOption.GetAllDisplay, null, criterion, context, this.GetType().FullName));
                    }
                }
                catch (Exception ex)
                {
                    string database = String.Format("server:{0};database:{1}", cn.DataSource, cn.Database);
                    Context.SetError(500, Exceptions.ComposeSqlException<T>(ModelActionOption.Get, ex, null, criterion, context, this.GetType().FullName, database)); string message = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
                    var props = eXtensibleConfig.GetProperties<T>(context.TypedItems, ModelActionOption.Get.ToString(), Module, Class, "379");
                    EventWriter.WriteError(ex, SeverityType.Error, category, props);
                }
            }
            if (list.Count > 0)
            {
                item = list[0];
            }
            return item;
        }

        public virtual T Put(T t, ICriterion criterion, IContext context)
        {
            IRequestContext ctx = context as IRequestContext;
            T item = default(T);
            List<T> list = new List<T>();
            ISqlCommandContext<T> resolver = (ISqlCommandContext<T>)this;
            SqlCommand cmd = null;
            using (SqlConnection cn = DbConnection)
            {
                try
                {
                    cn.Open();
                    cmd = resolver.UpdateCommand(cn, t, criterion, context);
                    if (cmd == null)
                    {
                        cmd = SqlResolver.ResolveUpdateCommand<T>(cn, resolver.Schema, t);
                    }
                    if (cmd != null)
                    {
                        if (eXtensibleConfig.CaptureMetrics())
                        {
                            ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, cmd.CommandType.ToString(), cmd.CommandText);
                            ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.Scope.SqlCommand.Begin, DateTimeOffset.Now);
                        }
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            try
                            {
                                BorrowReader(reader, list);
                            }
                            catch (Exception ex)
                            {
                                Context.SetError(500, Exceptions.ComposeBorrowReaderError<T>(ModelActionOption.Put, ex, t, null, context, this.GetType().FullName));
                                string message = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
                                var props = eXtensibleConfig.GetProperties<T>(context.TypedItems, ModelActionOption.Put.ToString(), Module, Class, "425");
                                EventWriter.WriteError(ex, SeverityType.Error, category, props);
                            }

                        }
                        if (eXtensibleConfig.CaptureMetrics())
                        {
                            ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.Scope.SqlCommand.End, DateTimeOffset.Now);
                        }
                    }
                    else
                    {
                        context.SetError(500, Exceptions.ComposeNullSqlCommand<T>(ModelActionOption.Put, t, null, context, this.GetType().FullName));
                    }
                }
                catch (Exception ex)
                {
                    string database = String.Format("server:{0};database:{1}", cn.DataSource, cn.Database);
                    Context.SetError(500, Exceptions.ComposeSqlException<T>(ModelActionOption.Put, ex, t, null, context, this.GetType().FullName, database));
                    string message = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
                    var props = eXtensibleConfig.GetProperties<T>(context.TypedItems, ModelActionOption.Put.ToString(), Module, Class, "86");
                    EventWriter.WriteError(ex, SeverityType.Error, category, props);
                }
            }
            if (list.Count >= 1)
            {
                item = list[0];
            }
            return item;
        }

        public virtual IEnumerable<T> GetAll(ICriterion criterion, IContext context)
        {
            IRequestContext ctx = context as IRequestContext;
            List<T> list = new List<T>();
            ISqlCommandContext<T> resolver = (ISqlCommandContext<T>)this;
            SqlCommand cmd = null;
            using (SqlConnection cn = DbConnection)
            {
                try
                {
                    cn.Open();
                    cmd = resolver.SelectManyCommand(cn, criterion, context);
                    if (cmd == null)
                    {
                        cmd = SqlResolver.ResolveReadListCommand<T>(cn, resolver.Schema, criterion);
                    }
                    if (cmd != null)
                    {
                        if (eXtensibleConfig.CaptureMetrics())
                        {
                            ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, cmd.CommandType.ToString(), cmd.CommandText);
                            ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.Scope.SqlCommand.Begin, DateTimeOffset.Now);
                        }
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            try
                            {
                                BorrowReader(reader, list);
                            }
                            catch (Exception ex)
                            {
                                string s = cn.ConnectionString;
                                Context.SetError(500, Exceptions.ComposeBorrowReaderError<T>(ModelActionOption.GetAll, ex, null, criterion, context, this.GetType().FullName));
                                string message = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
                                var props = eXtensibleConfig.GetProperties<T>(context.TypedItems, ModelActionOption.GetAll.ToString(), Module, Class, "488");
                                EventWriter.WriteError(ex, SeverityType.Error, category, props);
                            }

                        }
                        if (eXtensibleConfig.CaptureMetrics())
                        {
                            ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.Scope.SqlCommand.End, DateTimeOffset.Now);
                        }
                    }
                    else
                    {
                        context.SetError(500, Exceptions.ComposeNullSqlCommand<T>(ModelActionOption.GetAll, null, criterion, context, this.GetType().FullName));
                    }
                }
                catch (Exception ex)
                {
                    string database = String.Format("server:{0};database:{1}", cn.DataSource, cn.Database);
                    Context.SetError(500, Exceptions.ComposeSqlException<T>(ModelActionOption.GetAll, ex, null, criterion, context, this.GetType().FullName, database));
                    string message = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
                    var props = eXtensibleConfig.GetProperties<T>(context.TypedItems, ModelActionOption.GetAll.ToString(), Module, Class, "508");
                    EventWriter.WriteError(ex, SeverityType.Error, category, props);
                }
            }

            return list;
        }

        public virtual ICriterion Delete(ICriterion criterion, IContext context)
        {
            IRequestContext ctx = context as IRequestContext;
            Criterion item = new Criterion();
            ISqlCommandContext<T> resolver = (ISqlCommandContext<T>)this;
            SqlCommand cmd = null;
            using (SqlConnection cn = DbConnection)
            {
                try
                {
                    cn.Open();
                    cmd = resolver.DeleteCommand(cn, criterion, context);
                    if (cmd == null)
                    {
                        cmd = SqlResolver.ResolveDeleteCommand<T>(cn, resolver.Schema, criterion);
                    }
                    if (cmd != null)
                    {
                        if (eXtensibleConfig.CaptureMetrics())
                        {
                            ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, cmd.CommandType.ToString(), cmd.CommandText);
                            ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.Scope.SqlCommand.Begin, DateTimeOffset.Now);
                        }
                        int i = cmd.ExecuteNonQuery();
                        bool b = (i == 1) ? true : false;
                        item.AddItem("Success", b);
                        if (eXtensibleConfig.CaptureMetrics())
                        {
                            ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.Scope.SqlCommand.End, DateTimeOffset.Now);
                        }
                    }
                    else
                    {
                        context.SetError(500, Exceptions.ComposeNullSqlCommand<T>(ModelActionOption.Delete, null, criterion, context, this.GetType().FullName));
                    }
                }
                catch (Exception ex)
                {
                    string database = String.Format("server:{0};database:{1}", cn.DataSource, cn.Database);
                    Context.SetError(500, Exceptions.ComposeSqlException<T>(ModelActionOption.Delete, ex, null, criterion, context, this.GetType().FullName, database));
                    string message = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
                    var props = eXtensibleConfig.GetProperties<T>(context.TypedItems, ModelActionOption.Delete.ToString(), Module, Class, "86");
                    EventWriter.WriteError(ex, SeverityType.Error, category, props);
                }
            }
            return item;
        }

        public virtual IEnumerable<DisplayItem> GetAllDisplay(ICriterion criterion, IContext context)
        {
            IRequestContext ctx = context as IRequestContext;
            List<DisplayItem> list = new List<DisplayItem>();
            ISqlCommandContext<T> resolver = (ISqlCommandContext<T>)this;
            SqlCommand cmd = null;
            using (SqlConnection cn = DbConnection)
            {
                try
                {
                    cn.Open();
                    cmd = resolver.SelectManyDisplayCommand(cn, criterion, context);
                    if (cmd == null)
                    {
                        cmd = SqlResolver.ResolveReadListDisplayCommand<T>(cn, resolver.Schema, criterion);
                    }
                    if (cmd != null)
                    {
                        if (eXtensibleConfig.CaptureMetrics())
                        {
                            ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, cmd.CommandType.ToString(), cmd.CommandText);
                            ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.Scope.SqlCommand.Begin, DateTimeOffset.Now);
                        }
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            try
                            {
                                BorrowReaderDisplay(reader, list);
                            }
                            catch (Exception ex)
                            {
                                Context.SetError(500, Exceptions.ComposeBorrowReaderError<T>(ModelActionOption.GetAllDisplay, ex, null, criterion, context, this.GetType().FullName));
                                string message = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
                                var props = eXtensibleConfig.GetProperties<T>(context.TypedItems, ModelActionOption.GetAllDisplay.ToString(), Module, Class, "598");
                                EventWriter.WriteError(ex, SeverityType.Error, category, props);
                            }
                        }
                        if (eXtensibleConfig.CaptureMetrics())
                        {
                            ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.Scope.SqlCommand.End, DateTimeOffset.Now);
                        }
                    }
                    else
                    {
                        context.SetError(500, Exceptions.ComposeNullSqlCommand<T>(ModelActionOption.GetAllDisplay, null, criterion, context, this.GetType().FullName));
                    }
                }
                catch (Exception ex)
                {
                    string database = String.Format("server:{0};database:{1}", cn.DataSource, cn.Database);
                    Context.SetError(500, Exceptions.ComposeSqlException<T>(ModelActionOption.GetAllDisplay, ex, null, criterion, context, this.GetType().FullName, database));
                    string message = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
                    var props = eXtensibleConfig.GetProperties<T>(context.TypedItems, ModelActionOption.GetAllDisplay.ToString(), Module, Class, "616");
                    EventWriter.WriteError(ex, SeverityType.Error, category, props);
                }
            }

            return list;
        }

        public virtual object ExecuteAction<U>(T t, ICriterion criterion, IContext context)
        {
            return DynamicExecuteAction<U>(t, criterion, context);
        }

        private object DynamicExecuteAction<U>(T t, ICriterion criterion, IContext context)
        {
            IRequestContext ctx = context as IRequestContext;
            object o = null;

            if (criterion.Contains(XFConstants.Application.ACTIONEXECUTESTRATEGY))
            {
                string method = criterion.GetStringValue(XFConstants.Application.ACTIONEXECUTEMETHODNAME);
                Type impl = GetType();
                MethodInfo[] infos = impl.GetMethods();
                MethodInfo info = this.GetType().GetMethod(method);
                if (info == null)
                {
                }
                else
                {
                    List<object> paramList = new List<object>();
                    int j = 1;
                    string key = String.Format("{0}:{1}", XFConstants.Application.STRATEGYKEY, j.ToString());
                    foreach (var item in criterion.Items)
                    {
                        if (item.Key.Equals(key, StringComparison.OrdinalIgnoreCase))
                        {
                            paramList.Add(item.Value);
                            j++;
                            key = String.Format("{0}:{1}", XFConstants.Application.STRATEGYKEY, j.ToString());
                        }
                    }
                    try
                    {
                        o = info.Invoke(this, paramList.ToArray());
                    }
                    catch (Exception ex)
                    {
                        string message = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
                        var props = eXtensibleConfig.GetProperties<T>(context.TypedItems, ModelActionOption.ExecuteAction.ToString(), Module, Class, "666");
                        EventWriter.WriteError(ex, SeverityType.Error, category, props);
                    }

                    if (o is U)
                    {
                        return (U)o;
                    }
                }
            }
            return o;
        }

        public virtual SqlCommand InsertDbCommand(SqlConnection cn, T t, IContext context)
        {
            return null;
        }

        public virtual SqlCommand SelectOneDbCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            return null;
        }

        public virtual SqlCommand UpdateDbCommand(SqlConnection cn, T t, ICriterion criterion, IContext context)
        {
            return null;
        }

        public virtual SqlCommand DeleteDbCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            return null;
        }

        public virtual SqlCommand SelectManyDbCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            return null;
        }

        public virtual SqlCommand SelectManyDisplayDbCommand(SqlConnection cn, ICriterion criterion, IContext context)
        {
            return null;
        }

        public virtual void BorrowReader(SqlDataReader reader, List<T> list)
        {
            IListBorrower<T> borrower = new Borrower<T>(GetDataMaps());
            if (borrower != null)
            {
                try
                {
                    borrower.BorrowReader(reader, list);
                }
                catch (Exception ex)
                {
                    string message = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
                    EventWriter.WriteError(ex, SeverityType.Error, category);
                }

            }
        }

        public virtual void BorrowReaderDisplay(SqlDataReader reader, List<DisplayItem> list)
        {
            try
            {
                string s = GetModelType().FullName;
                bool hasGroupField = reader.FieldExists("Group");
                bool hasIntValField = reader.FieldExists("IntVal");
                bool hasAltDisplayField = reader.FieldExists("ItemDisplayAlt");
                while (reader.Read())
                {
                    DisplayItem item = new DisplayItem() { Typename = s };
                    item.ItemId = (reader["ItemId"] != DBNull.Value) ? reader["ItemId"].ToString() : String.Empty;
                    item.ItemDisplay = (reader["ItemDisplay"] != DBNull.Value) ? reader["ItemDisplay"].ToString() : String.Empty;
                    if (hasAltDisplayField)
                    {
                        item.ItemDisplayAlt = (reader["ItemDisplayAlt"] != DBNull.Value) ? reader["ItemDisplayAlt"].ToString() : String.Empty;
                    }
                    if (hasGroupField)
                    {
                        item.Group = (reader["Group"] != DBNull.Value) ? reader["Group"].ToString() : String.Empty;
                    }
                    if (hasIntValField)
                    {
                        int i;
                        if (reader["IntVal"] != DBNull.Value && Int32.TryParse(reader["IntVal"].ToString(), out i))
                        {
                            item.IntVal = i;
                        }
                    }

                    list.Add(item);
                }
            }
            catch (Exception ex)
            {
                string message = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
                EventWriter.WriteError(ex, SeverityType.Error, category);
            }
        }



        public virtual List<DataMap> GetDataMaps()
        {
            return null;
        }

        public virtual Type GetModelType()
        {
            return GetModelType<T>();
        }

        public virtual string GetDatabaseSchema()
        {
            return XFConstants.Application.Defaults.SqlServerSchema;
        }

        #endregion

        #region helper methods
        private static Type GetModelType<T>()
        {
            return Activator.CreateInstance<T>().GetType();
        }



        #endregion

        void IModelDataGatewayInitializeable.Initialize<T>(ModelActionOption option, IContext context, T t, ICriterion criterion, Func<IContext, string> dbkeyResolver)
        {
            string key = dbkeyResolver.Invoke(context);
            if (String.IsNullOrEmpty(key))
            {
                context.SetError(500, Exceptions.ComposeDbConnectionStringKeyResolutionError<T>(option, t, context));
                EventWriter.WriteError(Exceptions.ComposeDbConnectionStringKeyResolutionError<T>(option, t, context,true), SeverityType.Error, category);
            }
            else
            {
                SqlConnection connection = SqlResolver.GetConnection(key);
                if (connection == null)
                {

                    context.SetError(500, Exceptions.ComposeDbConnectionCreationError<T>(option, t, context, key));
                    EventWriter.WriteError(Exceptions.ComposeDbConnectionCreationError<T>(option, t, context, key,true), SeverityType.Error, category);
                }
                else
                {
                    DbConnection = connection;
                    if (eXtensibleConfig.CaptureMetrics())
                    {
                        IRequestContext ctx = context as IRequestContext;
                        ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.SqlServer.Datasource, String.Format("server:{0};database:{1};", connection.DataSource, connection.Database)); ;
                    }
                }
            }
        }

    }
}
