using System;
using MediatR;

namespace EmployeeManagement.Contracts.V1.EmployeeManagement.Commands
{
    public class RemoveEmployeeCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}