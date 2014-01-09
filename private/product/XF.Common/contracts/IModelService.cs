// <copyright file="IModelService.cs" company="eXtensoft LLC">
// Copyright © 2014 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IModelService
    {
        #region synchronous

        T Post<T>(T model) where T : class, new();

        T Put<T>(T model, ICriterion criterion) where T : class, new();

        ICriterion Delete<T>(ICriterion criterion) where T : class, new();

        T Get<T>(ICriterion criterion) where T : class, new();

        IEnumerable<T> GetAll<T>(ICriterion criterion) where T : class, new();

        IEnumerable<IDisplay> GetAllDisplay<T>(ICriterion criterion) where T : class, new();

        U ExecuteAction<T, U>(T model, ICriterion criterion) where T : class, new();

        #endregion

        #region asynchronous

        void PostAsync<T>(T model, Action<T> callback) where T : class, new();

        void PutAsync<T>(T model, ICriterion criterion, Action<T> callback) where T : class, new();

        void DeleteAsync<T>(ICriterion criterion, Action<T> callback) where T : class, new();

        void GetAsync<T>(ICriterion criterion, Action<T> callback) where T : class, new();

        void GetAllAsync<T>(ICriterion criterion, Action<IEnumerable<T>> callback) where T : class, new();

        void GetAllDisplayAsync<T>(ICriterion criterion, Action<IEnumerable<IDisplay>> callback) where T : class, new();

        void ExecuteActionAsync<T, U>(T model, ICriterion criterion, Action<U> callback) where T : class, new();

        #endregion

    }

}
