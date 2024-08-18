using EmployeeManagementAPI.DAL.DBContext;
using EmployeeManagementAPI.DAL.Entities;
using EmployeeManagementAPI.DAL.IRepositories;

namespace EmployeeManagementAPI.DAL.Repositories
{
    public class EmployeeRepository : Repository<Employee, EmployeeContext>, IEmployeeRepository
    {
        public EmployeeRepository(EmployeeContext dbContext) : base(dbContext)
        {
        }
    }
}
