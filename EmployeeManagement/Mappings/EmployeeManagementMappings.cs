using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EmployeeManagement.Entities;
using EmployeeManagement.Models;

namespace EmployeeManagement.Mappings
{
    public class EmployeeManagementMappings : Profile
    {
        public EmployeeManagementMappings()
        {
            CreateMap<Employee, EmployeeModel>();
            CreateMap<ESex, string>().ConvertUsing(src => src.ToString());
        }
    }
}
