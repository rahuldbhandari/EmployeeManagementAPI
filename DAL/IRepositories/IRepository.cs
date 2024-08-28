using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Models.Query;
using System.Linq.Expressions;

namespace EmployeeManagementAPI.DAL.IRepositories
{
    public interface IRepository<T> where T : class
    {
        public Task<PagedResponse<IEnumerable<T>>> GetAllAsync(PaginationQuery paginationQuery, Expression<Func<T, bool>>? filter = null, List<FilterQuery> dynamicFilters = null);

        Task<T> GetFirstAsync(Expression<Func<T, bool>>? filter = null);

        Task<bool> AddAsync(T entity);

        Task<int> SaveChangesAsync();
    }
}
