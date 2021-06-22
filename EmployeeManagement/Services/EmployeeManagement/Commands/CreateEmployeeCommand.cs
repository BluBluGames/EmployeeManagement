using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Entities;
using EmployeeManagement.Models;
using MediatR;

namespace EmployeeManagement.Services.EmployeeManagement.Commands
{
    public class CreateEmployeeCommand : IRequest<EmployeeModel>
    {
        public string Pesel { get; set; }
        public DateTime BirthDate { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public ESex Sex { get; set; }
    }
}
