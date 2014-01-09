// <copyright company="eXtensoft LLC" file="IModelDataGatewayInitializer.cs">
// Copyright © 2014 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IModelDataGatewayInitializeable
    {
        void Initialize<T>(ModelActionOption option, IContext context, T t, ICriterion criterion, Func<IContext,string> dbkeyResolver) where T : class, new();
    }

}
