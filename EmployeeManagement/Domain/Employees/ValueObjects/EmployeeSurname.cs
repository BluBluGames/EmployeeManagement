using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ValueOf;

namespace EmployeeManagement.Domain.Employees.ValueObjects
{
    [Owned]
    public class EmployeeSurname : ValueOf<string, EmployeeSurname>
    {
        protected override void Validate()
        {
            if (string.IsNullOrWhiteSpace(Value))
                throw new InvalidSurnameException("Surname cannot be empty");
            if (Value.Length is < 1)
                throw new InvalidSurnameException("Surname cannot be shorter than 1 character");
            if (Value.Length is > 50) 
                throw new InvalidSurnameException("Surname cannot be longer than 50 characters");
        }

        private class InvalidSurnameException : Exception
        {
            public InvalidSurnameException(string message) : base($"{message}")
            { }
        }
    }

}
