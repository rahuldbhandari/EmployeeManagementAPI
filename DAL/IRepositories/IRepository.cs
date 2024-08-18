using System.Linq.Expressions;

namespace EmployeeManagementAPI.DAL.IRepositories
{
    public interface Repository<T> where T : class
    {
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null);

        Task<T> GetFirstAsync(Expression<Func<T, bool>>? filter = null);

        Task<bool> AddAsync(T entity);

        Task<int> SaveChangesAsync();
    }
}
