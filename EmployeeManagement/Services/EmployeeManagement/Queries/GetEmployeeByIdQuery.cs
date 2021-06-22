using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Models;
using MediatR;

namespace EmployeeManagement.Services.EmployeeManagement.Queries
{
    public class GetEmployeeByIdQuery : IRequest<EmployeeModel>
    {
        public int Id { get; }

        public GetEmployeeByIdQuery(int id)
        {
            Id = id;
        }
    }
}
