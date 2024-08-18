using EmployeeManagementAPI.Models;

namespace EmployeeManagementAPI.BAL
{
    public class BaseService
    {
        public virtual ValidatorResponseModel Validate()
        {
            return new ValidatorResponseModel() { IsValid = true };
        }
    }
}
