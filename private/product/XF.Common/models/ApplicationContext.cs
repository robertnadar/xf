// <copyright file="ApplicationContext.cs" company="eXtensoft LLC">
// Copyright © 2014 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;
    using System.Threading.Tasks;

    public class ApplicationContext : IContext
    {
        private string _ApplicationContextKey = XFConstants.Context.DefaultApplication;
        string IContext.ApplicationContextKey
        {
            get { return _ApplicationContextKey; }
        }

        private string _UserIdentity = String.Empty;
        string IContext.UserIdentity
        {
            get { return _UserIdentity; }
        }

        private List<string> _Claims = new List<string>();
        IEnumerable<string> IContext.Claims
        {
            get { return _Claims; }
        }

        private string _UICulture = String.Empty;
        string IContext.UICulture
        {
            get { return _UICulture; }
        }

        T IContext.GetValue<T>(string key)
        {
            throw new NotImplementedException();
        }

        private List<TypedItem> _Items = new List<TypedItem>();
        IEnumerable<TypedItem> IContext.TypedItems
        {
            get { return _Items; }
        }

        
        void IContext.SetError(int errorCode, string errorMessage)
        {
            throw new NotImplementedException();
        }

        public ApplicationContext(string applicationContextKey, string instanceId) 
        {
            _ApplicationContextKey = applicationContextKey;
        }

        public ApplicationContext(string applicationContextKey,  string uiCulture, string userIdentity)
        {
            _ApplicationContextKey = applicationContextKey;
            _UICulture = uiCulture;
            _UserIdentity = userIdentity;
        }


        public ApplicationContext Prototype()
        {
            ApplicationContext ctx = new ApplicationContext(_ApplicationContextKey, _UICulture, _UserIdentity);
            return ctx;
        }
    }
}
