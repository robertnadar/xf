// <copyright file="Parameter.cs" company="eXtensoft LLC">
// Copyright © 2014 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Data;
    using System.Xml.Serialization;

    [Serializable]
    public class Parameter
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("dataType")]
        public DbType DataType { get; set; }

        [XmlAttribute("mode")]
        public string Mode { get; set; }

        [XmlAttribute("target")]
        public string Target { get; set; }
    }
}
