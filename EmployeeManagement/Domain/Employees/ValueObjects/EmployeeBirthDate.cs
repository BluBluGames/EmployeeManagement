using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ValueOf;

namespace EmployeeManagement.Domain.Employees.ValueObjects
{
    [Owned]
    public class EmployeeBirthDate : ValueOf<DateTime, EmployeeBirthDate>
    {
        protected override void Validate()
        {
        }

        private class InvalidBirthDateException : Exception
        {
            public InvalidBirthDateException(string message) : base($"{message}")
            { }
        }
    }
}
