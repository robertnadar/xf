// <copyright company="eXtensoft LLC" file="ExtensionMethods.cs">
// Copyright © 2014 All Right Reserved
// </copyright>

namespace XF.DataServices
{
    using MongoDB.Bson;
    using MongoDB.Driver;
    using MongoDB.Driver.Builders;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using XF.Common;

    public static class ExtensionMethods
    {
        private static IList<string> queryExclusions = new List<string>
        {
            {"skip"},
            {"take"},
            {"limit"}
        };

        public static QueryDocument ToQueryDocument<T>(this ICriterion criterion) where T : class, new()
        {
            return criterion.ToQueryDocument<T>("id");
        }

        public static QueryDocument ToQueryDocument<T>(this ICriterion criterion, string modelId) where T : class, new()
        {
            string modelName = GetModelName<T>().ToLower();
            string[] t = new string[2] { modelId, modelName };
            Dictionary<string, object> d = new Dictionary<string, object>();
            HashSet<string> hs = new HashSet<string>();
            foreach (var item in criterion.Items)
            {
                if (hs.Add(item.Key))
                {
                    if (t.Contains(item.Key.ToLower()))
                    {
                        d.Add("_id", item.Value);
                    }
                    else if(!queryExclusions.Contains(item.Key.ToLower()))
                    {
                        d.Add(item.Key, item.Value);
                    }
                    
                }
            }
            return new QueryDocument(d);
        }

        public static IMongoQuery ToMongoQuery(this ICriterion criterion)
        {
            List<IMongoQuery> list = new List<IMongoQuery>();
            foreach (var item in criterion.Items)
            {
                if (!queryExclusions.Contains(item.Key.ToLower()))
                {
                    list.Add(ToMongoQuery(item));
                }
            }

            return Query.And(list);
        
        
        }

        private static IMongoQuery ToMongoQuery(TypedItem item)
        {
            IMongoQuery query = null;

            switch (item.Operator)
            {
                case OperatorTypeOption.None:
                case OperatorTypeOption.EqualTo:
                    query = Query.EQ(item.Key, item.ToBsonValue());//object to BsonValue
                    break;
                case OperatorTypeOption.NotEqualTo:
                    query = Query.NE(item.Key, item.ToBsonValue());
                    break;
                case OperatorTypeOption.LessThan:
                    query = Query.LT(item.Key, item.ToBsonValue());
                    break;
                case OperatorTypeOption.GreaterThan:
                    query = Query.GT(item.Key, item.ToBsonValue());
                    break;
                case OperatorTypeOption.X:
                    break;
                case OperatorTypeOption.And:
                    break;
                case OperatorTypeOption.Or:
                    break;                    
                default:
                    query = Query.EQ(item.Key, item.ToBsonValue());
                    break;
            }
            return query;
        }

        private static BsonValue ToBsonValue(this TypedItem item)
        {
            BsonValue bson = null;
            Type type = item.Value.GetType();
            switch (type.Name.ToLower())
            {
                case "string":
                    bson = new BsonString(item.Value.ToString());
                    break;
                case "datetime":
                    bson = new BsonDateTime((DateTime)item.Value);
                    break;
                case "int16":                    
                case "int32":
                    bson = new BsonInt32((Int32)item.Value);
                    break;
                case "int64":
                    bson = new BsonInt64((Int64)item.Value);
                    break;
                case "double":
                    bson = new BsonDouble((double)item.Value);
                    break;
                case "boolean":
                    bson = new BsonBoolean((bool)item.Value);
                    break;
                case "byte[]":
                    bson = new BsonObjectId((byte[])item.Value);
                    break;
                default:
                    bson = new BsonString(item.Value.ToString());
                    break;
            }
            return bson;
        }

        private static string GetModelName<T>()
        {
            return GetModelType<T>().Name;
        }

        private static Type GetModelType<T>()
        {
            return Activator.CreateInstance<T>().GetType();
        }

    }

}
