using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace EmployeeManagementAPI.DAL.DTOs
{
    public class EmployeeFetchDTO
    {
        public int empno { get; set; }
        public DateOnly bdate { get; set; }

        public string fname { get; set; } = null!;

        public string lname { get; set; } = null!;

        public string sex { get; set; }

        public DateOnly hdate { get; set; }

        public string deptname { get; set; }
    }
}

