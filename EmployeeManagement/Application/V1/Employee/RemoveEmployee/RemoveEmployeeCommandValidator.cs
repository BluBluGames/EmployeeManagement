using EmployeeManagement.Repositories;
using FluentValidation;

namespace EmployeeManagement.Application.V1.Employee.RemoveEmployee
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