using System;
using System.Collections.Generic;
using System.Reflection;

namespace Cal.Core.Runtime
{
    public static class ReflectionUtils
    {
       
        public static T GetCustomAttributeT<T>(this Type t) where T : Attribute
        {
            var customAttributes = t.GetCustomAttributes(typeof(T), false);
            if (customAttributes.Length == 0)
                return default(T);
            return (T)customAttributes[0];
        }

        public static T SetItem<TK, T>(this Dictionary<TK, T> dictionary, TK key) 
            where T : new()
        {
            T referencesList;
            if (!dictionary.TryGetValue(key, out referencesList))
            {
                referencesList = new T();
                dictionary[key] = referencesList;
            }
            return referencesList;
        }

        public static T GetCustomAttributeT<T>(this MemberInfo member) where T : Attribute
        {
            var customAttributes = member.GetCustomAttributes(typeof(T), false);
            if (customAttributes.Length == 0)
                return default(T);
            return (T)customAttributes[0];
        }
    }
}