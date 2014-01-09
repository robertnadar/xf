﻿// <copyright file="RequestStatus.cs" company="eXtensoft LLC">
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
    public class RequestStatus
    {
        [XmlAttribute("code")]
        public int Code { get; set; }

        [XmlAttribute("desc")]
        public string Description { get; set; }

    }
}
