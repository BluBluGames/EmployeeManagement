using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.DbContexts;
using EmployeeManagement.Domain.Employees;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<string>> GetAllRegistrationNumbersAsync()
        {
            return await _context.Employees.Select(e => e.RegistrationNumber.Value).ToListAsync();
        }

        public async Task<Employee> CreateEmployeeAsync(Employee employee)
        {
            await _context.AddAsync(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public Employee GetEmployeeById(Guid id)
        {
            return _context.Employees.AsNoTracking().FirstOrDefault(e => e.EmployeeId == id);
        }

        public bool CheckIfPeselExistsInDb(string pesel)
        {
            return _context.Employees.Any(e => e.Pesel.Value == pesel);
        }

        public bool CheckIfEmployeeExists(Guid id)
        {
            return _context.Employees.Any(e => e.EmployeeId == id);
        }

        public async Task<Employee> GetEmployeeByIdAsync(Guid id)
        {
            return await _context.Employees.AsNoTracking().FirstOrDefaultAsync(e => e.EmployeeId == id);
        }

        public async Task<bool> RemoveEmployeeByIdAsync(Employee employee)
        {
            try
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CheckIfRegistrationNumberExistsOnDifferentEmployee(string requestRegistrationNumber,
            Guid currentEmployeeId)
        {
            return _context.Employees
                .Any(e => e.RegistrationNumber.Value == requestRegistrationNumber && e.EmployeeId != currentEmployeeId);
        }

        public bool CheckIfPeselExistsOnDifferentEmployee(string requestPesel, Guid currentEmployeeId)
        {
            return _context.Employees
                .Any(e => e.Pesel.Value == requestPesel && e.EmployeeId != currentEmployeeId);
        }

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            _context.Update(employee);
            await _context.SaveChangesAsync();
            return employee;
        }
    }
}