// <copyright file="Switch.cs" company="eXtensoft LLC">
// Copyright © 2014 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [Serializable]
    public class Switch
    {
        [XmlAttribute("criteriaKey")]
        public string CriteriaKey { get; set; }

        [XmlAttribute("dataType")]
        public string DataType { get; set; }

        public List<Case> Cases { get; set; }
    }
}
