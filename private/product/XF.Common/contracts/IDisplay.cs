// <copyright file="IDisplay.cs" company="eXtensoft LLC">
// Copyright © 2014 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IDisplay
    {
        string ItemId { get; }
        string ItemDisplay { get; }
        string ItemDisplayAlt { get; }
        string Uri { get; }
        string Typename { get; }
        IEnumerable<TypedItem> Items { get; }

    }
}
