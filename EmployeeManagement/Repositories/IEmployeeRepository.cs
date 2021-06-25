using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeManagement.Domain.Employees;

namespace EmployeeManagement.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        Task<List<string>> GetAllRegistrationNumbersAsync();
        Task<Employee> CreateEmployeeAsync(Employee employee);
        Employee GetEmployeeById(Guid id);
        Task<Employee> GetEmployeeByIdAsync(Guid id);
        Task<bool> RemoveEmployeeByIdAsync(Employee employee);

        bool CheckIfRegistrationNumberExistsOnDifferentEmployee(string requestRegistrationNumber,
            Guid currentEmployeeId);

        bool CheckIfPeselExistsOnDifferentEmployee(string requestPesel, Guid currentEmployeeId);
        Task<Employee> UpdateEmployee(Employee employee);
        bool CheckIfPeselExistsInDb(string pesel);
        bool CheckIfEmployeeExists(Guid id);
    }
}