// <copyright file="IBorrower`1.cs" company="eXtensoft LLC">
// Copyright © 2014 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System.Data.SqlClient;

    public interface IBorrower<T>
    {
        void BorrowReader(SqlDataReader reader, T t);
    }
}
