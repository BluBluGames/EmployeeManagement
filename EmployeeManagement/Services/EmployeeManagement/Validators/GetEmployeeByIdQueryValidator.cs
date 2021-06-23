using EmployeeManagement.Services.EmployeeManagement.Queries;
using FluentValidation;

namespace EmployeeManagement.Services.EmployeeManagement.Validators
{
    public class GetEmployeeByIdQueryValidator : AbstractValidator<GetEmployeeByIdQuery>
    {
        public GetEmployeeByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}