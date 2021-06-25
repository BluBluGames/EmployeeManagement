using AutoMapper;
using EmployeeManagement.Application.V1.Employee.CreateEmployee;
using EmployeeManagement.Application.V1.Employee.UpdateEmployee;
using EmployeeManagement.Domain.Employees;
using EmployeeManagement.Models;

namespace EmployeeManagement.Mappings
{
    public class EmployeeManagementMappings : Profile
    {
        public EmployeeManagementMappings()
        {
            CreateMap<Employee, EmployeeResponse>()
                .ForMember(dest => dest.EmployeeId,
                    opt => opt.MapFrom(src => src.EmployeeId))
                .ForMember(dest => dest.RegistrationNumber,
                    opt => opt.MapFrom(src => src.RegistrationNumber.Value))
                .ForMember(dest => dest.BirthDate,
                    opt => opt.MapFrom(src => src.BirthDate.Value))
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.Name.Value))
                .ForMember(dest => dest.Pesel,
                    opt => opt.MapFrom(src => src.Pesel.Value))
                .ForMember(dest => dest.Sex,
                    opt => opt.MapFrom(src => src.Sex.Value))
                .ForMember(dest => dest.Surname,
                    opt => opt.MapFrom(src => src.Surname.Value));


            CreateMap<CreateEmployeeCommand, Employee>()
                .ForPath(dest => dest.BirthDate.Value,
                    opt => opt.MapFrom(src => src.BirthDate))
                .ForPath(dest => dest.Name.Value,
                    opt => opt.MapFrom(src => src.Name))
                .ForPath(dest => dest.Pesel.Value,
                    opt => opt.MapFrom(src => src.Pesel))
                .ForPath(dest => dest.Sex.Value,
                    opt => opt.MapFrom(src => src.Sex))
                .ForPath(dest => dest.Surname.Value,
                    opt => opt.MapFrom(src => src.Surname));
            
            CreateMap<UpdateEmployeeCommand, Employee>()
                .ForMember(dest => dest.EmployeeId,
                    opt => opt.MapFrom(src => src.EmployeeId))
                .ForPath(dest => dest.RegistrationNumber.Value,
                    opt => opt.MapFrom(src => src.RegistrationNumber))
                .ForPath(dest => dest.BirthDate.Value,
                    opt => opt.MapFrom(src => src.BirthDate))
                .ForPath(dest => dest.Name.Value,
                    opt => opt.MapFrom(src => src.Name))
                .ForPath(dest => dest.Pesel.Value,
                    opt => opt.MapFrom(src => src.Pesel))
                .ForPath(dest => dest.Sex.Value,
                    opt => opt.MapFrom(src => src.Sex))
                .ForPath(dest => dest.Surname.Value,
                    opt => opt.MapFrom(src => src.Surname));
        }
    }
}