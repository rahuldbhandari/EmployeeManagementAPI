using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementAPI.DAL.DTOs
{
    public class UserFetchDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public bool IsActive { get; set; }
    }
}
