using System.Collections.Generic;
using EmployeeManagement.Models;
using MediatR;

namespace EmployeeManagement.Services.EmployeeManagement.Queries
{
    public class GetAllEmployeesQuery : IRequest<List<EmployeeModel>>
    {
    }
}