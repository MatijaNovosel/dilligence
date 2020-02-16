using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace tvz2api.Helpers
{
    public static class QueryableExtensions
    {
        public static IQueryable<TSource> WithQueryOptions<TSource, TKey>(this IQueryable<TSource> queryable, QueryOptions queryOptions, Expression<Func<TSource, TKey>> defaultSort, bool defaultSortDesc = false)
        {
            var sort = queryOptions.Sort;
            IOrderedQueryable<TSource> res;

            if (sort.Count() == 0) 
            {
                res = defaultSortDesc ? queryable.OrderByDescending(defaultSort) : queryable.OrderBy(defaultSort);
            } 
            else 
            {
                var fs = sort.First();
                res = fs.Descending ? queryable.OrderByDescending(fs.By) : queryable.OrderBy(fs.By);
                foreach (var item in sort.Skip(1)) 
                {
                    res = item.Descending ? res.ThenByDescending(item.By) : res.ThenBy(item.By);
                }
            }

            return res.Skip(queryOptions.Skip).Take(queryOptions.Take);
        }

        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> query, string propertyName, IComparer<object> comparer = null)
        {
            return CallOrderedQueryable(query, "OrderBy", propertyName, comparer);
        }

        public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> query, string propertyName, IComparer<object> comparer = null)
        {
            return CallOrderedQueryable(query, "OrderByDescending", propertyName, comparer);
        }

        public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> query, string propertyName, IComparer<object> comparer = null)
        {
            return CallOrderedQueryable(query, "ThenBy", propertyName, comparer);
        }

        public static IOrderedQueryable<T> ThenByDescending<T>(this IOrderedQueryable<T> query, string propertyName, IComparer<object> comparer = null)
        {
            return CallOrderedQueryable(query, "ThenByDescending", propertyName, comparer);
        }

        public static IOrderedQueryable<T> CallOrderedQueryable<T>(this IQueryable<T> query, string methodName, string propertyName,
                IComparer<object> comparer = null)
        {
            var param = Expression.Parameter(typeof(T), "x");
            var body = propertyName.Split('.').Aggregate<string, Expression>(param, Expression.PropertyOrField);

            return comparer != null
                ? (IOrderedQueryable<T>)query.Provider.CreateQuery(
                    Expression.Call(
                        typeof(Queryable),
                        methodName,
                        new[] { typeof(T), body.Type },
                        query.Expression,
                        Expression.Lambda(body, param),
                        Expression.Constant(comparer)
                    )
                )
                : (IOrderedQueryable<T>)query.Provider.CreateQuery(
                    Expression.Call(
                        typeof(Queryable),
                        methodName,
                        new[] { typeof(T), body.Type },
                        query.Expression,
                        Expression.Lambda(body, param)
                    )
                );
        }
    }
}