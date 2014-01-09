// <copyright file="IMessage_1.cs" company="eXtensoft LLC">
// Copyright © 2014 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IMessage<T> where T : class, new()
    {
        IEnumerable<TypedItem> Context { get; set; }

        string ModelTypename { get; }

        string Verb { get; }
    }

}
