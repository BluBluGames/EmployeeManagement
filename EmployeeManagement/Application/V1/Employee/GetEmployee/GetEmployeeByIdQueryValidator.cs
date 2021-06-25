using FluentValidation;

namespace EmployeeManagement.Application.V1.Employee.GetEmployee
{
    public class GetEmployeeByIdQueryValidator : AbstractValidator<GetEmployeeByIdQuery>
    {
        public GetEmployeeByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}