// <copyright file="ModelDataGateway`1.cs" company="eXtensoft LLC">
// Copyright © 2014 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Data.SqlClient;

    [InheritedExport(typeof(ITypeMap))]
    public class ModelDataGateway<T> : IModelDataGateway<T> where T : class, new()
    {
        private const string Category = "dataservice";
        private const string Module = "XF.Common.Server.Contracts";
        private const string Class = "ModelDataGatewayT";

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


        Type ITypeMap.TypeResolution
        {
            get { return this.GetType(); }
        }

        T IModelDataGateway<T>.Post(T t, IContext context)
        {
            return Create(t, context);
        }

        T IModelDataGateway<T>.Get(ICriterion criterion, IContext context)
        {
            return Read(criterion, context);
        }

        T IModelDataGateway<T>.Put(T t, ICriterion criterion, IContext context)
        {
            return Update(t, criterion, context);
        }

        ICriterion IModelDataGateway<T>.Delete(ICriterion criterion, IContext context)
        {
            return Delete(criterion, context);
        }

        IEnumerable<T> IModelDataGateway<T>.GetAll(ICriterion criterion, IContext context)
        {
            return ReadList(criterion, context);
        }

        IEnumerable<DisplayItem> IModelDataGateway<T>.GetAllDisplay(ICriterion criterion, IContext context)
        {
            return ReadListDisplay(criterion, context);
        }

        U IModelDataGateway<T>.ExecuteAction<U>(T t, ICriterion criterion, IContext context)
        {
            U u = default(U);
            object o = null;
            try
            {
                o = ExecuteAction<U>(t, criterion, context);
                if (o is U)
                {
                    u = (U)o;
                }
            }
            catch (Exception ex)
            {
                string message = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
                var props = eXtensibleConfig.GetProperties<T>(context.TypedItems, ModelActionOption.ExecuteAction.ToString(), Module, Class, "214");
                EventWriter.WriteError(ex, SeverityType.Error, Category, props);
            }
            return u;
        }

        #region helper methods

        public virtual T Create(T t, IContext context)
        {
            string message = String.Format("{0}.{1}<{2}> is not implemented", this.GetType().FullName, ModelActionOption.Post.ToString(), GetModelType().FullName);
            EventWriter.WriteError(message, SeverityType.Error, Category);
            throw new NotImplementedException(message);
        }

        public virtual T Read(ICriterion criterion, IContext context)
        {
            string message = String.Format("{0}.{1}<{2}> is not implemented", this.GetType().FullName, ModelActionOption.Get.ToString(), GetModelType().FullName);
            EventWriter.WriteError(message, SeverityType.Error, Category);
            throw new NotImplementedException(message);
        }

        public virtual T Update(T t, ICriterion criterion, IContext context)
        {
            string message = String.Format("{0}.{1}<{2}> is not implemented", this.GetType().FullName, ModelActionOption.Put.ToString(), GetModelType().FullName);
            EventWriter.WriteError(message, SeverityType.Error, Category);
            throw new NotImplementedException(message);
        }

        public virtual ICriterion Delete(ICriterion criterion, IContext context)
        {
            string message = String.Format("{0}.{1}<{2}> is not implemented", this.GetType().FullName, ModelActionOption.Delete.ToString(), GetModelType().FullName);
            EventWriter.WriteError(message, SeverityType.Error, Category);
            throw new NotImplementedException(message);
        }

        public virtual IEnumerable<T> ReadList(ICriterion criterion, IContext context)
        {
            string message = String.Format("{0}.{1}<{2}> is not implemented", this.GetType().FullName, ModelActionOption.GetAll.ToString(), GetModelType().FullName);
            EventWriter.WriteError(message, SeverityType.Error, Category);
            throw new NotImplementedException(message);
        }

        public virtual IEnumerable<DisplayItem> ReadListDisplay(ICriterion criterion, IContext context)
        {
            string message = String.Format("{0}.{1}<{2}> is not implemented", this.GetType().FullName, ModelActionOption.GetAllDisplay.ToString(), GetModelType().FullName);
            EventWriter.WriteError(message, SeverityType.Error, Category);
            throw new NotImplementedException(message);
        }

        public virtual U ExecuteAction<U>(T t, ICriterion criterion, IContext context)
        {
            string message = String.Format("{0}.{1}<{2}> is not implemented", this.GetType().FullName, ModelActionOption.ExecuteAction.ToString(), GetModelType().FullName);
            EventWriter.WriteError(message, SeverityType.Error, Category);
            throw new NotImplementedException(message);
        }

        private Type GetModelType()
        {
            return Activator.CreateInstance<T>().GetType();
        }
        #endregion



    }
}
