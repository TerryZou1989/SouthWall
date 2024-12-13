using Newtonsoft.Json;
using System.Linq.Expressions;

namespace SouthWall
{
    public static class MyExtension
    {
        public static int ToInt32(this string s)
        {
            return int.Parse(s);
        }
        public static bool ToBoolean(this string s)
        {           
            return bool.Parse(s);
        }
        public static T ToObject<T>(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(s);
        }
        public static string ToJson(this object obj)
        {
            if (obj==null)
            {
                return null;
            }
            return JsonConvert.SerializeObject(obj);
        }

        public static List<T> OrderByRandom<T>(this List<T> list)
        {
            if (list != null) { 
                return list.OrderBy(t=>Guid.NewGuid()).ToList();
            }
            return null;
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2) {
            var parameter = Expression.Parameter(typeof(T), typeof(T).Name);
            var body = Expression.AndAlso(
                Expression.Invoke(expr1, parameter),
                Expression.Invoke(expr2, parameter));
            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            var parameter = Expression.Parameter(typeof(T), typeof(T).Name);
            var body = Expression.OrElse(
                Expression.Invoke(expr1, parameter),
                Expression.Invoke(expr2, parameter));
            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }
    }
}
