using EmployeeManagementAPI.DAL.IRepositories;
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

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = dbSet.Take(100);
            return await ((filter != null) ? query.Where(filter) : query).ToListAsync();
        }

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
