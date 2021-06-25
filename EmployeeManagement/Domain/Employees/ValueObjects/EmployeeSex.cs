using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ValueOf;

namespace EmployeeManagement.Domain.Employees.ValueObjects
{
    [Owned]
    public class EmployeeSex : ValueOf<ESex, EmployeeSex>
    {
        protected override void Validate()
        {
            if (Enum.IsDefined(typeof(ESex), Value) == false)
                throw new InvalidSexException("Invalid Sex value");
        }

        private class InvalidSexException : Exception
        {
            public InvalidSexException(string message) : base($"{message}")
            { }
        }
    }
}
