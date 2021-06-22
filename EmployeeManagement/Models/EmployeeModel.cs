﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Entities;

namespace EmployeeManagement.Models
{
    public class EmployeeModel
    {
        public int EmployeeId { get; set; }
        public string RegistrationNumber { get; set; }
        public string Pesel { get; set; }
        public DateTime BirthDate { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public ESex Sex { get; set; }
    }
}