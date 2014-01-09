// <copyright file="KeyPairStrategyResolver.cs" company="eXtensoft LLC">
// Copyright © 2014 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;

    public sealed class KeyPairStrategyResolver : eXtensibleStrategyResolver, IStrategyResolver
    {
        string IStrategyResolver.Resolve(params string[] args)
        {
            string resolution = string.Empty;
            if (args.Length == 2)
            {
                resolution = LocalResolve(args[0], args[1]);
            }
            else
            {
                resolution = LocalResolve(args[0], null);
            }
            return resolution;
        }

        private string LocalResolve(string primaryKey, string secondaryKey)
        {
            string resolution = string.Empty;
            List<eXtensibleStrategyElement> list = Strategies.GetForStrategyType(StrategyTypeOption.KeyPair);
            if (!string.IsNullOrEmpty(secondaryKey))
            {
                var found = list.Find(x => x.ParamDomain.Equals(secondaryKey, StringComparison.OrdinalIgnoreCase) & x.ParamKey.Equals(primaryKey, StringComparison.OrdinalIgnoreCase));
                if (found != null)
                {
                    resolution = found.Resolution;
                }
            }
            else if (!string.IsNullOrEmpty(primaryKey))
            {
                var found = list.Find(x => x.ParamKey.Equals(primaryKey, StringComparison.OrdinalIgnoreCase));
                if (found != null)
                {
                    resolution = found.Resolution;
                }
            }
            return resolution;
        }
    }
}
