// <copyright file="CacheStrategyLoader.cs" company="eXtensoft LLC">
// Copyright © 2014 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;

    public static class CacheStrategyLoader
    {
        public static ICache Load()
        {
            // make this pluggable
            // based upon configuration load desired caching strategy: {Memcached, NetMemoryCache, CustomCache, etc)
            return new NetMemoryCache();
        }
    }
}
