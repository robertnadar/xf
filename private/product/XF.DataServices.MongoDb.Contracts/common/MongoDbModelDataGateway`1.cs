// <copyright file="MongoDbModelDataGateway`1.cs" company="eXtensoft LLC">
// Copyright © 2014 All Right Reserved
// </copyright>

namespace XF.DataServices
{
    using MongoDB.Bson;
    using MongoDB.Driver;
    using MongoDB.Driver.Builders;
    using MongoDB.Driver.Linq;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;
    using XF.Common;

    [InheritedExport(typeof(ITypeMap))]
    public abstract class MongoDbModelDataGateway<T> : IModelDataGateway<T>, IModelDataGatewayInitializeable where T : class, new()
    {
        private static IList<string> queryExclusions = new List<string>
        {
            {"skip"},
            {"take"},
            {"limit"}
        };

        string ITypeMap.Domain
        {
            get { return "foobar.dataservice"; }
        }

        Type ITypeMap.KeyType
        {
            get { return GetModelType(); }
        }

        Type ITypeMap.TypeResolution
        {
            get { return this.GetType(); }
        }

        private IDatastoreService _DataService;
        IDatastoreService IModelDataGateway<T>.DataService
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

        IContext IModelDataGateway<T>.Context
        {
            get
            {
                return _Context;
            }
            set
            {
                _Context = value;
            }
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
            }

            return result;
        }

        public MongoDatabase MongoDb { get; set; }

        public MongoCollection<T> Collection { get; set; }

        protected IQueryable<T> IQueryable
        {
            get { return Collection.AsQueryable<T>(); }
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
                //throw;
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
                    var props = eXtensibleConfig.GetProperties<T>(context.TypedItems, ModelActionOption.ExecuteAction.ToString(), "Module", "Class", "166");
                    //Logger.Write(message, new string[] { }, -1, -1, TraceEventTypeOption.Error, "Exception:ExecuteAction", props);
                }
            }
            if (o is U)
            {
                u = (U)o;
            }
            return u;
        }

        #region overridable model actions


        protected virtual T Post(T t, IContext context)
        {
            Collection.Insert(t);
            return t;
        }

        protected virtual T Get(ICriterion criterion, IContext context)
        {
            IMongoQuery query = new QueryDocument(ModelIdName, criterion.GetStringValue(ModelIdName));
            return Collection.FindOneAs<T>(query);
        }

        protected virtual T Put(T t, ICriterion criterion, IContext context)
        {
            Collection.Insert<T>(t);
            return t;
        }

        protected virtual ICriterion Delete(ICriterion criterion, IContext context)
        {
            ICriterion result = new Criterion();
            WriteConcernResult writeConcern = Collection.Remove(criterion.ToQueryDocument<T>());
            if (writeConcern.Ok)
            {
                long affected = writeConcern.DocumentsAffected;
                result.AddItem("documentsAffected", affected);
            }
            else
            {
                IRequestContext ctx = context as IRequestContext;
                ctx.SetError(500, Exceptions.ComposeDatastoreException<T>(ModelActionOption.Delete, writeConcern.ErrorMessage, null, criterion, context, this.GetType().FullName,"MongoDB"));
            }
            return result;
        }

        protected virtual IEnumerable<T> GetAll(ICriterion criterion, IContext context)
        {
            var query = criterion.ToMongoQuery();
            return Collection.FindAs<T>(query).ToList();
        }

        protected virtual IEnumerable<DisplayItem> GetAllDisplay(ICriterion criterion, IContext context)
        {
            throw new NotImplementedException();
        }

        protected virtual U ExecuteAction<U>(T t, ICriterion criterion, IContext context)
        {
            throw new NotImplementedException();
        }


        #endregion

        #region overrideables

        protected virtual IMongoQuery InsertQuery(T t, IContext context)
        {
            throw new NotImplementedException();
        }

        protected virtual IMongoQuery UpdateQuery(T t, IContext context)
        {
            throw new NotImplementedException();
        }

        protected virtual IMongoQuery RemoveQuery(ICriterion criterion, IContext context)
        {
            throw new NotImplementedException();
        }

        protected virtual IMongoQuery FindOneQuery(ICriterion criterion, IContext context)
        {
            throw new NotImplementedException();
        }

        protected virtual IMongoQuery FindQuery(ICriterion criterion, IContext context)
        {
            throw new NotImplementedException();
        }

        protected virtual IMongoQuery FindDisplayQuery(ICriterion criterion, IContext context)
        {
            throw new NotImplementedException();
        }


        #endregion

        protected virtual string GetCollectionKey()
        {
            string typename = GetModelType().Name.ToLower();
            return typename;
        }

        protected MongoCollection<T> GetCollection(string key = "")
        {
            if (String.IsNullOrWhiteSpace(key))
            {
                key = GetCollectionKey();
            }
            return MongoDb.GetCollection<T>(key);
        }

        public virtual Type GetModelType()
        {
            return Activator.CreateInstance<T>().GetType();
        }

        public virtual string ModelIdName
        {
            get { return "Id"; }
        }

        

        #region helper methods

        private object DynamicExecuteAction<U>(T t, ICriterion criterion, IContext context)
        {
            throw new NotImplementedException();
        }

        #endregion


        void IModelDataGatewayInitializeable.Initialize<T>(ModelActionOption option, IContext context, T t, ICriterion criterion, Func<IContext, string> dbkeyResolver)
        {
            string key = dbkeyResolver.Invoke(context);
            if (String.IsNullOrEmpty(key))
            {
                context.SetError(500, Exceptions.ComposeDbConnectionStringKeyResolutionError<T>(option, t, context));
            }
            else
            {
                var connection = "mongodb://localhost";
                var client = new MongoClient(connection);
                var server = client.GetServer();
                MongoDb = server.GetDatabase(key);
                if (MongoDb == null)
                {
                    context.SetError(500, Exceptions.ComposeDbConnectionCreationError<T>(option, t, context, key));
                }
                else
                {
                    Collection = GetCollection();
                    if (Collection == null)
                    {
                        context.SetError(500, "MongoDB.Collection is Null");
                    }
                    else if (eXtensibleConfig.CaptureMetrics())
                    {
                        IRequestContext ctx = context as IRequestContext;
                        ctx.SetMetric(XFConstants.Metrics.Scope.DataStore, XFConstants.Metrics.SqlServer.Datasource, String.Format("server:{0};database:{1};", connection, key)); ;
                    }
                }
            }
        }
    }
}
