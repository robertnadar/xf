// <copyright file="ISqlCommandContext`1.cs" company="eXtensoft LLC">
// Copyright © 2014 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Data.SqlClient;

    public interface ISqlCommandContext<T> where T : class, new()
    {
        SqlCommand InsertCommand(SqlConnection cn, T t, IContext context);

        SqlCommand SelectOneCommand(SqlConnection cn, ICriterion criterion, IContext context);

        SqlCommand UpdateCommand(SqlConnection cn, T t, ICriterion criterion, IContext context);

        SqlCommand DeleteCommand(SqlConnection cn, ICriterion criterion, IContext context);

        SqlCommand SelectManyCommand(SqlConnection cn, ICriterion criterion, IContext context);

        SqlCommand SelectManyDisplayCommand(SqlConnection cn, ICriterion criterion, IContext context);

        string Schema { get; }
    }
}
