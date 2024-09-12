using EmployeeManagementAPI.DAL.DTOs;
using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Models.Query;

namespace EmployeeManagementAPI.BAL.IServices
{
    public interface IEmployeeService
    {
        public Task<int> addService(EmployeeCreateDTO req);
        public Task<PagedResponse<IEnumerable<EmployeeFetchDTO>>> fetchService(DynamicQuery dDynamicQuery = null);
    }
}
