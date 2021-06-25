using EmployeeManagement.Contracts.V1.EmployeeManagement.Commands;
using EmployeeManagement.Repositories;
using FluentValidation;

namespace EmployeeManagement.Contracts.V1.EmployeeManagement.Validators
{
    public class RemoveEmployeeCommandValidator : AbstractValidator<RemoveEmployeeCommand>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public RemoveEmployeeCommandValidator(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            RuleFor(x => x.Id).NotEmpty().Must(id => _employeeRepository.CheckIfEmployeeExists(id));
        }
    }
}