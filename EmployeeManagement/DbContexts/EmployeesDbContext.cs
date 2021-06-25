using System;
using EmployeeManagement.Domain.Employees;
using EmployeeManagement.Domain.Employees.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.DbContexts
{
    public class EmployeesDbContext : DbContext
    {
        public EmployeesDbContext(DbContextOptions<EmployeesDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .OwnsOne(e => e.Pesel, a => a.Property(p => p.Value)
                    .HasColumnName("Pesel")
                    .IsRequired());
            modelBuilder.Entity<Employee>()
                .OwnsOne(e => e.BirthDate, a => a.Property(p => p.Value)
                    .HasColumnName("BirthDate")
                    .IsRequired());
            modelBuilder.Entity<Employee>()
                .OwnsOne(e => e.Name, a => a.Property(p => p.Value)
                    .HasColumnName("Name")
                    .IsRequired());
            modelBuilder.Entity<Employee>()
                .OwnsOne(e => e.RegistrationNumber, a => a.Property(p => p.Value)
                    .HasColumnName("RegistrationNumber")
                    .IsRequired());
            modelBuilder.Entity<Employee>()
                .OwnsOne(e => e.Sex, a => a.Property(p => p.Value)
                    .HasColumnName("Sex")
                    .IsRequired());
            modelBuilder.Entity<Employee>()
                .OwnsOne(e => e.Surname, a => a.Property(p => p.Value)
                    .HasColumnName("Surname")
                    .IsRequired());

            modelBuilder.Entity<Employee>()
                .OwnsOne(e => e.Pesel, a => a.HasIndex(p => p.Value)
                    .IsUnique());
            modelBuilder.Entity<Employee>()
                .OwnsOne(e => e.RegistrationNumber, a => a.HasIndex(p => p.Value)
                    .IsUnique());

        }
    }
}