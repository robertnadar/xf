// <copyright file="ModelRequestService.cs" company="eXtensoft LLC">
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

    public class ModelRequestService
    {
        #region Context

        private IContext _Context = null;
        protected IContext Context
        {
            get
            {

                if (_Context == null)
                {
                    string key = (!String.IsNullOrWhiteSpace(eXtensibleConfig.Context)) ? eXtensibleConfig.Context : XFConstants.Context.DefaultApplication;
                    string instanceid = (!String.IsNullOrWhiteSpace(eXtensibleConfig.InstanceIdentifier)) ? eXtensibleConfig.InstanceIdentifier : String.Empty;

                    _Context = new ApplicationContext(key,IdentityProvider.UICulture,IdentityProvider.Username);   
                }
                ApplicationContext ctx = _Context as ApplicationContext;
                if (ctx != null)
                {
                    return ctx.Prototype();
                }
                return _Context;
            }
        }

        #endregion Context

        #region UserIdentityProvider
        private IUserIdentityProvider _IdentityProvider;
        public IUserIdentityProvider IdentityProvider 
        {
            get
            {
                if (_IdentityProvider == null)
                {
                    _IdentityProvider = new WindowsIdentityProvider();
                }
                return _IdentityProvider;
            }
            set { _IdentityProvider = value; } 
        }

        #endregion UserIdentityProvider



    }
}
