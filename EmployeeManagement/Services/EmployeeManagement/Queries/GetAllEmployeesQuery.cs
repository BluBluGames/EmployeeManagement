using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Models;
using MediatR;

namespace EmployeeManagement.Services.EmployeeManagement.Queries
{
    public class GetAllEmployeesQuery : IRequest<List<EmployeeModel>>
    {
    }
}
