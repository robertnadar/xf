// <copyright file="ICacheable`1.cs" company="eXtensoft LLC">
// Copyright © 2014 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;

    public interface ICacheable<T> where T : class, new()
    {
        ICache Cache { set; }
    }
}
