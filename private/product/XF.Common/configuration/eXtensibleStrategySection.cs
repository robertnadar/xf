// <copyright file="eXtensibleStrategySection.cs" company="eXtensoft LLC">
// Copyright (c) 2014 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System.Configuration;

    public sealed class eXtensibleStrategySection : ConfigurationSection
    {
        [ConfigurationProperty(ConfigConstants.ContextAttributeName, IsRequired = true)]
        public string Context
        {
            get { return (string)this[ConfigConstants.ContextAttributeName]; }
            set { this[ConfigConstants.ContextAttributeName] = value; }
        }

        [ConfigurationProperty(ConfigConstants.ResolverTypeAttributeName, IsRequired = true, DefaultValue = "default")]
        public string ResolverType
        {
            get { return (string)this[ConfigConstants.ResolverTypeAttributeName]; }
            set { this[ConfigConstants.ResolverTypeAttributeName] = value; }
        }

        [ConfigurationProperty(ConfigConstants.StrategyCollectionAttributeName, IsRequired = true)]
        public eXtensibleStrategyElementCollection Strategies
        {
            get { return this[ConfigConstants.StrategyCollectionAttributeName] as eXtensibleStrategyElementCollection ?? new eXtensibleStrategyElementCollection(); }
        }
    }
}