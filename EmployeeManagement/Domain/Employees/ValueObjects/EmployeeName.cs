using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ValueOf;

namespace EmployeeManagement.Domain.Employees.ValueObjects
{
    [Owned]
    public class EmployeeName : ValueOf<string, EmployeeName>
    {
        protected override void Validate()
        {
            if (string.IsNullOrWhiteSpace(Value))
                throw new InvalidNameException("Name cannot be empty");
            if (Value.Length is < 1)
                throw new InvalidNameException("Name cannot be shorter than 1 character"); 
            if (Value.Length is > 25)
                throw new InvalidNameException("Name cannot be longer than 25 characters");

        }

        private class InvalidNameException : Exception
        {
            public InvalidNameException(string message) : base($"{message}")
            { }
        }
    }
}
