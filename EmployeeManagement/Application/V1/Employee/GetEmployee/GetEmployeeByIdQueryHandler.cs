using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EmployeeManagement.Models;
using EmployeeManagement.Repositories;
using MediatR;

namespace EmployeeManagement.Application.V1.Employee.GetEmployee
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
            return employee != null ? _mapper.Map<Domain.Employees.Employee, EmployeeResponse>(employee) : null;
        }
    }
}