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
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, EmployeeModel>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public UpdateEmployeeCommandHandler(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<EmployeeModel> Handle(UpdateEmployeeCommand command, CancellationToken cancellationToken)
        {
            var employee = _mapper.Map<UpdateEmployeeCommand, Employee>(command);
            var updatedEmployee = await _employeeRepository.UpdateEmployee(employee);
            return _mapper.Map<Employee, EmployeeModel>(updatedEmployee);
        }
    }
}
