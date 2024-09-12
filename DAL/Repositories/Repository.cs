using EmployeeManagementAPI.DAL.IRepositories;
using EmployeeManagementAPI.Helper;
using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Models.Query;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EmployeeManagementAPI.DAL.Repositories
{
    public class Repository<T, Tcontext> : IRepository<T> where T : class where Tcontext : DbContext
    {
        private readonly Tcontext _dbContext;
        internal DbSet<T> dbSet;
        public Repository(Tcontext dbContext)
        {
            _dbContext = dbContext;
            this.dbSet = _dbContext.Set<T>();
        }

        public async Task<bool> AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            return true;
        }

        public async Task<PagedResponse<IEnumerable<T>>> GetAllAsync(DynamicQuery dynamicQuery = null)
        {
            var skip = ((dynamicQuery.PageIndex)  * dynamicQuery.PageSize);
            IQueryable<T> query = dbSet;


            if (dynamicQuery.filterQueries != null)
            {
                if (dynamicQuery.filterQueries != null && dynamicQuery.filterQueries.Any())
                {
                    foreach (var filters in dynamicQuery.filterQueries)
                    {
                        var dynimicFilterExpression = ExpressionHelper.GetFilterExpression<T>(filters.Field, filters.Value, filters.Operator);
                        query = query.Where(dynimicFilterExpression);
                    }
                }
            }

            // Add Dynamic Sort Parameters
            string? orderByField = dynamicQuery?.sortParameters?.Field;
            string? orderByOrder = dynamicQuery?.sortParameters?.Order;
            if (!string.IsNullOrWhiteSpace(orderByField))
            {
                var parameter = Expression.Parameter(typeof(T), "x");
                var property = Expression.Property(parameter, orderByField);
                var lambda = Expression.Lambda<Func<T, object>>(Expression.Convert(property, typeof(object)), parameter);
                query = (orderByOrder == "ASC") ? query.OrderBy(lambda) : query.OrderByDescending(lambda);
            }



            IEnumerable<T> data = await query.Skip(skip).Take(dynamicQuery.PageSize).ToListAsync();
            return new PagedResponse<IEnumerable<T>>(data, dynamicQuery.PageIndex, dynamicQuery.PageSize, dbSet.CountAsync().Result);
        }
        /*public async Task<IEnumerable<T>> GetAllAsync(PaginationQuery paginationQuery, Expression<Func<T, bool>>? filter = null)
        {
            var skip = ((paginationQuery.PageNumber - 1) * paginationQuery.PageSize);
            IQueryable<T> query = dbSet;
            return await ((filter != null) ? query.Where(filter) : query).Skip(skip).Take(paginationQuery.PageSize).ToListAsync();
        }*/

        public async Task<T> GetFirstAsync(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = dbSet;
            return await ((filter != null) ? query.Where(filter) : query).FirstAsync();  
        }


        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
