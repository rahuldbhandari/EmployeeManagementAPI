using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementAPI.DAL.DTOs
{
    public class EmployeeCreateDTO
    {
        public DateOnly BirthDate { get; set; }

        [StringLength(14)]
        public string FirstName { get; set; } = null!;

        [StringLength(16)]
        public string LastName { get; set; } = null!;

        [MaxLength(1)]
        public char Gender { get; set; }

        public DateOnly HireDate { get; set; }
    }
}
