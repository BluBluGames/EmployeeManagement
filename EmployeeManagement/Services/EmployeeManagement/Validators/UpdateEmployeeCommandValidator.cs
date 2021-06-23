using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Repositories;
using EmployeeManagement.Services.EmployeeManagement.Commands;
using EmployeeManagement.Services.EmployeeManagement.Queries;
using FluentValidation;

namespace EmployeeManagement.Services.EmployeeManagement.Validators
{
    public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public UpdateEmployeeCommandValidator(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            RuleFor(x => x.EmployeeId).NotEmpty().Must(id => _employeeRepository.CheckIfEmployeeExists(id));

            RuleFor(x => x.Pesel).NotEmpty().Length(11).Matches(@"^[\d]{11}$");

            RuleFor(x => new {x.Pesel, x.EmployeeId, x.RegistrationNumber}).Must((command, values) =>
            {
                var employee = _employeeRepository.GetEmployeeById(values.EmployeeId);
                if (employee.RegistrationNumber != command.RegistrationNumber)
                {
                    var isRegistrationNumberPresentOnDifferentEmployee =
                        _employeeRepository.CheckIfRegistrationNumberExistsOnDifferentEmployee(
                            command.RegistrationNumber, employee.EmployeeId);
                    return !isRegistrationNumberPresentOnDifferentEmployee;
                }

                if (employee.Pesel != command.Pesel)
                {
                    var isPeselPresentOnDifferentEmployee =
                        _employeeRepository.CheckIfPeselExistsOnDifferentEmployee(command.Pesel, employee.EmployeeId);
                    return !isPeselPresentOnDifferentEmployee;
                }

                return true;
            });

            RuleFor(x => x.Name).NotEmpty().MinimumLength(1).MaximumLength(25);
            RuleFor(x => x.Surname).NotEmpty().MinimumLength(1).MaximumLength(50);
            RuleFor(x => x.BirthDate).NotEmpty();
            RuleFor(x => x.RegistrationNumber).NotEmpty().Length(8).Matches(@"^[\d]{8}$"); ;
        }
    }
}
