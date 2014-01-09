// <copyright file="NetMemoryCache.cs" company="eXtensoft LLC">
// Copyright © 2014 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Caching;
    using System.Runtime.Serialization;
    using System.Text;
    using System.Threading.Tasks;


    public class NetMemoryCache : ICache
    {
        private MemoryCache _Cache;

        public NetMemoryCache()
        {
            _Cache = MemoryCache.Default;
        }

        //bool ICache.Store(string key, object item)
        //{
        //    return _Cache.Add(new CacheItem(key, item), new CacheItemPolicy()
        //    {
        //        Priority = CacheItemPriority.Default,
        //        SlidingExpiration = TimeSpan.FromMinutes(5)
        //    });
        //}

        //bool ICache.Store(string key, object item, TimeSpan timeSpan)
        //{
        //    return _Cache.Add(new CacheItem(key, item), new CacheItemPolicy()
        //    {
        //        Priority = CacheItemPriority.Default,
        //        SlidingExpiration = timeSpan
        //    });
        //}

        //object ICache.Retrieve(string key)
        //{
        //    return LocalRetrieve(key);
        //}

        //TModel ICache.Retrieve<TKey,TModel>(TKey key)
        //{
        //    return LocalRetrieve<TKey, TModel>(key);
        //}

        //IDictionary<TKey, TModel> ICache.Retrieve<TKey, TModel>(params TKey[] keys)
        //{
        //    Dictionary<TKey, TModel> result = new Dictionary<TKey, TModel>();

        //    foreach (TKey key in keys)
        //    {
        //        TModel model = LocalRetrieve<TKey, TModel>(key);
        //    }


        //    return result;
        //}

        //IEnumerable<TModel> ICache.RetrieveSet<TKey, TModel>(TKey key)
        //{
        //    throw new NotImplementedException();
        //}

        //void ICache.ClearItem<TKey>(TKey key)
        //{
        //    _Cache.Remove(key.ToString());
        //}

        //void ICache.ClearAll()
        //{
        //    throw new NotImplementedException();
        //}


        //private TModel LocalRetrieve<TKey,TModel>(TKey key)
        //{
        //    object o = _Cache.Get(key.ToString());
        //    if (o is TModel)
        //    {
        //        return (TModel)o;
        //    }
        //    else
        //    {
        //        return default(TModel);
        //    }
        //}
        //private object LocalRetrieve(string key)
        //{
        //    MemoryCache cache = MemoryCache.Default;
        //    return cache.Get(key);
        //}

        //private IEnumerable<T> RetrieveSet<T>(string key)
        //{
        //    List<T> list = new List<T>();

        //    List<string> setOfKeys = Retrieve<List<string>>(key);
        //    if (setOfKeys != null && setOfKeys.Count > 0)
        //    {
        //        var result = Retrieve(setOfKeys.ToArray());
        //        foreach (var item in result.Values)
        //        {
        //            list.Add((T)item);
        //        }
        //    }
        //    return list;
        //}




        bool ICache.Store(string key, object item)
        {
            return _Cache.Add(new CacheItem(key, item), new CacheItemPolicy()
            {
                Priority = CacheItemPriority.Default,
                SlidingExpiration = TimeSpan.FromMinutes(5)
            });
        }

        bool ICache.Store(string key, object item, TimeSpan timeSpan)
        {
            return _Cache.Add(new CacheItem(key, item), new CacheItemPolicy()
            {
                Priority = CacheItemPriority.Default,
                SlidingExpiration = timeSpan
            });
        }

        object ICache.Retrieve(string key)
        {
            return LocalRetrieve(key);
        }

        T ICache.Retrieve<T>(string key)
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

        IDictionary<string, object> ICache.Retrieve(params string[] keys)
        {
            Dictionary<string, object> d = new Dictionary<string, object>();
            foreach (string key in keys)
            {
                object o = LocalRetrieve(key);
            }
            return d;
        }

        IEnumerable<T> ICache.RetrieveSet<T>(string key)
        {
            throw new NotImplementedException();
        }

        void ICache.ClearItem(string key)
        {
            _Cache.Remove(key);
        }

        void ICache.ClearAll()
        {
            throw new NotImplementedException();
        }

        private object LocalRetrieve(string key)
        {
            return _Cache.Get(key);
        }

    }
}
