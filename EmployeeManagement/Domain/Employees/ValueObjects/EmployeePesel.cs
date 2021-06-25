using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EmployeeManagement.Repositories;
using Microsoft.EntityFrameworkCore;
using ValueOf;

namespace EmployeeManagement.Domain.Employees.ValueObjects
{
    [Owned]
    public class EmployeePesel : ValueOf<string, EmployeePesel>
    {
        protected override void Validate()
        {
            if (Regex.Match(Value, @"^[\d]{11}$").Success == false)
                throw new InvalidPeselException("Pesel has to have only digits and have length of 11");
        }

        private class InvalidPeselException : Exception
        {
            public InvalidPeselException(string message) : base($"{message}")
            { }
        }
    }
}
