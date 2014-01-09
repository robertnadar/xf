// <copyright file="eXtensibleFrameworkElement.cs" company="eXtensoft LLC">
// Copyright © 2014 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System.Configuration;

    public sealed class eXtensibleFrameworkElement : ConfigurationElement
    {
        public eXtensibleFrameworkElement()
        {
        }

        [ConfigurationProperty("key", IsRequired = true)]
        public string Key
        {
            get { return (string)this["key"]; }
            set { this["key"] = value; }
        }

        [ConfigurationProperty("loggingStrategy", IsRequired = true)]
        public string LoggingStrategy
        {
            get { return (string)this["loggingStrategy"]; }
            set { this["loggingStrategy"] = value; }
        }

        [ConfigurationProperty("publishSeverity", IsRequired = true)]
        public string PublishSeverity
        {
            get { return (string)this["publishSeverity"]; }
            set
            {
                this["publishSeverity"] = value;
            }
        }
    }
}