using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EmployeeManagement.Contracts.V1.EmployeeManagement.Commands;
using EmployeeManagement.Domain.Employees;
using EmployeeManagement.Domain.Employees.ValueObjects;
using EmployeeManagement.Models;
using EmployeeManagement.Repositories;
using MediatR;

namespace EmployeeManagement.Services.EmployeeManagement.Handlers
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, EmployeeResponse>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<EmployeeResponse> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = _mapper.Map<CreateEmployeeCommand, Employee>(request);

            await employee.GenerateRegistrationNumber(_employeeRepository);
            var createdEmployee = await _employeeRepository.CreateEmployeeAsync(employee);

            return _mapper.Map<Employee, EmployeeResponse>(createdEmployee);
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