// <copyright file="ITypeMap.cs" company="eXtensoft LLC">
// Copyright © 2014 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;

    public interface ITypeMap
    {
        string Domain { get; }

        Type KeyType { get; }

        Type TypeResolution { get; }
    }
}
