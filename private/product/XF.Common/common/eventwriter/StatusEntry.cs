// <copyright company="eXtensoft LLC" file="StatusEntry.cs">
// Copyright © 2014 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class StatusEntry : Entry
    {
        private EventTypeOption _EventType = EventTypeOption.Status;
        public override EventTypeOption EventType
        {
            get
            {
                return _EventType;
            }
        }

        public DateTimeOffset Effective { get; set; }
    }

}
