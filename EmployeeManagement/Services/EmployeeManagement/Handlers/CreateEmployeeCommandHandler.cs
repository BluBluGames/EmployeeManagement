using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EmployeeManagement.Entities;
using EmployeeManagement.Models;
using EmployeeManagement.Repositories;
using EmployeeManagement.Services.EmployeeManagement.Commands;
using MediatR;

namespace EmployeeManagement.Services.EmployeeManagement.Handlers
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, EmployeeModel>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<EmployeeModel> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var newRegistrationNumber = await SetNewRegistrationNumber();

            var employee = _mapper.Map<CreateEmployeeCommand, Employee>(request);
            employee.RegistrationNumber = newRegistrationNumber;

            var createdEmployee = await _employeeRepository.CreateEmployeeAsync(employee);
            return _mapper.Map<Employee, EmployeeModel>(createdEmployee);
        }

        private async Task<string> SetNewRegistrationNumber()
        {
            var registrationNumbers = await _employeeRepository.GetAllRegistrationNumbers();
            var nextNumber = registrationNumbers.Where(x => int.TryParse(x, out _)).Max(x => Convert.ToInt32(x));
            nextNumber++;
            return $"{nextNumber:00000000}";
        }
    }
}
