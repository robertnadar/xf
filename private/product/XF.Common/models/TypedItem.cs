// <copyright file="TypedItem.cs" company="eXtensoft LLC">
// Copyright © 2014 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;

    [Serializable]
    public class TypedItem
    {
        [XmlAttribute("key")]
        public string Key { get; set; }

        [XmlAttribute("domain")]
        public string Domain { get; set; }

        [XmlAttribute("scope")]
        public string Scope { get; set; }

        [XmlAttribute("tds")]
        public DateTimeOffset Tds { get; set; }

        [XmlAttribute("txt")]
        public string Text { get; set; }

        [XmlAttribute("op")]
        public OperatorTypeOption Operator { get; set; }

        public object Value { get; set; }



        #region constructors

        public TypedItem()
        {
        }

        public TypedItem(string key, object value)
        :this(key,value, OperatorTypeOption.EqualTo){}

        public TypedItem(string key, object value, OperatorTypeOption op)
        {
            Key = key;
            Value = value;
            Tds = DateTimeOffset.Now;
            Operator = op;
        }

        #endregion constructors

        public override string ToString()
        {
            string valueString = Value != null ? Value.ToString() : "{x:Null}";
            return String.Format("{0}<{1}> : {2}", Key, Value.GetType().Name, valueString);
        }

    }
}
