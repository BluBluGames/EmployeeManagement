using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using EmployeeManagement.Repositories;
using Microsoft.EntityFrameworkCore;
using ValueOf;

namespace EmployeeManagement.Domain.Employees.ValueObjects
{
    [Owned]
    public class EmployeeRegistrationNumber : ValueOf<string, EmployeeRegistrationNumber>
    {
        protected override void Validate()
        {
            if (Regex.Match(Value, @"^[\d]{8}$").Success == false)
                throw new InvalidRegistrationNumberException("Registration number has to have only digits and have length of 8");
        }

        private class InvalidRegistrationNumberException : Exception
        {
            public InvalidRegistrationNumberException(string message) : base($"{message}")
            { }
        }
    }
}
