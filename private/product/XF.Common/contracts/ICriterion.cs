// <copyright file="ICriterion.cs" company="eXtensoft LLC">
// Copyright © 2014 All Right Reserved
// </copyright>


namespace XF.Common
{
    using System.Collections.Generic;

    public interface ICriterion
    {
        T GetValue<T>(string key);

        IEnumerable<TypedItem> Items { get; }
    }
}
