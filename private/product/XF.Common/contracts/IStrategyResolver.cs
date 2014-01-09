// <copyright file="IStrategyResolver.cs" company="eXtensoft LLC">
// Copyright © 2014 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;

    public interface IStrategyResolver
    {
        string Resolve(params string[] args);
    }
}
