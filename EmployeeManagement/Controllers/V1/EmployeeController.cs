using System;
using System.Threading.Tasks;
using EmployeeManagement.Application.V1.Employee.CreateEmployee;
using EmployeeManagement.Application.V1.Employee.GetAllEmployees;
using EmployeeManagement.Application.V1.Employee.GetEmployee;
using EmployeeManagement.Application.V1.Employee.RemoveEmployee;
using EmployeeManagement.Application.V1.Employee.UpdateEmployee;
using EmployeeManagement.Contracts.V1;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers.V1
{
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(ApiRoutes.Employees.GetAll)]
        public async Task<IActionResult> GetAll([FromQuery] GetAllEmployeesQuery query)
        {
            var result = await _mediator.Send(query);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpGet(ApiRoutes.Employees.Get)]
        public async Task<IActionResult> Get([FromRoute] Guid employeeId)
        {
            var query = new GetEmployeeByIdQuery {Id = employeeId};
            var result = await _mediator.Send(query);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpPost(ApiRoutes.Employees.Create)]
        public async Task<IActionResult> Create([FromBody] CreateEmployeeCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut(ApiRoutes.Employees.Update)]
        public async Task<IActionResult> Update([FromBody] UpdateEmployeeCommand command)
        {
            if (command == null)
                return BadRequest();

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete(ApiRoutes.Employees.Delete)]
        public async Task<IActionResult> Remove([FromRoute] Guid employeeId)
        {
            var command = new RemoveEmployeeCommand
            {
                Id = employeeId
            };

            var result = await _mediator.Send(command);
            return result ? Ok() : BadRequest();
        }
    }
}