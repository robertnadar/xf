// <copyright file="ModelDataGatewayDataService.cs" company="eXtensoft LLC">
// Copyright © 2014 All Right Reserved
// </copyright>

namespace XF.DataServices
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using XF.Common;

    public class ModelDataGatewayDataService : IDatastoreService
    {
        #region local fields

        private static ITypeMapCache _TypeCache;

        #endregion

        #region properties

        #region DbKey Resolver

        private IStrategyResolver _DatabaseKeyResolver = null;

        public IStrategyResolver DatabaseKeyResolver
        {
            get
            {
                if (_DatabaseKeyResolver == null)
                {
                    _DatabaseKeyResolver = ConfigStrategyResolverLoader.Load<KeyPairStrategyResolver>("DatabaseKeyResolution");
                }
                return _DatabaseKeyResolver;
            }
        }

        #endregion DbKey Resolver

        #region ICachingService

        private ICache _Cache = null;

        public ICache Cache
        {
            get
            {
                if (_Cache == null)
                {
                    _Cache = new XF.Caching.NetMemoryCache();
                }
                return _Cache;
            }
            set
            {
                _Cache = value;
            }
        }


        #endregion



        #endregion

        #region constructors

        public ModelDataGatewayDataService() { }

        static ModelDataGatewayDataService()
        {
            Initialize();
        }

        private static void Initialize()
        {
            _TypeCache = new TypeMapCache();
            _TypeCache.Initialize();
        }

        #endregion

        #region interface implementations

        T IDatastoreService.Post<T>(T model, IRequestContext requestContext)
        {
            DateTimeOffset begin = DateTimeOffset.Now;
            requestContext.SetMetric( XFConstants.Metrics.Scope.DataService, "MDG.Begin",begin);
            T item = default(T);
            try
            {
                item = Create<T>(model, requestContext);
            }
            catch (Exception ex)
            {
                requestContext.SetError(500, Exceptions.ComposeGeneralExceptionError<T>(ModelActionOption.Post, ex, model, null, requestContext));
                //Logger.Log
            }
            if (item == null && requestContext.HasError())
            {
                requestContext.SetError(500, "Internal Server Error");
            }
            return item;
        }

        T IDatastoreService.Put<T>(T model, ICriterion criterion, IRequestContext requestContext)
        {
            T item = null;
            try
            {
                item = Update<T>(model, criterion, requestContext);
            }
            catch (Exception ex)
            {
                requestContext.SetError(500, Exceptions.ComposeGeneralExceptionError<T>(ModelActionOption.Put, ex, model, null, requestContext));
            }
            return item;
        }

        ICriterion IDatastoreService.Delete<T>(ICriterion criterion, IRequestContext requestContext)
        {
            
            ICriterion item = null;
            try
            {
                item = Delete<T>(criterion, requestContext);
            }
            catch (Exception ex)
            {
                requestContext.SetError(500, Exceptions.ComposeGeneralExceptionError<T>(ModelActionOption.Delete, ex, null, criterion, requestContext));
                //Logger.Log
            }
            if (item == null && !requestContext.HasError())
            {
                item = new Criterion();
                requestContext.SetError(500, "Internal Server Error");
            }
            return item;
        }

        T IDatastoreService.Get<T>(ICriterion criterion, IRequestContext requestContext)
        {
            T item = null;
            try
            {
                item = Get<T>(criterion, requestContext);
            }
            catch (Exception ex)
            {
                requestContext.SetError(500, Exceptions.ComposeGeneralExceptionError<T>(ModelActionOption.Get, ex, null, criterion, requestContext));
            }
            if (item == null && !requestContext.HasError())
            {
                requestContext.SetError(404, Exceptions.ComposeResourceNotFoundError<T>(ModelActionOption.Get, null, criterion, requestContext));
            }
            return item;
        }

        IEnumerable<T> IDatastoreService.GetAll<T>(ICriterion criterion, IRequestContext requestContext)
        {
            
            IEnumerable<T> list = null;
            try
            {
                list = GetAll<T>(criterion, requestContext);
            }
            catch (Exception ex)
            {
                requestContext.SetError(500, Exceptions.ComposeGeneralExceptionError<T>(ModelActionOption.GetAll, ex, null, criterion, requestContext));
            }
            if (list != null && list.Count() == 0 && !requestContext.HasError())
            {
                requestContext.SetError(404, Exceptions.ComposeResourceNotFoundError<T>(ModelActionOption.GetAll, null, criterion, requestContext));                
            }
            else if (list == null && !requestContext.HasError())
            {
                requestContext.SetError(500, "Internal Server Error");
            }
            return list;
        }

        IEnumerable<IDisplay> IDatastoreService.GetAllDisplay<T>(ICriterion criterion, IRequestContext requestContext)
        {
            
            IEnumerable<IDisplay> list = null;
            try
            {
                list = GetAllDisplay<T>(criterion, requestContext);
            }
            catch (Exception ex)
            {
                requestContext.SetError(500, Exceptions.ComposeGeneralExceptionError<T>(ModelActionOption.GetAllDisplay, ex, null, criterion, requestContext));
                //Logger.Log
            }
            if (list != null && list.Count() == 0 && !requestContext.HasError())
            {
                requestContext.SetError(404, Exceptions.ComposeResourceNotFoundError<T>(ModelActionOption.GetAllDisplay, null, criterion, requestContext));
            }
            else if(list == null && !requestContext.HasError())
            {
                list = new List<DisplayItem>();
                requestContext.SetError(500, "Internal Server Error");
            }
            return list;
        }

        U IDatastoreService.ExecuteAction<T, U>(T model, ICriterion criterion, IRequestContext requestContext)
        {
            U u = default(U);
            try
            {
                u = ExecuteAction<T, U>(model, criterion, requestContext);
            }
            catch (Exception ex)
            {
                requestContext.SetError(500, Exceptions.ComposeGeneralExceptionError<T>(ModelActionOption.ExecuteAction, ex, model, criterion, requestContext));
                //Logger.Log               
            }
            if (u == null)
            {
                u = default(U);
                requestContext.SetError(500, "Internal Server Error");
            }
            return u;

        }
        #endregion


        #region local implementations

        private T Create<T>(T model, IRequestContext requestContext) where T : class, new()
        {
            T result = default(T);
            var implementor = ResolveImplementor<T>(ModelActionOption.Post, requestContext, model,null);
            if (implementor != null)
            {
                IContext context = requestContext as IContext;
                result = implementor.Post(model, context);
            }
            return result;
        }

        private T Update<T>(T model, ICriterion criterion, IRequestContext requestContext) where T : class, new()
        {
            T result = default(T);
            var implementor = ResolveImplementor<T>(ModelActionOption.Put, requestContext, model,null);
            if (implementor != null)
            {
                IContext context = requestContext as IContext;
                result = implementor.Put(model, criterion, context);
            }
            return result;            
        }

        private ICriterion Delete<T>(ICriterion criterion, IRequestContext requestContext) where T : class, new()
        {
            ICriterion result = null;
            var implementor = ResolveImplementor<T>(ModelActionOption.Delete, requestContext, null, criterion);
            if (implementor != null)
            {
                IContext context = requestContext as IContext;
                result = implementor.Delete(criterion, context);
            }
            return result;
        }

        private T Get<T>(ICriterion criterion, IRequestContext requestContext) where T : class, new()
        {
            T result = default(T);
            var implementor = ResolveImplementor<T>(ModelActionOption.Get, requestContext, null, criterion);
            if (implementor != null)
            {
                IContext context = requestContext as IContext;
                result = implementor.Get(criterion, context);
            }
            return result;
        }

        private IEnumerable<T> GetAll<T>(ICriterion criterion, IRequestContext requestContext) where T : class, new()
        {
            IEnumerable<T> result = null;
            var implementor = ResolveImplementor<T>(ModelActionOption.GetAll, requestContext, null, criterion);
            if (implementor != null)
            {
                IContext context = requestContext as IContext;
                result = implementor.GetAll(criterion, context);
            }
            return result;
        }

        private IEnumerable<IDisplay> GetAllDisplay<T>(ICriterion criterion, IRequestContext requestContext) where T : class, new()
        {
            IEnumerable<IDisplay> result = null;
            var implementor = ResolveImplementor<T>(ModelActionOption.GetAllDisplay, requestContext, null, criterion);
            if (implementor != null)
            {
                IContext context = requestContext as IContext;
                result = implementor.GetAllDisplay(criterion, context);
            }
            return result;
        }

        private U ExecuteAction<T, U>(T model, ICriterion criterion, IRequestContext requestContext) where T : class, new()
        {
            U result = default(U);
            var implementor = ResolveImplementor<T>(ModelActionOption.ExecuteAction, requestContext,null, criterion);
            if (implementor != null)
            {
                IContext context = requestContext as IContext;
                result = implementor.ExecuteAction<U>(model, criterion, context);
            }
            return result;
        }

        #endregion

        private string ResolveDbKey(IContext context)
        {
            string s = context.GetValue<string>(XFConstants.Context.Application);
            return DatabaseKeyResolver.Resolve(s, eXtensibleConfig.Zone);
        }

        private IModelDataGateway<T> ResolveImplementor<T>(ModelActionOption option, IContext context,T t, ICriterion criterion) where T : class, new()
        {
            IModelDataGateway<T> implementor = null;
            
            Type type = _TypeCache.ResoveType<T>();
            if (type == null)
            {
                context.SetError(500, Exceptions.ComposeImplementorResolutionError<T>(option, t, context));
                //Logger.Write(message, new string[] { }, -1, -1, TraceEventTypeOption.Error, "Data Access Error");
            }
            else
            {
                implementor = Activator.CreateInstance(type) as IModelDataGateway<T>;
                if (implementor == null)
                {
                    context.SetError(500, Exceptions.ComposeImplementorInstantiationError<T>(option, t, context, type.FullName));
                    // Logger.Log
                }
                else
                {
                    implementor.DataService = this as IDatastoreService;
                    implementor.Context = context;

                    ICacheable<T> cacheable = implementor as ICacheable<T>;
                    if (cacheable != null)
                    {
                        cacheable.Cache = Cache;
                    }
                    // TODO, outsource this
                    IModelDataGatewayInitializeable initializable = implementor as IModelDataGatewayInitializeable;
                    if (initializable != null)
                    {
                        initializable.Initialize<T>(option,context,t,criterion,ResolveDbKey);
                    }

                }
            }
            return implementor;
        }
    }
}
