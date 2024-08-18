using EmployeeManagementAPI.DAL.DTOs;

namespace EmployeeManagementAPI.BAL.IServices
{
    public interface IEmployeeService
    {
        public Task<int> addService(EmployeeCreateDTO req);
        public Task<IEnumerable<EmployeeFetchDTO>> fetchService();
    }
}
