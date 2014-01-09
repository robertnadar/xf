// <copyright file="TypeMapContainer.cs" company="eXtensoft LLC">
// Copyright © 2014 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System.Collections.Generic;
    using System.ComponentModel.Composition;

    public sealed class TypeMapContainer
    {

        [ImportMany(typeof(ITypeMap))]
        public List<ITypeMap> TypeMaps { get; set; }
    }
}
