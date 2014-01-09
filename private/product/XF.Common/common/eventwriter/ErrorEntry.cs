// <copyright company="eXtensoft LLC" file="ErrorEntry.cs">
// Copyright © 2014 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ErrorEntry : Entry
    {
        private EventTypeOption _EventType = EventTypeOption.Error;
        public override EventTypeOption EventType
        {
            get
            {
                return _EventType;
            }
        }
        public SeverityType Severity { get; set; }

    }

}
