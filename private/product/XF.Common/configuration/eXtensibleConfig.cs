// <copyright file="eXtensibleConfig.cs" company="eXtensoft LLC">
// Copyright © 2014 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;

    public static class eXtensibleConfig
    {
        public static readonly bool Extant;
        public static readonly string Exception;
        public static readonly string Zone;
        public static readonly string Layer;
        public static readonly string Tier;
        public static readonly string InstanceIdentifier;
        public static readonly LoggingStrategyOption LoggingStrategy;
        public static readonly TraceEventTypeOption LoggingSeverity;
        public static readonly string ModelPlugins;
        public static readonly string ArchitecturalPlugins;
        public static readonly string ModelServicesStrategySectionGroupName;
        public static readonly string DataServicesStrategySectionGroupName;
        public static readonly string DataPlugins;
        public static readonly string Context;
        public static readonly bool IsAsync;
        public static readonly string AppUserIdentity;
        public static readonly string CommonServicesData;
        public static readonly string DefaultLoggingCategory;

        private static readonly Dictionary<string, object> _ExtendedProperties = new Dictionary<string,object>();

        static eXtensibleConfig()
        {
            try
            {
                string s = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
                var fileMap = new ExeConfigurationFileMap() { ExeConfigFilename = s };
                Configuration config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);

                //Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                eXtensibleFrameworkSection section = config.Sections[XFConstants.Config.SECTIONNAME] as eXtensibleFrameworkSection;
                if (section != null)
                {
                    Extant = true;
                    Zone = (!String.IsNullOrEmpty(section.Zone)) ? section.Zone : XFConstants.ZONE.DEVELOPMENT;
                    Layer = section.Layer;
                    Tier = section.Tier;
                    ModelPlugins = (!String.IsNullOrEmpty(section.ModelPluginsFolderpath)) ? section.ModelPluginsFolderpath : XFConstants.Config.MODELS;
                    ArchitecturalPlugins = (!String.IsNullOrEmpty(section.ArchitecturalPluginsFolderpath)) ? section.ArchitecturalPluginsFolderpath : XFConstants.Config.ARCHITECTURALPLUGINS;
                    DataPlugins = (!String.IsNullOrEmpty(section.DataPluginsFolderpath)) ? section.DataPluginsFolderpath : XFConstants.Config.DATAACCESSCONTEXTFOLDERPATH;
                    Context = (!String.IsNullOrEmpty(section.Context)) ? section.Context : XFConstants.Config.DEFAULTCONTEXT;
                    IsAsync = section.IsAsync;
                    ModelServicesStrategySectionGroupName = (!String.IsNullOrEmpty(section.ModelServiceStrategySectionGroupName)) ? section.ModelServiceStrategySectionGroupName : XFConstants.Config.FRAMEWORKSTRATEGYGROUPNAME;
                    DataServicesStrategySectionGroupName = (!String.IsNullOrEmpty(section.DataServiceStrategySectionGroupName)) ? section.DataServiceStrategySectionGroupName : XFConstants.Config.FRAMEWORKSTRATEGYGROUPNAME;
                    InstanceIdentifier = (section.InstanceIdentifier != null) ? section.InstanceIdentifier : String.Empty;
                    AppUserIdentity = (!String.IsNullOrEmpty(section.UserIdentityParamName)) ? section.UserIdentityParamName : XFConstants.Config.APPUSERIDENTITYPARAMNAME;
                    CommonServicesData = (!String.IsNullOrEmpty(section.CommonServicesData)) ? section.CommonServicesData : XFConstants.Config.COMMONSERVICESDATAFOLDERPATH;
                    DefaultLoggingCategory = (!String.IsNullOrEmpty(section.DefaultLoggingCategory)) ? section.DefaultLoggingCategory : XFConstants.Category.GENERAL;
                    var found = section.Elements.GetForLoggingMode(section.LoggingStrategyKey);
                    LoggingStrategyOption option;
                    if (Enum.TryParse<LoggingStrategyOption>(found.LoggingStrategy, true, out option))
                    {
                        LoggingStrategy = option;
                    }
                    else
                    {
                        LoggingStrategy = LoggingStrategyOption.DevTool;
                    }
                    TraceEventTypeOption severity;
                    if (Enum.TryParse<TraceEventTypeOption>(found.PublishSeverity, true, out severity))
                    {
                        LoggingSeverity = severity;
                    }
                    else
                    {
                        LoggingSeverity = TraceEventTypeOption.Verbose;
                    }
                }
                else
                {
                    section = new eXtensibleFrameworkSection();
                    Zone = section.Zone = XFConstants.ZONE.DEVELOPMENT;
                    ModelPlugins = section.ModelPluginsFolderpath = XFConstants.Config.MODELS;
                    ArchitecturalPlugins = section.ArchitecturalPluginsFolderpath = XFConstants.Config.ARCHITECTURALPLUGINS;
                    DataPlugins = section.DataPluginsFolderpath = XFConstants.Config.DATAACCESSCONTEXTFOLDERPATH;
                    Context = section.Context = XFConstants.Config.DEFAULTCONTEXT;
                    IsAsync = section.IsAsync = false;
                    ModelServicesStrategySectionGroupName = section.ModelServiceStrategySectionGroupName = XFConstants.Config.FRAMEWORKSTRATEGYGROUPNAME;
                    DataServicesStrategySectionGroupName = section.DataServiceStrategySectionGroupName = XFConstants.Config.FRAMEWORKSTRATEGYGROUPNAME;
                    InstanceIdentifier = section.InstanceIdentifier = String.Empty;
                    AppUserIdentity = section.UserIdentityParamName = XFConstants.Config.APPUSERIDENTITYPARAMNAME;
                    CommonServicesData = section.CommonServicesData = XFConstants.Config.COMMONSERVICESDATAFOLDERPATH;
                    DefaultLoggingCategory = XFConstants.Category.GENERAL;
                    var found = section.Elements.GetForLoggingMode(section.LoggingStrategyKey);
                    LoggingStrategy = LoggingStrategyOption.DevTool;
                    LoggingSeverity = TraceEventTypeOption.Verbose;
                }
            }
            catch (Exception ex)
            {
                Exception = ex.Message;
                Zone = XFConstants.Config.DEFAULTZONE;
                Layer = XFConstants.Config.DEFAULTLAYER;
                Tier = XFConstants.Config.DEFAULTTIER;
                ModelPlugins = String.Empty;
                ArchitecturalPlugins = String.Empty;
                DataPlugins = XFConstants.Config.DATAACCESSCONTEXTFOLDERPATH;
                DefaultLoggingCategory = XFConstants.Category.GENERAL;
                LoggingStrategyOption option;
                if (Enum.TryParse<LoggingStrategyOption>(XFConstants.Config.DEFAULTLOGGINGSTRATEGY, true, out option))
                {
                    LoggingStrategy = option;
                }
                else
                {
                    LoggingStrategy = LoggingStrategyOption.DevTool;
                }
                TraceEventTypeOption severity;
                if (Enum.TryParse<TraceEventTypeOption>(XFConstants.Config.DEFAULTPUBLISHSEVERITY, true, out severity))
                {
                    LoggingSeverity = severity;
                }
                else
                {
                    LoggingSeverity = TraceEventTypeOption.Verbose;
                }
            }
            _ExtendedProperties.Add(XFConstants.Context.ZONE, Zone);
            _ExtendedProperties.Add(XFConstants.Context.LAYER, Layer);
            _ExtendedProperties.Add(XFConstants.Context.TIER, Tier);
        }

        public static bool IsSeverityAtLeast(TraceEventTypeOption option)
        {
            return LoggingSeverity >= option;
        }

        public static bool CaptureMetrics()
        {
            return true;
        }

        public static Dictionary<string, object> GetProperties<T>(IEnumerable<TypedItem> items, string action, string codeModule, string codeClass, string codeLine)
        {
            var extendedproperties = GetProperties<T>(items, action);
            extendedproperties.Add(XFConstants.Context.MODULE, codeModule);
            extendedproperties.Add(XFConstants.Context.CLASS, codeClass);
            extendedproperties.Add(XFConstants.Context.LINE, codeLine);
            return extendedproperties;
        }

        public static Dictionary<string, object> GetProperties<T>(IEnumerable<TypedItem> items, string action)
        {
            T t = Activator.CreateInstance<T>();
            string model = t.GetType().FullName;
            Dictionary<string, object> extendedproperties = GetProperties(items);
            extendedproperties.Add("Model", model);
            extendedproperties.Add("Action", action);
            return extendedproperties;
        }

        public static Dictionary<string, object> GetProperties(IEnumerable<TypedItem> items)
        {
            Dictionary<string, object> extendedproperties = new Dictionary<string, object>();
            foreach (var item in items)
            {
                extendedproperties.Add(item.Key, item.Value);
            }
            foreach (var item in _ExtendedProperties)
            {
                if (!extendedproperties.ContainsKey(item.Key))
                {
                    extendedproperties.Add(item.Key, item.Value);
                }
            }
            return extendedproperties;
        }

        public static Dictionary<string, object> GetProperties()
        {
            Dictionary<string, object> extendedproperties = new Dictionary<string, object>();
            foreach (var item in _ExtendedProperties)
            {
                extendedproperties.Add(item.Key, item.Value);
            }
            return extendedproperties;
        }

        public static Dictionary<string, object> GetProperties(string codeModule, string codeClass, string codeLine)
        {
            var extendedproperties = GetProperties();
            extendedproperties.Add(XFConstants.Context.MODULE, codeModule);
            extendedproperties.Add(XFConstants.Context.CLASS, codeClass);
            extendedproperties.Add(XFConstants.Context.LINE, codeLine);
            return extendedproperties;
        }
    }
}