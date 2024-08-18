using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementAPI.DAL.DTOs
{
    public class EmployeeFetchDTO
    {
        public DateOnly BirthDate { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public char Gender { get; set; }

        public DateOnly HireDate { get; set; }
    }
}
