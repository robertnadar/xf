// <copyright file="Case.cs" company="eXtensoft LLC">
// Copyright © 2014 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    public class Case
    {
        [XmlAttribute("value")]
        public string Value { get; set; }

        [XmlAttribute("sqlCommandKey")]
        public string SqlCommandKey { get; set; }
    }
}
