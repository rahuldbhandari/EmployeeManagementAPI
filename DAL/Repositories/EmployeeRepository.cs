using EmployeeManagementAPI.DAL.DBContext;
using EmployeeManagementAPI.DAL.DTOs;
using EmployeeManagementAPI.DAL.Entities;
using EmployeeManagementAPI.DAL.IRepositories;
using EmployeeManagementAPI.Models.Query;
using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Helper;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EmployeeManagementAPI.DAL.Repositories
{
    public class EmployeeRepository : Repository<Employee, EmployeeContext>, IEmployeeRepository
    {
        EmployeeContext _dbContext;
        public EmployeeRepository(EmployeeContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PagedResponse<IEnumerable<EmployeeFetchDTO>>> GetAll(string deptno, DynamicListQueryModel dynamicQuery)
        {
            var query = (from emp in _dbContext.Employees
                         join demp in _dbContext.DeptEmps on emp.EmpNo equals demp.EmpNo
                         join dept in _dbContext.Departments on demp.DeptNo equals dept.DeptNo
                         select new EmployeeFetchDTO
                         {
                             empno = emp.EmpNo,
                             fname = emp.FirstName,
                             lname = emp.LastName,
                             bdate = emp.BirthDate,
                             sex = (emp.Gender == 'M') ? "Male" : "Female",
                             hdate = emp.HireDate,
                             deptname = dept.DeptName,

                         });

            return await DynamicQueryHelper<EmployeeFetchDTO>.DynamicQueryResolver(query, dynamicQuery);

            /*var final_query = DynamicQueryHelper<EmployeeFetchDTO>.PaginationQueryResolver(query, dynamicQuery.PageIndex, dynamicQuery.PageSize);

            return new PagedResponse<IEnumerable<EmployeeFetchDTO>>(final_query.ToListAsync().Result, dynamicQuery.PageIndex, dynamicQuery.PageSize, query.CountAsync().Result);*/
        }
    }
}
