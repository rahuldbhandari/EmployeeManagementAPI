﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementAPI.DAL.Entities
{
    [Table("dept_manager")]
    public partial class DeptManager
    {
        [Key]
        [Column("emp_no")]
        public int EmpNo { get; set; }
        [Key]
        [Column("dept_no")]
        [StringLength(4)]
        public string DeptNo { get; set; } = null!;
        [Column("from_date")]
        public DateOnly FromDate { get; set; }
        [Column("to_date")]
        public DateOnly ToDate { get; set; }

        [ForeignKey("DeptNo")]
        [InverseProperty("DeptManagers")]
        public virtual Department DeptNoNavigation { get; set; } = null!;
        [ForeignKey("EmpNo")]
        [InverseProperty("DeptManagers")]
        public virtual Employee EmpNoNavigation { get; set; } = null!;
    }
}
