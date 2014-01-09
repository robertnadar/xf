// <copyright file="ExtensionMethods.cs" company="eXtensoft LLC">
// Copyright © 2014 All Right Reserved
// </copyright>

namespace XF.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class ExtensionMethods
    {    
        public static IDictionary<string,object> ToIDictionary(this ICriterion criterion)
        {
            Dictionary<string,object> d = new Dictionary<string,object>();
            HashSet<string> hs = new HashSet<string>();
            foreach (var item in criterion.Items)
            {
                if (hs.Add(item.Key))
                {
                    d.Add(item.Key, item.Value);
                }
            }
            return d;
        }

        public static bool Contains(this ICriterion item, string key)
        {
            bool b = false;

            if (item.Items != null)
            {
                foreach (var typedItem in item.Items)
                {
                    if (typedItem.Key.Equals(key, StringComparison.OrdinalIgnoreCase))
                    {
                        b = true;
                        break;
                    }
                }
            }
            return b;
        }

        public static Type GetItemType(this ICriterion criteria, string key)
        {
            Type t = null;
            if (criteria.Items != null)
            {
                foreach (var item in criteria.Items)
                {
                    if (item.Key.Equals(key, StringComparison.OrdinalIgnoreCase))
                    {
                        t = item.Value.GetType();
                        break;
                    }
                }
            }
            return t;
        }

        public static bool IsSerializable(this Type type)
        {
            var result = type.GetCustomAttributes(typeof(SerializableAttribute), false);
            return (result != null && result.Length > 0);
        }

        public static void SetStrategyKeyValue(this ICriterion item, string strategyKey, string strategyValue)
        {
            Criterion criteria = item as Criterion;
            if (criteria != null)
            {
                criteria.AddItem(XFConstants.Application.STRATEGYKEY, strategyKey);
                criteria.AddItem(strategyKey, strategyValue);
            }
        }

        public static bool ContainsStrategy(this ICriterion item)
        {
            string key = item.GetValue<string>(XFConstants.Application.STRATEGYKEY);
            return !String.IsNullOrEmpty(key);
        }

        public static void AddItem(this ICriterion item, string key, object value)
        {
            Criterion criteria = item as Criterion;
            if (criteria != null)
            {
                if (criteria.Items == null)
                {
                    criteria.Items = new List<TypedItem>();
                }
            }
            criteria.Add(key, value);
        }

        public static string GetStrategyKey(this ICriterion item)
        {
            string resolution = String.Empty;
            string key = item.GetValue<string>(XFConstants.Application.STRATEGYKEY);
            if (!String.IsNullOrEmpty(key))
            {
                resolution = item.GetValue<string>(key);
            }
            return resolution;
        }

        public static DateTime NextWeekDay(this DateTime date)
        {
            DateTime next = date.AddDays(1);
            int i = Convert.ToInt16(next.DayOfWeek);
            while (i == 0 | i == 6)
            {
                next = next.AddDays(1);
                i = Convert.ToInt16(next.DayOfWeek);
            }
            return next.Date;
        }

        public static string GetStringValue(this ICriterion criteria, string key)
        {
            string s = String.Empty;
            if (criteria.Items != null)
            {
                foreach (var item in criteria.Items)
                {
                    if (item.Key.Equals(key, StringComparison.OrdinalIgnoreCase))
                    {
                        s = item.Value.ToString();
                        break;
                    }
                }
            }
            return s;
        }

    }
}
