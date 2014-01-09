﻿// <copyright file="Model.cs" company="eXtensoft LLC">
// Copyright © 2014 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [Serializable]
    public class Model
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("clrType")]
        public string modelType { get; set; }

        [XmlAttribute("dbSchema")]
        public string dbSchema { get; set; }

        [XmlElement("ActionExecutorType")]
        public string actionExecutorType { get; set; }

        [XmlElement("CustomType")]
        public string customType { get; set; }

        [XmlElement("ModelDataGatewayType")]
        public string modelDataGatewayType { get; set; }

        public List<ModelAction> ModelActions { get; set; }

        public List<SQLCommand> SqlCommands { get; set; }

        public List<DataMap> DataMaps { get; set; }
    }
}
