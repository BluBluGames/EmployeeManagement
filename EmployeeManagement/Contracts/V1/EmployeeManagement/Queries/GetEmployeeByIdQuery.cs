using System;
using EmployeeManagement.Models;
using MediatR;

namespace EmployeeManagement.Contracts.V1.EmployeeManagement.Queries
{
    public class GetEmployeeByIdQuery : IRequest<EmployeeResponse>
    {
        public Guid Id { get; set; }
    }
}