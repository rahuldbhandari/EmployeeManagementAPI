using EmployeeManagementAPI.DAL.DTOs;
using EmployeeManagementAPI.DAL.Entities;
using EmployeeManagementAPI.Models.Query;
using EmployeeManagementAPI.Models;

namespace EmployeeManagementAPI.DAL.IRepositories
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        public Task<PagedResponse<IEnumerable<EmployeeFetchDTO>>> GetAll(string deptno, DynamicListQueryModel dynamicQuery);
    }
}
