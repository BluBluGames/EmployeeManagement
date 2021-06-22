using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Services.EmployeeManagement.Commands;
using EmployeeManagement.Services.EmployeeManagement.Queries;
using MediatR;

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeManagementController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeeManagementController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("GetAllEmployees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var query = new GetAllEmployeesQuery();
            var result = await _mediator.Send(query);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpGet]
        [Route("GetEmployeeById/{employeeId}")]
        public async Task<IActionResult> GetEmployeeById(int employeeId)
        {
            var query = new GetEmployeeByIdQuery(employeeId);
            var result = await _mediator.Send(query);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpPost]
        [Route("CreateEmployee")]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpDelete]
        [Route("RemoveEmployee/{employeeId}")]
        public async Task<IActionResult> RemoveEmployee(int employeeId)
        {
            var query = new RemoveEmployeeCommand(employeeId);
            var result = await _mediator.Send(query);
            return result ? Ok() : BadRequest();
        }
    }
}
