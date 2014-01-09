// <copyright file="IDataRequestService.cs" company="eXtensoft LLC">
// Copyright © 2014 All Right Reserved
// </copyright>

namespace XF.Common
{
    public interface IDataRequestService
    {
        IResponse<T> Post<T>(IRequest<T> request) where T : class, new();

        IResponse<T> Put<T>(IRequest<T> request) where T : class, new();

        IResponse<T> Delete<T>(IRequest<T> request) where T : class, new();

        IResponse<T> Get<T>(IRequest<T> request) where T : class, new();

        IResponse<T> GetAll<T>(IRequest<T> request) where T : class, new();

        IResponse<T> GetAllDisplay<T>(IRequest<T> request) where T : class, new();

        IResponse<U> ExecuteAction<T, U>(IRequest<T> request) where T : class, new();
    }
}
