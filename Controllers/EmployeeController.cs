using Quiz10142023.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace Quiz10142023.Controllers
{
    [Route("api/[controller]")]
    public class EmployeeController : ApiController
    {
        private static List<Employee> employees = new List<Employee>();

        // GET: api/Employee
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                if (employees.Any())
                {
                    return Ok(employees);
                }
                else
                {
                    return NotFound("No employees found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        // GET: api/Employee/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var employee = employees.FirstOrDefault(e => e.Id == id);
                if (employee != null)
                {
                    return Ok(employee);
                }
                else
                {
                    return NotFound($"Employee with ID {id} not found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        // POST: api/Employee
        [HttpPost]
        public IActionResult Post([FromBody] Employee employee)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (employees.Any(e => e.EmployeeNo == employee.EmployeeNo))
                {
                    return Conflict($"Employee with EmployeeNo {employee.EmployeeNo} already exists.");
                }

                employee.Id = employees.Count + 1;
                employees.Add(employee);

                return CreatedAtAction(nameof(Get), new { id = employee.Id }, employee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        // PUT: api/Employee/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Employee employee)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var existingEmployee = employees.FirstOrDefault(e => e.Id == id);
                if (existingEmployee != null)
                {
                    if (employees.Any(e => e.EmployeeNo == employee.EmployeeNo && e.Id != id))
                    {
                        return Conflict($"Employee with EmployeeNo {employee.EmployeeNo} already exists.");
                    }

                    existingEmployee.LastName = employee.LastName;
                    existingEmployee.FirstName = employee.FirstName;
                    existingEmployee.Birthday = employee.Birthday;
                    existingEmployee.Address = employee.Address;
                    existingEmployee.EmployeeNo = employee.EmployeeNo;

                    return NoContent();
                }
                else
                {
                    return NotFound($"Employee with ID {id} not found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        // DELETE: api/Employee/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var employeeToRemove = employees.FirstOrDefault(e => e.Id == id);
                if (employeeToRemove != null)
                {
                    employees.Remove(employeeToRemove);
                    return NoContent();
                }
                else
                {
                    return NotFound($"Employee with ID {id} not found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
