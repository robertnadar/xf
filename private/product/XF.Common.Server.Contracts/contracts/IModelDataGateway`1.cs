// <copyright file="IModelDataGateway`1.cs" company="eXtensoft LLC">
// Copyright © 2014 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IModelDataGateway<T> : ITypeMap where T : class, new()
    {
        IDatastoreService DataService { get; set; }

        IContext Context { get; set; }

        T Post(T t, IContext context);

        T Get(ICriterion criterion, IContext context);

        T Put(T t, ICriterion criterion, IContext context);

        ICriterion Delete(ICriterion criterion, IContext context);

        IEnumerable<T> GetAll(ICriterion criterion, IContext context);

        IEnumerable<DisplayItem> GetAllDisplay(ICriterion criterion, IContext context);

        U ExecuteAction<U>(T t, ICriterion criterion, IContext context);

    }
}
