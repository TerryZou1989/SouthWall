namespace SouthWall
{
    public class DBAccessBase
    {
        protected readonly SWDbContext _context;
        public DBAccessBase(SWDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        //protected Expression<Func<T, bool>> CombineExpressions<T>(
        //    Expression<Func<T, bool>> expr1,
        //    Expression<Func<T, bool>> expr2)
        //{
        //    var parameter = Expression.Parameter(typeof(T), typeof(T).Name);
        //    var body = Expression.AndAlso(
        //        Expression.Invoke(expr1, parameter),
        //        Expression.Invoke(expr2, parameter));
        //    return Expression.Lambda<Func<T, bool>>(body, parameter);
        //}
    }
}
