using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagement.Domain.Employees.Operations;
using EmployeeManagement.Domain.Employees.ValueObjects;
using EmployeeManagement.Repositories;

namespace EmployeeManagement.Domain.Employees
{
    public class Employee
    {
        [Key]
        public Guid EmployeeId { get; set; }

        public EmployeeRegistrationNumber RegistrationNumber { get; set; }

        public EmployeePesel Pesel { get; set; }

        public EmployeeName Name { get; set; }

        public EmployeeSurname Surname { get; set; }

        public EmployeeBirthDate BirthDate { get; set; }

        public EmployeeSex Sex { get; set; }

        public async Task GenerateRegistrationNumber(IEmployeeRepository employeeRepository)
        {
            var registrationNumbers = await employeeRepository.GetAllRegistrationNumbersAsync();
            int nextNumber;
            if (registrationNumbers.Any())
                nextNumber = registrationNumbers.Where(x => int.TryParse(x, out _)).Max(x => Convert.ToInt32(x));
            else
                nextNumber = 0;
            nextNumber++;

            RegistrationNumber = EmployeeRegistrationNumber.From($"{nextNumber:00000000}");
        }

        public async Task<bool> IsPeselInDatabase(IEmployeeRepository employeeRepository)
        {
            return !employeeRepository.CheckIfPeselExistsInDb(Pesel.Value);
        }

    }
}
