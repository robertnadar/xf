// <copyright file="eXtensibleStrategyElement.cs" company="eXtensoft LLC">
// Copyright (c) 2014 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System.Configuration;

    public sealed class eXtensibleStrategyElement : ConfigurationElement
    {
        public eXtensibleStrategyElement()
        {
        }

        [ConfigurationProperty(ConfigConstants.StrategyNameAttributeName, IsRequired = true)]
        public string Name
        {
            get { return (string)this[ConfigConstants.StrategyNameAttributeName]; }
            set { this[ConfigConstants.StrategyNameAttributeName] = value; }
        }

        [ConfigurationProperty(ConfigConstants.StrategyTypeAttributeName, IsRequired = true, DefaultValue = StrategyTypeOption.Key)]
        public StrategyTypeOption StrategyType
        {
            get { return (StrategyTypeOption)this[ConfigConstants.StrategyTypeAttributeName]; }
            set { this[ConfigConstants.StrategyTypeAttributeName] = value.ToString(); }
        }

        [ConfigurationProperty(ConfigConstants.StrategyValueAttributeName, IsRequired = false)]
        public string StrategyValue
        {
            get { return (string)this[ConfigConstants.StrategyValueAttributeName]; }
            set { this[ConfigConstants.StrategyValueAttributeName] = value; }
        }

        [ConfigurationProperty(ConfigConstants.StrategyParamKeyAttributeName, IsRequired = false)]
        public string ParamKey
        {
            get { return (string)this[ConfigConstants.StrategyParamKeyAttributeName]; }
            set { this[ConfigConstants.StrategyParamKeyAttributeName] = value; }
        }

        [ConfigurationProperty(ConfigConstants.StrategyParamDomainAttributeName, IsRequired = false)]
        public string ParamDomain
        {
            get { return (string)this[ConfigConstants.StrategyParamDomainAttributeName]; }
            set { this[ConfigConstants.StrategyParamDomainAttributeName] = value; }
        }

        [ConfigurationProperty(ConfigConstants.StrategyParamValueAttributeName, IsRequired = false)]
        public string ParamValue
        {
            get { return (string)this[ConfigConstants.StrategyParamValueAttributeName]; }
            set { this[ConfigConstants.StrategyParamValueAttributeName] = value; }
        }

        [ConfigurationProperty(ConfigConstants.StrategyResolutionAttributeName, IsRequired = true)]
        public string Resolution
        {
            get { return (string)this[ConfigConstants.StrategyResolutionAttributeName]; }
            set { this[ConfigConstants.StrategyResolutionAttributeName] = value; }
        }
    }
}