using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeManagement.Entities;

namespace EmployeeManagement.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        Task<List<string>> GetAllRegistrationNumbersAsync();
        Task<Employee> CreateEmployeeAsync(Employee employee);
        Employee GetEmployeeById(int id);
        Task<Employee> GetEmployeeByIdAsync(int id);
        Task<bool> RemoveEmployeeByIdAsync(Employee id);

        bool CheckIfRegistrationNumberExistsOnDifferentEmployee(string requestRegistrationNumber,
            int currentEmployeeId);

        bool CheckIfPeselExistsOnDifferentEmployee(string requestPesel, int currentEmployeeId);
        Task<Employee> UpdateEmployee(Employee employee);
        bool CheckIfPeselExistsInDb(string pesel);
        bool CheckIfEmployeeExists(int id);
    }
}