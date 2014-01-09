// <copyright file="eXtensibleFrameworkSection.cs" company="eXtensoft LLC">
// Copyright © 2014 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System.Configuration;

    public sealed class eXtensibleFrameworkSection : ConfigurationSection
    {
       
        [ConfigurationProperty("zone", IsRequired = true)]
        public string Zone
        {
            get { return (string)this["zone"]; }
            set { this["zone"] = value; }
        }

        [ConfigurationProperty("loggingStrategyKey", IsRequired = true)]
        public string LoggingStrategyKey
        {
            get { return (string)this["loggingStrategyKey"]; }
            set { this["loggingStrategyKey"] = value; }
        }

        [ConfigurationProperty("architecturalTier", IsRequired = true)]
        public string Tier
        {
            get { return (string)this["architecturalTier"]; }
            set { this["architecturalTier"] = value; }
        }

        [ConfigurationProperty("architecturalLayer", IsRequired = true)]
        public string Layer
        {
            get { return (string)this["architecturalLayer"]; }
            set { this["architecturalLayer"] = value; }
        }

        [ConfigurationProperty("context", IsRequired = false)]
        public string Context
        {
            get { return (string)this["context"]; }
            set { this["context"] = value; }
        }

        [ConfigurationProperty("instanceIdentifier", IsRequired = false)]
        public string InstanceIdentifier
        {
            get { return (string)this["instanceIdentifier"]; }
            set { this["instanceIdentifier"] = value; }
        }

        [ConfigurationProperty("architecturalPluginsFolderpath", IsRequired = false)]
        public string ArchitecturalPluginsFolderpath
        {
            get { return (string)this["architecturalPluginsFolderpath"]; }
            set { this["architecturalPluginsFolderpath"] = value; }
        }

        [ConfigurationProperty("modelPluginsFolderpath", IsRequired = false)]
        public string ModelPluginsFolderpath
        {
            get { return (string)this["modelPluginsFolderpath"]; }
            set { this["modelPluginsFolderpath"] = value; }
        }

        [ConfigurationProperty("dataPluginsFolderpath", IsRequired = false)]
        public string DataPluginsFolderpath
        {
            get { return (string)this["dataPluginsFolderpath"]; }
            set { this["dataPluginsFolderpath"] = value; }
        }

        [ConfigurationProperty("modelServiceStrategySectionGroupName", IsRequired = false)]
        public string ModelServiceStrategySectionGroupName
        {
            get { return (string)this["modelServiceStrategySectionGroupName"]; }
            set { this["modelServiceStrategySectionGroupName"] = value; }
        }

        [ConfigurationProperty("dataServiceStrategySectionGroupName", IsRequired = false)]
        public string DataServiceStrategySectionGroupName
        {
            get { return (string)this["dataServiceStrategySectionGroupName"]; }
            set { this["dataServiceStrategySectionGroupName"] = value; }
        }

        [ConfigurationProperty("isAsync", IsRequired = false)]
        public bool IsAsync
        {
            get { return (bool)this["isAsync"]; }
            set { this["isAsync"] = value; }
        }

        [ConfigurationProperty("captureMetrics", IsRequired = false)]
        public bool CaptureMetrics
        {
            get { return (bool)this["captureMetrics"]; }
            set { this["captureMetrics"] = value; }
        }

        [ConfigurationProperty("userIdentityParamName", IsRequired = false)]
        public string UserIdentityParamName
        {
            get { return (string)this["userIdentityParamName"]; }
            set { this["userIdentityParamName"] = value; }
        }

        [ConfigurationProperty("elements", IsRequired = true)]
        public eXtensibleFrameworkElementCollection Elements
        {
            get { return this["elements"] as eXtensibleFrameworkElementCollection ?? new eXtensibleFrameworkElementCollection(); }
        }

        [ConfigurationProperty("commonServicesData", IsRequired = false)]
        public string CommonServicesData
        {
            get { return (string)this["commonServicesData"]; }
            set { this["commonServicesData"] = value; }
        }

        [ConfigurationProperty("defaultLoggingCategory", IsRequired = false)]
        public string DefaultLoggingCategory
        {
            get { return (string)this["defaultLoggingCategory"]; }
            set { this["defaultLoggingCategory"] = value; }
        }

        [ConfigurationProperty("rpcPlugins", IsRequired = false)]
        public string RpcPluginsFolderpath
        {
            get { return (string)this["rpcPlugins"]; }
            set { this["rpcPlugins"] = value; }
        }
    }
}