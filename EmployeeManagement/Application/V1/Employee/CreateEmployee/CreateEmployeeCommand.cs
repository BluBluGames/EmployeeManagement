using System;
using EmployeeManagement.Domain.Employees;
using EmployeeManagement.Models;
using MediatR;

namespace EmployeeManagement.Application.V1.Employee.CreateEmployee
{
    public class CreateEmployeeCommand : IRequest<EmployeeResponse>
    {
        public string Pesel { get; set; }
        public DateTime BirthDate { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public ESex Sex { get; set; }
    }
}