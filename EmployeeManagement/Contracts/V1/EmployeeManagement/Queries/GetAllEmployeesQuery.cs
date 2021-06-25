using System.Collections.Generic;
using EmployeeManagement.Models;
using MediatR;

namespace EmployeeManagement.Contracts.V1.EmployeeManagement.Queries
{
    public class GetAllEmployeesQuery : IRequest<List<EmployeeResponse>>
    {
    }
}