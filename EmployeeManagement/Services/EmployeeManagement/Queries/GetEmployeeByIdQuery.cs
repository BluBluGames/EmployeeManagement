using EmployeeManagement.Models;
using MediatR;

namespace EmployeeManagement.Services.EmployeeManagement.Queries
{
    public class GetEmployeeByIdQuery : IRequest<EmployeeModel>
    {
        public int Id { get; set; }
    }
}