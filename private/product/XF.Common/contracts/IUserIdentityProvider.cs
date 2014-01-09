// <copyright file="IUserIdentityProvider.cs" company="eXtensoft LLC">
// Copyright © 2014 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;

    public interface IUserIdentityProvider
    {
        string Username { get; }

        string Culture { get; }

        string UICulture { get; }
    }
}
