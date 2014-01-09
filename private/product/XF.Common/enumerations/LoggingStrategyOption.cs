// <copyright file="LoggingStrategyOption.cs" company="eXtensoft LLC">
// Copyright © 2014 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;

    public enum LoggingStrategyOption
    {
        None,
        Console,
        ReferencedAssembly,
        Plugin,
        Silent,
        DevTool,
        CommonServices,
        WindowsEventLog
    }
}
