// <copyright file="ModuleLoader`1.cs" company="eXtensoft LLC">
// Copyright © 2014 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.IO;

    public sealed class ModuleLoader<T> where T : new()
    {
        #region fields

        private Type[] _types = null;

        #endregion fields

        #region constructors

        public ModuleLoader()
        {
        }

        public ModuleLoader(params Type[] types)
        {
            _types = types;
        }

        #endregion constructors

        #region instance methods

        public bool Load(out T t)
        {
            bool b = false;
            t = new T();
            using (AggregateCatalog catalog = new AggregateCatalog())
            {
                catalog.Catalogs.Add(new DirectoryCatalog(AppDomain.CurrentDomain.BaseDirectory));
                string webbindirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin");
                if (Directory.Exists(webbindirectory))
                {
                    catalog.Catalogs.Add(new DirectoryCatalog(webbindirectory));
                }
                if (_types != null && _types.Length > 0)
                {
                    foreach (Type type in _types)
                    {
                        catalog.Catalogs.Add(new AssemblyCatalog(type.Assembly));
                    }
                }
                using (CompositionContainer container = new CompositionContainer(catalog, true))
                {
                    try
                    {
                        container.ComposeParts(t);
                        b = true;
                    }
                    catch (Exception ex)
                    {
                        string message = (ex.InnerException != null) ? ex.InnerException.Message : ex.Message;
                        EventWriter.WriteError(message, SeverityType.Critical, "ModuleLoader");
                    }
                }
            }
            return b;
        }

        #endregion instance methods
    }
}
