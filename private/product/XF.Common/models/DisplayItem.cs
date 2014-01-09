// <copyright file="DisplayItem.cs" company="eXtensoft LLC">
// Copyright © 2014 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;
    using System.Threading.Tasks;

    [Serializable]
    [DataContract(Namespace = "http://eXtensibleSolutions/schemas/2014/04")]
    public class DisplayItem : IDisplay
    {

        #region properties

        [DataMember]
        public string ItemId { get; set; }

        [DataMember]
        public string ItemDisplay { get; set; }

        [DataMember]
        public string ItemDisplayAlt { get; set; }

        [DataMember]
        public string Typename { get; set; }

        [DataMember]
        public string Group { get; set; }

        [DataMember]
        public int IntVal { get; set; }

        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public bool IsSelected { get; set; }

        [DataMember]
        public string Uri { get; set; }

        [DataMember]
        public IEnumerable<TypedItem> Items { get; set; }

        #endregion properties


    }
}
