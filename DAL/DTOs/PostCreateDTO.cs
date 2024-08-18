using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementAPI.DAL.DTOs
{
    public class PostCreateDTO
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [StringLength(2000)]
        [Required]
        public string Description { get; set; }

        [Required]
        public int Category { get; set; }

        [Required]
        public int CreatedBy { get; set; }
    }
}
