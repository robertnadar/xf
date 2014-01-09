﻿// <copyright file="ModelAction.cs" company="eXtensoft LLC">
// Copyright © 2014 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [Serializable]
    public class ModelAction
    {
        [XmlAttribute("verb")]
        public ModelActionOption Verb { get; set; }

        [XmlAttribute("strategy")]
        public StrategyOption Strategy { get; set; }

        [XmlAttribute("sqlCommandKey")]
        public string SqlCommandKey { get; set; }

        public List<Switch> Switches { get; set; }
    }
}
