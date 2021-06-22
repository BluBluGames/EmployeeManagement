using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.DbContexts;
using EmployeeManagement.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;

namespace EmployeeManagement.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeesDbContext _context;

        public EmployeeRepository(EmployeesDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<List<string>> GetAllRegistrationNumbers()
        {
            return await _context.Employees.Select(e=>e.RegistrationNumber).ToListAsync();
        }

        public async Task<Employee> CreateEmployeeAsync(Employee employee)
        {
            await _context.AddAsync(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<bool> CheckIfPeselExistsInDb(string pesel)
        {
            return await _context.Employees.AnyAsync(e => e.Pesel == pesel);
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            return await _context.Employees.FindAsync(id);
        }

        public async Task<bool> RemoveEmployeeByIdAsync(Employee employee)
        {
            try
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
