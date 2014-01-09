// <copyright file="ITypeMapCache.cs" company="eXtensoft LLC">
// Copyright © 2014 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface ITypeMapCache
    {
        void Initialize();
        Type ResoveType<T>() where T : class, new();

    }
}
