// <copyright file="Message_1.cs" company="eXtensoft LLC">
// Copyright © 2014 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Message<T> : IMessage<T>, IRequest<T>, IResponse<T>, IRequestContext where T : class, new()
    {
        #region properties

        private List<T> _Content = new List<T>();
        public List<T> Content
        {
            get { return _Content; }
            set { _Content = value; }
        }

        private List<TypedItem> _Items = new List<TypedItem>();
        public List<TypedItem> Items
        {
            get { return _Items; }
            set { _Items = value; }

        }

        public ICriterion Criterion { get; set; }

        public IEnumerable<IDisplay> Display { get; set; }

        public object ActionResult { get; set; }

        #endregion

        #region interface implementations

        IEnumerable<TypedItem> IMessage<T>.Context
        {
            get
            {
                return _Items.Where(x => x.Scope.Equals(XFConstants.Message.Context));
            }
            set
            {
                _Items.RemoveAll(x => x.Scope.Equals(XFConstants.Message.Context));
                foreach (var item in value)
                {
                    item.Scope = XFConstants.Message.Context;
                    _Items.Add(item);
                }
            }
        }

        string IRequest<T>.Verb
        {
            get
            {
                var found = _Items.FirstOrDefault(x => x.Key.Equals(XFConstants.Message.Verb));
                return (found != null) ? found.Value.ToString() : XFConstants.Message.Verbs.None;
            }
            set
            {
                var found = _Items.FirstOrDefault(x => x.Key.Equals(XFConstants.Message.Verb));
                if (found != null)
                {
                    _Items.Remove(found);
                }
                _Items.Add(new TypedItem() { Key = XFConstants.Message.Verb, Value = value });
            }
        }

        T IRequest<T>.Model
        {
            get
            {
                return Content.FirstOrDefault();
            }
            set
            {
                Content.Add(value);
            }
        }

        IEnumerable<T> IRequest<T>.Content
        {
            get
            {
                return _Content;
            }
            set
            {
                if (value != null)
                {
                    _Content = value.ToList();
                }                
            }
        }

        bool IResponse<T>.IsOkay
        {
            get
            {
                var found = _Items.FirstOrDefault(x => x.Key.Equals(XFConstants.Message.RequestStatus));
                return (found != null && (int)found.Value > 399) ? false : true;
            }
        }

        T IResponse<T>.Model
        {
            get { return Content.FirstOrDefault(); }
        }

        RequestStatus IResponse<T>.Status
        {
            get
            {
                var found = _Items.FirstOrDefault(x => x.Key.Equals(XFConstants.Message.RequestStatus));
                return (found != null) ? new RequestStatus() { Code = (int)found.Value, Description = found.Text } : new RequestStatus() { Code = 500, Description = RequestStatii.Http500 };

            }
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            foreach (T t in Content)
            {
                yield return t;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region local interface implementations



        #endregion

        #region constructors


        public Message(IContext context, ModelActionOption modelActionOption)
        {
            Items.Add(new TypedItem(XFConstants.Message.Verb, XFConstants.Message.VerbConstList[modelActionOption]));
            Items.Add(new TypedItem(XFConstants.Context.Application, context.ApplicationContextKey) { Domain = XFConstants.Domain.Context });
            Items.Add(new TypedItem(XFConstants.Context.USERIDENTITY, context.UserIdentity) { Domain = XFConstants.Domain.Context });
            Items.Add(new TypedItem(XFConstants.Context.UICULTURE, context.UICulture) { Domain = XFConstants.Domain.Context });
            if (context.Claims != null && context.Claims.Count() > 0)
            {
                int i = 1;
                foreach (var item in context.Claims)
                {
                    Items.Add(new TypedItem(String.Format("{0}.{1}", XFConstants.Context.Claim, i++), item) 
                    { 
                        Domain = XFConstants.Domain.Claims 
                    });
                }
            }
            Items.Add(new TypedItem(XFConstants.Context.INSTANCEIDENTIFIER, Guid.NewGuid()) { Domain = XFConstants.Domain.Context });
            Items.Add(new TypedItem(XFConstants.Context.RequestBegin, DateTimeOffset.Now) { Domain = XFConstants.Domain.Metrics });
        }

        #endregion

        string IMessage<T>.ModelTypename
        {
            get { return GetType().FullName; }
        }


        string IMessage<T>.Verb
        {
            get 
            {
                var found = _Items.FirstOrDefault(x => x.Key.Equals(XFConstants.Message.Verb));
                return (found != null) ? found.Value.ToString() : XFConstants.Message.Verbs.None;
            }
        }

        string IContext.ApplicationContextKey
        {
            get 
            {
                var found = _Items.Find(x => x.Key.Equals(XFConstants.Context.Application));
                return (found != null) ? found.Value.ToString() : XFConstants.Context.DefaultApplication;

            }
        }

        string IContext.UserIdentity
        {
            get {
                var found = _Items.Find(x => x.Key.Equals(XFConstants.Context.USERIDENTITY));
                return (found != null) ? found.Value.ToString() : XFConstants.Context.USERIDENTITY;         
            }
        }

        string IRequestContext.InstanceIdentifier
        {
            get {
                var found = _Items.Find(x => x.Key.Equals(XFConstants.Context.INSTANCEIDENTIFIER));
                return (found != null) ? found.Value.ToString() : XFConstants.Context.INSTANCEIDENTIFIER;            
            }
        }

        IEnumerable<string> IContext.Claims
        {
            get { throw new NotImplementedException(); }
        }

        string IContext.UICulture
        {
            get {
                var found = _Items.Find(x => x.Key.Equals(XFConstants.Context.UICULTURE));
                return (found != null) ? found.Value.ToString() : XFConstants.Context.UICULTURE;            
            }
        }

        bool IRequestContext.HasError()
        {
            bool b = false;
            var found = this._Items.Find(x=>x.Key.Equals(XFConstants.Message.RequestStatus));
            if (found != null)
            {
                b = true;
            }
            return b;
        }


        public void SetError(int code, string message)
        {
            this._Items.Add(new TypedItem() { Key = XFConstants.Message.RequestStatus, Value = code, Text = message, Tds = DateTimeOffset.Now });
        }

        void IRequestContext.SetMetric( string scope, string key, object value)
        {
            _Items.Add(new TypedItem(key, value) { Domain = XFConstants.Domain.Metrics, Scope = scope });
        }


        U IContext.GetValue<U>(string key)
        {
            U u = default(U);
            bool b = false;

            if (Items != null)
            {
                for (int i = 0; !b && i < Items.Count; i++)
                {
                    if (Items[i].Key.Equals(key, StringComparison.OrdinalIgnoreCase))
                    {
                        TypedItem item = Items.Single(x => x.Key.Equals(key, StringComparison.OrdinalIgnoreCase));

                        if (item != null)
                        {
                            u = (U)item.Value;
                        }
                        b = true;
                    }
                }
            }
            return u;           
        }

        IEnumerable<TypedItem> IContext.TypedItems
        {
            get { return Items; }
        }


        public override string ToString()
        {
            var found = _Items.FirstOrDefault(x => x.Key.Equals(XFConstants.Message.Verb));
            string verb = (found != null) ? found.Value.ToString() : XFConstants.Message.Verbs.None;
            StringBuilder sb = new StringBuilder();
            sb.Append(String.Format("Message<{0}>.{1}", GetModelType().FullName, XFConstants.Message.ConstVerbList[verb]));
            foreach (var item in Items)
            {
                sb.AppendLine(String.Format("{0}.{1}.{2}", item.Domain, item.Key, item.Value.ToString()));
            }
            return sb.ToString();
        }

        private Type GetModelType()
        {
            T t = Activator.CreateInstance<T>();
            return t.GetType();
        }


    }
}
