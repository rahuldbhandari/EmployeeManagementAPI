using EmployeeManagementAPI.DAL.IRepositories;
using EmployeeManagementAPI.Helper;
using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Models.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Linq.Expressions;

namespace EmployeeManagementAPI.DAL.Repositories
{
    public class Repository<Entity, Tcontext> : IRepository<Entity> where Entity : class where Tcontext : DbContext
    {
        private readonly Tcontext _dbContext;
        internal DbSet<Entity> dbSet;
        public Repository(Tcontext dbContext)
        {
            _dbContext = dbContext;
            this.dbSet = _dbContext.Set<Entity>();
        }

        public async Task<bool> AddAsync(Entity entity)
        {
            await dbSet.AddAsync(entity);
            return true;
        }

        public async Task<PagedResponse<IEnumerable<DTO>>> GetAllAsync<DTO>(Expression<Func<Entity, bool>>? filterExpression, Expression<Func<Entity, DTO>> selectExpression, DynamicListQueryModel dynamicQuery)
        {
            IQueryable<Entity> query = dbSet;

            query = filterExpression != null ? query.Where(filterExpression) : query;
            query = DynamicQueryHelper<Entity>.FilterQueryResolver(query, dynamicQuery.filterQueries);
            query = DynamicQueryHelper<Entity>.SortQueryResolver(query, dynamicQuery.sortParameters);
            var final_query = DynamicQueryHelper<Entity>.PaginationQueryResolver(query, dynamicQuery.PageIndex, dynamicQuery.PageSize);

            IEnumerable<DTO> data = await final_query.Select(selectExpression).ToListAsync();
            return new PagedResponse<IEnumerable<DTO>>(data, dynamicQuery.PageIndex, dynamicQuery.PageSize, query.CountAsync().Result);
        }

        public async Task<Entity> GetFirstAsync(Expression<Func<Entity, bool>>? filter = null)
        {
            IQueryable<Entity> query = dbSet;
            return await ((filter != null) ? query.Where(filter) : query).FirstAsync();  
        }


        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
