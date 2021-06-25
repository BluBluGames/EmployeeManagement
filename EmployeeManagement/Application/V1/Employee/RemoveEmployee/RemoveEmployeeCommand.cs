using System;
using MediatR;

namespace EmployeeManagement.Application.V1.Employee.RemoveEmployee
{
    public class RemoveEmployeeCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}