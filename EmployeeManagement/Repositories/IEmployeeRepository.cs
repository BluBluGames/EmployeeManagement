using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Entities;

namespace EmployeeManagement.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        Task<List<string>> GetAllRegistrationNumbers();
        Task<Employee> CreateEmployeeAsync(Employee employee);
        Task<bool> CheckIfPeselExistsInDb(string pesel);
        Task<Employee> GetEmployeeByIdAsync(int id);
        Task<bool> RemoveEmployeeByIdAsync(Employee id);
        Task<bool> CheckIfRegistrationNumberExistsOnDifferentEmployee(string requestRegistrationNumber, int currentEmployeeId);
        Task<bool> CheckIfPeselExistsOnDifferentEmployee(string requestPesel, int currentEmployeeId);
        Task<Employee> UpdateEmployee(Employee employee);

    }
}
