using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using EmployeeManagementAPI.DAL.Entities;

namespace EmployeeManagementAPI.DAL.DBContext
{
    public partial class EmployeeContext : DbContext
    {
        public EmployeeContext()
        {
        }

        public EmployeeContext(DbContextOptions<EmployeeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CurrentDeptEmp> CurrentDeptEmps { get; set; } = null!;
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<DeptEmp> DeptEmps { get; set; } = null!;
        public virtual DbSet<DeptEmpLatestDate> DeptEmpLatestDates { get; set; } = null!;
        public virtual DbSet<DeptManager> DeptManagers { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Salary> Salaries { get; set; } = null!;
        public virtual DbSet<Title> Titles { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Name=ConnectionStrings:EmployeeDBConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CurrentDeptEmp>(entity =>
            {
                entity.ToView("current_dept_emp");

                entity.Property(e => e.DeptNo).IsFixedLength();
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.DeptNo)
                    .HasName("departments_pkey");

                entity.Property(e => e.DeptNo).IsFixedLength();
            });

            modelBuilder.Entity<DeptEmp>(entity =>
            {
                entity.HasKey(e => new { e.EmpNo, e.DeptNo })
                    .HasName("dept_emp_pkey");

                entity.Property(e => e.DeptNo).IsFixedLength();

                entity.HasOne(d => d.DeptNoNavigation)
                    .WithMany(p => p.DeptEmps)
                    .HasForeignKey(d => d.DeptNo)
                    .HasConstraintName("dept_emp_dept_no_fkey");

                entity.HasOne(d => d.EmpNoNavigation)
                    .WithMany(p => p.DeptEmps)
                    .HasForeignKey(d => d.EmpNo)
                    .HasConstraintName("dept_emp_emp_no_fkey");
            });

            modelBuilder.Entity<DeptEmpLatestDate>(entity =>
            {
                entity.ToView("dept_emp_latest_date");
            });

            modelBuilder.Entity<DeptManager>(entity =>
            {
                entity.HasKey(e => new { e.EmpNo, e.DeptNo })
                    .HasName("dept_manager_pkey");

                entity.Property(e => e.DeptNo).IsFixedLength();

                entity.HasOne(d => d.DeptNoNavigation)
                    .WithMany(p => p.DeptManagers)
                    .HasForeignKey(d => d.DeptNo)
                    .HasConstraintName("dept_manager_dept_no_fkey");

                entity.HasOne(d => d.EmpNoNavigation)
                    .WithMany(p => p.DeptManagers)
                    .HasForeignKey(d => d.EmpNo)
                    .HasConstraintName("dept_manager_emp_no_fkey");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.EmpNo)
                    .HasName("employees_pkey");

                entity.Property(e => e.EmpNo).HasIdentityOptions(500000L);
            });

            modelBuilder.Entity<Salary>(entity =>
            {
                entity.HasKey(e => new { e.EmpNo, e.FromDate })
                    .HasName("salaries_pkey");

                entity.HasOne(d => d.EmpNoNavigation)
                    .WithMany(p => p.Salaries)
                    .HasForeignKey(d => d.EmpNo)
                    .HasConstraintName("salaries_emp_no_fkey");
            });

            modelBuilder.Entity<Title>(entity =>
            {
                entity.HasKey(e => new { e.EmpNo, e.Title1, e.FromDate })
                    .HasName("titles_pkey");

                entity.HasOne(d => d.EmpNoNavigation)
                    .WithMany(p => p.Titles)
                    .HasForeignKey(d => d.EmpNo)
                    .HasConstraintName("titles_emp_no_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
