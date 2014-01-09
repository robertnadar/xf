// <copyright file="IListBorrower`1.cs" company="eXtensoft LLC">
// Copyright © 2014 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System.Collections.Generic;

    public interface IListBorrower<T> : IBorrower<List<T>>
    {
    }
}
