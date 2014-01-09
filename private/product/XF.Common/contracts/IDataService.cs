// <copyright file="IDataService.cs" company="eXtensoft LLC">
// Copyright © 2014 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System.Collections.Generic;

    public interface IDatastoreService
    {
        T Post<T>(T model, IRequestContext requestContext) where T : class, new();

        T Put<T>(T model, ICriterion criterion, IRequestContext requestContext) where T : class, new();

        ICriterion Delete<T>(ICriterion criterion, IRequestContext requestContext) where T : class, new();

        T Get<T>(ICriterion criterion, IRequestContext requestContext) where T : class, new();

        IEnumerable<T> GetAll<T>(ICriterion criterion, IRequestContext requestContext) where T : class, new();

        IEnumerable<IDisplay> GetAllDisplay<T>(ICriterion criterion, IRequestContext requestContext) where T : class, new();

        U ExecuteAction<T, U>(T model, ICriterion criterion, IRequestContext requestContext) where T : class, new();
    }
}
