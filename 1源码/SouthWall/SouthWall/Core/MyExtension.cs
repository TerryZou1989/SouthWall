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
