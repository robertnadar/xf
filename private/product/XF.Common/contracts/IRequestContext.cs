// <copyright file="IRequestContext.cs" company="eXtensoft LLC">
// Copyright © 2014 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IRequestContext : IContext
    {
        string InstanceIdentifier { get; }
        void SetMetric(string scope, string key, object value);
        bool HasError();
    }

}
