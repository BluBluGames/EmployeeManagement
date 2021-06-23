using EmployeeManagement.Repositories;
using EmployeeManagement.Services.EmployeeManagement.Commands;
using FluentValidation;

namespace EmployeeManagement.Services.EmployeeManagement.Validators
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