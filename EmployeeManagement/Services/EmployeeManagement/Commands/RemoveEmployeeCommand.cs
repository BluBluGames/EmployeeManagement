using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace EmployeeManagement.Services.EmployeeManagement.Commands
{
    public class RemoveEmployeeCommand : IRequest<bool>
    {
        public int Id { get; }

        public RemoveEmployeeCommand(int id)
        {
            Id = id;
        }
    }
}
