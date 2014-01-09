// <copyright file="IResponse_1.cs" company="eXtensoft LLC">
// Copyright © 2014 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IResponse<T> : IEnumerable<T>
    {
        bool IsOkay { get; }

        T Model { get; }

        RequestStatus Status { get; }

        IEnumerable<IDisplay> Display { get; }

    }

}
