using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EmployeeManagement.Contracts.V1.EmployeeManagement.Queries;
using EmployeeManagement.Domain.Employees;
using EmployeeManagement.Models;
using EmployeeManagement.Repositories;
using MediatR;

namespace EmployeeManagement.Services.EmployeeManagement.Handlers
{
    public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeResponse>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public GetEmployeeByIdQueryHandler(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<EmployeeResponse> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(request.Id);
            return employee != null ? _mapper.Map<Employee, EmployeeResponse>(employee) : null;
        }
    }
}