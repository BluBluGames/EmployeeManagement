using System;
using EmployeeManagement.Domain.Employees;

namespace EmployeeManagement.Models
{
    public class EmployeeResponse
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