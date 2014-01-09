// <copyright file="IRequest_1.cs" company="eXtensoft LLC">
// Copyright © 2014 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IRequest<T>
    {
        string Verb { get; set; }

        T Model { get; set; }

        IEnumerable<T> Content { get; set; }

        ICriterion Criterion { get; }

        IEnumerable<IDisplay> Display { get; set; }

        object ActionResult { get; set; }
    }
}
