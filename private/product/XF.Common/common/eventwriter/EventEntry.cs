// <copyright company="eXtensoft LLC" file="EventEntry.cs">
// Copyright © 2014 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class EventEntry : Entry
    {
        private EventTypeOption _EventType = EventTypeOption.Event;
        public override EventTypeOption EventType
        {
            get
            {
                return _EventType;
            }
        }



    }

}
