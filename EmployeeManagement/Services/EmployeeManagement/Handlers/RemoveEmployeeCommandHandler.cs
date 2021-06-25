using System.Threading;
using System.Threading.Tasks;
using EmployeeManagement.Contracts.V1.EmployeeManagement.Commands;
using EmployeeManagement.Repositories;
using MediatR;

namespace EmployeeManagement.Services.EmployeeManagement.Handlers
{
    public class RemoveEmployeeCommandHandler : IRequestHandler<RemoveEmployeeCommand, bool>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public RemoveEmployeeCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<bool> Handle(RemoveEmployeeCommand command, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(command.Id);
            if (employee == null) return false;
            return await _employeeRepository.RemoveEmployeeByIdAsync(employee);
        }
    }
}