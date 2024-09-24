using AutoMapper;
using EmployeeManagementAPI.BAL.IServices;
using EmployeeManagementAPI.DAL.DTOs;
using EmployeeManagementAPI.DAL.Entities;
using EmployeeManagementAPI.DAL.IRepositories;
using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Models.Query;

namespace EmployeeManagementAPI.BAL
{
    public class EmployeeService : BaseService,  IEmployeeService
    {
        IMapper _mapper;
        IEmployeeRepository _employeeRepo;

        public EmployeeService(IMapper mapper, IEmployeeRepository employeeRepo)
        {
            _mapper = mapper;
            _employeeRepo = employeeRepo;
        }

        public async Task<int> addService(EmployeeCreateDTO req)
        {
            Employee newEmployee = _mapper.Map<Employee>(req);
            await _employeeRepo.AddAsync(newEmployee);
            await _employeeRepo.SaveChangesAsync();
            return newEmployee.EmpNo;
        }

        public async Task<PagedResponse<IEnumerable<EmployeeFetchDTO>>> GetAll(string deptno, DynamicListQueryModel dynamicQuery)
        {

            return await _employeeRepo.GetAll(deptno, dynamicQuery);
        }
    }
}
