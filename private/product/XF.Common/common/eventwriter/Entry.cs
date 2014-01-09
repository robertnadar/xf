// <copyright company="eXtensoft LLC" file="Entry.cs">
// Copyright © 2014 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public abstract class Entry
    {
        public abstract EventTypeOption EventType { get; }

        public string Category { get; set; }

        public IDictionary<string, object> Properties { get; set; }
    }

}
