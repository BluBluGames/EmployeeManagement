using System;
using EmployeeManagement.Models;
using MediatR;

namespace EmployeeManagement.Application.V1.Employee.GetEmployee
{
    public class GetEmployeeByIdQuery : IRequest<EmployeeResponse>
    {
        public Guid Id { get; set; }
    }
}