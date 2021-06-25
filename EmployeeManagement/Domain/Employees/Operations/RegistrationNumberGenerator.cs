using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Domain.Employees.ValueObjects;
using EmployeeManagement.Repositories;

namespace EmployeeManagement.Domain.Employees.Operations
{
    public class RegistrationNumberGenerator
    {
        private readonly IEmployeeRepository _employeeRepository;

        public RegistrationNumberGenerator(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        private async Task<EmployeeRegistrationNumber> SetNewRegistrationNumber()
        {
            var registrationNumbers = await _employeeRepository.GetAllRegistrationNumbersAsync();
            int nextNumber;
            if (registrationNumbers.Any())
                nextNumber = registrationNumbers.Where(x => int.TryParse(x, out _)).Max(x => Convert.ToInt32(x));
            else
                nextNumber = 0;
            nextNumber++;

            return EmployeeRegistrationNumber.From($"{nextNumber:00000000}");
        }
    }
}
