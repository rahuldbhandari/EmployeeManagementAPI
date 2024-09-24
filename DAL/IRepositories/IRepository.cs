using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Models.Query;
using System.Linq.Expressions;

namespace EmployeeManagementAPI.DAL.IRepositories
{
    public interface IRepository<Entity> where Entity : class
    {
        public Task<PagedResponse<IEnumerable<DTO>>> GetAllAsync<DTO>(Expression<Func<Entity, bool>>? filterExpression, Expression<Func<Entity, DTO>> selectExpression, DynamicListQueryModel dynamicQuery);

        Task<Entity> GetFirstAsync(Expression<Func<Entity, bool>>? filter = null);

        Task<bool> AddAsync(Entity entity);

        Task<int> SaveChangesAsync();
    }
}
