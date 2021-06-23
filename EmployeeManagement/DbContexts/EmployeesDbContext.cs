using System;
using EmployeeManagement.Entities;
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
                .HasIndex(e => e.RegistrationNumber)
                .IsUnique();
            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.Pesel)
                .IsUnique();

            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 1,
                RegistrationNumber = "00000001",
                Pesel = "54092397111",
                BirthDate = new DateTime(2015, 12, 25),
                Surname = "Stark",
                Name = "Tony",
                Sex = ESex.Male
            });
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 2,
                RegistrationNumber = "00000002",
                Pesel = "90102826611",
                BirthDate = new DateTime(1988, 03, 09),
                Surname = "Banner",
                Name = "Bruce",
                Sex = ESex.Male
            });
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 3,
                RegistrationNumber = "00000003",
                Pesel = "38100948214",
                BirthDate = new DateTime(1975, 05, 12),
                Surname = "Storm",
                Name = "Sue",
                Sex = ESex.Female
            });
        }
    }
}