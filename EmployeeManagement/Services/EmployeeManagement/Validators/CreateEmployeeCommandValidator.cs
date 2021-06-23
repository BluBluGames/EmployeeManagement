using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.DbContexts;
using EmployeeManagement.Repositories;
using EmployeeManagement.Services.EmployeeManagement.Commands;
using FluentValidation;

namespace EmployeeManagement.Services.EmployeeManagement.Validators
{
    public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public CreateEmployeeCommandValidator(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;

            RuleFor(x => x.Pesel).NotEmpty().Length(11).Matches(@"^[\d]{11}$").Must(pesel => !_employeeRepository.CheckIfPeselExistsInDb(pesel));
            RuleFor(x => x.Name).NotEmpty().MinimumLength(1).MaximumLength(25);
            RuleFor(x => x.Surname).NotEmpty().MinimumLength(1).MaximumLength(50);
            RuleFor(x => x.BirthDate).NotEmpty();
        }
    }
}
