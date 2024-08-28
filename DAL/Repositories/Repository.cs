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

        public async Task<PagedResponse<IEnumerable<T>>> GetAllAsync(PaginationQuery paginationQuery, Expression<Func<T, bool>>? filter = null, List<FilterQuery>? dynamicFilters = null)
        {
            var skip = ((paginationQuery.PageNumber - 1)  * paginationQuery.PageSize);
            IQueryable<T> query = dbSet;


            if (dynamicFilters != null)
            {
                if (dynamicFilters != null && dynamicFilters.Any())
                {
                    foreach (var filters in dynamicFilters)
                    {
                        var dynimicFilterExpression = ExpressionHelper.GetFilterExpression<T>(filters.Field, filters.Value, filters.Operator);
                        query = query.Where(dynimicFilterExpression);
                    }
                }
            }
            IEnumerable<T> data = await ((filter != null) ? query.Where(filter) : query).Skip(skip).Take(paginationQuery.PageSize).ToListAsync();
            return new PagedResponse<IEnumerable<T>>(data, paginationQuery.PageNumber, paginationQuery.PageSize, dbSet.CountAsync().Result);
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
