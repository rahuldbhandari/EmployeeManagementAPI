using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Models.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace EmployeeManagementAPI.Helper
{
    public static class DynamicQueryHelper<T>
    {
        public static async Task<PagedResponse<IEnumerable<T>>> DynamicQueryResolver(IQueryable<T> query, DynamicListQueryModel dynamicQuery)
        {
            query = DynamicQueryHelper<T>.FilterQueryResolver(query, dynamicQuery.filterQueries);
            query = DynamicQueryHelper<T>.SortQueryResolver(query, dynamicQuery.sortParameters);
            IEnumerable <T> data = await DynamicQueryHelper<T>.PaginationQueryResolver(query, dynamicQuery.PageIndex, dynamicQuery.PageSize).ToListAsync();
            return new PagedResponse<IEnumerable<T>>(data, dynamicQuery.PageIndex, dynamicQuery.PageSize, query.CountAsync().Result);
        }

        public static IQueryable<T> FilterQueryResolver(IQueryable<T> query, IEnumerable<FilterQuery>? filterQueries)
        {
            if (filterQueries != null)
            {
                foreach (var filters in filterQueries)
                {
                        var dynimicFilterExpression = ExpressionHelper.GetFilterExpression<T>(filters.Field, filters.Value, filters.Operator);
                        query = query.Where(dynimicFilterExpression);
                }
            }
            return query;
        }
        public static IQueryable<T> SortQueryResolver(IQueryable<T> query, SortParameter? sortParameters)
        {
            if (sortParameters != null)
            {
                string? orderByField = sortParameters.Field;
                string? orderByOrder = sortParameters.Order;
                if (!string.IsNullOrWhiteSpace(orderByField))
                {
                    var parameter = Expression.Parameter(typeof(T), "x");
                    var property = Expression.Property(parameter, orderByField);
                    var lambda = Expression.Lambda<Func<T, object>>(Expression.Convert(property, typeof(object)), parameter);
                    query = (orderByOrder == "ASC") ? query.OrderBy(lambda) : query.OrderByDescending(lambda);
                }
            }

            return query;
        }

        public static IQueryable<T> PaginationQueryResolver(IQueryable<T> query, int PageIndex, int PageSize)
        {
            return query.Skip((PageIndex)).Take(PageSize);
        }
    }
}
