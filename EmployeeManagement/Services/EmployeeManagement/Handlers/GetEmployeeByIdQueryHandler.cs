using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EmployeeManagement.Entities;
using EmployeeManagement.Models;
using EmployeeManagement.Repositories;
using EmployeeManagement.Services.EmployeeManagement.Queries;
using MediatR;

namespace EmployeeManagement.Services.EmployeeManagement.Handlers
{
    public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeModel>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public GetEmployeeByIdQueryHandler(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }
        public async Task<EmployeeModel> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(request.Id);
            return employee != null ? _mapper.Map<Employee, EmployeeModel>(employee) : null;
        }
    }
}
