// <copyright file="eXtensibleStrategyResolver.cs" company="eXtensoft LLC">
// Copyright © 2014 All Right Reserved
// </copyright>

namespace XF.Common
{
    public class eXtensibleStrategyResolver
    {
        protected eXtensibleStrategyElementCollection Strategies
        {
            get { return _Section.Strategies; }
        }

        private eXtensibleStrategySection _Section;

        public virtual void Initialize(eXtensibleStrategySection section)
        {
            _Section = section;
        }
    }
}
