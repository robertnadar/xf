// <copyright file="TypeMapCache.cs" company="eXtensoft LLC">
// Copyright © 2014 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TypeMapCache : ITypeMapCache
    {
        private volatile Dictionary<string, List<Type>> _TypeCache;

        private object _lockObject = new object();

        internal int Count
        {
            get
            {
                return _TypeCache.Count;
            }
        }


        void ITypeMapCache.Initialize()
        {
            Initialize();
        }


        Type ITypeMapCache.ResoveType<T>()
        {
            return this.ResoveType<T>();
        }

        private void Initialize()
        {
            if (_TypeCache == null)
            {
                lock (_lockObject)
                {
                    if (_TypeCache == null)
                    {
                        DiscoverTypes();
                    }
                }
            }
        }

        private void DiscoverTypes()
        {
            TypeMapContainer container = null;
            ModuleLoader<TypeMapContainer> loader = new ModuleLoader<TypeMapContainer>();
            if (loader.Load(out container))
            {
                _TypeCache = new Dictionary<string, List<Type>>();
                foreach (var item in container.TypeMaps)
                {
                    if (!_TypeCache.ContainsKey(item.KeyType.FullName))
                    {
                        _TypeCache.Add(item.KeyType.FullName, new List<Type>());
                    }
                    _TypeCache[item.KeyType.FullName].Add(item.TypeResolution);
                }
            }
        }

        private Type ResoveType<T>() where T : class, new()
        {
            string modeltypename = Activator.CreateInstance<T>().GetType().FullName;
            if (_TypeCache.ContainsKey(modeltypename))
            {
                return _TypeCache[modeltypename][0];
            }
            else
            {
                return null;
            }
        }
    }
}
