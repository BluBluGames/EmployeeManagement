using EmployeeManagement.Repositories;
using FluentValidation;

namespace EmployeeManagement.Application.V1.Employee.UpdateEmployee
{
    public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public UpdateEmployeeCommandValidator(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            RuleFor(x => x.EmployeeId).NotEmpty().Must(id => _employeeRepository.CheckIfEmployeeExists(id));

            RuleFor(x => x.Pesel).NotEmpty().Length(11).Matches(@"^[\d]{11}$");

            RuleFor(x => new {x.Pesel, x.EmployeeId}).Must((command, values) =>
            {
                var employee = _employeeRepository.GetEmployeeById(values.EmployeeId);

                if (employee.Pesel.Value == command.Pesel) return true;

                var isPeselPresentOnDifferentEmployee =
                    _employeeRepository.CheckIfPeselExistsOnDifferentEmployee(command.Pesel, employee.EmployeeId);
                return !isPeselPresentOnDifferentEmployee;

            }).WithMessage("Pesel exists in database");

            RuleFor(x => new {x.EmployeeId, x.RegistrationNumber}).Must((command, values) =>
            {
                var employee = _employeeRepository.GetEmployeeById(values.EmployeeId);

                if (employee.RegistrationNumber.Value == command.RegistrationNumber) return true;

                var isRegistrationNumberPresentOnDifferentEmployee =
                    _employeeRepository.CheckIfRegistrationNumberExistsOnDifferentEmployee(
                        command.RegistrationNumber, employee.EmployeeId);
                return !isRegistrationNumberPresentOnDifferentEmployee;

            }).WithMessage("Registration number exists in database");

            RuleFor(x => x.Name).NotEmpty().MinimumLength(1).MaximumLength(25);
            RuleFor(x => x.Surname).NotEmpty().MinimumLength(1).MaximumLength(50);
            RuleFor(x => x.BirthDate).NotEmpty();
            RuleFor(x => x.RegistrationNumber).NotEmpty().Length(8).Matches(@"^[\d]{8}$");
            ;
        }
    }
}