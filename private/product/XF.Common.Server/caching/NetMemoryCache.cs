﻿// <copyright file="NetMemoryCache.cs" company="eXtensoft LLC">
// Copyright © 2014 All Right Reserved
// </copyright>

namespace XF.Caching
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Caching;
    using System.Text;
    using System.Threading.Tasks;
    using XF.Common;

    public sealed class NetMemoryCache : ICache
    {
        #region local fields

        private MemoryCache _Cache;

        #endregion local fields

        #region constructors

        public NetMemoryCache()
        {
            _Cache = MemoryCache.Default;
        }

        #endregion constructors

        public bool Store(string key, object item)
        {
            return _Cache.Add(new CacheItem(key, item), new CacheItemPolicy()
            {
                Priority = CacheItemPriority.Default,
                SlidingExpiration = TimeSpan.FromMinutes(5)
            });
        }

        public bool Store(string key, object item, TimeSpan timeSpan)
        {
            return _Cache.Add(new CacheItem(key, item), new CacheItemPolicy()
            {
                Priority = CacheItemPriority.Default,
                SlidingExpiration = timeSpan
            });
        }

        public object Retrieve(string key)
        {
            return LocalRetrieve(key);
        }

        public T Retrieve<T>(string key)
        {
            object o = _Cache.Get(key);
            if (o is T)
            {
                return (T)o;
            }
            else
            {
                return default(T);
            }
        }

        public IDictionary<string, object> Retrieve(params string[] keys)
        {
            Dictionary<string, object> d = new Dictionary<string, object>();
            foreach (string key in keys)
            {
                object o = LocalRetrieve(key);
                if (o != null)
                {
                    d.Add(key, o);
                }
            }
            return d;
        }

        public IEnumerable<T> RetrieveSet<T>(string key)
        {
            List<T> list = new List<T>();

            List<string> setOfKeys = Retrieve<List<string>>(key);
            if (setOfKeys != null && setOfKeys.Count > 0)
            {
                var result = Retrieve(setOfKeys.ToArray());
                foreach (var item in result.Values)
                {
                    list.Add((T)item);
                }
            }
            return list;
        }

        public void ClearItem(string key)
        {
            _Cache.Remove(key);
        }

        public void ClearAll()
        {
            throw new NotImplementedException();
        }


        #region helper methods

        private object LocalRetrieve(string key)
        {
            MemoryCache cache = MemoryCache.Default;
            return cache.Get(key);
        }

        #endregion helper methods
    }
}
