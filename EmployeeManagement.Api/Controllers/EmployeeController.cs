﻿using EmployeeManagement.Core.DTO;
using EmployeeManagement.Core.Exceptions;
using EmployeeManagement.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagement.Api.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        /// <summary>
        /// Gets employee
        /// </summary>
        /// <param name="id">The employee ID.</param>
        /// <returns>Employee.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmployeeCreate))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EmployeeCreate>> Get([FromRoute] string id)
        {
            var employee = await _employeeService.GetAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        /// <summary>
        /// Gets employees with an optional manager ID.
        /// </summary>
        /// <param name="managerId">The optional manager ID.</param>
        /// <returns>A list of employees.</returns>
        [HttpGet("list/{managerId?}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<EmployeeView>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<EmployeeView>>> GetList([FromRoute] string? managerId)
        {
            var employees = await _employeeService.GetAllAsync(managerId);

            if (employees.Any())
            {
                return Ok(employees);
            }

            return NotFound();
        }

        /// <summary>
        /// Gets list of managers.
        /// </summary>
        /// <returns>A list of managers.</returns>
        [HttpGet("managers")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<EmployeeView>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<EmployeeView>>> GetManagers()
        {
            var managers = await _employeeService.GetAllManagersAsync();

            if (managers.Any())
            {
                return Ok(managers);
            }

            return NotFound();
        }

        /// <summary>
        /// Creates new employee
        /// </summary>
        /// <param name="employee">Employee object</param>
        /// <returns>Ststus</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Post([FromBody] EmployeeCreate employee)
        {
            try
            {
                await _employeeService.AddEmployeeWithRolesAsync(employee);
                return Ok();
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Updates employee
        /// </summary>
        /// <param name="employee">Employee object</param>
        /// <returns>Ststus</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Put(string id, [FromBody] EmployeeCreate employee)
        {
            try
            {
                await _employeeService.UpdateEmployeeWithRolesAsync(id, employee);
                return Ok();
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Deletes employee
        /// </summary>
        /// <param name="id">employee id</param>
        /// <returns>status</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                await _employeeService.DeleteAsync(id);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}