// <copyright file="IContext.cs" company="eXtensoft LLC">
// Copyright © 2014 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;

    public interface IContext
    {
        string ApplicationContextKey { get; }
        string UserIdentity { get; }
        IEnumerable<string> Claims { get; }
        string UICulture { get; }
        IEnumerable<TypedItem> TypedItems { get; }

        T GetValue<T>(string key);
        void SetError(int errorCode, string errorMessage);
    
    }
}
