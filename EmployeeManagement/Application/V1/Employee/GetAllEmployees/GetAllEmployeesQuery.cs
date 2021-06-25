using System.Collections.Generic;
using EmployeeManagement.Models;
using MediatR;

namespace EmployeeManagement.Application.V1.Employee.GetAllEmployees
{
    public class GetAllEmployeesQuery : IRequest<List<EmployeeResponse>>
    {
    }
}