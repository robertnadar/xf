// <copyright file="PassThroughModelRequestService.cs" company="eXtensoft LLC">
// Copyright © 2014 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class PassThroughModelRequestService :ModelRequestService, IModelRequestService
    {
        #region local fields


        #endregion

        #region properties

        [Import(typeof(IDatastoreService), AllowDefault=true)]
        public IDataRequestService Service { get; set; }

        #endregion

        #region constructors

        public PassThroughModelRequestService() { }

        public PassThroughModelRequestService(IDataRequestService service)
        {
            Service = service;
        }

        #endregion

        #region interface implementations (synchronous)

        IResponse<T> IModelRequestService.Post<T>(T model)
        {
            return Create<T>(model);
        }

        IResponse<T> IModelRequestService.Put<T>(T model, ICriterion criterion)
        {
            return Update<T>(model,criterion);
        }

        IResponse<T> IModelRequestService.Delete<T>(ICriterion criterion)
        {
            return Delete<T>(criterion);
        }

        IResponse<T> IModelRequestService.Get<T>(ICriterion criterion)
        {
            return Read<T>(criterion);
        }

        IResponse<T> IModelRequestService.GetAll<T>(ICriterion criterion)
        {
            return ReadList<T>(criterion);
        }

        IResponse<T> IModelRequestService.GetAllDisplay<T>(ICriterion criterion)
        {
            return ReadListDisplay<T>(criterion);
        }


        IResponse<U> IModelRequestService.ExecuteAction<T, U>(T model, ICriterion criterion)
        {
            return ExecuteAction<T, U>(model, criterion);
        }

        #endregion

        #region interface implementations (asynchronous)

        void IModelRequestService.PostAsync<T>(T model, Action<IResponse<T>> callback)
        {
            new Action(async () =>
                {
                    IResponse<T> result = await Task.Run<IResponse<T>>(() => CreateAsync<T>(model));
                    callback.Invoke(result);
                }).Invoke();
        }

        void IModelRequestService.PutAsync<T>(T model, ICriterion criterion, Action<IResponse<T>> callback)
        {
            new Action(async () =>
                {
                    IResponse<T> result = await Task.Run<IResponse<T>>(() => UpdateAsync<T>(model,criterion));
                    callback.Invoke(result);
                }).Invoke();
        }

        void IModelRequestService.DeleteAsync<T>(ICriterion criterion, Action<IResponse<T>> callback)
        {
            new Action(async () =>
                {
                    IResponse<T> result = await Task.Run<IResponse<T>>(() => DeleteAsync<T>(criterion));
                    callback.Invoke(result);
                }).Invoke();
        }

        void IModelRequestService.GetAsync<T>(ICriterion criterion, Action<IResponse<T>> callback)
        {
            new Action(async () =>
                {
                    IResponse<T> result = await Task.Run<IResponse<T>>(() => ReadAsync<T>(criterion));
                    callback.Invoke(result);
                }).Invoke();
        }

        void IModelRequestService.GetAllAsync<T>(ICriterion criterion, Action<IResponse<T>> callback)
        {
            new Action(async () =>
                {
                    IResponse<T> result = await Task.Run<IResponse<T>>(() => ReadListAsync<T>(criterion));
                    callback.Invoke(result);
                }).Invoke();
        }

        void IModelRequestService.GetAllDisplayAsync<T>(ICriterion criterion, Action<IResponse<T>> callback)
        {
            new Action(async () =>
                {
                    IResponse<T> result = await Task.Run<IResponse<T>>(() => ReadListDisplayAsync<T>(criterion));
                    callback.Invoke(result);
                }).Invoke();
        }

        void IModelRequestService.ExecuteActionAsync<T, U>(T model, ICriterion criterion, Action<IResponse<U>> callback)
        {
            new Action(async () =>
                {
                    IResponse<U> result = await Task.Run<IResponse<U>>(() => ExecuteActionAsync<T, U>(model, criterion));
                    callback.Invoke(result);
                }).Invoke();
        }

        #endregion

        #region sync helpers

        private IResponse<T> Create<T>(T model) where T : class, new()
        {
            IRequest<T> request = new Message<T>(Context,ModelActionOption.Post) { };
            request.Model = model;
            return Service.Post<T>(request);
        }

        private IResponse<T> Update<T>(T model, ICriterion criterion) where T : class, new()
        {
            IRequest<T> request = new Message<T>(Context, ModelActionOption.Put) { Criterion = criterion };
            request.Model = model;
            return Service.Put<T>(request);
        }

        private IResponse<T> Delete<T>(ICriterion criterion) where T : class, new()
        {
            IRequest<T> request = new Message<T>(Context,ModelActionOption.Delete) { Criterion = criterion };
            return Service.Delete<T>(request);
        }

        private IResponse<T> Read<T>(ICriterion criterion) where T : class, new()
        {
            IRequest<T> request = new Message<T>(Context,ModelActionOption.Get) { Criterion = criterion };
            return Service.Get<T>(request);
        }

        private IResponse<T> ReadList<T>(ICriterion criterion) where T : class, new()
        {
            IRequest<T> request = new Message<T>(Context, ModelActionOption.GetAll) { Criterion = criterion };
            return Service.GetAll<T>(request);
        }

        private IResponse<T> ReadListDisplay<T>(ICriterion criterion) where T : class, new()
        {
            IRequest<T> request = new Message<T>(Context, ModelActionOption.GetAllDisplay) { Criterion = criterion};
            return Service.GetAllDisplay<T>(request);
        }

        private IResponse<U> ExecuteAction<T, U>(T model, ICriterion criterion) where T : class, new()
        {
            IRequest<T> request = new Message<T>(Context,ModelActionOption.ExecuteAction) { Criterion = criterion };
            request.Model = model;
            return Service.ExecuteAction<T, U>(request);
        }

        #endregion

        #region async helpers

        private Task<IResponse<T>> CreateAsync<T>(T t) where T : class, new()
        {
            return Task.Run<IResponse<T>>(() => Create<T>(t));
        }

        private Task<IResponse<T>> UpdateAsync<T>(T t, ICriterion criterion) where T : class, new()
        {
            return Task.Run<IResponse<T>>(() => Update<T>(t,criterion));
        }

        private Task<IResponse<T>> DeleteAsync<T>(ICriterion criterion) where T : class, new()
        {
            return Task.Run<IResponse<T>>(() => Delete<T>(criterion));
        }

        private Task<IResponse<T>> ReadAsync<T>(ICriterion criterion) where T : class, new()
        {
            return Task.Run<IResponse<T>>(() => Delete<T>(criterion));
        }

        private Task<IResponse<T>> ReadListAsync<T>(ICriterion criterion) where T : class, new()
        {
            return Task.Run<IResponse<T>>(() => ReadList<T>(criterion));
        }

        private Task<IResponse<T>> ReadListDisplayAsync<T>(ICriterion criterion) where T : class, new()
        {
            return Task.Run<IResponse<T>>(() => ReadListDisplay<T>(criterion));
        }

        private Task<IResponse<U>> ExecuteActionAsync<T,U>(T model, ICriterion criterion) where T : class, new()
        {
            return Task.Run<IResponse<U>>(() => ExecuteAction<T,U>(model,criterion));
        }

        #endregion

    }
}
