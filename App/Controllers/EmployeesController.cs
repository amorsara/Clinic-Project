using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.GeneratedModels;
using Services.DTO;
using Services.Employees;

namespace App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ClinicDBContext _context;
        private readonly IEmployeesData _iEmployeesData;

        public EmployeesController(ClinicDBContext context, IEmployeesData employeesData)
        {
            _context = context;
            _iEmployeesData = employeesData;
        }

        [HttpGet]
        [Route("/api/employees/getallemployees")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployees()
        {
            var employees = await _iEmployeesData.GetAllEmployees();
            if (employees == null)
            {
                return NotFound();
            }
            return employees;
        }

        [HttpGet]
        [Route("/api/employees/getallnameemployees")]
        public async Task<ActionResult<IEnumerable<string>>> GetAllNameEmployees()
        {
            var employees = await _iEmployeesData.GetAllNameEmployees();
            if (employees == null)
            {
                return NotFound();
            }
            return employees;
        }

        [HttpGet]
        [Route("/api/employees/GetEmployeesFields")]
        public async Task<ActionResult<IEnumerable<EmployeeFieldsDto>>> GetAllEmployeesFields()
        {
            var employees = await _iEmployeesData.GetAllEmployeesFields();
            if (employees == null)
            {
                return NotFound();
            }
            return employees;
        }

        [HttpGet]
        [Route("/api/employees/getemployeebyid/{id}")]
        public async Task<ActionResult<Employee>> GetEmployeeById(int id)
        {
            var employee = await _iEmployeesData.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return employee;
        }


        [HttpPut]
        [Route("/api/employees/updateemployee/{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, Employee employee)
        {
            if (id != employee.Idemployee)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_iEmployeesData.EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        [Route("/api/employees/createemployee")]
        public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
        {
            var result = await _iEmployeesData.CreateEmployee(employee);
            if (result)
            {
                return CreatedAtAction("CreateEmployee", new { id = employee.Idemployee }, employee);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("/api/employees/deleteemployee/{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            if (_context.Employees == null)
            {
                return NotFound();
            }
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
