using MediatR;

namespace EmployeeManagement.Services.EmployeeManagement.Commands
{
    public class RemoveEmployeeCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}