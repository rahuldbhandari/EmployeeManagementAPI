using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementAPI.DAL.DTOs
{
    public class UserCreateDTO
    {
        [Required]
        [StringLength(50)]        
        public string Name { get; set; }

        [StringLength(50)]
        public string Password { get; set; }
    }
}
