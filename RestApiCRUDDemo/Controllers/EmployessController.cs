using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestApiCRUDDemo.EmployeeData;
using RestApiCRUDDemo.Models;

namespace RestApiCRUDDemo.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class EmployessController : ControllerBase
    {
        private IEmployeeData _employeeData;
        public EmployessController(IEmployeeData employeeData)
        {
            _employeeData = employeeData;
        }


        //Get all employees
        [HttpGet]
        public IActionResult GetEmployees()
        {
            return Ok(_employeeData.GetEmployees());
        }

        //Get employee by id
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetEmployee(Guid id)
        {
            var employee = _employeeData.GetEmployee(id);

            if (employee != null)
            {
                return Ok(employee);
            }

            return NotFound($"Employee with Id: {id} was not found");
        }

        [HttpPost]
        public IActionResult AddEmployee(Employee employee)
        {
            _employeeData.AddEmployee(employee);

            return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + employee.Id,
                employee);
          
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteEmployee(Guid id)
        {
            var employee = _employeeData.GetEmployee(id);

            if (employee != null)
            {
                _employeeData.DeleteEmployee(employee);
                return Ok();
            }

            return NotFound($"Employee with Id: {id} doesn't exist");

        }

        [HttpPatch]
        [Route("{id}")]
        public IActionResult EditEmployee(Guid id, Employee employee)
        {
            var existingEmployee = _employeeData.GetEmployee(id);

            if (existingEmployee != null)
            {
                employee.Id = existingEmployee.Id;
                _employeeData.EditEmployee(employee);
                
            }

            return Ok(employee);
        }




    }
}
