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

        [HttpPost]
        [Route("/api/employees/changeemployees")]
        public async Task<IActionResult> ChangeEmployees(List<List<RoomEmployeeDto>> employees)
        {
            var res = await _iEmployeesData.ChangeEmployees(employees);
            return Ok("ok");
        }

        [HttpGet]
        [Route("/api/employees/getallemployeeswithtypes")]
        public async Task<ActionResult<IEnumerable<List<RoomEmployeeDto>>>> GetAllEmployeesWithTypes()
        {
            var employees = await _iEmployeesData.GetAllEmployeesWithTypes();
            if (employees == null)
            {
                return NotFound();
            }
            return employees;
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
        [Route("/api/employees/getdetailsemployees")]
        public async Task<ActionResult<IEnumerable<EmployeeDetails>>> GetDetailsEmployees()
        {
            var employees = await _iEmployeesData.GetEmployeeDetails();
            if (employees == null)
            {
                return NotFound();
            }
            return employees;
        }

        [HttpGet]
        [Route("/api/employees/getemployeesfields")]
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

        [HttpGet]
        [Route("/api/employees/deleteemployeebyid/{id}")]
        public async Task<ActionResult<Employee>> DeleteEmployeeById(int id)
        {
            var isOk = await _iEmployeesData.DeleteEmployeeById(id);
            if (isOk == false)
            {
                return BadRequest();
            }
            return Ok(true);
        }


        [HttpPut]
        [Route("/api/employees/updateemployee/{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, Employee employee)
        {
            if (id != employee.Idemployee)
            {
                return BadRequest();
            }
            var res = await _iEmployeesData.UpdateEmployee(id, employee);
            if (res == false)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPost]
        [Route("/api/employees/createemployee")]
        public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
        {
            employee.Isshow = true;
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
