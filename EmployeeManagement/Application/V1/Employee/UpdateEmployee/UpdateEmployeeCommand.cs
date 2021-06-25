using System;
using EmployeeManagement.Domain.Employees;
using EmployeeManagement.Models;
using MediatR;

namespace EmployeeManagement.Application.V1.Employee.UpdateEmployee
{
    public class UpdateEmployeeCommand : IRequest<EmployeeResponse>
    {
        public Guid EmployeeId { get; set; }
        public string RegistrationNumber { get; set; }
        public string Pesel { get; set; }
        public DateTime BirthDate { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public ESex Sex { get; set; }
    }
}