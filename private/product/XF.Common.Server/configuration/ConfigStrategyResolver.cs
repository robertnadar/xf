// <copyright file="ConfigStrategyResolver.cs" company="eXtensoft LLC">
// Copyright © 2014 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Configuration;

    public static class ConfigStrategyResolverLoader
    {
        public static IStrategyResolver Load<T>(string sectionName) where T : eXtensibleStrategyResolver
        {
            KeyPairStrategyResolver resolver = new KeyPairStrategyResolver();
            string filepath = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
            var fileMap = new ExeConfigurationFileMap() { ExeConfigFilename = filepath };
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            eXtensibleStrategySectionGroup group = config.SectionGroups["StrategyResolution"] as eXtensibleStrategySectionGroup;
            if (group != null)
            {
                eXtensibleStrategySection section = group.Sections[sectionName] as eXtensibleStrategySection;
                if (section != null)
                {
                    resolver.Initialize(section);
                    return resolver;
                }
            }
            return new AppSettingsStrategyResolver();
        }
    }
}
