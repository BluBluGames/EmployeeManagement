using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EmployeeManagement.Contracts.V1.EmployeeManagement.Commands;
using EmployeeManagement.Domain.Employees;
using EmployeeManagement.Models;
using EmployeeManagement.Repositories;
using MediatR;

namespace EmployeeManagement.Services.EmployeeManagement.Handlers
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, EmployeeResponse>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public UpdateEmployeeCommandHandler(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<EmployeeResponse> Handle(UpdateEmployeeCommand command, CancellationToken cancellationToken)
        {
            var employee = _mapper.Map<UpdateEmployeeCommand, Employee>(command);
            var updatedEmployee = await _employeeRepository.UpdateEmployee(employee);
            return _mapper.Map<Employee, EmployeeResponse>(updatedEmployee);
        }
    }
}