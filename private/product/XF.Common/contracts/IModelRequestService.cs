// <copyright file="IModelRequestService.cs" company="eXtensoft LLC">
// Copyright © 2014 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IModelRequestService
    {
        #region synchronous

        IResponse<T> Post<T>(T model) where T : class, new();

        IResponse<T> Put<T>(T model, ICriterion criterion) where T : class, new();

        IResponse<T> Delete<T>(ICriterion criterion) where T : class, new();

        IResponse<T> Get<T>(ICriterion criterion) where T : class, new();

        IResponse<T> GetAll<T>(ICriterion criterion) where T : class, new();

        IResponse<T> GetAllDisplay<T>(ICriterion criterion) where T : class, new();

        IResponse<U> ExecuteAction<T, U>(T model, ICriterion criterion) where T : class, new();

        #endregion

        #region asynchronous

        void PostAsync<T>(T model, Action<IResponse<T>> callback) where T : class, new();

        void PutAsync<T>(T model, ICriterion criterion, Action<IResponse<T>> callback) where T : class, new();

        void DeleteAsync<T>(ICriterion criterion, Action<IResponse<T>> callback) where T : class, new();

        void GetAsync<T>(ICriterion criterion, Action<IResponse<T>> callback) where T : class, new();

        void GetAllAsync<T>(ICriterion criterion, Action<IResponse<T>> callback) where T : class, new();

        void GetAllDisplayAsync<T>(ICriterion criterion, Action<IResponse<T>> callback) where T : class, new();

        void ExecuteActionAsync<T, U>(T model, ICriterion criterion, Action<IResponse<U>> callback) where T : class, new();

        #endregion

    }
}
