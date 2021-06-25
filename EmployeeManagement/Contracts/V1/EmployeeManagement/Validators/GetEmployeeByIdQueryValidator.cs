using EmployeeManagement.Contracts.V1.EmployeeManagement.Queries;
using FluentValidation;

namespace EmployeeManagement.Contracts.V1.EmployeeManagement.Validators
{
    public class GetEmployeeByIdQueryValidator : AbstractValidator<GetEmployeeByIdQuery>
    {
        public GetEmployeeByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}