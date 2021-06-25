using EmployeeManagement.Repositories;
using FluentValidation;

namespace EmployeeManagement.Application.V1.Employee.CreateEmployee
{
    public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public CreateEmployeeCommandValidator(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            
            RuleFor(x => x.Pesel).NotEmpty().Length(11).Matches(@"^[\d]{11}$")
                .Must(pesel => !_employeeRepository.CheckIfPeselExistsInDb(pesel)).WithMessage("Pesel esists in database");
            RuleFor(x => x.Name).NotEmpty().MinimumLength(1).MaximumLength(25);
            RuleFor(x => x.Surname).NotEmpty().MinimumLength(1).MaximumLength(50);
            RuleFor(x => x.BirthDate).NotEmpty();
        }
    }
}